   M S B A M L   `   `   `  ����E  AShopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ShopClient.ViewsShopClient.Views  5   	   5clr-namespace:ShopClient.HelperShopClient.Helper  5   	   %    ShopClient.Views.OrderInDetails   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:ShopClient.Views     5   	   0helpclr-namespace:ShopClient.Helper     5   	     � Title$  OrderInDetails��5	   	   $	��450��6    $	��600��6-    � WindowStartupLocation$ CenterScreen=�69    � 
ResizeMode$ NoResize=�6^    � WindowStyle$
 None=�6t   .��5
      � .+�5   
   �� $��5q�6   ���	#FF1A315B K� ZIndex$ 2��5      ._�5      � 
������� � RowDefinitions 5      �� 5      $��60 �6$   �� 5      �� 5      $
��auto �6$   .+�5      � $��0��6   
�������.+�5      �� .+�5      ��  �� Orientation$ 
Horizontal=�6%   .+�5      �� $��	Дата:��6$   $��16(�64   (  "System.Windows.Controls.DatePicker  5      $	��180��6*   $��16(�5   ^      SelectedDate 6   �� *	OrderDate+ �� StringFormat$ {0:dd.MM.yyyy}��6^   5      �� 5      $ 
Horizontal=�6%   .+�5      �� $��Поставщик:��6$   $��16(�69   �� 5      $��16(�6A   ��6$   �� *
Supplier+6A   5      5      5       �� 5!      $��1��6C   $��5q�6P   $��16(�6[   ��6   �� *ProductOrderIns+	 �� View	 5"      �� 5#      .P�5$      �� $��№ партии��6-   
 �� DisplayMemberBinding
 6?   �� *Id+�� 5%      $��Наименование��6-   
 6C   �� *Product.Title+�� 5&      $��Кол-во��6-   
 6=   �� *Count+�� 5'      $!��Остаток партии��6-   
 6E   �� *	Remains+�� 5(      $��Цена��6-   
 6;   �� *Price+5*      5.      �� 50      $��Отмена��6   $��2��6E   $��30��6R   $��5q�6^   $
��Left=�6i   $��Bottom=�6�   $��2��6�   $��Century Gothic)�6�   
���  ��
������ 
���  ��$��16(�51   @   ��50   *   �� *Cancel+51   @   �� 52      $ 
Horizontal=�6   $��2��66   $
��Left=�6C   $��Center=�6^   $��	100 0 0 0q�6y   .+�53      �� $��Итого:��6   $��16(�6-   �� 54      $��16(�66   ��6   �� *Total+66   55      �� 56      $��Сохранить��6   $��2��6F   $��30��6S   $��5q�6_   $��Right=�6j   $��Bottom=�6�   $��2��6�   $��Century Gothic)�6�   
��� � �
������ 
��� � �$��16(�57   B   ��56   -   �� *Save+57   B   58      59      5:      5;      