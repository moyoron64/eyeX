   M S B A M L   `   `   `  ����J  FUserPresenceWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:UserPresenceWpf.UtilitiesUserPresenceWpf.Utilities  5   	        UserPresenceWpf.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   =	utilities'clr-namespace:UserPresenceWpf.Utilities     5   	     � Title$  User Presence WPF Sample��5	   	   $	��800��6*   $	��600��66    � WindowStartupLocation$ CenterScreen=�6C    � 	Resources 5      .   (UserPresenceWpf.Utilities.YesNoConverter   YesNoConverter3   -UserPresenceWpf.Utilities.VisibilityConverter  VisibilityConverter��%:   &	        &	        5   
   6#     5   
   6(   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6    5      �� 5      $
��Auto �6    �� 5       $
��Auto �6     � ColumnDefinitions 5"      �� 5#      $
��Auto �6#   �� 5$      $��* �6#   .+�5'      �� $��0��6   $��0��6!   $��Is user present?��61   �� 5(      $��0��6   $��1��6!   $
��Bold"�61   ��6C   �� *IsUserPresent+ �� 	Converter# [  �� 5*      $��1��6   $��0��6!   $��Is tracking gaze?��61   �� 5+      $��1��6   $��1��6&   .+�5,      �� $
��Bold"�6   ��6+   �� *IsTrackingGaze+# [  y�5-      �� *IsTrackingGazeSupported+# [ �� 5.      $
��Bold"�6   $��Requires EyeX Engine 1.4��6*   
���  ��y�5/      �� *IsTrackingGazeNotSupported+# [ 50      52      �� 54   
   $��1��6   ��6   �� *ImageSource+�� 55   
   $��2��6   $B��<Try playing peek-a-boo with this guy and see what happens...��6   57      58      