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

        public UserControlEdit(MainWindow mainWindow)
        {
            InitializeComponent();

            MainWindow = mainWindow;
            prio.ItemsSource = MainWindow.priorityList;
            status.ItemsSource = MainWindow.statusList;

            selectedNote = MainWindow.listView.SelectedItem as Note;
            if (selectedNote is null)
            {
                return;
            }

            content.Text = selectedNote.ContentText;
            start.SelectedDate = selectedNote.StartDate;
            end.SelectedDate = selectedNote.EndDate;
            prio.Text = selectedNote.Priority.Name;
            status.Text = selectedNote.Status.Name;
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
            int priorityInt = MainWindow.priorityList.FindIndex(x => x.Name == prio.Text);
            int statusInt = MainWindow.statusList.FindIndex(x => x.Name == status.Text);
            if (EndDate < DateTime.Now && StartDate < EndDate)
            {
                statusInt = 0;
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
                    string priorityT = delNote.Priority.Name;
                    string statusT = delNote.Status.Name;

                    MainWindow.listView.Items.Remove(delNote);

                    Note newNote = new Note()
                    {
                        ContentText = contentText,
                        EndDate = EndDate,
                        StartDate = StartDate,
                        PriorityId = priorityInt,
                        StatusId = statusInt
                    };

                    MainWindow.listView.Items.Add(newNote);

                    newNote.GetPriority();
                    newNote.GetStatus();
                    LogIfChanged("Treść", textT, contentText);
                    LogIfChanged("Data rozpoczęcia", startT.ToString("dd.MM.yyyy"), StartDate.ToString("dd.MM.yyyy"));
                    LogIfChanged("Data zakończenia", endT.ToString("dd.MM.yyyy"), EndDate.ToString("dd.MM.yyyy"));
                    LogIfChanged("Priorytet", priorityT, newNote.Priority.Name);
                    LogIfChanged("Status", statusT, newNote.Status.Name);
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