   M S B A M L   `   `   `  ����  Mail    Mail.MailboxWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Mailbox��5   	   $	��600��6   $	��800��6&   .��5      � .+�5	   
   �� -   5         ��Name$ ServiceComboBox��5	      $
��Left=�6,   $	��Top=�6G   $	��200��6_   .@�5
      �� $��Mail.ru��6   �� 5      $��	Yandex.ru��6   �� 5      $��
Rambler.ru��6   5      �� 5   
   -   5         ��Name$ FolderListView��5      $
��Left=�6+   $	��Top=�6F   $	��200��6^    �� View 5      �� 5      .P�5      �� $!��Название папки��6%    �� Width$	 150��6=    �� DisplayMemberBinding 6I   �� *
FolderName+�� 5      $%��Количество писем��6%   $ 50��6?    6J   �� *
EmailCount+5      5      �� 5   
   -   5        $ MessageListView��5      $
��Left=�6,   $	��Top=�6G   $	��500��6_   $��
229,-1,0,0q�6k    5      �� 5      .P�5      �� $��Отправитель��6%   $	 150��6:    6F   �� *Sender+�� 5      $��Тема��6%   $	 200��63    6?   �� *	Subject+�� 5      $��Дата��6%   $	 150��63    6?   �� *Date+5       5"       � 5%   
   -   5          �Name$ MessageFrame��5%      $
��Left=�6&   $	��Top=�6A   $	��400��6Y   $	��300��6e   $��0,48,0,0q�6r   5&      5'      