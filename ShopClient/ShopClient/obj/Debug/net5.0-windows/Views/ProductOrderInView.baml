   M S B A M L   `   `   `  ����E  AShopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ShopClient.ViewsShopClient.Views  5      b ^Microsoft.VisualBasic.Core, Version=10.0.6.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3aaGclr-namespace:Microsoft.VisualBasic;assembly=Microsoft.VisualBasic.CoreMicrosoft.VisualBasic 5   L   )    #ShopClient.Views.ProductOrderInView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:ShopClient.Views     5      Hi:http://schemas.microsoft.com/expression/2010/interactivity     5      Hei9http://schemas.microsoft.com/expression/2010/interactions     5      Vi1Gclr-namespace:Microsoft.VisualBasic;assembly=Microsoft.VisualBasic.Core     6L     N� 
Background
  ����� N� Title$ ProductOrderInView��5      .��5      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $
��1.6* �6   �� 5      �� 5      $��60 �6    � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5      .+�5   
   � $��0��6   $��1��6    5      �� 5      $
��auto �6#   �� 5      $
��auto �6$   �� 5      $
��auto �6$   �� 5      $��* �6#   .+�5      �� $��0��6   .+�5      �� $)��#Фильтрация по типу:��6   $��16(�66   $��Century Gothic)�6D   �� 5       $	��180��6    �� DisplayMemberPath$ Title��6'   $��16(�6A   ��6O   �� *ProductTypeFilter+ �� SelectedItem 6y   �� *SelectedProductTypeFilter+5!      �� 5"      $��	10 0 10 0q�6   $��1��6,   .+�5#      �� $��Поиск:��6   $��16(�6)   $��Century Gothic)�67   �� 5$      $	��160��6   $��25��6&   $��16(�62   $��Century Gothic)�6�   ��6@   �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=�6�   5%      �� 5&      $��2��6   .+�5'      �� $$��Критерий поиска:��6   $��16(�64   $��Century Gothic)�6B   �� 5(      $	��180��6   $��16(�6'   ��65   �� *
SearchType+ 6X   �� *SelectedSearchType+5)      �� 5*      $��3��6   $��Center=�6)   $��Right=�6D   .+�5+      �� $��ПОСТУПЛЕНИЕ��6   $��Century Gothic)�60   $��20(�6L   $��	ExtraBold"�6Z   5,      5-      R� 5.   
   -   5         R�Name$ trvName��5.   �   $��17(�6   $��0��6!   $��0��6.   $��3��6>   $��0 5 5 0q�6P   
�������$��1q�6t   
 � Tag 6�   �� *ClickedTreeElement+	 �� Mode$	 TwoWay=�$ PropertyChanged=�.@�5/      Q� $��Все��6   ��6(   �� *ProductTypeTreeViews+��50      �� 51      
 �� ItemsSource
 6/   ��  �� Path$ Fabricators�.R�52      �� ��6$   �� *Title+ �� ItemTemplate 53      �� 54      .R�55   "   �� ��6,   �� *Title+ � 	Resources 56   &    �� 
TargetType �� Property �� Value �� Source��%�   'Q�       �� 57   *   " Q�60   .�58   .   ��  �@Q�
IsSelected 6K   �� *
IsSelected+$ Check��$	 TwoWay=�59   +   5:   '   5;   #   5<      5>      5@      5A      �� 5B   
   $��1��6   $��1��6    $��15(�60   
���������6>   �� *
Products+ 6`   �� *SelectedProduct+ �� View 5C      �� 5D      .P�5E      �� $��Артикул��6%    �� DisplayMemberBinding 66   �� *	Article+�� 5F      $��Штрихкод��6%    67   �� *	Barcode+�� 5G      $��Название��6%    67   �� *Title+�� 5H      $ ��Производитель��6%    �� Width$	 130��6o    6<   �� *Fabricator.Title+6o   �� 5I      $��Тип��6%   $ 70��6f    62   �� *ProductType.Title+6f   �� 5J      $��Ед.��6%   $ 50��6^    62   �� *
Unit.Title+6^   �� 5K      $��Розн. цена��6%    69   �� *RetailPrice+�� 5L      $��Опт. цена��6%    68   �� *WholesalePrice+�� 5M      $��Кол-во��6%    65   �� *Count+5N       � ContextMenu 5P      �� 5Q      .@�5R      m� $&�� Добавить в список��6>    m� Command 6   �� *
AddProduct+6>   5S      5U      �� 5V   
   $��15(�6   $��2��6!   $��1��6.   $��0 10 0 0q�6>   ���	#FFFBEDED��6P   �� *ProductOrderIns+ 6y   �� *SelectedProductOrderIn+ 5W      �� 5X      .P�5Y      �� $��Артикул��6%    66   �� *Product.Article+�� 5Z      $��Наименование��6%    6;   �� *Product.Title+�� 5[      $��Кол-во��6%    65   �� *Count+�� 5\      $��Закуп. цена��6%    6:   �� *Price+�� 5]      $��Розн. цена��6%    69   �� *Product.RetailPrice+�� 5^      $��Опт. цена��6%    68   �� *Product.WholesalePrice+�� 5_      $��Тип��6%    62   �� *Product.ProductType.Title+�� 5`      $��Ед.��6%    62   �� *Product.Unit.Title+5a       5c      �� 5d      .@�5e      m� $!��Убрать позицию��6    67   �� *DeleteProductOrderIn+5f      5h      �� 5j   
   $��3��6   $��1��6"    �� Orientation$ Vertical=�62   $
��Left=�6I   $	��180��6d   .+�5k      �� $��Поставщик:��6   $��15(�6)   �� 5l      $��15(�6   $ Title��6w    �� 
IsEditable$
 True��6�   ��6%   �� *LegalClients+ 6J   �� *SelectedLegalClient+6�   5m      �� 5n   
   $��3��6   $��1��6"   $ Vertical=�62   $
��Left=�6I   $	��180��6d   $��	200 0 0 0q�6p   .+�5o      �� $��	Дата:��6   $��15(�6$   (  "System.Windows.Controls.DatePicker  5p      $	��180��6   $��15(�5q   a      SelectedDate 6   �� *SelectedDate+ �� StringFormat$ {0:dd.MM.yyyy}��6a   5r      �� 5s   
   $��3��6   $��1��6"   $ Vertical=�62   $��Right=�6I   .+�5t      �� $ 
Horizontal=�6   .+�5u      �� $��Итого:��6   $��16(�6)   �� 5v      $��16(�62   ��6   �� *Total+62   5w      �� 5x      $��Принять��6   $��25��6'   $	��120��63   $��16(�6\   ��6?   �� *
AddOrder+6\   5y      5{      5|      