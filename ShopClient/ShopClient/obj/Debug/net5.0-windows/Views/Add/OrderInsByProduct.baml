   M S B A M L   `   `   `  ����E  AShopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ShopClient.Views.AddShopClient.Views.Add  5   	   ,    &ShopClient.Views.Add.OrderInsByProduct   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:ShopClient.Views.Add     5   	     � Title$  OrderInsByProduct��5   	   $	��600��6#   $	��600��60    � WindowStartupLocation$ CenterScreen=�6<    � 
ResizeMode$ NoResize=�6a    � WindowStyle$
 None=�6w   .��5	      � .+�5
   
   �� $��5q�6   ���	#FF1A315B K� ZIndex$ 2��5      ._�5      � 
������� � RowDefinitions 5      �� 5      $
��auto �6$   �� 5      $��* �6$   �� 5      $
��auto �6$   .+�5      � $��0��6   
�������.+�5      �� $
��Left=�6"   $��5q�6=    �� Orientation$ 
Horizontal=�6H   .+�5      �� $��Не пустые��6#   $��Century Gothic)�67   $��16(�6S    d� 	IsChecked 6a   �� *
IsNotEmpty+ �� UpdateSourceTrigger$ PropertyChanged=�5      5      �� 5      $��16(�6n   $��1��6|   $��5q�6�   ��6   �� *OrderInsByProduct+	 �� SelectedItem	 6E   �� *SelectedOrderIn+
 �� View
 5      �� 5      .P�5      �� $��№ Заказа��6-    �� DisplayMemberBinding 6?   �� *	IdOrder+�� 5      $��№ Поставки��6-    6A   �� *Id+�� 5       6-   �� *
Order.Date+ �� StringFormat$ {0:dd.MM.yyyy}�� �� HeaderTemplate 5   "   �� 5   "   .R�5   &   �� $ 
Horizontal=�61   .+�5    *   �� -   5        $��Дата��5    0   $��14(�6@   
 � Tag$
 Date��6N   6Y   �� 5!   *   $��/Images/filter.png��60   $��15��6L   5"   '   5#   #   5%      �� 5&      $��Цена��6-    6;   �� *Price+�� 5'      $��Остаток��6-    6>   �� *	Remains+�� 5(      $��Количество��6-    6A   �� *Count+5)      5+      �� 5,      $��
Выход��6   $��2��6D   $��30��6Q   $��5q�6]   $
��Left=�6h   $��Bottom=�6�   $��2��6�   $��Century Gothic)�6�   
���  ��
������ 
���  ��$��16(�5-   @   ��5,   )   �� *Cancel+5-   @   50      51      52      53      