using System;
using System.Windows;
using MimeKit;
using MailKit.Net.Smtp;

namespace Mail
{
    public partial class ComposeMessageWindow : Window
    {
        private readonly string senderEmail;

        public ComposeMessageWindow(string senderEmail, string subject)
        {
            InitializeComponent();
            this.senderEmail = senderEmail;

            // Заполните поле "Получатель" адресом отправителя текущего сообщения
            RecipientTextBox.Text = senderEmail;

            // Заполните поле "Тема" темой текущего сообщения (можете дополнить "Re:")
            SubjectTextBox.Text = subject;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string recipient = RecipientTextBox.Text;
                string subject = SubjectTextBox.Text;
                string content = ContentTextBox.Text;

                // Создайте MimeMessage и установите его поля (отправитель, получатель, тема, содержание)
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Имя отправителя", senderEmail));
                message.To.Add(new MailboxAddress("Имя получателя", recipient));
                message.Subject = subject;

                var textPart = new TextPart("plain")
                {
                    Text = content
                };

                // Добавьте текстовую часть к сообщению
                message.Body = textPart;

                // Отправьте сообщение
                SendEmail(message);

                MessageBox.Show("Сообщение успешно отправлено!");

                // Закройте окно после отправки
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при отправке сообщения: " + ex.Message);
            }
        }

        private void SendEmail(MimeMessage message)
        {
            // Замените настройками SMTP вашего почтового сервиса
            string smtpServer = "smtp.example.com";
            int smtpPort = 587;
            string username = "your_username";
            string password = "your_password";

            using (var client = new SmtpClient())
            {
                client.Connect(smtpServer, smtpPort, false);
                client.Authenticate(username, password);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
