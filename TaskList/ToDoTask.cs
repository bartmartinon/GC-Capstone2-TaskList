using System;
using System.Collections.Generic;
using System.Text;

namespace TaskList
{
    class ToDoTask
    {
        // Fields
        private string memberName;
        private string description;
        private DateTime deadline;
        private bool isComplete;

        // Properties
        public string MemberName
        {
            get { return memberName; }
            set { memberName = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public DateTime Deadline
        {
            get { return deadline; }
            set { deadline = value; }
        }

        public bool IsComplete
        {
            get { return isComplete; }
            set { isComplete = value; }
        }

        // Construtors
        // Standard (All Arguments)
        public ToDoTask(string mn, string desc, DateTime dedln, bool i)
        {
            memberName = mn;
            description = desc;
            deadline = dedln;
            isComplete = i;
        }

        // Task Starts as Incomplete (3/4 Arguments)
        public ToDoTask(string mn, string desc, DateTime dedln)
        {
            memberName = mn;
            description = desc;
            deadline = dedln;
            isComplete = false;
        }

        // Methods

        // Prints properties into a string
        public string PrintTask()
        {
            string taskInfo =
                "\n Team Member: " + MemberName +
                "\n Task Desc.:  " + Description +
                "\n Deadline:    " + Deadline.ToShortDateString() +
                "\n Complete?:   " + IsComplete;
            return taskInfo;
        } 

        // Toggles IsComplete true to false (or vice versa)
        public void ToggleComplete()
        {
            IsComplete = !IsComplete;
        }
    }
}
