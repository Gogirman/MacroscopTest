using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Forms;
using System.IO;
using System.Net.Http;
using CoreStandart.Services;
using System;
using Client.Models;

namespace Client
{
    /// <summary>
    ///   Модель представления клиента.
    /// </summary>
    class MainWindowVM : ViewModelBase
    {
        /// <summary>
        ///   Команда добавления документов по указанной директории.
        /// </summary>
        private RelayCommand _populateDocumentsCommand;

        /// <summary>
        ///   Команда отправки запросов.
        /// </summary>
        private RelayCommand _sendRequestsCommand;

        /// <summary>
        ///   Команда добавления документов по указанной директории.
        /// </summary>
        public ICommand PopulateDocumentsCommand
            => _populateDocumentsCommand ?? (_populateDocumentsCommand = new RelayCommand(PopulateDocuments));


        /// <summary>
        ///   Команда отправки запросов.
        /// </summary>
        public ICommand SendRequestsCommand
            => _sendRequestsCommand ?? (_sendRequestsCommand = new RelayCommand(SendRequests));


        private HttpClient _client = new HttpClient();

        /// <summary>
        ///   Список выбранных документов.
        /// </summary>
        private List<DocumentVM> _documents
            = new List<DocumentVM>();
        private readonly IServicePalindrom _servicePalindrom;

        /// <summary>
        ///   Конструктор.
        /// </summary>
        public MainWindowVM(IServicePalindrom servicePalindrom)
        {
            _servicePalindrom = servicePalindrom;
        }

        /// <summary>
        ///   Список документов.
        /// </summary>
        public List<DocumentVM> Documents
            => new List<DocumentVM>(_documents);

        /// <summary>
        ///   Заполнение списка документов.
        /// </summary>
        private void PopulateDocuments()
        {
            string[] fileNames;
            List<Document> tempDocs = new List<Document>();
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                fileNames = Directory.GetFiles(dialog.SelectedPath);
                foreach (var fileName in fileNames)
                {
                    using (FileStream fstream = File.OpenRead(fileName))
                    {
                        byte[] array = new byte[fstream.Length];
                        // считываем данные
                        fstream.Read(array, 0, array.Length);
                        // декодируем байты в строку
                        var textFromFile = Encoding.UTF8.GetString(array);
                        tempDocs.Add(new Document
                        {
                            Name = fileName,
                            RequestStatus = Document.RequestStatuses.NotSent,
                            Text = textFromFile
                        });
                    }
                }

                _documents.Clear();
                _documents.AddRange(tempDocs.OrderBy(d => d.Name).Select(d => new DocumentVM(d, this)));
                RaisePropertyChanged(nameof(Documents));
            }
        }

        /// <summary>
        ///   Отправка запросов.
        /// </summary>
        private void SendRequests()
        {
            var tasks = _documents.Select(x => Task.Run(() => PutAsync(x.Text, x.Document.Id))).ToArray();
            var results = Task.WhenAll(tasks).GetAwaiter().GetResult();

            HandlingResponse(results);
        }

        /// <summary>
        ///   Обработка запросов.
        /// </summary>
        private void HandlingResponse(ResponseResult[] results)
        {
            foreach (var result in results)
            {
                var document = _documents.Where(t => t.Document.Id == result.DocumentId).FirstOrDefault();
                document.IsPalindrome = result.Answer;
                document.RequestStatus = Document.RequestStatuses.Sent;
            }
        }
        /// <summary>
        ///   Асинхронный запрос серверу.
        /// </summary>
        private async Task<ResponseResult> PutAsync(string text, Guid id)
        {
            var haveResult = false;
            bool result = false;
            //Отправляем запросы, пока на все не получим ответ
            while(!haveResult)
            {
                try
                {
                    result = await _servicePalindrom.CheckPalindrom(text);
                    haveResult = true;
                }
                catch
                { }
            }

            return new ResponseResult { DocumentId = id, Answer = result };
        }
    }
}
