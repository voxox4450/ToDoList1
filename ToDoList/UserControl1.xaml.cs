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
            DateTime EndDate = DateTime.Parse(end.Text);
            DateTime StartDate = DateTime.Parse(start.Text);
            string Priority = prio.Text;
            string Status = status.Text;

        }
        public class Note()
        {

        }
    }
}
