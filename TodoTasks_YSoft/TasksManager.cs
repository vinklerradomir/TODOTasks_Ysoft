using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoTasks_YSoft
{
    class TasksManager
    {
        public List<Task> taskList;

        public TasksManager()
        {
            this.taskList = new List<Task>();
        }

        public void AddTask(Task task)
        {
            taskList.Add(task);
        }

        public void Create()
        {
            Console.Write("Enter task description: ");
            string taskDesc = Console.ReadLine();

            Task newTask = new Task(taskDesc);
            taskList.Add(newTask);
        }

        public bool Complete(int id)
        {
            if (id >= 0 && id < taskList.Count)
            {
                taskList[id].CompleteTask();
                return true;
            }
            else
                return false;
        }

        public bool Complete(int[] ids)
        {
            if (ids.Max() < taskList.Count && ids.Min() >= 0)
            {
                foreach (int i in ids)
                    taskList[i].CompleteTask();
                return true;
            }
            else
                return false;
        }

        public bool Delete(int id)
        {
            if (id >= 0 && id < taskList.Count)
            {
                taskList.RemoveAt(id);
                return true;
            }
            else
                return false;
        }

        public bool Delete(int[] ids)
        {
            if (ids.Max() < taskList.Count && ids.Min() >= 0)
            {
                foreach (int i in ids.OrderByDescending(x => x))
                    taskList.RemoveAt(i);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Returns string which contains selected tasks depending on type
        /// </summary>
        /// <param name="type">1 for all tasks, 2 for incomplete tasks, 3 for completed tasks</param>
        /// <returns>String containing formatted text with selected tasks</returns>
        public void Display(string type)
        {
            switch (type)
            {
                case "all":
                    foreach (Task task in taskList)
                    {
                        Console.WriteLine(TaskToString(task));
                    }
                    break;
                case "incomplete":
                    foreach (Task task in taskList.Where(x => !x.Completed))
                    {
                        Console.WriteLine(TaskToString(task));
                    }
                    break;
                case "completed":
                    foreach (Task task in taskList.Where(x => x.Completed))
                    {
                        Console.WriteLine(TaskToString(task));
                    }
                    break;
                default:
                    Console.WriteLine("Wrong choice");
                    break;
            }
        }

        private string TaskToString(Task task)
        {
            string output = "";

            string id = String.Format("ID: {0}", taskList.IndexOf(task)).PadRight(10);
            string completed = task.Completed ? String.Format("Completed on: {0}", task.DateCompleted) : "Incpomlete";

            output += id + completed + "\n";

            int lastWhiteIndex = 0;
            int lastLineIndex = 0;
            string descString = task.Description;
            string outDesc = "";
            for (int i = 0; i < descString.Length; i++)
            {
                if (i >= Console.WindowWidth + lastLineIndex)
                {
                    if (lastLineIndex == lastWhiteIndex + 1)
                    {
                        outDesc += descString.Substring(lastLineIndex, Console.WindowWidth);
                        lastLineIndex = lastLineIndex + Console.WindowWidth;
                    }
                    else
                    {
                        outDesc += descString.Substring(lastLineIndex, lastWhiteIndex - lastLineIndex) + "\n";
                        i = lastWhiteIndex + 1;
                        lastLineIndex = lastWhiteIndex + 1;
                    }
                }

                if (descString[i] == ' ')
                    lastWhiteIndex = i;
            }

            //Copy remaining chars
            outDesc += descString.Substring(lastLineIndex, descString.Length - lastLineIndex);

            output += outDesc + "\n";

            return output;
        }
    }
}
