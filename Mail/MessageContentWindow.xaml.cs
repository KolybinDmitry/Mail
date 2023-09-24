using System;
using System.Windows;
using MimeKit;

namespace Mail
{
    public partial class MessageContentWindow : Window
    {
        private MimeMessage message;

        public MessageContentWindow(MimeMessage message)
        {
            InitializeComponent();

            
            this.message = message;

            
            SenderTextBlock.Text = message.From.ToString();
            RecipientTextBlock.Text = message.To.ToString();
            SubjectTextBlock.Text = message.Subject;
            ContentTextBlock.Text = GetMessageContent(message);

            
            if (message.From.Count > 0)
            {
                string senderEmail = message.From[0].ToString();
                ReplyButton.Content = $"Ответить ({senderEmail})";
            }
        }

        private string GetMessageContent(MimeMessage message)
        {
           

           
            return message.TextBody;
        }

        private void ReplyButton_Click(object sender, RoutedEventArgs e)
        {
            
            string senderEmail = message.From.ToString();
            string subject = "Re: " + message.Subject;

          
            var composeWindow = new ComposeMessageWindow(senderEmail, subject);
            composeWindow.Show();

        }

    }
}