   M S B A M L   `   `   `  ����E  AShopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ShopClient.ViewsShopClient.Views  5          ShopClient.Views.UnitView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:ShopClient.Views     5        N� 
Background
  ����� N� Title$ UnitView��5	      .��5
      �  � RowDefinitions 5   
   �� 5      �� 5      $��40 �6   .+�5   
   �� $��0��6   $��16(�6>   ��6    �� *Units+ �� SelectedItem 6L   �� *SelectedUnit+ �� View 5      �� 5      .P�5      �� $	��№��6%    �� DisplayMemberBinding 60   �� *Id+�� 5      $��Название��6%    67   �� *Title+5       � ContextMenu 5      �� 5      .@�5      m� $ ��Редактировать��6    m� Command 62   �� *
EditUnit+m� 5      $��Удалить��6    6,   �� *
DeleteUnit+5      5      � 5   
   ���	#FF1A315B$��1��6&   .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $
��auto��6   
������ 	 � ToolTip$	 Добавить��6?   $��0q�5       $��5,0,0,0q�6-   ��5!      �� *	AddUnit+��5"      �� 5#      $��/Images/add2.png��6   5%      5'      � 5(      5)      5*      5+      