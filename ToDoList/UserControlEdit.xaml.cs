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
        public string textBox { get; set; }
        public string priorityBox { get; set; }
        public string statusBox { get; set; }
        public Note selectedNote { get; set; }

        public UserControlEdit(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;

            selectedNote = MainWindow.listView.SelectedItem as Note;
            if (selectedNote is null)
            {
                return;
            }

            textBox = content.Text = selectedNote.ContentText;
            priorityBox = prio.Text = selectedNote.Priority;
            statusBox = status.Text = selectedNote.Status;
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
            if (StartDate < DateTime.Now)
            {
                Status = "Dodano";
            }
            if (EndDate < DateTime.Now)
            {
                Status = "Ukończono";
            }
            if (DateTime.Compare(StartDate, EndDate) > 0)
            {
                MessageBox.Show("Rozpoczecie musi być większe niż zakończenie", "Błąd");
            }
            else
            {
                object DelNote = MainWindow.listView.SelectedItem;
                MainWindow.listView.Items.Remove(DelNote);
                Note newNote = new Note(contentText, EndDate, StartDate, Priority, Status);
                MainWindow.listView.Items.Add(newNote);
            }
            Hide();
            MainWindow.Show();
        }
    }
}