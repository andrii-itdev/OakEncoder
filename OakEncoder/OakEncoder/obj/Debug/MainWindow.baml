   M S B A M L   `   `   `  ����E  AOakEncoder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:OakEncoder
OakEncoder  5   	       OakEncoder.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:OakEncoder     5   	     � Title$  
MainWindow��5   	   $	��250��5	   	   $	��500��5
   	   .��5      � -   5         �Name$ 
gridLayout��5       � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��5      $��Center=�5      $
��40 0q�5      .+�5      �� -   5         ��Name$ 	BtnBrowse��5      $��Browse��5      $��60��5      5       �� 5!      -   5         ��Name$ LblPath��5!      $��0��5"      $��16 0 0 0q�5#      $
��Left=�5%      ��5$      �� *
FilePath+ �� Mode$ OneWay=�5&      5'      �� 5)   
   -   5         ��Name$ txtNewFileName��5)      $��1��5*      $��Stretch=�5+      $��Center=�5,      $
��40 0q�5-      ��5.      �� *
OutputName+$ TwoWay=�.��66   5/      �� 51   
   -   5        $ txtBoxPassword��51      $��2��52      $��Stretch=�53      $��Center=�54      $
��40 0q�55      ��56      �� *KeyInt+$ OneWayToSource=�.��6:   57      �� 59   
   -   5        $ 	BtnEncode��59      $��4��5:      $��Center=�5;      $��Center=�5<      $��Encode��5=      $��0,0,0,0q�5>      $��75��5?      5@      5B      5C      