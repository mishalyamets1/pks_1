using System;
using System.Windows;
using LibraryManager.Models;
using LibraryManager.Services;
using LibraryManager.ViewModels;

namespace LibraryManager.Views
{
    public partial class GenreWindow : Window
    {
        private DataService _dataService = new DataService();
        private MainViewModel _mainViewModel;

        public GenreWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            _mainViewModel = mainViewModel;
            this.DataContext = mainViewModel;
        }

        private void AddGenre_Click(object sender, RoutedEventArgs e)
        {
            var addGenreWindow = new GenreEditWindow(null);
            if (addGenreWindow.ShowDialog() == true)
            {
                _mainViewModel.LoadAllData();
            }
        }

        private void EditGenre_Click(object sender, RoutedEventArgs e)
        {
            if (_mainViewModel.SelectedGenre == null)
            {
                MessageBox.Show("Пожалуйста, выберите жанр для редактирования");
                return;
            }

            var editGenreWindow = new GenreEditWindow(_mainViewModel.SelectedGenre);
            if (editGenreWindow.ShowDialog() == true)
            {
                _mainViewModel.LoadAllData();
            }
        }

        private void DeleteGenre_Click(object sender, RoutedEventArgs e)
        {
            if (_mainViewModel.SelectedGenre == null)
            {
                MessageBox.Show("Пожалуйста, выберите жанр для удаления");
                return;
            }

            var result = MessageBox.Show(
                $"Вы уверены, что хотите удалить жанр '{_mainViewModel.SelectedGenre.Name}'?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _dataService.DeleteGenre(_mainViewModel.SelectedGenre.Id);
                    _mainViewModel.LoadAllData();
                    MessageBox.Show("Жанр успешно удален");
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
