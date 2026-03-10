using System.Configuration;
using System.Data;
using System.Windows;
using LibraryManager.Data;

namespace LibraryManager;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        // Инициализируем БД при запуске приложения
        try
        {
            LibraryContext.InitializeDatabase();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка инициализации БД: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}