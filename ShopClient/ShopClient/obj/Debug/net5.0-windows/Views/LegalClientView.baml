   M S B A M L   `   `   `  ����E  AShopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ShopClient.HelperShopClient.Helper  5      3clr-namespace:ShopClient.ViewsShopClient.Views  5      &     ShopClient.Views.LegalClientView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2helperclr-namespace:ShopClient.Helper     5      0localclr-namespace:ShopClient.Views     5        N� 
Background
  ����� N� Title$ LegalClientView��5
       � 	Resources 5      /   )ShopClient.Helper.SupplierStatusConverter   supplierStatusConverter��%   &	         5   
   6:   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      �� 5      $��40 �6   .+�5   
   � $��0��6   
�������.+�5      �� $��0��6   $
��Left=�6&   $��5q�6A    �� Orientation$ 
Horizontal=�6L   .+�5      �� $��0 0 10 0q�6   .+�5      �� $��Поиск:��6   $��16(�6-   $��Century Gothic)�6<   �� 5      $	��160��6   $��16(�6*   $��25��68   $��Century Gothic)�6�   ��6D   �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=�6�   5      �� 5      .+�5      �� $��1��6   $��16(�6,   $$��Критерий поиска:��6:   $��Century Gothic)�6V   �� 5      $	��160��6   $��16(�6+   ��69   �� *
SearchType+ �� SelectedItem 6\   �� *SelectedSearchType+5      �� 5      $ 
Horizontal=�6   $��Bottom=�67   $��	100 0 0 5q�6R   .+�5      �� $'��!Только поставщики��6   $��Century Gothic)�6<   $��16(�6X    d� 	IsChecked 6f   �� *IsOnlySuppliers+$ PropertyChanged=�5       5!      5"      �� 5#   
   $��1��6   $��16(�6j   ��6    �� *LegalClients+$ PropertyChanged=� 6x   �� *SelectedLegalClient+ �� View 5$      �� 5%      .P�5&      �� $��Наименование��6%   	 �� DisplayMemberBinding	 6;   �� *Title+�� 5'      $��ИНН��6%   	 62   �� *Inn+�� 5(      $��Email��6%   	 64   �� *Email+�� 5)      $��Телефон��6%   	 66   �� *Client.Phone+�� 5*      $��
Адрес��6%   	 64   �� *Client.Address+�� 5+      	 6%   �� *Client.OrdersCount+
 �� HeaderTemplate
 5,      �� 5-      .R�5.   "   �� $ 
Horizontal=�6-   .+�5/   &   �� -   5        $ ��Кол-во заказов��5/   ,   $��14(�6F   
 � Tag$ Count��6T   6`   �� 50   &   $��/Images/filter.png��6,   $��15��6H   51   #   52      54      �� 55      $��Поставщик��6%    �� CellTemplate 56      �� 57      .R�58   "   �� ��6,   ��  �� Path$ 
IsSupplier� �� 	Converter# [  59      5;      5<       � ContextMenu 5>      �� 5?      .@�5@      m� $��Удалить��6    m� Command 60   �� *DeleteLegalClient+m� 5A      $ ��Редактировать��6    66   �� *EditLegalClient+5B      5D      � 5F   
   ���	#FF1A315B$��2��6&   .+�5G      �� $ 
Horizontal=�6   .+�5H      �� $
��auto��6   
������  � ToolTip$ Добавить��6@   $��0q�5I      $��5,0,0,0q�6-   ��5J      �� *AddLegalClient+��5K      �� 5L      $��/Images/add2.png��6    5N      5P      � 5Q      5R      5S      5T      