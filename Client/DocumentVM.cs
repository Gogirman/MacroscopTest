using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    /// <summary>
    ///   Модель представления документа.
    /// </summary>
    class DocumentVM : ViewModelBase
    {
        /// <summary>
        ///   Словарь источников значков.
        /// </summary>
        private readonly Dictionary<Document.RequestStatuses, string> imageSources = new Dictionary<Document.RequestStatuses, string>
        {
            {Document.RequestStatuses.NotSent, "pack://application:,,,/Client;component/Images/notsent.png"},
            {Document.RequestStatuses.Sent, "pack://application:,,,/Client;component/Images/successrequest.png"}
        };


        /// <summary>
        ///   Конструктор.
        /// </summary>
        public DocumentVM(Document document, MainWindowVM parentVM)
        {
            Document = document;
            ParentVM = parentVM;
        }

        /// <summary>
        ///   Документов.
        /// </summary>
        public Document Document
        { get; set; }

        /// <summary>
        ///   Родительская модель представления.
        /// </summary>
        public MainWindowVM ParentVM
        { get; set; }

        /// <summary>
        ///   Название документа.
        /// </summary>
        public string Name =>
            Document.Name;

        /// <summary>
        ///   Содержание документа.
        /// </summary>
        public string Text =>
            Document.Text;

        /// <summary>
        ///   Статус запроса.
        /// </summary>
        public Document.RequestStatuses RequestStatus
        {
            get
            {
                return Document.RequestStatus;
            }
            set
            {
                Document.RequestStatus = value;
                RaisePropertyChanged(nameof(RequestStatus));
                RaisePropertyChanged(nameof(ImageStr));
                
            }
        }

        /// <summary>
        ///   Является ли содержание документа палиндромом?
        /// </summary>
        public bool? IsPalindrome
        {
            get
            {
                return Document.IsPalindrome;
            }
            set
            {
                Document.IsPalindrome = value;
                RaisePropertyChanged(nameof(IsPalindrome));
                RaisePropertyChanged(nameof(IsPalindromeStr));
                RaisePropertyChanged(nameof(IsPalindromeVisibility));

            }
        }

        /// <summary>
        ///   Отображение ответа "палидром ли?".
        /// </summary>
        public string IsPalindromeStr =>
            IsPalindrome.HasValue ? IsPalindrome.Value ? "Это палиндром" : "Это не палиндром" : "";

        /// <summary>
        ///   Видимость ответа "палиндром ли?".
        /// </summary>
        public Visibility IsPalindromeVisibility =>
            IsPalindrome.HasValue ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        ///   Источник значка.
        /// </summary>
        public string ImageStr => imageSources[RequestStatus];

    }
}
