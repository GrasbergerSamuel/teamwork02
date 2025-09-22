using System.Collections.ObjectModel;
using System.Globalization;
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
using System.Linq;
using System.Collections.Generic;

namespace ToDoApp
{
    public partial class MainWindow : Window
    {
        private List<Todo> todos = new List<Todo>();

        public MainWindow()
        {
            InitializeComponent();
            RefreshList();
        }

        private void AddTodo(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TodoInput.Text))
            {
                todos.Add(new Todo { Title = TodoInput.Text, IsDone = false });
                TodoInput.Clear();
                RefreshList();
            }
        }

        private void ToggleDone(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox cb && cb.Tag is Todo todo)
            {
                todo.IsDone = cb.IsChecked == true;
                RefreshList();
            }
        }

        private void ShowAll(object sender, RoutedEventArgs e) => RefreshList();
        private void ShowOpen(object sender, RoutedEventArgs e) => RefreshList(showDone: false);
        private void ShowDone(object sender, RoutedEventArgs e) => RefreshList(showOpen: false);

        private void RefreshList(bool showOpen = true, bool showDone = true)
        {
            TodoList.Items.Clear();
            foreach (var todo in todos.Where(t => (t.IsDone && showDone) || (!t.IsDone && showOpen)))
            {
                var cb = new CheckBox
                {
                    Content = todo.Title,
                    IsChecked = todo.IsDone,
                    Tag = todo
                };
                cb.Checked += ToggleDone;
                cb.Unchecked += ToggleDone;
                TodoList.Items.Add(cb);
            }
        }
    }

    public class Todo
    {
        public string Title { get; set; }
        public bool IsDone { get; set; }
    }
}