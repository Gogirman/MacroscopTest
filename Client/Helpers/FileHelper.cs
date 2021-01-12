using System.IO;
using System.Text;

namespace Client.Helpers
{
    public class FileHelper
    {
        public Document ReadFile(string fileName)
        {
            using (FileStream fstream = File.OpenRead(fileName))
            {
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                var textFromFile = Encoding.UTF8.GetString(array);
                return new Document
                {
                    Name = fileName,
                    RequestStatus = Document.RequestStatuses.NotSent,
                    Text = textFromFile
                };
            }
        }


    }
}
