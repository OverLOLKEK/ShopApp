   M S B A M L   `   `   `  ����E  AShopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ShopClient.ViewsShopClient.Views  5   	   &     ShopClient.Views.WriteOffDetails   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:ShopClient.Views     5   	     � Title$  WriteOffDetails��5   	   $	��450��6!   $	��600��6.    � WindowStartupLocation$ CenterScreen=�6:    � 
ResizeMode$ NoResize=�6_    � WindowStyle$
 None=�6u   .��5	      � .+�5
   
   �� $��5q�6   ���	#FF1A315B K� ZIndex$ 2��5      ._�5      � 
������� � RowDefinitions 5      �� 5      $��40 �6$   �� 5      �� 5      $
��auto �6$   .+�5      � $��0��6   
�������.+�5      ��  �� Orientation$ 
Horizontal=�6!   $��0 5 0 0q�6:   .+�5      �� $ Vertical=�6%   5      �� 5      $ Vertical=�6%   .+�5      �� $ 
Horizontal=�6)   .+�5   "   �� $��	Дата:��6(   $��16(�68   (  "System.Windows.Controls.DatePicker  5   "   $	��180��6.   $��16(�5   ^      SelectedDate 6   �� *	OrderDate+ �� StringFormat$ {0:dd.MM.yyyy}��6^   5      5      5      5      �� 5      $��1��6E   $��5q�6R   $��16(�6]   ��6   �� *ProductOrderOuts+	 �� View	 5       �� 5!      .P�5"      �� $��№ партии��6-   
 �� DisplayMemberBinding
 6?   �� *IdProductOrderIn+�� 5#      $��Наименование��6-   
 6C   �� *Product.Title+�� 5$      $��Кол-во��6-   
 6=   �� *Count+�� 5%      $��Цена��6-   
 6;   �� *Price+5&      5(      �� 5)      $��Отмена��6   $��2��6E   $��30��6R   $��5q�6^   $
��Left=�6i   $��Bottom=�6�   $��2��6�   $��Century Gothic)�6�   
���  ��
������ 
���  ��$��16(�5*   @   ��5)   *   �� *Cancel+5*   @   �� 5+      $��Сохранить��6   $��2��6F   $��30��6S   $��5q�6_   $��Right=�6j   $��Bottom=�6�   $��2��6�   $��Century Gothic)�6�   
��� � �
������ 
��� � �$��16(�5,   B   ��5+   -   �� *Save+5,   B   5-      5.      5/      50      