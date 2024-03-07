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
        public MainWindow()
        {
            InitializeComponent();
            userControl1 = new UserControl1(this);
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
    }
}