   M S B A M L   `   `   `  ����J  FWpfApplication1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication1WpfApplication1  5   	        WpfApplication1.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfApplication1     5   	     � Title$  T-Test Analysis��5   	   $	��350��6!   $	��650��6.    � 
ResizeMode$ NoResize=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	   ���#F5F5F5 � 	Resources 5         CustomButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius	 �� Triggers
 P� Property P� Value��%�  &	        �� 5   
   " ��6*   .�5      ��  � �� �#4CAF506+   �� 5       � ��
 �����6+   �� 5       � ��$ 16(�6)   �� 5       � ��$
 Bold"�6+   �� 5       � ��$ 0q�60   �� 5       � ��$
 10,5q�6(   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5   !   #��z��5   !   $ 4��5   !   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5!      �� 5"       � ��$ Center=�64   �� 5#       � ��$ Center=�62   �� 5$       � �� 5%      �� 5&      " ��6&   .R�5'      �� #��z��6!   #��z��5(   !   #��z��5)   !   $ 4��5*   !   ._�5+      �� $��Center=�6/   $��Center=�6L   5,      5-      5/      	 50      P� 51      
 �@J�IsMouseOver . . �52      ��  � �� �#79b04e63   53      P� 54      
 � �� . . �55      ��  � �� �#388E3C63   56      58      59      .��5:      � .+�5<   
   �� $'��!Настройки анализа��6   $��20(�5=      $
��Bold"�5>      ���#333333$��Center=�5@      $	��Top=�5A      $��0,10,0,0q�5B      � 5E   
   $��20,40,20,10q�6    � RowDefinitions 5F      �� 5G      $
��Auto �6    �� 5H      $
��Auto �6    �� 5I      $
��Auto �6    �� 5J      $��* �6    .+�5N      � $��0��6   $��50��6    $��	0,10,0,10q�6,    � ColumnDefinitions 5O      �� 5P      $
��Auto �6'   �� 5Q      $��* �6'   �� 5R      $
��Auto �6'   .+�5T      �� $��15(�6   $��10,0,0,0q�6*   $��Center=�5U      $
��Left=�5V      $��SemiBold"�5W      ���#333333$��0��5Y      '%Файл с результатами:5[      �� 5\      ���#A9A9A9$��1q�5]      $ 4��5^      $��30��5_      $	��270��5`      $��1��5a      $��Center=�5b      $��10,0,0,0q�5c      
�������._�5e      �� -   5         ��Name$ FilePathTextBlock��5f      $��Center=�5g      $��12(�5h      $	��5,0q�5i      
������ $��0q�5k      5l      �� 5m      -   5        #��[  5m      $��Открыть��5n      $��14(�5o      $��SemiBold"�5p      $	��100��5q      $��30��5r      $��2��5s      $��	10,0,15,0q�5t      5u      5v      � 5y      $��1��6   $��50��6    $��	0,10,0,10q�6,    5z      �� 5{      $
��Auto �6'   �� 5|      $��* �6'   .+�5~      �� $��15(�6   $��10,0,0,0q�6*   $��Center=�5      $
��Left=�5�      $��SemiBold"�5�      ���#333333$��0��5�      &$Уровень значимости:5�      �� 5�      -   5         ��Name$ LevelBorder��5�      $��20,0,0,0q�5�      
������ 
���   �$��1q�5�   )   $��1��5�      $	��150��5�      $��30��6!   $��Center=�5�      $
��Left=�5�      ._�5�      � $	��150��5�      $��30��6%   
�������.+�5�      � -   5         �Name$ 	LevelGrid��5�      
����ح�$��75��5�      $
��Left=�5�      5�      � 5�      $��75��5�      $
��Left=�5�      .+�5�      �� $��Center=�6(   $��Center=�6E   0.015�      5�      � 5�      $��75��5�      $��Right=�5�      .+�5�      �� $��Center=�6(   $��Center=�6E   0.055�      5�      5�      5�      5�      � 5�      $��2��6   $��50��6    $��	0,10,0,10q�6,    5�      �� 5�      $
��Auto �6'   �� 5�      $��* �6'   .+�5�      �� $��15(�6   $��10,0,0,0q�6*   $��Center=�5�      $
��Left=�5�      $��SemiBold"�5�      ���#333333$��0��5�      42Использовать предсказания:5�      �� 5�      -   5        $ PredictBorder��5�      $��20,0,0,0q�5�      
������ 
���   �$��1q�5�   )   $��1��5�      $	��175��5�      $��30��6!   $��Center=�5�      $
��Left=�5�      ._�5�      � $	��175��5�      $��30��6%   
�������.+�5�      � -   5        $ PredictGrid��5�      
����ح�$	��100��5�      $
��Left=�5�      5�      � 5�      $	��100��5�      $
��Left=�5�      .+�5�      �� $��Center=�6(   $��Center=�6E   Пользователи5�      5�      � 5�      $��75��5�      $��Right=�5�      .+�5�      �� $��Center=�6(   $��Center=�6E   GPT5�      5�      5�      5�      5�      � 5�      $��3��6   $��0,20,0,0q�6    .+�5�      �� -   5        #��[  5�      $��Начать анализ��5�      $	��200��5�      $��40��5�      $��Bottom=�5�      5�      5�      5�      5�      5�      