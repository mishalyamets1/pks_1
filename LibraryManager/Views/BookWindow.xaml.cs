using System;
using System.Windows;
using LibraryManager.Models;
using LibraryManager.Services;
using LibraryManager.ViewModels;

namespace LibraryManager.Views
{
    public partial class BookWindow : Window
    {
        private DataService _dataService = new DataService();
        private MainViewModel _mainViewModel;
        private Book? _book;

        public BookWindow(Book? book, MainViewModel mainViewModel)
        {
            InitializeComponent();
            _book = book;
            _mainViewModel = mainViewModel;

            try
            {
                // Загружаем авторов и жанры
                ComboAuthor.ItemsSource = _dataService.GetAllAuthors();
                ComboAuthor.DisplayMemberPath = "LastName";
                ComboAuthor.SelectedValuePath = "Id";

                ComboGenre.ItemsSource = _dataService.GetAllGenres();
                ComboGenre.DisplayMemberPath = "Name";
                ComboGenre.SelectedValuePath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                return;
            }

            if (_book != null)
            {
                TxtTitle.Text = _book.Title;
                ComboAuthor.SelectedValue = _book.AuthorId;
                ComboGenre.SelectedValue = _book.GenreId;
                TxtPublishYear.Text = _book.PublishYear.ToString();
                TxtISBN.Text = _book.ISBN;
                TxtQuantity.Text = _book.QuantityInStock.ToString();
                this.Title = "Редактировать книгу";
            }
            else
            {
                TxtQuantity.Text = "1";
                TxtPublishYear.Text = DateTime.Now.Year.ToString();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Валидация обязательных полей
            if (string.IsNullOrWhiteSpace(TxtTitle.Text))
            {
                MessageBox.Show("Пожалуйста, введите название книги", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ComboAuthor.SelectedValue == null)
            {
                MessageBox.Show("Пожалуйста, выберите автора", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ComboGenre.SelectedValue == null)
            {
                MessageBox.Show("Пожалуйста, выберите жанр", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtISBN.Text))
            {
                MessageBox.Show("Пожалуйста, введите ISBN", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Валидация числовых полей
            if (!int.TryParse(TxtPublishYear.Text, out int year) || year < 1000 || year > DateTime.Now.Year + 1)
            {
                MessageBox.Show("Год издания должен быть числом от 1000 до текущего года", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(TxtQuantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Количество должно быть положительным числом", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (_book == null)
                {
                    _book = new Book();
                }

                _book.Title = TxtTitle.Text.Trim();
                _book.AuthorId = (int)ComboAuthor.SelectedValue;
                _book.GenreId = (int)ComboGenre.SelectedValue;
                _book.PublishYear = year;
                _book.ISBN = TxtISBN.Text.Trim();
                _book.QuantityInStock = quantity;

                if (_book.Id == 0)
                {
                    _dataService.AddBook(_book);
                    MessageBox.Show("Книга успешно добавлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _dataService.UpdateBook(_book);
                    MessageBox.Show("Книга успешно обновлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                _mainViewModel.LoadBooks();
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка БД", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
