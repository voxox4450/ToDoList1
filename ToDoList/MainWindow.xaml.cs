using Microsoft.Extensions.Logging;
using Serilog;
using System.Windows;
using System.Windows.Controls;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserControl1 userControl1;
        private UserControlEdit userControlEdit;

        public MainWindow()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs.txt", rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:dd-MM-yyyy HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}")
                .CreateLogger();

            Log.Information("Włączono aplikację");
            InitializeComponent();
            userControl1 = new UserControl1(this);
            userControlEdit = new UserControlEdit(this);
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            Log.Information("Aplikacja została zamknięta.");
        }

        public void ButtonExit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void Show()
        {
            MainMenu.Visibility = Visibility.Visible;
        }

        public void Hide()
        {
            MainMenu.Visibility = Visibility.Collapsed;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Frame.Navigate(new UserControl1(this));
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem is Note delNote)
            {
                Log.Information("Użytkownik usunął obiekt z listy: [" + delNote.Show() + "]");

                listView.Items.Remove(delNote);
            }
            else
            {
                Log.Warning("Zaznaczony obiekt nie jest obiektem klasy Note");
            }
        }

        private void EditButton_Click(Object sender, RoutedEventArgs e)
        {
            object EditNote = listView.SelectedItem;
            if (EditNote != null)
            {
                Frame.Navigate(new UserControlEdit(this));
                Hide();
            }
            else
            {
                MessageBox.Show("Musisz wybrać element z listy", "Błąd");
                Log.Error("Nie wybrano elementu z listy");
            }
        }
    }
}