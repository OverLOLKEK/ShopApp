   M S B A M L   `   `   `  ����E  AShopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ShopClient.ViewsShopClient.Views  5      5clr-namespace:ShopClient.HelperShopClient.Helper  5           ShopClient.Views.OrderView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:ShopClient.Views     5      2helperclr-namespace:ShopClient.Helper     5        N� 
Background
  ����� N� Title$ 	OrderView��5
       � 	Resources 5   
   +   %ShopClient.Helper.ClientNameConverter   clientNameConverter��%   &	         5      6:   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      �� 5      $��40 �6   .+�5   
   � $��0��6   
�������.+�5      �� $��0��6   $
��Left=�6&   $��5q�6A    �� Orientation$ 
Horizontal=�6L   .+�5      �� .+�5      �� $)��#Фильтрация по типу:��6   $��16(�6:   $��Century Gothic)�6H   �� 5      $	��180��6    �� DisplayMemberPath$
 Name��6+   $��16(�6D   ��6R   �� *ActionTypeFilter+ �� SelectedItem 6{   �� *SelectedActionTypeFilter+5      �� 5      $��	10 0 10 0q�6   .+�5      �� $��Поиск:��6   $��16(�6-   $��Century Gothic)�6;   �� 5      $	��160��6   $��25��6*   $��16(�66   $��Century Gothic)�6�   ��6D   �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=�6�   5      �� 5      .+�5       �� $��1��6   $��16(�6,   $$��Критерий поиска:��6:   $��Century Gothic)�6V   �� 5!      $	��180��6   $��16(�6+   ��69   �� *
SearchType+ 6\   �� *SelectedSearchType+5"      5#      5$      �� 5&   
   -   5        $��	ListView1��5&   t   $��1��6   $��16(�6?   ��6    �� *Orders+ 6M   �� *SelectedOrder+ �� View 5'      �� 5(      .P�5)      �� $	��№��6%   	 �� DisplayMemberBinding	 60   �� *Id+�� 5*      	 6&   �� *Date+
 �� StringFormat$
 {0:dd.MM.yyyy}�� �� HeaderTemplate 5+      �� 5,      .R�5-   "   �� $ 
Horizontal=�6-   .+�5.   &   �� -   5        $��Дата��5.   ,   $��14(�6<   
 � Tag$
 Date��6J   6U   �� 5/   &   $��/Images/filter.png��6,   $��15��6H   50   #   51      53      �� 54      $��Тип операции��6%   	 6;   �� *ActionType.Name+�� 55      $#��Телефон клиента��6%   	 6>   �� *Client.Phone+5;      5<       � ContextMenu 5>      �� 5?      .@�5@      m� $��Детали заказа��6    m� Command 66   �� *OpenOrderDetails+5A      5C      � 5D   
   ���	#FF1A315B$��2��6&   .+�5E      �� $ 
Horizontal=�6   .+�5H      �� $��Center=�6<   
�������$��15(�6j   $��Century Gothic)�6x   ��6   �� *SearchCountRows+6x   �� 5I      $��<��6   $��20��6(   $��30��6P   ��63   �� *
BackPage+6P   �� 5J      $��Center=�62   
�������$��15(�6`   $��Century Gothic)�6n   ��6   �� *Pages+6n   �� 5K      $��>��6   $��20��6%   $��30��6P   ��60   �� *ForwardPage+6P   �� 5M      $7��1Кол-во записей на странице:��6   $��15(�6>   
�������$��Center=�6_   $��Century Gothic)�6z   �� 5N      $	��150��6   $��15(�6'   $��25��6�   ��65   �� *ViewCountRows+ 6[   �� *SelectedViewCountRows+6�   5P      � 5R      5S      5T      5U      