   M S B A M L   `   `   `  ����N  JAutoCompleteControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:AutoCompleteControlAutoCompleteControl  5   	   S.clr-namespace:AutoCompleteControl.AutoComplete AutoCompleteControl.AutoComplete  5   	   $    AutoCompleteControl.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:AutoCompleteControl     5   	   Gautocomplete.clr-namespace:AutoCompleteControl.AutoComplete     5   	     � Title$  
MainWindow��5	   	   $	��350��6   $	��525��6)   .��5      �  � RowDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $%��AutoComplete with single select��6   5   /AutoCompleteControl.AutoComplete.AutoCompleteUC  5   
   -   5          Name$ CityAutoComplete��5   3   -  'System.Windows.Input.KeyboardNavigation   TabNavigation$ Local=�5   &      AutoBoxHeight$ 22��5   3      AutoBoxWidth$	 140Q�5   3      CompleteBoxHeight$ 24��5   3      	DelayTime$	 500��5   3   $��Center=�6C   $��Center=�6^      	Threshold$ 1��5   3   	   IsMultiSelect	 .  $��5,1,0,0q�5   I   
   
Selections
 5   3   �� *AutoCompleteSelectedCity+ �� Mode$ TwoWay=�5      �� 5   
   -   5        $��Submit��5      $��30��6"   $��50��6.   $��
10,90,10,0q�6N   �� 5!   
   $#��AutoComplete with multiselect��6   $��1��68     5"   
   -   5        $ CountryAutoComplete��5&   3   $ Local=�5"   &   $��1��6O   $ 22��5#   3   $	 140Q�5$   3   $ 24��5%   3   $	 500��5'   3   $��Center=�6C   $��Center=�6^   $ 1��5(   3   	 . $��5,1,0,0q�5)   H   
 5*   3   �� *AutoCompleteSelectedCountry+$ TwoWay=�5+      �� 5.   
   -   5        $��Submit��5.      $��30��6"   $��50��6.   $��
10,90,10,0q�6P   $��1��6d   50      51      