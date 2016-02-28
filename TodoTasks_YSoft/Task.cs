using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoTasks_YSoft
{
    class Task
    {
        public string Description;
        public bool Completed;
        public DateTime DateCompleted;

        public Task(string desc)
        {
            this.Description = desc;
        }

        public void CompleteTask()
        {
            this.Completed = true;
            this.DateCompleted = DateTime.Now;
        }
    }
}
