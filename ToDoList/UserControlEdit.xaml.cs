using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for UserControlEdit.xaml
    /// </summary>
    public partial class UserControlEdit : UserControl
    {
        public MainWindow MainWindow { get; set; }

        public Note selectedNote { get; set; }
        private List<Priority> priorityList = new List<Priority>();
        private List<Status> statusList = new List<Status>();

        public UserControlEdit(MainWindow mainWindow)
        {
            InitializeComponent();
            priorityList.Add(new Priority { id = 1, name = "Wysoki" });
            priorityList.Add(new Priority { id = 2, name = "Średni" });
            priorityList.Add(new Priority { id = 3, name = "Niski" });

            statusList.Add(new Status { id = 1, name = "Ukońoczono" });
            statusList.Add(new Status { id = 2, name = "Rozpoczęto" });
            statusList.Add(new Status { id = 3, name = "Dodano" });
            MainWindow = mainWindow;
            prio.ItemsSource = priorityList;
            status.ItemsSource = statusList;

            selectedNote = MainWindow.listView.SelectedItem as Note;
            if (selectedNote is null)
            {
                return;
            }

            content.Text = selectedNote.ContentText;
            start.SelectedDate = selectedNote.StartDate;
            end.SelectedDate = selectedNote.EndDate;
            prio.Text = selectedNote.Priority;
            status.Text = selectedNote.Status;
        }

        public void Show()
        {
            EditControl.Visibility = Visibility.Visible;
        }

        public void Hide()
        {
            EditControl.Visibility = Visibility.Collapsed;
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            string contentText = content.Text;
            DateTime EndDate = Convert.ToDateTime(end.SelectedDate);
            DateTime StartDate = Convert.ToDateTime(start.SelectedDate);
            string Priority = prio.Text;
            string Status = status.Text;
            if (StartDate < DateTime.Now && EndDate > DateTime.Now)
            {
                Status = "Rozpoczęto";
            }
            else if (StartDate > DateTime.Now)
            {
                Status = "Dodano";
            }
            else if (EndDate < DateTime.Now && StartDate < EndDate)
            {
                Status = "Ukońoczono";
            }
            if (DateTime.Compare(StartDate, EndDate) >= 0)
            {
                MessageBox.Show("Rozpoczecie musi być większe niż zakończenie", "Błąd");
            }
            else
            {
                if (MainWindow.listView.SelectedItem is Note delNote)
                {
                    string textT = delNote.ContentText;
                    DateTime startT = delNote.StartDate;
                    DateTime endT = delNote.EndDate;
                    string priorityT = delNote.Priority;
                    string statusT = delNote.Status;

                    MainWindow.listView.Items.Remove(delNote);

                    Note newNote = new Note(contentText, EndDate, StartDate, Priority, Status);

                    MainWindow.listView.Items.Add(newNote);

                    LogIfChanged("Treść", textT, contentText);
                    LogIfChanged("Data rozpoczęcia", startT.ToString("dd.MM.yyyy"), StartDate.ToString("dd.MM.yyyy"));
                    LogIfChanged("Data zakończenia", endT.ToString("dd.MM.yyyy"), EndDate.ToString("dd.MM.yyyy"));
                    LogIfChanged("Priorytet", priorityT, Priority);
                    LogIfChanged("Status", statusT, Status);
                }
                Hide();
                MainWindow.Show();
            }
            void LogIfChanged(string propertyName, string oldValue, string newValue)
            {
                if (oldValue != newValue)
                {
                    Log.Information($"Użytkownik edytował obiekt z listy: {propertyName} - {oldValue} --> {newValue}");
                }
            }
        }
    }
}