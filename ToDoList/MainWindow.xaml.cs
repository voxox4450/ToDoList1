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
            InitializeComponent();
            userControl1 = new UserControl1(this);
            userControlEdit = new UserControlEdit(this);
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
            object DelNote = listView.SelectedItem;
            listView.Items.Remove(DelNote);
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
            }
            //listView.Items
        }
    }
}