   M S B A M L   `   `   `  ����Q  MDuplicate_File_Scanner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Duplicate_File_ScannerDuplicate_File_Scanner  5   	   '    !Duplicate_File_Scanner.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:Duplicate_File_Scanner     5   	     � Title$  Duplicate file scanner��5   	   $	��350��6(   $	��525��65   .��5
      � .+�5   
   �� -   5        $��btnDirectory��5   %   $��	Directory��6   $
��Left=�69   $��23��6T   $��	10,10,0,0q�6`   $	��Top=�6s   $��55��6�    J� RenderTransformOrigin$ -0.032,-1.397&�6�   6�   �� 5   
   -   5        $��txbDirectory��5   -   $
��Left=�6   $��23��6A   $��	70,10,0,0q�6M    �� TextWrapping$
 Wrap=�6`   $	��Top=�6t   $	��262��6�   �� 5   
   -   5        $��btnScan��5       $
��Scan��6   $
��Left=�6/   $��23��6J   $��
344,10,0,0q�6V   $	��Top=�6j   $��65��6�   {�.  6�   � 5   
   $��10q�6   .+�5      �� -   5         ��Name$ DuplicateListView��5   Q   $��
10,63,10,0q�6   $��Stretch=�6,    �� SelectionMode$ Multiple=�6l    �� View 5(      �� 5)      .P�5*      �� $	��Key��6)    �� DisplayMemberBinding 66   �� *
GroupKey+�� 5+      $
��Path��6)    67   �� *
FilePath+5,      5.      50      �� 52   
   -   5        $��	btnDelete��52   "   $��Delete��6   $
��Left=�63   $��23��6N   $��
425,10,0,0q�6Z   $	��Top=�6n   $��60��6�   6�   53      54      