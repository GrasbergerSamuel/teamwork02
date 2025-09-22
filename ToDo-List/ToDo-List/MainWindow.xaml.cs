using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDo_List
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddTodo(object sender, RoutedEventArgs e)
        {
            string task = TodoInput.Text.Trim();

            if (!string.IsNullOrEmpty(task))
            {
                TodoList.Items.Add(task);
                TodoInput.Clear();
            }
            else
            {
                MessageBox.Show("Bitte eine Aufgabe eingeben!");
            }
        }
    }
}