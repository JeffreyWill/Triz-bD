   M S B A M L   `   `   `  ����
  trizbd    trizbd.EditVacationWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$1  +Редактирование отпуска��5   	   $	��400��6(   $	��400��65   ���#2c2f33.��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��* �6   �� 5	      $��80 �6   .+�5   
   �� $��0��6   $��24(�6   $��Center=�6+   ���#7289da.��6]   -+редактирование отпуска6u   � 5   
   $��1��6   $��20q�6    5      �� 5      $��40 �6    �� 5      $��40 �6    �� 5      $��40 �6    �� 5      $��40 �6     � ColumnDefinitions 5      �� 5      $	��150 �6#   �� 5      $��* �6#   .+�5      �� $��0��6   $��0��6!   
�������$��14(�6D   $��Center=�6R   .��6m   сотрудник:6y   �� 5      -   5         ��Name$ comboBoxEmployee_ID��5      $��0��64   $��1��6A   ���#36393f
���   ����#7289da$��Center=�6�   $��30��6�    �� DisplayMemberPath$ 	Full_Name��6�    �� SelectedValuePath$ Employee_ID��6�    �� SelectedValue 6�   �� *Employee_ID+6  �� 5      $��1��6   $��0��6!   
�������$��14(�6D   $��Center=�6R   .��6m   дата начала:6{   �� 5      -   5         ��Name$ textBoxStart_Date��5      $��1��61   $��1��6>   ���#36393f
����������#7289da$��Center=�6�   $��30��6�   ��6�   �� *
Start_Date+ �� StringFormat$ {0:dd.MM.yyyy}���� 5      $��2��6   $��0��6!   
�������$��14(�6D   $��Center=�6R   .��6m   дата окончания:6~   �� 5      -   5        $ textBoxEnd_Date��5      $��2��6/   $��1��6<   ���#36393f
����������#7289da$��Center=�6�   $��30��6�   ��6�   �� *
End_Date+$ {0:dd.MM.yyyy}���� 5      $��3��6   $��0��6!   
�������$��14(�6D   $��Center=�6R   .��6m   тип отпуска:6{   �� 5      -   5        $ textBoxVacation_Type��5      $��3��64   $��1��6A   ���#36393f
����������#7289da$��Center=�6�   $��30��6�   ��6�   �� *Vacation_Type+5      �� 5    
   $��2��6   	 �� Orientation$	 
Horizontal=�6"   $��Center=�6;   .+�5!      �� -   5        
 ��Name$
 
buttonSave��5!      $��40��6)   $	��150��65   $+��%сохранить изменения��6A   $��16(�6_   ���#7289da
�������$��0,0,10,0q�6�   �� 5"      -   5        $
 
buttonBack��5"      $��40��6)   $	��150��65   $��
назад��6A   $��16(�6Q   ���#7289da
�������6�   5#      5$      5%      