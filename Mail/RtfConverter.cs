using MimeKit.Text;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Mail
{
    public class RtfConverter
    {
        public string ExtractRtfFromMimeMessage(MimeMessage mimeMessage)
        {
            foreach (var bodyPart in mimeMessage.BodyParts)
            {
                if (bodyPart is TextPart textPart)
                {
                    var text = textPart.GetText(Encoding.UTF8);
                    if (IsRtf(text))
                    {
                        return text;
                    }
                }
            }

            // Если RTF-содержимое не найдено, вернуть пустую строку или выполнить другие действия по вашему усмотрению.
            return string.Empty;
        }

        private bool IsRtf(string text)
        {
            // Простая проверка на наличие RTF-метки.
            return text.Contains("{\\rtf");
        }
    }





}
