   M S B A M L   `   `   `  ����E  AShopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ShopClient.ViewsShopClient.Views  5      )    #ShopClient.Views.PhysicalClientView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:ShopClient.Views     5        N� 
Background
  ����� N� Title$ PhysicalClientView��5	      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      �� 5      $��40 �6   .+�5   
   � $��0��6   
�������.+�5      �� $��0��6   $
��Left=�6&   $��5q�6A    �� Orientation$ 
Horizontal=�6L   .+�5      �� $��0 0 10 0q�6   .+�5      �� $��Поиск:��6   $��16(�6-   $��Century Gothic)�6;   �� 5      $	��160��6   $��25��6*   $��16(�66   $��Century Gothic)�6�   ��6D   �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=�6�   5      �� 5      .+�5      �� $��1��6   $��16(�6,   $$��Критерий поиска:��6:   $��Century Gothic)�6V   �� 5      $	��180��6   $��16(�6+   ��69   �� *
SearchType+ �� SelectedItem 6\   �� *SelectedSearchType+5      5      5      �� 5   
   $��1��6   $��16(�6H   ��6    �� *PhysicalClients+ 6V   �� *SelectedPhysicalClient+ �� View 5      �� 5      .P�5       �� $��Имя��6%    �� DisplayMemberBinding 62   �� *	FirstName+�� 5!      $��Фамилия��6%    66   �� *
LastName+�� 5"      $��Отчество��6%    67   �� *
Patronymic+�� 5#      $��Телефон��6%    66   �� *Client.Phone+�� 5$      $��
Адрес��6%    64   �� *Client.Address+�� 5%       6%   �� *Client.OrdersCount+ �� HeaderTemplate 5&      �� 5'      .R�5(   "   �� $ 
Horizontal=�6-   .+�5)   &   �� -   5        $ ��Кол-во заказов��5)   ,   $��14(�6F   
	 � Tag$	 Count��6T   6`   �� 5*   &   $��/Images/filter.png��6,   $��15��6H   5+   #   5,      5.      5/      
 � ContextMenu
 51      �� 52      .@�53      m� $��Удалить��6    m� Command 60   �� *DeletePhysicalClient+m� 54      $ ��Редактировать��6    66   �� *EditPhysicalClient+55      57      � 59   
   ���	#FF1A315B$��2��6&   .+�5:      �� $ 
Horizontal=�6   .+�5;      �� $
��auto��6   
������ $��0q�5<      $��5,0,0,0q�6-   ��5=      �� *AddPhysicalClient+��5>      �� 5?      $��/Images/add2.png��6    5A      5C      � 5D      5E      5F      5G      