using Serilog;
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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public MainWindow MainWindow { get; set; }

        public UserControl1(MainWindow mainWindow)
        {
            InitializeComponent();

            MainWindow = mainWindow;
            prio.ItemsSource = MainWindow.priorityList;
            status.ItemsSource = MainWindow.statusList;
        }

        public void Show()
        {
            AddControl.Visibility = Visibility.Visible;
        }

        public void Hide()
        {
            AddControl.Visibility = Visibility.Collapsed;
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            Hide();
            MainWindow.Show();
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
                Log.Error("Błąd wybrano błędną datę");
            }
            else
            {
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
                Log.Information("Dodano zadanie:{@name}", newNote);
            }
        }
    }
}