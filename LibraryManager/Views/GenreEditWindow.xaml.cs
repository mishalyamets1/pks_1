using System;
using System.Windows;
using LibraryManager.Models;
using LibraryManager.Services;

namespace LibraryManager.Views
{
    public partial class GenreEditWindow : Window
    {
        private DataService _dataService = new DataService();
        private Genre? _genre;

        public GenreEditWindow(Genre? genre)
        {
            InitializeComponent();
            _genre = genre;

            if (_genre != null)
            {
                TxtName.Text = _genre.Name;
                TxtDescription.Text = _genre.Description;
                this.Title = "Редактировать жанр";
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtName.Text))
            {
                MessageBox.Show("Пожалуйста, введите название жанра", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (_genre == null)
                {
                    _genre = new Genre();
                }

                _genre.Name = TxtName.Text.Trim();
                _genre.Description = TxtDescription.Text?.Trim() ?? string.Empty;

                if (_genre.Id == 0)
                {
                    _dataService.AddGenre(_genre);
                    MessageBox.Show("Жанр успешно добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _dataService.UpdateGenre(_genre);
                    MessageBox.Show("Жанр успешно обновлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

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
