   M S B A M L   `   `   `  ����E  AShopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ShopClient
ShopClient  5   	       ShopClient.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a;	behaviors+http://schemas.microsoft.com/xaml/behaviors  5   	   $localclr-namespace:ShopClient  5   	     � Title$  
ShopMaster��5	   	   $	��720��6   $
��1280��6,    � WindowState$ Normal=�6=    � WindowStartupLocation$ CenterScreen=�6R    � WindowStyle$ ThreeDBorderWindow=�6w    � 
ResizeMode$ 	CanResize=�6�   .��5
      � .+�5   
   �� .@�5      �� ��5      �� 5      -   5         ��Name$ tabItem��5   1   $��Документы��6    $��14(�6b   *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5      +  %Microsoft.Xaml.Behaviors.EventTrigger  5         	EventName$ 	MouseDown��6-   /  )Microsoft.Xaml.Behaviors.EventTriggerBase   SourceObject 6C   �� 	 �� ElementName$	 tabItem��*  $Microsoft.Xaml.Behaviors.TriggerBase
   Actions.
 5      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 68   �� *ClickCommand+5      ��5      � 5       � ColumnDefinitions 5      �� 5       � RowDefinitions 5      �� 5      $	��120 �6,   �� 5      .+�5      � ���	#FF1A315B5'      � 5*      ���	#FF1A315B.+�5+      ��  �� Orientation$ 
Horizontal=�6)   $��2��6B   .+�5,   "   �� $ Vertical=�6-   .+�5-   &   �� $ 
Horizontal=�61   .+�5/   *   	� -   5         	�Name$ 	mainRadio��5/   H   $��Продажа��66   $��20(�50      
������� d� 	IsChecked$
 TrueY�6?      MenuButtonThemeVertical#��[  51       	� 	GroupName$ group1��52   C   ��6   �� *OpenOrderOutView+53   +   	� 54   *   $��Поступление��66   $��20(�55      
�������#��[  56      $ group1��57   I   ��6   �� *OpenProductOrderInView+58   #   	� 5:   "   $��Списание��6.   $��20(�5;      
�������#��[  5<      $ group1��6O   ��5=      �� *OpenWriteOffView+5>   #   5@      �� 5B      $��Управление��6(   $��Center=�6:   $��14(�6W   
�������$��Century Gothic)�6x   5C      �� 5D   "   $ Vertical=�6-   $��20 0 0 0q�6D   .+�5E   &   �� $ 
Horizontal=�61   .+�5G   *   	� $��Отчеты��66   $��20(�5H      
�������#��[  5I      $ group1��6O   ��5J      �� *OpenReportView+5L   +   	� 5M   *   $��Статистика��66   $��20(�5N      
�������#��[  5O      $ group1��6O   ��5P      �� *OpenDashView+5Q   +   5R   '   �� 5T   &   $��Статистика��60   $��Center=�6B   $��14(�6_   
�������$��Century Gothic)�6�   5U   #   5V      5X       � 5Z      $��1��6      � NavigationUIVisibility$ Hidden=�6-   $��1��5[      $��10q�6)   ��5\      �� *CurrentPage+5]      5_      �� 5`      ��5a      �� 5b      -   5        $ tabItemLists��5b   .   $��Списки��6    $��14(�6g    5d        5e      $ 	MouseDown��6-    6C   �� $	 tabItemLists��.
 5f         68   �� *ClickCommandLists+5g      ��5i      � 5j       5k      �� 5l      $	��250 �6/   �� 5m      .+�5o      � ���	#FF1A315B.+�5p      �� $��0,10,0,0q�6)   .+�5q   "   	� -   5        $ 	listRadio��5q   K   $��Товары��6.   $��60��6?   $��20(�5r      
�������$
 TrueY�6@     MenuButtonThemeHorizontal#��[ 5s      ��5t      �� *OpenProductView+5u   #   	� 5v   "   $��Заказы��6.   $��60��6?   $��20(�5w      
�������#��[ 5x      ��5y      �� *OpenOrderView+5z   #   	� 5{   "   $��Ед. измерения��6.   $��60��6F   $��20(�5|      
�������#��[ 5}      ��5~      �� *OpenUnitView+5   #   	� 5�   "   $!��Типы продуктов��6.   $��60��6G   $��20(�5�      
�������#��[ 5�      ��5�      �� *OpenProductTypeView+5�   #   	� 5�   "   $ ��Производители��6.   $��60��6F   $��20(�5�      
�������#��[ 5�      ��5�      �� *OpenFabricatorView+5�   #   	� 5�   "   $��Физ. лица��6.   $��60��6B   $��20(�5�      
�������#��[ 5�      ��5�      �� *OpenPhysicalClientView+5�   #   	� 5�   "   $��Юр. лица��6.   $��60��6A   $��20(�5�      
�������#��[ 5�      ��5�      �� *OpenLegalClientView+5�   #   5�      5�      � 5�      $��1��6   $��1��6,   
�������.+�5�       � $ Hidden=�6$   $��1��5�      $��10q�6)   ��5�      �� *CurrentListPage+5�      5�      5�      5�      5�      5�      