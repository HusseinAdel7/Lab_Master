   M S B A M L   `   `   `  ����O  KComputer Labs Sketch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Computer_Labs_SketchComputer_Labs_Sketch  5   	   '    !Computer_Labs_Sketch.StudentsInfo   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:Computer_Labs_Sketch     5   	     � Title$  StudentsInfo��5   	   $	��780��5	   
   $
��1300��6    � WindowStyle$
 None=�5
       � WindowStartupLocation$ CenterScreen=�5       � 
ResizeMode$ NoResize=�5      
������� � 	Resources 5         CenteredCellTextStyle �� 
TargetType �� Property �� Value��%�   &	        �� 5   
   " ��6.   .�5      ��  �@��TextAlignment$ Center=�6.   �� 5       � ��$ Center=�62   �� 5       � ��
 �   �6+   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      .+�5$   
   � -   5        ���#cccccc$��3��5$   $   $��0,-8,0,8q�6S   	 � ColumnDefinitions	 5%      �� 5&      �� 5'      .+�5)      � .+�5+      �� $N��Hمعلومات عن الطالب المستخدم لهذا الحهاز ��6   
���   �$��23(�5-      $��SemiBold"�5.      $��518,-4,-304,4q�6#   5/      � 50      $��2��6   .+�51      �� $��Right=�6   
 �� Orientation$
 
Horizontal=�52      .+�53      �� -   5        $��30��53      $��30��54      $	��—��55      
������ 
�������$��0q�58      $��22(�59      $
��bold"�5:      $��0 0 13 0q�5;      5<      �� 5=      -   5        $��30��5=      $��30��5>      $	��❌��5?      
������ 
���  ��$��0q�5B      $��20(�5C      5D      5E      5F      5G      � 5M   
   .+�5O      �� $��214,171,198,-542q�6   .+�5P      &   System.Windows.Controls.DataGrid  -   5          Name$ students��5Q      $	��410��5P         AutoGenerateColumns$ False��5R         
IsReadOnly$
 True��5S      $	��848��5T      $��Center=�5U      ���#9c9797 5W      =  7System.Windows.Controls.Primitives.DataGridColumnHeader*  $System.Windows.Controls.DataGridCell��%   '��       '  �     '  Q    '  �    �� 5X      " ��6    .�5Y      ��  � ��
 �   �6;   �� 5Z       � ��
 �   �6;   �� 5[       � �� �#0f0f2d6<   5\      �� 5]      "  6    .�5^      ��  � ��
 �   �6;   �� 5_       � ��$ 16(�69   �� 5`       � ��$ 1q�6@   �� 5a       � ��
 �����6<   5b      �� 5c      "  6    .�5d      ��  � �� �#cccccc6;   �� 5e       � ��
 �   �6;   �� 5f       � ��$ 20(�69   �� 5g       � ��$ SemiBold"�6;   �� 5h       � ��$ Center=�6K   �� 5i       � ��$ 1q�6@   �� 5j       � ��
 �����6<   5k      �� 5l      "  6    .�5m      ��  � �� �#ada8a86;   �� 5n       � ��
 �   �6;   5o      5p         Columns 5q      0  *System.Windows.Controls.DataGridTextColumn  5r      ,  &System.Windows.Controls.DataGridColumn   Header$. (الرقم التسلسلي للطالب��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$	 218 6j   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6L   �� *	studentID+   ElementStyle 5s      �� 5t   "   " ��6(    �� BasedOn# [  6H   5v        5w      $ اسم الطالب��6-   $	 260 6a    6A   �� *StudentName+ 5x      �� 5y   "   " ��6(   # [  6H   5{        5|      $ رقم الطالب��6-   $	 200 6c    6A   �� *studentNumber+ 5}      �� 5~   "   " ��6(   # [  6H   5�        5�      $ الفرقة��6-   $	 162 6[    6=   �� *	groupName+ 5�      �� 5�   "   " ��6(   # [  6H   5�      5�      5�      5�      �� 5�   
   -   5         ��Name$
 Back��5�      $�� ��5�      $
��Left=�5�      $��1171,24,0,657q�5�      $	��125��5�      
�������$��35(�5�      $��0q�5�      $��1��5�      ��5�      �� 5�       �� ImageSource$� �G:\Projects\TMC Projects\MyProject\Newest One\Projects\TMC Projects\Computer Labs Sketch\Computer Labs Sketch - Copy\Computer Labs Sketch\images\back.png��6    j� Stretch$ Uniform=�5�       j� 
AlignmentX$ Center=�5�       j� 
AlignmentY$ Center=�5�      ��5�      �� 5�      " ��6   .�5�      ��  � �� �#18173563   �� 5�       �@�Cursor$
 Hand��6/    �� Triggers 5�      P� 5�       P� Property �@J�IsMouseOver P� Value$
 True��69   . �5�      ��  � �� �#0f0f2d6;   5�      5�      ��5�      �� 5�       �� 
TargetType" ��6"   .R�5�      �� $��90��6   $��30��5�       �� CornerRadius$ 15��5�      #��z��5�      ._�5�      �� $��Center=�6-   $��Center=�6H   5�      5�      5�      5�      5�      