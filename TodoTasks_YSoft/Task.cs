using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoTasks_YSoft
{
    class Task
    {
        public int Id;
        public string Description;
        public bool Completed;
        public DateTime DateCompleted;

        public Task(int id, string desc)
        {
            this.Id = id;
            this.Description = desc;
        }

        public void CompleteTask()
        {
            this.Completed = true;
            this.DateCompleted = DateTime.Now;
        }
    }
}
