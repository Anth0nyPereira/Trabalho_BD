   M S B A M L   `   `   `  ����N  JPets_At_First_Sight, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Pets_At_First_SightPets_At_First_Sight  5      %    Pets_At_First_Sight.NewDonativo   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:Pets_At_First_Sight     5      V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$  NewDonativo��5
       N� WindowHeight$	 667Q�5       N� WindowWidth$	 375Q�5       N� 
Background �#DCE3E7.��5      � $	��667��6   .+�5   
   �� -   5         ��Name$ Titulo_Dons��5      $��36��6%   $#��Adicionar Donativo Monetário��61   $��75,24,35,607q�6X   $��18(�6n   $
��Bold"�6|   $��Center=�6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   �� 5   
   -   5         /  +MaterialDesignFloatingActionMiniLightButton#��[  5       � ToolTip$
 Back��6V   $��	16,20,0,0q�6e   $
��Left=�6�   $	��Top=�6�   .��5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ 	ArrowBack=�5      $��24��5      $��24��5      5      �� 5   
   $��42,164,45,383q�6   .+�5      �� $
��Bold"�6   $��20(�6&   $��Center=�64   $��Center=�6O   $��44��6s   $	��288��6   .��6�   Método de Pagamento:6�   �� 5       �� Orientation$ Vertical=�6   .+�5      	� -   5        	 	�Name$	 CM��5      
 d� 	IsChecked$

 TrueY�6*   .��6;   Cartão Multibanco6N   	� 5      -   5        $	 TB��5      .��6*   Transferência Bancária6B   	� 5      -   5        $		 MBW��5      .��6+   MB Way63   	� 5      -   5        $	 PP��5      .��6*   Paypal62   5      5       �� 5"   
   -   5         ��Name$ quantidade_��5#      $��72��5$       �� 
IsEditable . $��209,398,0,241q�5&   M   $ 1.294,0.714&�6d   $
��Left=�6�   $	��102��6�   ��6   �� *#!LongListToTestComboVirtualization+ �� Text 5'      �� 5(       �� Path$ SelectedTextTwo�5)       �� Mode$ TwoWay=�5*       �� UpdateSourceTrigger$ PropertyChanged=�5+      5,      ��5.      �� 5/      .R�50      $� 51      53      �� 54   
   $
��Bold"�6   $��20(�6"   $��Center=�60   $��Center=�6K   $��40��6o   $��60,398,192,229q�6{   $��Quantia:��6�   �� 55   
   -   5         " MaterialDesignRaisedDarkButton#��[ 56      $��50��57      +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 25��58      $7 1MaterialDesignRaisedDarkButton with Round Corners��59      $��103,496,105,121q�6I   .��5:      �� $��Registar��5;      5<      �� 5=   
   -	   5        $ abrigo��5>      $��72��5?      $��209,324,0,315q�6   $ 1.294,0.714&�62   $
��Left=�6V   $	��102��6q   .@�5@      �� $
��acme��6   �� 5A      $��agape��6   �� 5B      $��caocomcoracao��6   �� 5C      $��
doadores.b��6   �� 5D      $��	happy.paw��6   �� 5E      $��hooli��6   �� 5F      $��patas.focinhos��6   �� 5G      $��pet.pet��6   �� 5H      $��ruff.official.us��6   5I      �� 5J   
   $
��Bold"�6   $��20(�6"   $��Center=�60   $��Center=�6K   $��40��6o   $��60,324,192,303q�6{   $��Abrigo:��6�   5L      5M      