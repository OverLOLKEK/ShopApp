   M S B A M L   `   `   `  ����E  AShopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ShopClient.Views.AddShopClient.Views.Add  5   	   '    !ShopClient.Views.Add.ClientSelect   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:ShopClient.Views.Add     5   	     � Title$  ClientSelect��5   	   $	��600��6   $	��600��6+    � WindowStartupLocation$ CenterScreen=�67    � 
ResizeMode$ NoResize=�6\    � WindowStyle$
 None=�6r   .��5	      � .+�5
   
   �� $��5q�6   ���	#FF1A315B K� ZIndex$ 2��5      ._�5      � 
������� � RowDefinitions 5      �� 5      $
��auto �6$   �� 5      $��* �6$   �� 5      $
��auto �6$   .+�5      � $��0��6   
�������.+�5      �� $
��Left=�6"   $��5q�6=    �� Orientation$ 
Horizontal=�6H   .+�5      �� $�� 0 0 5 0q�6%   .+�5      �� $��Тип клиента:��6$   $��15(�6;   $��Century Gothic)�6I   �� 5      $	��120��6'   $��15(�63   ��6A   �� *ClientTypes+ �� SelectedItem 6e   �� *SelectedClientType+5      �� 5      $��0 0 10 0q�6%   .+�5      �� $��Поиск:��6$   $��15(�65   $��Century Gothic)�6C   �� 5      $	��140��6&   $��15(�62   $��25��6@   $��Century Gothic)�6�   ��6L   �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=�6�   5      �� 5      .+�5      �� $$��Критерий поиска:��6%   $��15(�6@   $��Century Gothic)�6N   �� 5      $	��160��6'   $��15(�63   ��6A   �� *
SearchType+ 6d   �� *SelectedSearchType+5      5$      5%      �� 5&      $��15(�6k   $��1��6y   $��5q�6�   ��6   �� *ClientViews+ 6?   �� *SelectedClientView+	 �� View	 5'      �� 5(      .P�5)      �� $��Наименование��6-   
 �� DisplayMemberBinding
 6C   �� *Title+�� 5*      $��Телефон��6-   
 6>   �� *Phone+�� 5+      $��
Адрес��6-   
 6<   �� *	Address+�� 5,      $��Тип клиента��6-   
 6B   �� *Type+5-      5/      �� 50      $��Отмена��6   $��2��6E   $��30��6R   $��5q�6^   $
��Left=�6i   $��Bottom=�6�   $��2��6�   $��Century Gothic)�6�   
���  ��
������ 
���  ��$��16(�51   @   ��50   *   �� *Cancel+51   @   �� 52      $��Сохранить��6   $��2��6F   $��30��6S   $��5q�6_   $��Right=�6j   $��Bottom=�6�   $��2��6�   $��Century Gothic)�6�   
��� � �
������ 
��� � �$��16(�53   B   ��52   -   �� *Save+53   B   54      55      56      57      