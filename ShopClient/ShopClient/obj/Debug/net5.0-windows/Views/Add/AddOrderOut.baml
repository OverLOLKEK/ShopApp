   M S B A M L   `   `   `  ����E  AShopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ShopClient.Views.AddShopClient.Views.Add  5   	   &     ShopClient.Views.Add.AddOrderOut   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:ShopClient.Views.Add     5   	     � Title$  AddOrderOut��5   	   $	��300��6   $	��540��6+    � WindowStartupLocation$ CenterScreen=�67    � 
ResizeMode$ NoResize=�6\    � WindowStyle$
 None=�6r   .��5	      � .+�5
   
   �� $��5q�6   ���	#FF1A315B K� ZIndex$ 2��5      ._�5      � 
������� � RowDefinitions 5      �� 5      �� 5      $
��auto �6$   .+�5      ��  �� Orientation$ Vertical=�6   .+�5      �� .+�5      �� $'��!Внести в документ:��6    $��Century Gothic)�6=   $��16(�6Y   �� 5      $��Stretch=�6$   
�������$	��0.5��6N   $	��480��6[   �� 5      $��Наименование:��6    $��Century Gothic)�68   $��15(�6T   �� 5      $��35��6A   $��Century Gothic)�6M   $
��Bold"�6i   $��16(�6{   ��6    �� *ProductTitle+6{   5      �� 5      $ 
Horizontal=�6!   $��Center=�6:   $��10 30 10 10q�6W   .+�5      �� $��Кол-во��6#   $
��Auto��63   $��Century Gothic)�6A   $��15(�6]   .��5      �� $ 
Horizontal=�6)   .+�5   "   �� $��30��6f   $��80��6r   $��16(�6}   $��Century Gothic)�6�   ��6*   �� *Count+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   "   $��+��6)   $��5 0 0 0q�6P   $��20��6a   ��65   �� *AddOne+6a   5      5      �� 5      $
��Auto��6E   $��Century Gothic)�6S   $��15(�6o   ��6#   �� *PickedSaleType+.��5       �� $ 
Horizontal=�6)   .+�5!   "   �� $��30��6f   $	��120��6r   $��16(�6~   $��Century Gothic)�6�    �� 
IsReadOnly$
 True��6�   ��6*   �� *Price+$ PropertyChanged=�6�   5"      5#      �� 5$      $��Скидка��6#   $
��Auto��63   $��Century Gothic)�6A   $��15(�6]   .��5%      �� $ 
Horizontal=�6)   .+�5&   "   �� $��30��6i   $��80��6u   $��16(�6�   $��Century Gothic)�6�   ��6*   �� *
Discount+$ PropertyChanged=�6�   5'      5(      �� 5)      $��
Всего��6#   $
��Auto��62   $��Century Gothic)�6@   $��15(�6\   .��5*      �� $ 
Horizontal=�6)   .+�5+   "   �� $��30��6f   $	��100��6r   $��16(�6~   $��Century Gothic)�6�   ${�False��6�   ��6*   �� *Total+$ PropertyChanged=�6�   5,      5-      5.      5/      �� 50      $��Отмена��6   $��1��6E   $��30��6R   $��5q�6^   $
��Left=�6i   $��Bottom=�6�   $��2��6�   $��Century Gothic)�6�   
���  ��
������ 
���  ��$��16(�51   @   ��50   *   �� *Cancel+51   @   �� 52      $��Сохранить��6   $��1��6F   $��30��6S   $��5q�6_   $��Right=�6j   $��Bottom=�6�   $��2��6�   $��Century Gothic)�6�   
��� � �
������ 
��� � �$��16(�53   B   ��52   -   �� *Save+53   B   54      55      56      57      