   M S B A M L   `   `   `  ����E  AShopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ShopClient.Views.AddShopClient.Views.Add  5   	   ,    &ShopClient.Views.Add.AddPhysicalClient   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:ShopClient.Views.Add     5   	     � Title$  AddPhysicalClient��5   	   $	��270��6#   $	��330��60    � WindowStartupLocation$ CenterScreen=�6<    � 
ResizeMode$ NoResize=�6a    � WindowStyle$
 None=�6w   .��5	      � .+�5
   
   �� $��5q�6   ���	#FF1A315B K� ZIndex$ 2��5      ._�5      � 
������� � RowDefinitions 5      �� 5      �� 5      $
��auto �6$   .+�5      ��  �� Orientation$ 
Horizontal=�6    � DataContext 66   �� *AddPhysicalClient+.+�5      �� $ Vertical=�6!   .+�5      �� $��Имя:��6       PropertyLabel#��[  6/   �� 5      $��Фамилия:��6    #��[  64   �� 5      $��Отчество:��6    #��[  64   �� 5      $��Телефон:��6    #��[  64   �� 5      $��Адрес:��6    #��[  62   5      �� 5      $ Vertical=�6!   $	��200��68   .+�5      ��   PropertyTextBox#��[ 6=   ��6"   �� *	FirstName+6=   �� 5      #��[ 6<   ��6"   �� *
LastName+6<   �� 5      #��[ 6>   ��6"   �� *
Patronymic+6>   �� 5      #��[ 6@   ��6"   �� *Client.Phone+6@   �� 5       #��[ 6B   ��6"   �� *Client.Address+6B   5!      5#      �� 5'      $��Отмена��6   $��1��6E   $��30��6R   $��5q�6^   $
��Left=�6i   $��Bottom=�6�   $��Century Gothic)�6�   
���  ��
������ 
���  ��$��16(�5(   @   ��5'   *   �� *Cancel+5(   @   �� 5)      $��Сохранить��6   $��1��6F   $��30��6S   $��5q�6_   $��Right=�6j   $��Bottom=�6�   $��Century Gothic)�6�   
��� � �
������ 
��� � �$��16(�5*   B   ��5)   -   �� *Save+5*   B   5,      5.      5/      50      