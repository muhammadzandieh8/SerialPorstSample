using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO.Ports;
using System.Diagnostics;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;

namespace SerialPorstSample1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Parameter
        int MyBaudRate;
        public DispatcherTimer _timer;
        public static bool IsConnected = false;
        public static bool IsConnectToSerialPort = false;
        public static List<string> List_SerilPortName = new List<string>();
        public MainWindow()
        {
            InitializeComponent();

            txtBox_BaudRate.Text = "9600";

            LoadSerialPort();

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000)
            };
            _timer.Tick += Set_up_timer;
            
            //Start Parser
            Parser parser = new Parser();
            parser.CreateThread();

        }
        public void LoadSerialPort()
        {
            List_SerilPortName = Communicator_SerialPort.GetAllPorts();
            // ... Assign the ItemsSource to the List.
            cmbo_SerialPorts.ItemsSource = List_SerilPortName;
            // ... Make the first item selected.
            cmbo_SerialPorts.SelectedIndex = 0;
            // ... Select LastPort was connected
            cmbo_SerialPorts.SelectedIndex = Properties.Settings.Default.lastPortConnect;
        }
        public void Set_up_timer(object sender, EventArgs eventArgs)
        {
            lbl_Byte1.Content = Parser.Byte1;
            lbl_Byte2.Content = Parser.Byte2;
            lbl_Byte3.Content = Parser.Byte3;
            lbl_DataRate.Content = (Communicator_SerialPort.TotalBoudRate.Rate).ToString("####0.0##") + " KB";//KB/s
        }
        private void CmboBox_SerialPorts_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSerialPort();
        }
        private void CmboBox_SerialPorts_DropDownOpened(object sender, EventArgs e)
        {
            LoadSerialPort();
        }
        private void Btn_ConnectToSerialPort_Click(object sender, RoutedEventArgs e)
        {

            Properties.Settings.Default.lastPortConnect = cmbo_SerialPorts.SelectedIndex;
            Properties.Settings.Default.Save();

            int SelIndex_SerialPort = cmbo_SerialPorts.SelectedIndex;
            if (SelIndex_SerialPort > -1)
            {
                if (txtBox_BaudRate.Text != "")
                {
                    if (button_ConnectToSerialPort.Content.ToString() == "Connect")
                    {
                        IsConnected = true;
                        if (txtBox_BaudRate.Text != "")
                        {
                            MyBaudRate = Convert.ToInt32(txtBox_BaudRate.Text.ToString().Trim());
                            int MyDataBite = 8;
                            SerialPort SelSerialPort = new SerialPort(List_SerilPortName[SelIndex_SerialPort], MyBaudRate, Parity.None, MyDataBite, StopBits.One);

                            Communicator_SerialPort.StopThread = false;
                            Communicator_SerialPort.Instance.Create_Thread_CommunicatorSerial(SelSerialPort);
                            if (Communicator_SerialPort.Instance.IsOpen)
                            {

                                //Thread mytestthread = new Thread();
                                //mytestthread.Start();

                                _timer.Start();


                                button_ConnectToSerialPort.Content = "Disconnect";
                                cmbo_SerialPorts.IsEnabled = false;
                                txtBox_BaudRate.IsEnabled = false;
                                //button_ConnectToSerialPort.IsEnabled = false;
                            }
                        }
                    }
                    else
                    {
                        IsConnected = false;
                        Communicator_SerialPort.StopThread = true;


                        Communicator_SerialPort.Instance.Close();
                        button_ConnectToSerialPort.Content = "Connect";
                        cmbo_SerialPorts.IsEnabled = true;
                        //led_IsDone_ConnectToSerialPort.Value = Communicator_SerialPort.Instance.IsOpen;
                        txtBox_BaudRate.IsEnabled = true;

                        _timer.Stop();
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter BaudRate... ", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please Select Port...");
            }
        }
        private void txtBox_NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
