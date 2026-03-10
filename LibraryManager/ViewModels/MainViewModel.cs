using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LibraryManager.Models;
using LibraryManager.Services;

namespace LibraryManager.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private DataService _dataService = new DataService();
        private ObservableCollection<Book> _books = new ObservableCollection<Book>();
        private ObservableCollection<Author> _authors = new ObservableCollection<Author>();
        private ObservableCollection<Genre> _genres = new ObservableCollection<Genre>();
        private string _searchText = string.Empty;
        private Author? _selectedAuthor;
        private Genre? _selectedGenre;
        private Book? _selectedBook;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Book> Books
        {
            get => _books;
            set { SetProperty(ref _books, value); }
        }

        public ObservableCollection<Author> Authors
        {
            get => _authors;
            set { SetProperty(ref _authors, value); }
        }

        public ObservableCollection<Genre> Genres
        {
            get => _genres;
            set { SetProperty(ref _genres, value); }
        }

        public string SearchText
        {
            get => _searchText;
            set { SetProperty(ref _searchText, value); }
        }

        public Author? SelectedAuthor
        {
            get => _selectedAuthor;
            set { SetProperty(ref _selectedAuthor, value); }
        }

        public Genre? SelectedGenre
        {
            get => _selectedGenre;
            set { SetProperty(ref _selectedGenre, value); }
        }

        public Book? SelectedBook
        {
            get => _selectedBook;
            set { SetProperty(ref _selectedBook, value); }
        }

        public MainViewModel()
        {
            LoadAllData();
        }

        public void LoadAllData()
        {
            LoadBooks();
            LoadAuthors();
            LoadGenres();
        }

        public void LoadBooks()
        {
            try
            {
                var books = _dataService.GetAllBooks();
                Books = new ObservableCollection<Book>(books);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка загрузки книг: {ex.Message}");
            }
        }

        public void SearchBooks()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    LoadBooks();
                    return;
                }

                var books = _dataService.SearchBooks(SearchText);
                Books = new ObservableCollection<Book>(books);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка поиска: {ex.Message}");
            }
        }

        public void FilterBooks()
        {
            try
            {
                var books = _dataService.FilterBooks(SelectedAuthor?.Id, SelectedGenre?.Id);
                Books = new ObservableCollection<Book>(books);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка фильтрации: {ex.Message}");
            }
        }

        public void DeleteBook()
        {
            if (SelectedBook == null)
            {
                System.Windows.MessageBox.Show("Пожалуйста, выберите книгу для удаления");
                return;
            }

            var result = System.Windows.MessageBox.Show(
                $"Вы уверены, что хотите удалить '{SelectedBook.Title}'?",
                "Подтверждение",
                System.Windows.MessageBoxButton.YesNo);

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                try
                {
                    _dataService.DeleteBook(SelectedBook.Id);
                    LoadBooks();
                    System.Windows.MessageBox.Show("Книга успешно удалена");
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Ошибка удаления: {ex.Message}");
                }
            }
        }

        private void LoadAuthors()
        {
            try
            {
                var authors = _dataService.GetAllAuthors();
                Authors = new ObservableCollection<Author>(authors);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка загрузки авторов: {ex.Message}");
            }
        }

        private void LoadGenres()
        {
            try
            {
                var genres = _dataService.GetAllGenres();
                Genres = new ObservableCollection<Genre>(genres);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка загрузки жанров: {ex.Message}");
            }
        }

        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (!Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
