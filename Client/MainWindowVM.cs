using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Forms;
using System.IO;
using CoreStandart.Services;
using Client.Models;
using Client.Helpers;

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

        /// <summary>
        ///   Список выбранных документов.
        /// </summary>
        private List<DocumentVM> _documents
            = new List<DocumentVM>();

        private readonly FileHelper _fileHelper;

        private readonly RequestHelper _requestHelper;

        /// <summary>
        ///   Конструктор.
        /// </summary>
        public MainWindowVM(IServicePalindrom servicePalindrom)
        {
            _fileHelper = new FileHelper();
            _requestHelper = new RequestHelper(servicePalindrom);
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
                    tempDocs.Add(_fileHelper.ReadFile(fileName));

                _documents.Clear();
                _documents.AddRange(tempDocs.OrderBy(d => d.Name).Select(d => new DocumentVM(d, this)));
                RaisePropertyChanged(nameof(Documents));
            }
        }

        /// <summary>
        ///   Отправка запросов.
        /// </summary>
        private async void SendRequests()
        {
            var tasks = _documents.Select(x => Task.Run(() => _requestHelper.SendRequest(x.Text, x.Document.Id).ContinueWith(r => HandlingResponse(r.Result), TaskContinuationOptions.OnlyOnRanToCompletion))).ToArray();
            await Task.WhenAll(tasks);
        }

        /// <summary>
        ///   Обработка запросов.
        /// </summary>
        private void HandlingResponse(ResponseResult result)
        {
            var document = _documents.Where(t => t.Document.Id == result.DocumentId).First();
            document.IsPalindrome = result.Answer;
            document.RequestStatus = Document.RequestStatuses.Sent;
            
        }
    }
}
