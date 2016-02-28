using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoTasks_YSoft
{
    class Task
    {
        public string Description { get; private set; }
        public bool Completed { get; private set; }
        public DateTime DateCompleted { get; private set; }

        public Task(string desc)
        {
            this.Description = desc;
        }

        public Task(string desc, DateTime dateCompleted)
        {
            this.Description = desc;
            this.Completed = true;
            this.DateCompleted = dateCompleted;
        }

        public void CompleteTask()
        {
            this.Completed = true;
            this.DateCompleted = DateTime.Now;
        }
    }
}
