   M S B A M L   `   `   `  ����E  AShopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ShopClient.ViewsShopClient.Views  5      5clr-namespace:ShopClient.HelperShopClient.Helper  5      !    ShopClient.Views.ReportView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:ShopClient.Views     5      4controlsclr-namespace:ShopClient.Helper     5        N� Title$  
ReportView��5
      .��5      � .+�5   
   �� ���	#FFBBBBBB.@�5      �� $&�� Отчет по продажам��6   $��14(�61   ��5      � 5      ���	#FFBBBBBB.+�5      � $��Center=�6   $��Center=�6<   
�������$	��380��6j   $	��400��6v    � RowDefinitions 5      �� 5   "   $
��auto �60   �� 5   "   $
��auto �61   �� 5   "   $
��auto �61   �� 5   "   $
��auto �61   �� 5   "   $
��auto �61   .+�5      �� $@��:Сформировать отчет по продажам:��6$   $��0��6N   $��18(�6[   �� 5       �� Orientation$ 
Horizontal=�6)   $��1��6B   $��Center=�6O   $��0 20 0 0q�6l   .+�5   "   �� $��C��6(   $��16(�64   (  "System.Windows.Controls.DatePicker  5   "   $	��150��6.   $��16(�5   ^   $��30��6m      SelectedDate 6   �� *	DateStart+ �� StringFormat$ {0:dd.MM.yyyy}��6m   �� 5   "   $
��ПО��6(   $��16(�65     5   "   $	��150��6.   $��16(�5   _   $��30��6m    6   �� *
DateFinish+$ {0:dd.MM.yyyy}��6m   5       �� 5!      $��2��6)   $��Center=�66   $��0 30 0 0q�6S   .+�5"   "   �� $��Розничные��6J   $��17(�6^    d� 	IsChecked 6+   �� *
IsRetail+6^   �� 5#   "   $��Оптовые��6M   $��17(�6`    6+   �� *IsWholesale+6`   5$      �� 5%      -   5        $��hintTextBox��5%   �   $��3��6&   $��0 20 0 0q�63   $��30��6E   $��16(�6Q   $	��250��6_   ��6k   �� *ProductArticle+.��6�   ��5&   "   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5'   A   �� 6&   6sys&clr-namespace:System;assembly=mscorlib     6A    �� 
TargetType" ��6,    �� 	Resources 5(   *      CueBannerBrush j� 
AlignmentX	 j� 
AlignmentY
 j� Stretch !� Visual��%�   &	        !� 5)   .   $
 Left=�6Q   $	 Center=�6c   $

 None=�6w    5*   2   �� 5+   6   $6��0Код товара(не обязательно)��6<   
�������$��16(�6w   5-   /   5.   +    �� Triggers 5/   *   P� 50   .    P� Property � �� �� Empty P� Value# Z 6F   . �51   2   ��  �� Property � �� �� Value# [  6O   52   /   P� 53   .    � �� 6F   Z� . �54   2   ��  � ��# [  6O   55   /   P� 56   .    �@J�IsKeyboardFocused$
 True��6S   . �57   2   ��  � ��
 �����6O   58   /   5:   '   .��5;   1   5<      �� 5=      $��OK��6%   $��4��6Q   $��	0 100 0 0q�6^   $	��140��6q   $��25��6}   $��16(�6�   $��Bottom=�6�   ��62   �� *
MakeReport+6�   5>      5?      5A      �� 5B      $(��"Отчет по поставкам��6   $��14(�62   ��5C      %   ShopClient.Helper.OrderInReport  5D      5F      5G      5I      5J      