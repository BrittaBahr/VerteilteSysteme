   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ClientClient  5   	   3clr-namespace:Client.ConverterClient.Converter  5   	       Client.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Client     5   	   4	converterclr-namespace:Client.Converter     5   	   -   5          � Title$  
Beasty Bar��5	   	   $	��450��6   $	��800��6)    � 	Resources 5      :   4Client.Converter.EnumerableCardVMToCardDataConverter   EnumCardConverter+   %Client.Converter.PathToBrushConverter  PathToBrushConv,   &Client.Converter.BoolToColourConverter  BoolToColourConverter-   'Client.Converter.ColourToBrushConverter  ColourToBrushConverter��%t   &	        &	      &	 &     &	 9       5   
   68     5   
   6)     5   
   6*     5   
   6+   5      .��5      � ��6   �� *PathToBackgroundImage+ �� 	Converter# [  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $��2��6.   $��10q�6?   ��6T   �� *	MenuWidth+._�5      � ��6   �� *PathToBackgroundMenuImage+# [  5      �� 5      $
��Auto �6$   �� 5       $
��Auto �6$   �� 5!      $
��Auto �6$   .+�5$      �� $��0��6   $��Spielanleitung��6&   $��10q�6?   $��30q�6K   6Z   �� 5&      $��1��6   $	��100��6*   $
��Auto��67   
�������$��10q�6W   .+�5'      �� $��	On Turn: ��6     �� Padding$ 10q�61   $��Medium"�6>   �� 5(      $ 10q�6H   $��Medium"�6�   $��Center=�6�   ��6    �� *CurrentPlayer.Nickname+��6U   �� *CurrentPlayer.Colour+# [ 6�   5)      �� 5+      $��2��6   $��10q�6(   $	��200��64   ��6D   �� *OtherPlayers+ �� View 5,      �� 5-      .P�5.      �� ��5/   "   �� 50   &   $��5q�6;   .��6G   Name52   '    �� CellTemplate 54   "   �� 55   &   .R�56   *   �� $��5q�64   ��6?   �� *
Nickname+57   '   59      �� 5:      ��5;   "   �� 5<   &   $��5q�6;   .��6G   Colour5>   '    5@   "   �� 5A   &   .R�5B   *   �� $��5q�64   $��10��6?   ��6N   �� *Colour+# [ 5C   '   5E      5F      5H      5I      5J      �� 5M   
   $��0��6   $��1��6"    �� Orientation$ 
Horizontal=�62   $
��Auto��6K   $
��Auto��6X   .+�5O      �� .+�5P      �� $��10q�6   $��Center=�6.   $��Center=�6K   ._�5Q      �� $��10q�6   ��62   �� *PathToBackgroundMenuImage+# [ ._�5R      �� $��Center=�5T      $��Center=�61   ��5R   $   �� *PathToClubImage+# [ ��5S      �� *
CardHeight+��62   �� *	CardWidth+5T   1   5U      5V      5W      �� 5Y      .+�5Z      �� ��6   �� *Queue+# [  ��5[      �� 5\      .R�5]      �� $ 
Horizontal=�6)   $��Center=�6B   $��Center=�6_   5^      ��5`      5  -System.Windows.Controls.ItemContainerTemplate�  5a      .R�5b      �� $��10q�6%   ._�5c   "   �� $��10q�6)   ��6>   �� *Stroke+._�5d   &   �� $��Center=�5e   ,   $��Center=�6I   ��5d   0   �� *Fill+��6G   �� *Height+��6a   �� *Width+5e   I   5f   #   5g      5h      5j      5k      5l      �� 5o   
   $��2��6   $��1��6"   $ 
Horizontal=�62   $
��Auto��6K   $
��Auto��6X   .+�5q      �� .+�5r      �� $��10q�6   $��Center=�6.   $��Center=�6K   ._�5s      �� $��10q�6   
���  ��._�5t      � .+�5u      �� $��Center=�5w      $��Center=�61   ��5u   (   �� *PathToDeckImage+# [ ��5v      �� *
CardHeight+��62   �� *	CardWidth+5w   1   �� 5x      ��6(   �� *Test+��6>   �� *
CardHeight+��6\   �� *	CardWidth+�� 5y      $��Nachziehstabel��6(   $��Medium"�6>   $��Center=�6n   $��Center=�6�   ��6R   �� *	CardWidth+6�   5z      5{      5|      5}      �� 5      .+�5�      �� ��6   �� *	Handcards+# [  ��5�      �� 5�      .R�5�      �� $ 
Horizontal=�6)   $��Center=�6B   $��Center=�6_   5�      ��5�        5�      .R�5�      �� $��10q�6%   ._�5�   "   �� $��10q�6)   ��6>   �� *Stroke+._�5�   &   � .+�5�   *   �� ��64   �� *Fill+��6J   �� *Height+��6d   �� *Width+�� 5�   *   -   5        ��5�   4   �� *
HoverLayer+��6P   �� *Height+��6j   �� *Width+6�   5�   '   5�   #   5�      5�      5�      5�      5�      5�      5�      