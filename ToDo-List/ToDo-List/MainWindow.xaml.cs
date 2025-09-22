using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.TextFormatting;

namespace ToDo_List
{
    public partial class MainWindow : Window
    {
        private List<string> tasks = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddTodo(object sender, RoutedEventArgs e)
        {
            string task = TodoInput.Text.Trim();
            if (!string.IsNullOrEmpty(task))
            {
                tasks.Add(task);
                TodoInput.Clear();
                RefreshList();
            }
        }

        private void RefreshList()
        {
            TodoList.ItemsSource = null;
            TodoList.ItemsSource = tasks;
        }

        private void TaskChecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                if (checkBox.Content is TextBlock tb)
                {
                    tb.TextDecorations = System.Windows.TextDecorations.Strikethrough;
                }
            }
        }

        private void TaskUnchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                if (checkBox.Content is TextBlock tb)
                {
                    tb.TextDecorations = null;
                }
            }
        }
        private void DeleteTodo(object sender, RoutedEventArgs e)
        {
            // Sicherstellen, dass eine Aufgabe ausgewählt ist
            if (TodoList.SelectedItem != null)
            {
                // Entferne die ausgewählte Aufgabe
                TodoList.Items.Remove(TodoList.SelectedItem);
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie eine Aufgabe zum Löschen aus.");
            }
        }
    }
}
