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
                if (selectedNote != null)
                {
                    using (var dbContext = new Database())
                    {
                        var noteToUpdate = dbContext.Notes.FirstOrDefault(n => n.Id == selectedNote.Id);

                        if (noteToUpdate != null)
                        {
                            noteToUpdate.ContentText = contentText;
                            noteToUpdate.StartDate = StartDate;
                            noteToUpdate.EndDate = EndDate;
                            noteToUpdate.PriorityId = priorityInt;
                            noteToUpdate.StatusId = statusInt;

                            dbContext.SaveChanges();

                            selectedNote.ContentText = contentText;
                            selectedNote.StartDate = StartDate;
                            selectedNote.EndDate = EndDate;
                            selectedNote.PriorityId = priorityInt;
                            selectedNote.StatusId = statusInt;
                            selectedNote.GetPriority();
                            selectedNote.GetStatus();

                            LogIfChanged("Treść", selectedNote.ContentText, contentText);
                            LogIfChanged("Data rozpoczęcia", selectedNote.StartDate.ToString("dd.MM.yyyy"), StartDate.ToString("dd.MM.yyyy"));
                            LogIfChanged("Data zakończenia", selectedNote.EndDate.ToString("dd.MM.yyyy"), EndDate.ToString("dd.MM.yyyy"));
                            LogIfChanged("Priorytet", selectedNote.Priority.Name, MainWindow.priorityList[priorityInt].Name);
                            LogIfChanged("Status", selectedNote.Status.Name, MainWindow.statusList[statusInt].Name);
                            MainWindow.Notes = dbContext.GetNotesWithDetails();
                        }
                        else
                        {
                            Log.Error("Nie można znaleźć notatki do edycji w bazie danych.");
                        }
                    }
                }
                else
                {
                    Log.Error("Nie wybrano notatki do edycji.");
                }
                MainWindow.listView.ItemsSource = MainWindow.Notes;

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