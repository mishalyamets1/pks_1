using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using LibraryManager.Data;
using LibraryManager.Models;

namespace LibraryManager.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadBooks_Click(object sender, RoutedEventArgs e)
        {
            using var context = new LibraryContext();
            var books = context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .ToList();
            DataGridBooks.ItemsSource = books;
        }
    }
}