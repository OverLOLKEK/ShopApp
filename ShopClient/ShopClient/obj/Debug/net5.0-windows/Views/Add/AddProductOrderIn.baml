   M S B A M L   `   `   `  ����E  AShopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ShopClient.Views.AddShopClient.Views.Add  5   	   ,    &ShopClient.Views.Add.AddProductOrderIn   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:ShopClient.Views.Add     5   	     � Title$  AddProductOrderIn��5   	   $	��300��6#   $	��540��60    � WindowStartupLocation$ CenterScreen=�6<    � 
ResizeMode$ NoResize=�6a    � WindowStyle$
 None=�6w   .��5	      � .+�5
   
   �� $��5q�6   ���	#FF1A315B K� ZIndex$ 2��5      ._�5      � 
������� � RowDefinitions 5      �� 5      �� 5      $
��auto �6$   .+�5      ��  �� Orientation$ Vertical=�6   .+�5      �� .+�5      �� $'��!Внести в документ:��6    $��Century Gothic)�6=   $��16(�6Y   �� 5      $��Stretch=�6$   
�������$	��0.5��6N   $	��470��6[   �� 5      $��25��6?   $��Century Gothic)�6K   $��13(�6g   ��6    �� *
LastPrices+6g   �� 5      $��Наименование:��6    $��Century Gothic)�68   $��15(�6T   �� 5      $��35��6A   $��Century Gothic)�6M   $
��Bold"�6i   $��16(�6{   ��6    �� *ProductTitle+6{   5      �� 5      $ 
Horizontal=�6!   $��Center=�6:   $��10 30 10 10q�6W   .+�5      �� $��Кол-во��6#   $
��Auto��63   $��Century Gothic)�6A   $��15(�6]   .��5      �� $ 
Horizontal=�6)   .+�5   "   �� $��30��6f   $��80��6r   $��15(�6}   $��Century Gothic)�6�   ��6*   �� *Count+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   "   $��+��6)   $��5 0 0 0q�6P   $��20��6a   ��65   �� *AddOne+6a   5      5      �� 5       $��
Закуп��6#   $
��Auto��62   $��Century Gothic)�6@   $��15(�6\   .��5!      �� $ 
Horizontal=�6)   .+�5"   "   �� $��30��6f   $	��100��6r   $��15(�6~   $��Century Gothic)�6�   ��6*   �� *Price+$ PropertyChanged=�6�   5#      5$      �� 5%      $��
Всего��6#   $
��Auto��62   $��Century Gothic)�6@   $��15(�6\   .��5&      �� $ 
Horizontal=�6)   .+�5'   "   �� $��30��6f   $	��100��6r   $��15(�6~   $��Century Gothic)�6�   ${�False��6�   ��6*   �� *Total+$ PropertyChanged=�6�   5(      5)      5*      5+      �� 5,      $��Отмена��6   $��1��6E   $��30��6R   $��5q�6^   $
��Left=�6i   $��Bottom=�6�   $��2��6�   $��Century Gothic)�6�   
���  ��
������ 
���  ��$��16(�5-   @   ��5,   *   �� *Cancel+5-   @   �� 5.      $��Сохранить��6   $��1��6F   $��30��6S   $��5q�6_   $��Right=�6j   $��Bottom=�6�   $��2��6�   $��Century Gothic)�6�   
��� � �
������ 
��� � �$��16(�5/   B   ��5.   -   �� *Save+5/   B   50      51      52      53      