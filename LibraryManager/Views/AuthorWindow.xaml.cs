using System;
using System.Windows;
using LibraryManager.Models;
using LibraryManager.Services;
using LibraryManager.ViewModels;

namespace LibraryManager.Views
{
    public partial class AuthorWindow : Window
    {
        private DataService _dataService = new DataService();
        private MainViewModel _mainViewModel;

        public AuthorWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            _mainViewModel = mainViewModel;
            this.DataContext = mainViewModel;
        }

        private void AddAuthor_Click(object sender, RoutedEventArgs e)
        {
            var addAuthorWindow = new AuthorEditWindow(null);
            if (addAuthorWindow.ShowDialog() == true)
            {
                _mainViewModel.LoadAllData();
            }
        }

        private void EditAuthor_Click(object sender, RoutedEventArgs e)
        {
            if (_mainViewModel.SelectedAuthor == null)
            {
                MessageBox.Show("Пожалуйста, выберите автора для редактирования");
                return;
            }

            var editAuthorWindow = new AuthorEditWindow(_mainViewModel.SelectedAuthor);
            if (editAuthorWindow.ShowDialog() == true)
            {
                _mainViewModel.LoadAllData();
            }
        }

        private void DeleteAuthor_Click(object sender, RoutedEventArgs e)
        {
            if (_mainViewModel.SelectedAuthor == null)
            {
                MessageBox.Show("Пожалуйста, выберите автора для удаления");
                return;
            }

            var result = MessageBox.Show(
                $"Вы уверены, что хотите удалить автора '{_mainViewModel.SelectedAuthor.FirstName} {_mainViewModel.SelectedAuthor.LastName}'?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _dataService.DeleteAuthor(_mainViewModel.SelectedAuthor.Id);
                    _mainViewModel.LoadAllData();
                    MessageBox.Show("Автор успешно удален");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}");
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
