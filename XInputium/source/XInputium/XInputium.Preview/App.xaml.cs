using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace XInputium.Preview;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private void Application_Startup(object sender, StartupEventArgs e)
    {
        if (IsConsole2DeskRunning())
        {
            // Se Console2Desk.exe è in esecuzione, avvia l'applicazione normalmente
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
        else
        {
            // Altrimenti, mostra un messaggio di errore e chiudi l'applicazione
            MessageBox.Show("Failed to start Console2Desk XInput Test.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Shutdown();
        }
    }

    private bool IsConsole2DeskRunning()
    {
        // Cerca il processo Console2Desk.exe
        return Process.GetProcessesByName("Console2Desk").Any();
    }
}
