   M S B A M L   `   `   `  ����E  AShopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ShopClient.Views.AddShopClient.Views.Add  5   	   )    #ShopClient.Views.Add.AddLegalClient   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:ShopClient.Views.Add     5   	     � Title$  AddLegalClient��5   	   $	��280��6!   $	��350��6.    � WindowStartupLocation$ CenterScreen=�6:    � 
ResizeMode$ NoResize=�6_    � WindowStyle$
 None=�6u   .��5	      � .+�5
   
   �� $��5q�6   ���	#FF1A315B K� ZIndex$ 2��5      ._�5      � 
������� � RowDefinitions 5      �� 5      �� 5      $
��auto �6$   .+�5      �� .+�5      ��  �� Orientation$ 
Horizontal=�6!   $��0 10 0 0q�6a    � DataContext 6:   �� *AddLegalClient+.+�5      �� $ Vertical=�6%   .+�5      �� $��Название:��6$      PropertyLabel#��[  68   �� 5      $��ИНН:��6$   #��[  63   �� 5      $��Email:��6$   #��[  65   �� 5      $��Телефон:��6$   #��[  67   �� 5      $��Адрес:��6$   #��[  65   5      �� 5      $ Vertical=�6%   $	��230��6<   .+�5      ��   PropertyTextBox#��[ 6=   ��6&   �� *Title+6=   �� 5      #��[ 6;   ��6&   �� *Inn+6;   �� 5      #��[ 6=   ��6&   �� *Email+6=   �� 5       #��[ 6D   ��6&   �� *Client.Phone+6D   �� 5!      #��[ 6F   ��6&   �� *Client.Address+6F   5#      5%      �� 5&      $��Поставщик��6@   $��16(�6T   $��7q�6b   $��Center=�6m    d� 	IsChecked 6   �� *
IsSupplier+6m   5'      �� 5+      $��Отмена��6   $��1��6E   $��30��6R   $��5q�6^   $
��Left=�6i   $��Bottom=�6�   $��Century Gothic)�6�   
���  ��
������ 
���  ��$��16(�5,   @   ��5+   *   �� *Cancel+5,   @   �� 5-      $��Сохранить��6   $��1��6F   $��30��6S   $��5q�6_   $��Right=�6j   $��Bottom=�6�   $��Century Gothic)�6�   
��� � �
������ 
��� � �$��16(�5.   B   ��5-   -   �� *Save+5.   B   50      52      53      54      