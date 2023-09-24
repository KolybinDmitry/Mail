using MailKit;
using MailKit.Net.Imap;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mail
{
    public class ImapHelper
    {
        private readonly string host;
        private readonly int port;
        private readonly bool useSsl;

        public ImapHelper(string host, int port, bool useSsl)
        {
            this.host = host;
            this.port = port;
            this.useSsl = useSsl;
        }

        public async Task<IEnumerable<string>> GetFoldersAsync(string username, string password)
        {
            using (var client = new ImapClient())
            {
                await client.ConnectAsync(host, port, useSsl);
                await client.AuthenticateAsync(username, password);

                var folderNames = new List<string>();

                // Получение списка всех доступных папок
                var personal = client.GetFolder(client.PersonalNamespaces[0]);
                await personal.OpenAsync(FolderAccess.ReadOnly);

                // Получаем список всех папок
                var allFolders = personal.GetSubfolders();

                // Добавляем имена папок в список
                folderNames.AddRange(allFolders.Select(folder => folder.Name));

                return folderNames;
            }
        }





        public async Task<int> GetEmailCountAsync(string username, string password, string folderName)
        {
            using (var client = new ImapClient())
            {
                await client.ConnectAsync(host, port, useSsl);
                await client.AuthenticateAsync(username, password);

                var folder = client.GetFolder(folderName);
                folder.Open(FolderAccess.ReadOnly);

                return folder.Count;
            }
        }
    }
}
