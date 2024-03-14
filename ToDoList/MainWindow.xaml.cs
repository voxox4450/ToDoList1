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

        //public List<Priority> priorityList { get; set; } = Constants.GetPriorities().ToList();
        //public List<Status> statusList { get; set; } = Constants.GetStatus().ToList();
        public List<Priority> priorityList { get; set; } = Constants.GetPriorities();

        public List<Note> Notes { get; set; } // Lista notatek

        public List<Status> statusList { get; set; } = Constants.GetStatuses();

        public MainWindow()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithProperty("DeviceName", Environment.MachineName)
                .WriteTo.File("logs.txt", rollingInterval: RollingInterval.Day, outputTemplate: "{Properties} {Timestamp:dd-MM-yyyy HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}")
                .WriteTo.Debug(outputTemplate: "{Properties} {Timestamp:dd-MM-yyyy HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}")
                .CreateLogger();

            Log.Information("Włączono aplikację");

            InitializeComponent();

            userControl1 = new UserControl1(this);
            userControlEdit = new UserControlEdit(this);
            using (var dbContext = new Database())
            {
                Notes = dbContext.GetNotesWithDetails();
            }
            listView.ItemsSource = Notes;
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
                using (var dbContext = new Database())
                {
                    var noteToRemove = dbContext.Notes.FirstOrDefault(n => n.Id == delNote.Id);
                    if (noteToRemove != null)
                    {
                        dbContext.Notes.Remove(noteToRemove);
                        dbContext.SaveChanges();
                        Log.Information("Usunięto zadanie: {@name}", delNote);
                        Notes = dbContext.GetNotesWithDetails();
                    }
                    else
                    {
                        Log.Warning("Nie można znaleźć notatki do usunięcia w bazie danych.");
                    }
                }
                //listView.Items.Remove(delNote);
            }
            else
            {
                Log.Warning("Zaznaczony obiekt nie jest obiektem klasy Note");
            }
            listView.ItemsSource = Notes;
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