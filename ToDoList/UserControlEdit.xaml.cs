using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        public UserControlEdit(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;

            var selectedNote = MainWindow.listView.SelectedItem as Note;
            if (selectedNote is null)
            {
                return;
            }

            textBox = content.Text = selectedNote.ContentText;
            //start.DataContext = Convert.ToDateTime(selectedNote.EndDate);
            //end.DataContext = Convert.ToDateTime(selectedNote.StartDate);
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
            Read(textBox, priorityBox, statusBox);
            Hide();
            MainWindow.Show();
        }

        private void Read(string text, string prio, string status)
        {
            var selectedNote = MainWindow.listView.SelectedItem as Note;
            if (selectedNote is null)
            {
                return;
            }

            selectedNote.ContentText = text;
            //selectedNote.EndDate = end;
            //selectedNote.StartDate = start;
            selectedNote.Priority = prio;
            selectedNote.Status = status;
        }
    }
}