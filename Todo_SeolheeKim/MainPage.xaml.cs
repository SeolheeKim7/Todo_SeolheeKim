using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Todo_SeolheeKim
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private PriorityQueue todo;
        public MainPage()
        {
            this.InitializeComponent();

            todo = new PriorityQueue();

            DrawCombobox();
        }
        private void DrawCombobox()
        {
            //Generate Importance value to 2 combobox
            foreach (ImportanceLevel level in Enum.GetValues(typeof(ImportanceLevel)))
            {
                cboImportance.Items.Add(level.ToString());
                cboImportanceFilter.Items.Add(level.ToString());
            }
        }
        private void DisplayLists(IEnumerable<TodoItem> items)
        {
            txtOutput.Text = "";
            txtCount.Text = "";
            // Check if there is any item in the queue
            foreach (TodoItem item in items)
            {
                txtOutput.Text += $"{item.TaskName} - {item.ImportanceLevel}\n";
            }

            txtCount.Text = items.Count().ToString();
        }
        private void DisplayPeek()
        {
            
            if (todo.Count > 0)
            {
                txtFirstToDo.Text = todo.Peek().ToString();
            }
            else
            {
                txtFirstToDo.Text = "Queue is empty.";
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string taskName = txtInput.Text;

            if (cboImportance.SelectedItem != null && !string.IsNullOrWhiteSpace(taskName))
            {
                if (Enum.TryParse(cboImportance.SelectedItem.ToString(), out ImportanceLevel selectedLevel))
                {
                    TodoItem newItem = new TodoItem(taskName, selectedLevel);
                   
                    todo.Enqueue(newItem);
                   
                    txtInput.Text = "";
                    cboImportance.SelectedIndex = -1;
                    
                    DisplayPeek();
                    DisplayLists(todo);
                }
            }
            else
            {
                ShowDialog("Error", "Please enter a task and choose the level of importance.");
            }
        }
        private async void ShowDialog(string title, string content)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "OK"
            };

            await dialog.ShowAsync();
        }

        private void FilterItemsByImportance(ImportanceLevel selectedImportance)
        {
            List<TodoItem> filteredItems = todo.GetItemsByImportance(selectedImportance);
            DisplayLists(filteredItems);
        }

        private void btnShowLists_Click(object sender, RoutedEventArgs e)
        {
            if (cboImportanceFilter.SelectedItem != null)
            {
                // Parse the selected item from the ComboBox
                if (Enum.TryParse(cboImportanceFilter.SelectedItem.ToString(), out ImportanceLevel selectedImportance))
                {
                    FilterItemsByImportance(selectedImportance);
                    cboImportanceFilter.SelectedIndex = -1;
                }
                else
                {
                    ShowDialog("Error", "Please choose the level of importance again.");
                }
            }
            else
            {
                // If nothing is selected, display all items
                DisplayLists(todo);
            }
        }

        private void chkDone_Checked(object sender, RoutedEventArgs e)
        {
            txtFirstToDo.Text = "";
            
            if (todo.Count > 0)
            {
                
                todo.Dequeue();
                DisplayLists(todo);

                //If there is the next item in the list, display the next
                if (todo.Count > 0)
                {
                    txtFirstToDo.Text = todo.Peek().ToString();
                    
                }
            }
            chkDone.IsChecked = false;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            todo.Clear();

            txtOutput.Text = "";
            txtFirstToDo.Text = "";
            txtCount.Text = "";
        }

        private void btnRemoveFilter_Click(object sender, RoutedEventArgs e)
        {
            DisplayLists(todo);
        }
    }
}
