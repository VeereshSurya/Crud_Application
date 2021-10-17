using CrudApplication.Commands;
using CrudApplication.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CrudApplication.Modules.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged , IDataErrorInfo
    {

        #region Properties and Declarations 


        private string _bookName;

        public string BookName
        {
            get { return _bookName; }
            set
            {
                _bookName = value;
                NotifyPropertyChanged(nameof(BookName));
            }
        }

        private string _author;
        public string Author
        {
            get { return _author; }
            set
            {
                _author = value;
                NotifyPropertyChanged(nameof(Author));
            }
        }

        private int _price;
        public int Price
        {
            get { return _price; }
            set
            {
                _price = value;
                NotifyPropertyChanged(nameof(Price));
            }
        }

        private string _publications;
        public string Publications
        {
            get { return _publications; }
            set
            {
                _publications = value;
                NotifyPropertyChanged(nameof(Publications));
            }
        }

        private Visibility _isVisibleUpdateAndCancle { get; set; }

        public Visibility IsVisibleUpdateAndCancle
        {
            get { return _isVisibleUpdateAndCancle; }
            set
            {
                _isVisibleUpdateAndCancle = value;
            }
        }

        private ObservableCollection<Book> _allBooks;
        public ObservableCollection<Book> AllBooks
        {
            get { return _allBooks; }
            set
            {
                _allBooks = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string _error;
        public string Error
        {
            get { return _error; }
            set
            {
                if (_error != value)
                {
                    _error = value;
                    NotifyPropertyChanged(nameof(Error));
                }
            }
        }

        #endregion

        #region Commands 

        public ICommand AddBookCommand { get; }
        public ICommand CancleCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }


        #endregion

        #region Constructor

        public HomeViewModel()
        {
            AllBooks = new ObservableCollection<Book>();
            IsVisibleUpdateAndCancle = Visibility.Hidden;
            AddBookCommand = new RelayCommand(AddBookCommandHandler, f => CanExecuteAddBookCommandHandler());
            CancleCommand = new RelayCommand(CancleCommandHandler, f => true);
            UpdateCommand = new RelayCommand(UpdateCommandHanlder, f => CanExecuteUpdateCommandHandler());
            DeleteCommand = new RelayCommand(DeleteCommandHandler, f => true);
        }

        #endregion

        #region Privite Methods 

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                switch (columnName)
                {
                    case "BookName":

                        if(BookName != null)
                        {
                            if(string.Empty == BookName.Trim())
                            {
                                result = "Book name is required";
                                break;
                            }

                            if (!Regex.IsMatch(BookName.Trim(), @"^[a-zA-Z]+$"))
                            {
                                result = "Please enter valid book name";
                                break;
                            }
                        }

                        break;

                    case "Price":

                        if(Price < 0)
                        {
                            result = "Price should be greater then 0";
                        }

                        break;


                    case "Author":
                        if (Author != null)
                        {
                            if (string.Empty == Author.Trim())
                            {
                                result = "Author is required";
                                break;
                            }

                            if (!Regex.IsMatch(Author.Trim(), @"^[a-zA-Z]+$"))
                            {
                                result = "Please enter valide Author";
                                break;
                            }
                        }
                        break;

                    case "Publications":
                        if (Publications != null)
                        {
                            if (string.Empty == Publications.Trim())
                            {
                                result = "Publications is required";
                                break;
                            }

                            if (!Regex.IsMatch(Publications.Trim(), @"^[a-zA-Z]+$"))
                            {
                                result = "Please enter valid Publications";
                                break;
                            }
                        }
                        break;
                }

                if (string.IsNullOrEmpty(result))
                {
                    Error = null;
                }

                else
                {
                    Error = "Error!";
                }

                return result;

            }
        }

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void AddBookCommandHandler(object parameter)
        {
            Book book = new Book()
            {
                Name = BookName,
                Author = Author,
                Price = Price,
                Publications = Publications
            };

            AllBooks.Add(book);
        }

        private bool CanExecuteAddBookCommandHandler()
        {
            if(string.IsNullOrEmpty(BookName) || string.IsNullOrEmpty(Author) || string.IsNullOrEmpty(Publications) ||
                Price <= 0 || Error != null)
            {
                return false;
            }

            return true;
        }


        private void CancleCommandHandler(object parameter)
        {
            BookName = null;
            Author = null;
            Price = 0;
            Publications = null;
        }

        private void UpdateCommandHanlder(object parameter)
        {
            // call the dbservice method by passing all the details and before call
        }

        private bool CanExecuteUpdateCommandHandler()
        {
            // do input validation here 
            return true;
        }

        private void DeleteCommandHandler(object parameter)
        {
            // call db service method here and show proper message to the user .
        }

        #endregion

        #region Public Methods



        #endregion
    }
}
