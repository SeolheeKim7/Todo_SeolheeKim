using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo_SeolheeKim
{
    public class TodoItem 
    {
        private string taskName;
        private ImportanceLevel importanceLevel;
        public TodoItem next;

        public string TaskName
        {
            get { return taskName; }
            set { taskName = value; }
        }
        public ImportanceLevel ImportanceLevel
        {
            get { return importanceLevel; }
            set { importanceLevel = value; }
        }

        public TodoItem(string taskName, ImportanceLevel importanceLevel)
        {
            this.TaskName = taskName;
            this.ImportanceLevel = importanceLevel;
            this.next = null;
        }

        public override string ToString()
        {
            return $"{TaskName} - {ImportanceLevel}";
        }
    }

    public enum ImportanceLevel
    {
        Urgent,
        Important,
        Normal,
        Minor,
        Trivial
    }

}
