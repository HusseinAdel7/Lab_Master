   M S B A M L   `   `   `  ����O  KComputer Labs Sketch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Computer_Labs_SketchComputer_Labs_Sketch  5   	   '    !Computer_Labs_Sketch.SoftwareInfo   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:Computer_Labs_Sketch     5   	     � Title$  SoftwareInfo��5   	   $	��780��5	   
   $
��1300��6    � WindowStyle$
 None=�5
       � WindowStartupLocation$ CenterScreen=�5       � 
ResizeMode$ NoResize=�5      
�������.��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      .+�5   
   � -   5        ���#cccccc$��3��5   $   $��0,-8,0,8q�6S    � ColumnDefinitions 5      �� 5      �� 5      .+�5      � .+�5       �� $L��Fمعلومات عن البرامح المثبتة علي الحهاز ��6   
���   �$��23(�5"      $��SemiBold"�5#      $��518,-4,-220,4q�6&   5$      � 5%      $��2��6   .+�5&      �� $��Right=�6    �� Orientation$ 
Horizontal=�5'      .+�5(      �� -   5        $��30��5(      $��30��5)      $	��—��5*      
������ 
�������$��0q�5-      $��22(�5.      $
��bold"�5/      $��0 0 13 0q�50      51      �� 52      -   5        $��30��52      $��30��53      $	��❌��54      
������ 
���  ��$��0q�57      $��20(�58      59      5:      5;      5<      � 5B   
   .+�5D      �� $��214,171,212,-542q�6   .+�5E      &   System.Windows.Controls.DataGrid  -   5          Name$ 	Softwares��5F      $	��410��5E         AutoGenerateColumns$ False��5G      	   
IsReadOnly$
	 True��5H      $	��862��5I      $��Center=�5J      ���#9c9797
 � 	Resources
 5L       �� 
TargetType �� Property �� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader*  $System.Windows.Controls.DataGridCell   CenteredCellTextStyle��%�  '��       '  �     '  Q    '  �    &	  �    �� 5M      " ��6    .�5N      ��  � ��
 �   �6;   �� 5O       � ��
 �   �6;   �� 5P       � �� �#0f0f2d6<   5Q      �� 5R      "  6    .�5T      ��  � ��
 �   �6;   �� 5U       � ��$ 16(�69   �� 5V       � ��$ 1q�6@   �� 5W       � ��
 �����6<   5X      �� 5Z      "  6    .�5[      ��  � �� �#cccccc6;   �� 5\       � ��
 �   �6;   �� 5]       � ��$ 20(�69   �� 5^       � ��$ SemiBold"�6;   �� 5_       � ��$ Center=�6K   �� 5`       � ��$ 1q�6@   �� 5a       � ��
 �����6<   5b      �� 5d      "  6    .�5e      ��  � �� �#ada8a86;   �� 5f       � ��
 �   �6;   5g      �� 5i      " ��6>   .�5j      ��  �@��TextAlignment$ Center=�6>   �� 5k       � ��$ Center=�6B   �� 5l       � ��
 �   �6;   5m      5n         Columns 5o      0  *System.Windows.Controls.DataGridTextColumn  5p      ,  &System.Windows.Controls.DataGridColumn   Header$ اسم البرنامج��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$	 300 6d   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6C   �� *softwareName+   ElementStyle 5q      �� 5r   "   " ��6(    �� BasedOn# [  6H   5t        5u      $ اصدار البرامج��6-   $	 200 6`    6D   �� *	version+ 5v      �� 5w   "   " ��6(   # [  6H   5y        5z      $ فئة البرنامج��6-   $	 336 6h    6C   �� *softwareCategory+ 5{      �� 5|   "   " ��6(   # [  6H   5~      5�      5�      5�      �� 5�   
   -   5         ��Name$
 Back��5�   	   $�� ��5�      $
��Left=�5�   	   $��1171,24,0,657q�5�   	   $	��125��5�   	   
�������$��35(�5�   	   $��0q�5�   	   $��1��5�      ��5�      �� 5�       �� ImageSource$� �G:\Projects\TMC Projects\MyProject\Newest One\Projects\TMC Projects\Computer Labs Sketch\Computer Labs Sketch - Copy\Computer Labs Sketch\images\back.png��6    j� Stretch$ Uniform=�5�       j� 
AlignmentX$ Center=�5�       j� 
AlignmentY$ Center=�5�      ��5�      �� 5�      " ��6   .�5�      ��  � �� �#18173563   �� 5�       �@�Cursor$
 Hand��6/    �� Triggers 5�      P� 5�       P� Property �@J�IsMouseOver P� Value$
 True��69   . �5�      ��  � �� �#0f0f2d6;   5�      5�      ��5�      �� 5�       �� 
TargetType" ��6"   .R�5�      �� $��90��6   $��30��5�       �� CornerRadius$ 15��5�      #��z��5�      ._�5�      �� $��Center=�6-   $��Center=�6H   5�      5�      5�      5�      5�      