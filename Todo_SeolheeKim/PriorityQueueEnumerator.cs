using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo_SeolheeKim
{
    internal class PriorityQueueEnumerator : IEnumerator<TodoItem>
    {
        private PriorityQueue todoItems;
        private TodoItem current;
        private int index;

        public PriorityQueueEnumerator(PriorityQueue list)
        {
            todoItems = list;
            index = -1;
            current = null;
        }

        public TodoItem Current => current;

        object IEnumerator.Current => current;

        public void Dispose()
        {
            //do not implement
        }

        public bool MoveNext()
        {
            //move to the next element in the list/collection
            index++;

            if (index >= todoItems.Count) return false;

            TodoItem c = todoItems.First;
            for (int i = 0; i < index; i++)
            {
                c = c.next;
            }
            current = c;
            return true;

        }

        public void Reset()
        {
            index = -1;
            current = null;
        }
    }
}
