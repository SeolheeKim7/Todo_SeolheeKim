using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Todo_SeolheeKim
{
    public class PriorityQueue : IEnumerable<TodoItem>
    {
        private TodoItem first;
        private int count;

        public TodoItem First //read-only
        {
            get { return first; }
        }
        public int Count //read-only
        {
            get { return count; }

        }

        public TodoItem this[int i]
        {
            get
            {
                if (i < 0) throw new IndexOutOfRangeException("Negative index not allowed.");
                if (first == null) throw new Exception("List is empty.");
                if (i >= Count) throw new IndexOutOfRangeException("Index is out of list bounds.");

                TodoItem current = first;
                int counter = 0;
                while (current != null)
                {
                    if (counter == i) break;    //stop the loop when we reach this counter
                    current = current.next;
                    counter++;
                }
                return current;
            }
        }
        public List<TodoItem> GetItemsByImportance(ImportanceLevel selectedImportance)
        {
            List<TodoItem> filteredItems = new List<TodoItem>();

            TodoItem current = first;

            while (current != null)
            {
                if (current.ImportanceLevel == selectedImportance)
                {
                    filteredItems.Add(current);
                }

                current = current.next;
            }

            return filteredItems;
        }
        public void Enqueue(TodoItem item)
        {
            TodoItem current = first;
            TodoItem previous = null;

            //case1. empty queue or when the new item has higher(less importance) or equal priority => add at the end
            if (current == null || item.ImportanceLevel.CompareTo(current.ImportanceLevel) < 0) // urgent =0, important =1, Normal =3, Minor =4, Trivia =5
            {
                item.next = current;
                first = item;
            }
            //case2. when the current is the at the beginning and new item is the same importancelevel => add it behind the current
            else if ((current.next == null && item.ImportanceLevel.CompareTo(current.ImportanceLevel) == 0))
            {
                current.next = item;
            }
            else //case3. when the current is not at the beginning of the list and the new item is less priority (more important) =>move to the front and add it
            {
                while (current != null && item.ImportanceLevel.CompareTo(current.ImportanceLevel) >= 0)
                {
                    previous = current;
                    current = current.next;
                }

                
                // Insert at the beginning
                if (current == null)
                {
                    previous.next = item;
                    //item.next = first;
                    //first = item;
                }
                else
                {
                    // Insert in the middle
                    item.next = current;
                    previous.next = item;
                }
            }

            count++;
            
        }
        public void Clear()
        {
            first = null;
            count = 0;
        }

        public TodoItem Dequeue()
        {
            
            if (first == null)
            {
                // The queue is empty
                return null;
            }

            TodoItem dequeuedItem = first;
            first = first.next;
            count--;

            return dequeuedItem;
        }

        public TodoItem Peek()
        {
            return first;
        }
        public IEnumerator<TodoItem> GetEnumerator()
        {

            return new PriorityQueueEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new PriorityQueueEnumerator(this);
        }
    }
}
