   M S B A M L   `   `   `  ����N  JAutoCompleteControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:AutoCompleteControl.AutoComplete AutoCompleteControl.AutoComplete  5      5    /AutoCompleteControl.AutoComplete.AutoCompleteUC   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      @local.clr-namespace:AutoCompleteControl.AutoComplete     5        � 	Resources  5
       �� MergedDictionaries �� Source   AutoLabelStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius	 ��Name
 � Cursor  CloseButtonStyle �� Triggers P� Property P� Value �� 
TargetName�� 5   
    5      �� 5      $= 7..\AutoComplete\Resources\Styles\StyleAutoComplete.xaml?�6%   %�  &	        7  �� 5      " ��6+   �5      �� 5       � ��$ 2q�6/   6;   �� 5       � ��$ 10q�60   6=   �� 5       �@K�ZIndex$ 1��66   6B   �� 5       � �� 5      �� 5      " ��6.   .R�5   "   �� $ Border��6f   
�����_�$��1q�6A   $ 2��6U   
�������._�5   &   ]� .+�5   *   �� ��. $��Center=�6V   $
��Left=�6q   6�   �� 5   *   $	 	BtnCancel��61   -   5        $

 Hand��5   D   $��x��6R   $��15��6^   $	��Top=�6i   $��Right=�6�   8��  $	��Top=�6�   5   +   5   '   5   #    5   "   P� 5   &    �@J�IsMouseOver . . �5    *   �� $ Border��61    � ��
 �����6o   5!   '   5#      5%      5'      5(      .��5*      �  � RowDefinitions 5+   
   �� 5,      $
��Auto �6   6,   �� 5-      $
��Auto �6   6,   �� 5.      $��* �6   6)    � ColumnDefinitions 50   
   �� 51      $��* �6   6+   �� 52      $��* �6   6+   �� 53      $
��Auto �6   6.   .+�55   
   �� -   5        $ BrdAuto��55      $��0��6"   $��0��6/    K� ZIndex$ 3��6?   $��3��6Q   $y�Visible=�6e   ._�56      �� -   5         ��Name$ StackAutoComplete��56   _    �� Orientation$ 
Horizontal=�6   $
��Left=�62   $ 3��6M   .+�57      � $ 3��6   .+�58      �� -   5         ��Name$ TextBoxAutoComplete��58       ^� ShowOnDisabled . $ 3��5:   h     AutoTextBoxStyle#��[ 6�   ��6   ��  �� Self �� RelativeSource# Z  �� Path$
 Text�.��5;   �   5<      5=      �� 5>      -   5         ��Name$ 	ImgLoader��5>      $ 1��6+     AutoImageStyle#��[ 6=   $y�	Collapsed=�6e   5?      ��5@      �� 5A      " ��6   .�5B      ��  � ��  	GreyBrush# [ 64   6Y   �� 5C       � ��$ 1q�68   6D   �� 5D       � ��$
 Left=�6<   6K   �� 5E       � ��$ Center=�6:   6K   �� 5F       � ��$ 35��6/   6<   �� 5G       �@��CornerRadius$ 3��65    �� Triggers 5H      �� 5I       �� Binding 6&   �� *	IsFocused+ �� ElementName$ TextBoxAutoComplete�� �� Value true6e   .a�5J      ��  � ��
 ��ح�6X   5K      �� 5L       6&   �� *	IsFocused+$ TextBoxAutoComplete��	 false6e   .a�5M      ��  � ��# [ 6C   6h   5N      5P      5R      �� 5S   
   -   5         ��Name$ ComboAutoComplete��5T       . $��0��5S   8   $��0��6E   $��2��6U   $ 1��5T   .     AutoComboStyle#��[ 5U       �� DisplayMemberPath$ DisplayName��5V      5X      �� 5Y   
   -   5          ��Name$  LblNoResults��5Y      $��1��6'   $��0��64   $��10(�6D   $��No Results found.��6R   $y�	Collapsed=�6n   
���  ��6�   � 5Z   
   -   5        ! �Name$! ResultPanel��5Z   E   $��3��6   $��2��6(   $��0��65   $y�	Collapsed=�6Z   $��0,2,0,0q�6q   $
��Left=�6�   5[      5\      5^      