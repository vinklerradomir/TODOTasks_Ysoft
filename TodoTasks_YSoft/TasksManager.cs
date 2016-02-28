using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoTasks_YSoft
{
    class TasksManager
    {
        public Dictionary<int, Task> taskList;

        public TasksManager()
        {
            this.taskList = new Dictionary<int, Task>();
        }

        public void AddTask(Task task)
        {
            taskList.Add(task.Id, task);
        }

        /// <summary>
        /// Returns string which contains selected tasks depending on type
        /// </summary>
        /// <param name="type">1 for all tasks, 2 for incomplete tasks, 3 for completed tasks</param>
        /// <returns>String containing formatted text with selected tasks</returns>
        public string DisplayTasks(int type)
        {
            string output = "";

            switch (type)
            {
                case 1:
                    foreach (Task task in taskList.Values)
                    {
                        output += TaskToString(task) + "\n";
                    }
                    break;
                case 2:
                    foreach (Task task in taskList.Values.Where(x => !x.Completed))
                    {
                        output += TaskToString(task) + "\n";
                    }
                    break;
                case 3:
                    foreach (Task task in taskList.Values.Where(x => x.Completed))
                    {
                        output += TaskToString(task) + "\n";
                    }
                    break;
                default:
                    output = "Wrong choice";
                    break;
            }

            return output;
        }

        private string TaskToString(Task task)
        {
            string output = "";

            string id = String.Format("ID: {0}", task.Id).PadRight(10);
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
