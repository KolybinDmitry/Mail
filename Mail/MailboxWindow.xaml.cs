using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MimeKit;
using MailKit;
using MailKit.Net.Imap;
using System.Linq;
using System.Windows.Input;

namespace Mail
{
    public partial class MailboxWindow : Window
    {
        // Коллекция для хранения данных о папках пользователя
        private ObservableCollection<string> userFolders;
        private ImapClient imapClient; // Поле для клиента ImapClient

        public MailboxWindow()
        {
            InitializeComponent();
            userFolders = new ObservableCollection<string>();
            FolderListView.ItemsSource = userFolders;

            imapClient = new ImapClient(); // Создание экземпляра ImapClient

            // Загрузка папок пользователя после авторизации
            LoadUserFolders();
        }

        private async void LoadUserFolders()
        {
            // Подключение к почтовому серверу и аутентификация (замените на реальные данные)
            await imapClient.ConnectAsync("imap.mail.ru", 993, true);
            await imapClient.AuthenticateAsync("kolybin05@mail.ru", "RXZpuPTUgUUqW6CRGXHs");

            // Получение списка папок пользователя
            var personal = imapClient.GetFolder(imapClient.PersonalNamespaces[0]);
            personal.Open(FolderAccess.ReadOnly);
            var folderNames = personal.GetSubfolders().Select(folder => folder.Name);

            // Очистка и заполнение коллекции папок
            userFolders.Clear();
            foreach (var folderName in folderNames)
            {
                userFolders.Add(folderName);
            }
        }

        private async void FolderListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FolderListView.SelectedItem is string selectedFolder)
            {
                // Подключение к выбранной папке и загрузка сообщений
                var folder = imapClient.GetFolder(selectedFolder);
                await folder.OpenAsync(FolderAccess.ReadOnly);
                var messages = await folder.FetchAsync(0, -1, MessageSummaryItems.Full);

                // Очистка списка сообщений и отображение сообщений
                MessageListView.Items.Clear();
                foreach (var message in messages)
                {
                    var listItem = new ListViewItem
                    {
                        Content = new
                        {
                            Sender = message.Envelope.From,
                            Subject = message.Envelope.Subject,
                            Date = message.Date
                        }
                    };
                    MessageListView.Items.Add(listItem);
                }
            }
        }


        // Обработчик двойного щелчка по сообщению
        private void MessageListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MessageListView.SelectedItem is ListViewItem selectedItem)
            {
                // Получаем MimeMessage из элемента списка
                var message = GetMessageFromListViewItem(selectedItem);

                // Открываем окно просмотра сообщения с переданным MimeMessage
                ShowMessageContent(message);
            }
        }


        // Отображение окна с содержимым сообщения
        private void ShowMessageContent(MimeMessage message)
        {
            var messageContentWindow = new MessageContentWindow(message);
            messageContentWindow.Show();
        }

        // Парсинг MimeMessage из элемента списка
        private MimeMessage GetMessageFromListViewItem(ListViewItem item)
        {
            var content = (dynamic)item.Content;
            var sender = content.Sender;
            var subject = content.Subject;
            var date = content.Date;

            // Здесь реализуйте логику получения сообщения на основе sender, subject и date
            // Верните MimeMessage с данными сообщения
            // Пример:
            var message = new MimeMessage();
            message.From.Add(sender);
            message.Subject = subject;
            message.Date = date;
            // Добавьте другие данные сообщения, такие как тело, вложения и т. д.
            return message;
        }


    }
}
