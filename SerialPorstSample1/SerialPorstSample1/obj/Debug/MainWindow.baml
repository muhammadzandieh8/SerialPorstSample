   M S B A M L   `   `   `  ����M  ISerialPorstSample1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:SerialPorstSample1SerialPorstSample1  5   	   #    SerialPorstSample1.MainWindow   5   	   *local clr-namespace:SerialPorstSample1  5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    5   	   _ [MaterialDesignThemes.Wpf, Version=4.4.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � Title$  
SerialPort��5      $	��170��6-   $	��800��6:    � 
ResizeMode$ NoResize=�6F   .��5	      �  � ColumnDefinitions 5
   
   �� 5      �� 5      .+�5   
   ?  9MaterialDesignThemes.Wpf.Transitions.TransitioningContent  $��0��6.   $��0��6;   C  =MaterialDesignThemes.Wpf.Transitions.TransitioningContentBase   OpeningEffect 6K   D  >MaterialDesignThemes.Wpf.Transitions.TransitionEffectExtension     Kind$ SlideInFromLeft=�.��5      �� -   5        $��Connect_to_SerialPort��5   �   $��0��6   $��0��6$   ���#232f34
���   �$
��Bold"�6]   $��Stretch=�6o   $��Stretch=�6�   $��5q�6�   $��Connect to serial port��6�   .��5      � $��Stretch=�6   $��Stretch=�65    5      �� 5      $��91 �6+   �� 5      $��91 �6+   �� 5      $��91 �6+   �� 5      $��91 �6+    � RowDefinitions 5      �� 5      $��30 �6(   �� 5      $��35 �6(   .+�5        $��1��6:   $��2��6G   $��2��6W    6k     $ FadeIn=�.��5      �� -   5         ��Name$  button_ConnectToSerialPort��5   �   $��1��6%   $��2��62   $��Center=�6V   $��Center=�6q   $��2��6�   
�������$
��Bold"�6�   $��3��6�   $��Connect��6!  5      �� 5      $��0��6:   $��0��6G   
���   �$��Stretch=�6k   $��Stretch=�6�   $��Center=�6�   $��Ports��6�   �� 5      -   5         ��Name$ cmbo_SerialPorts��5    �   $��1��5   :   $��0��6G   
���   �$��Center=�6j   $��Center=�6�   $��1q�6�   
����ح�$��Stretch=�5    :   $��Stretch=�6X   $��0��6u   6�   �� 5!      $��0��6:   $��1��6G   
���   �$��Stretch=�6j   $��Stretch=�6�   $��Center=�6�   $��BaudRate��6�   �� 5"      -   5        $��txtBox_BaudRate��5"   �   $��2��6:   $��1��6G   
���   �$��1q�6j   $��Center=�6u   $��Center=�6�   $��2��6�   6�   �� 5#      $��0��6:   $��2��6G   $��2��6W    �� Orientation$ 
Horizontal=�6k   $��Center=�6�   $��Center=�6�   .+�5$      �� $��Rate:��6)   
���   �69   �� 5%      -   5        	 ��Name$	 lbl_DataRate��5%   L   $��000.0��6)   
���   �$��14(�6b   $��Stretch=�6r   $��Center=�6�   $��Center=�6�   $
��Bold"�6�   
�������
������ $��Stretch=�6  5&      5'      5(      5)        5*   
   $��0��6.   $��1��6;    6K     $ SlideInFromRight=�.��5+      �� $��0��6   $��1��6$   $
��Bold"�64   $��5q�6F   $��Data Packet��6Q   .��5,      �  5-      �� 5.      �� 5/       51      �� 52      �� 53      �� 54      .+�56      �� $��0��6   $��0��6)   $��Stretch=�6:   $��Stretch=�6V   $��Center=�6t   $��Center=�6�   
���   �$
��Bold"�6�   $��Byte1��6�   �� 57      $��0��6   $��1��6)   $��Stretch=�6:   $��Stretch=�6V   $��Center=�6t   $��Center=�6�   
���   �$
��Bold"�6�   $��Byte2��6�   �� 58      $��0��6   $��2��6)   $��Stretch=�6:   $��Stretch=�6V   $��Center=�6t   $��Center=�6�   
���   �$
��Bold"�6�   $��Byte3��6�   �� 5:      -   5        $��	lbl_Byte1��5:   �   $��1��6   $��0��6)   $��Stretch=�6:   $��Stretch=�6V   $	��Top=�6t   $��Center=�6�   
���   �$
��0000��6�   6�   �� 5;      -   5        $��	lbl_Byte2��5;   �   $��1��6   $��1��6)   $��Stretch=�6:   $��Stretch=�6V   $	��Top=�6t   $��Center=�6�   
���   �$
��0000��6�   6�   �� 5<      -	   5        $��	lbl_Byte3��5<   �   $��1��6   $��2��6)   $��Stretch=�6:   $��Stretch=�6V   $	��Top=�6t   $��Center=�6�   
���   �$
��0000��6�   6�   5=      5>      5?      5@      5A      