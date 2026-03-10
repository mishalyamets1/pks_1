using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LibraryManager.Models;
using LibraryManager.Services;
using LibraryManager.ViewModels;
using LibraryManager.Views;

namespace LibraryManager
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel = new MainViewModel();
        private DataService _dataService = new DataService();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = _viewModel;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SearchBooks();
        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SearchText = string.Empty;
            _viewModel.LoadBooks();
            ComboAuthors.SelectedItem = null;
            ComboGenres.SelectedItem = null;
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.FilterBooks();
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            ComboAuthors.SelectedItem = null;
            ComboGenres.SelectedItem = null;
            _viewModel.SelectedAuthor = null;
            _viewModel.SelectedGenre = null;
            _viewModel.LoadBooks();
        }

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            var bookWindow = new BookWindow(null, _viewModel);
            bookWindow.ShowDialog();
        }

        private void EditBook_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedBook == null)
            {
                MessageBox.Show("Пожалуйста, выберите книгу для редактирования");
                return;
            }

            var bookWindow = new BookWindow(_viewModel.SelectedBook, _viewModel);
            bookWindow.ShowDialog();
        }

        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteBook();
        }

        private void ManageAuthors_Click(object sender, RoutedEventArgs e)
        {
            var authorWindow = new AuthorWindow(_viewModel);
            authorWindow.ShowDialog();
        }

        private void ManageGenres_Click(object sender, RoutedEventArgs e)
        {
            var genreWindow = new GenreWindow(_viewModel);
            genreWindow.ShowDialog();
        }

        private void LoadBooks_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.LoadBooks();
        }
    }
}