using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System;

namespace ToDo_List
{

    public partial class MainWindow : Window
    {
        private List<TaskItem> tasks = new List<TaskItem>();
        private string currentFilter = "Alle";
        private readonly string dataFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "ToDo_List_tasks.txt");

        public MainWindow()
        {
            InitializeComponent();
            FilterComboBox.SelectionChanged += FilterComboBox_SelectionChanged;
            this.Loaded += (s, e) =>
            {
                LoadTasks();
                RefreshList();
            };
        }

        private void AddTodo(object sender, RoutedEventArgs e)
        {
            string task = TodoInput.Text.Trim();
            if (!string.IsNullOrEmpty(task))
            {
                tasks.Add(new TaskItem { Text = task, IsCompleted = false });
                TodoInput.Clear();
                SaveTasks();
                RefreshList();
            }
        }

        private void RefreshList()
        {
            if (TodoList == null) return;

            IEnumerable<TaskItem> filtered = tasks;
            if (currentFilter == "Offen")
                filtered = tasks.Where(t => !t.IsCompleted);
            else if (currentFilter == "Erledigt")
                filtered = tasks.Where(t => t.IsCompleted);

            TodoList.ItemsSource = null;
            TodoList.ItemsSource = filtered;
        }

        private void DeleteTodo(object sender, RoutedEventArgs e)
        {
            if (TodoList.SelectedItem is TaskItem selected)
            {
                tasks.Remove(selected);
                SaveTasks();
                RefreshList();
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie eine Aufgabe zum Löschen aus.");
            }
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilterComboBox.SelectedItem is ComboBoxItem item)
            {
                currentFilter = item.Content.ToString();
                RefreshList();
            }
        }

        private void TodoList_CheckChanged(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox cb && cb.DataContext is TaskItem task)
            {
                task.IsCompleted = cb.IsChecked == true;
                SaveTasks();
                RefreshList();
            }
        }

        private void SaveTasks()
        {
            try
            {
                var lines = tasks.Select(t => $"{t.Text}|{t.IsCompleted}");
                File.WriteAllLines(dataFile, lines);
            }
            catch
            {
                MessageBox.Show("Fehler beim Speichern der Aufgaben.");
            }
        }

        private void LoadTasks()
        {
            tasks.Clear();
            if (File.Exists(dataFile))
            {
                foreach (var line in File.ReadAllLines(dataFile))
                {
                    var parts = line.Split('|');
                    if (parts.Length == 2)
                    {
                        tasks.Add(new TaskItem
                        {
                            Text = parts[0],
                            IsCompleted = bool.TryParse(parts[1], out var completed) && completed
                        });
                    }
                }
            }
        }
    }

    public class TaskItem
    {
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
    }
}
