   M S B A M L   `   `   `  ����E  AShopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ShopClient.HelperShopClient.Helper  5      %    ShopClient.Helper.OrderInReport   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:ShopClient.Helper     5      .��5	      � ���	#FFBBBBBB.+�5
   
   � $��Center=�6   $��Center=�6,   
�������$	��380��6Z   $	��400��6f     � RowDefinitions  5      �� 5      $
��auto �6    �� 5      $
��auto �6!   �� 5      $
��auto �6!   �� 5      $
��auto �6!   �� 5      $
��auto �6!   .+�5      �� $B��<Сформировать отчет по поставкам:��6   $��0��6?   $��18(�6L   �� 5       �� Orientation$ 
Horizontal=�6   $��1��62   $��Center=�6?   $��0 20 0 0q�6\   .+�5      �� $��C��6   $��16(�6$   (  "System.Windows.Controls.DatePicker  5      $	��150��6   $��16(�5   ^   $��30��6m      SelectedDate 6   �� *	DateStart+ �� StringFormat$ {0:dd.MM.yyyy}��6m   �� 5      $
��ПО��6   $��16(�6%     5      $	��150��6   $��16(�5   _   $��30��6m    6   �� *
DateFinish+$ {0:dd.MM.yyyy}��6m   5      �� 5      -   5        $��hintTextBox��5   |   $��3��6   $��	0 100 0 0q�6#   $��30��66   $��16(�6B   $	��250��6P   ��6\   �� *ProductArticle+.��6�   ��5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   1   �� 6   6sys&clr-namespace:System;assembly=mscorlib     61    �� 
TargetType" ��6    �� 	Resources 5         CueBannerBrush j� 
AlignmentX j� 
AlignmentY j� Stretch	 !� Visual��%�   &	        !� 5       $
 Left=�6A   $ Center=�6S   $
 None=�6g   	 5!   "   �� 5"   &   $6��0Код товара(не обязательно)��6,   
�������$��16(�6g   5$      5%      
 �� Triggers
 5&      P� 5'       P� Property � �� �� Empty P� Value# Z 66   . �5(   "   ��  �� Property � �� �� Value# [  6?   5)      P� 5*       � �� 66   Z� . �5+   "   ��  � ��# [  6?   5,      P� 5-       �@J�IsKeyboardFocused$
 True��6C   . �5.   "   ��  � ��
 �����6?   5/      51      .��52   !   53      �� 54      $��OK��6   $��4��6A   $��	0 100 0 0q�6N   $	��140��6a   $��25��6m   $��16(�6y   $��Bottom=�6�   ��6"   �� *
MakeReport+6�   55      56      57      