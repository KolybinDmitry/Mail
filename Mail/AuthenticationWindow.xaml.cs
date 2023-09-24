using System;
using System.Windows;
using System.Threading.Tasks;
using MailKit.Net.Imap;
using MimeKit;
using System.Windows.Controls;

namespace Mail
{
    public partial class AuthenticationWindow : Window
    {
        public AuthenticationWindow()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            // Получите выбранный сервис из ComboBox
            string selectedService = (ServiceComboBox.SelectedItem as ComboBoxItem)?.Content as string;

            try
            {
                bool isAuthenticated = false;

                // В зависимости от выбранного сервиса, подключаемся к соответствующему серверу и проверяем аутентификацию
                if (selectedService == "Mail.ru")
                {
                    isAuthenticated = await IsUserAuthenticated(email, password, "imap.mail.ru", 993);
                }
                else if (selectedService == "Yandex.ru")
                {
                    isAuthenticated = await IsUserAuthenticated(email, password, "imap.yandex.ru", 993);
                }
                else if (selectedService == "Rambler.ru")
                {
                    isAuthenticated = await IsUserAuthenticated(email, password, "imap.rambler.ru", 993);
                }

                if (isAuthenticated)
                {
                    MessageBox.Show("Авторизация успешна!");

                    // Переход к следующему окну, например, окну с папками
                    var mailboxWindow = new MailboxWindow();
                    mailboxWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка авторизации. Проверьте email и пароль.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при авторизации: " + ex.Message);
            }
        }

        private async Task<bool> IsUserAuthenticated(string email, string password, string imapServer, int port)
        {
            using (var client = new ImapClient())
            {
                await client.ConnectAsync(imapServer, port, true);
                await client.AuthenticateAsync(email, password);

                if (client.IsAuthenticated)
                {
                    await client.DisconnectAsync(true);
                    return true;
                }

                await client.DisconnectAsync(true);
                return false;
            }
        }
    }
}
