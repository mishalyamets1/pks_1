using System;
using System.Windows;
using LibraryManager.Models;
using LibraryManager.Services;

namespace LibraryManager.Views
{
    public partial class AuthorEditWindow : Window
    {
        private DataService _dataService = new DataService();
        private Author? _author;

        public AuthorEditWindow(Author? author)
        {
            InitializeComponent();
            _author = author;

            if (_author != null)
            {
                TxtFirstName.Text = _author.FirstName;
                TxtLastName.Text = _author.LastName;
                DtpBirthDate.SelectedDate = _author.BirthDate;
                TxtCountry.Text = _author.Country;
                this.Title = "Редактировать автора";
            }
            else
            {
                DtpBirthDate.SelectedDate = DateTime.Now;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtFirstName.Text) || 
                string.IsNullOrWhiteSpace(TxtLastName.Text) ||
                string.IsNullOrWhiteSpace(TxtCountry.Text) ||
                !DtpBirthDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Пожалуйста, заполните все поля", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (_author == null)
                {
                    _author = new Author();
                }

                _author.FirstName = TxtFirstName.Text.Trim();
                _author.LastName = TxtLastName.Text.Trim();
                _author.BirthDate = DtpBirthDate.SelectedDate.Value;
                _author.Country = TxtCountry.Text.Trim();

                if (_author.Id == 0)
                {
                    _dataService.AddAuthor(_author);
                    MessageBox.Show("Автор успешно добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _dataService.UpdateAuthor(_author);
                    MessageBox.Show("Автор успешно обновлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
