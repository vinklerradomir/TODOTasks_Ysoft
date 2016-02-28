using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoTasks_YSoft
{
    class Task
    {
        private int id;
        private string description;
        private bool completed;
        private DateTime dateCompleted;

        public void CompleteTask()
        {
            this.completed = true;
            this.dateCompleted = DateTime.Now;
        }
    }
}
