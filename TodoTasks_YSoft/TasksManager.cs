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

        public void Complete(string input)
        {
            char[] delimiters = new char[] { ' ', ',' };
            string[] splitInput = input.Split(delimiters);

            if (splitInput.Length == 1)
            {
                Console.WriteLine("Wrong argument, specify one or more IDs separated by commas");
                return;
            }

            HashSet<int> removeIndices = new HashSet<int>();

            //create indices from input string
            for (int i = 1; i < splitInput.Length; i++)
            {
                int result;
                if (int.TryParse(splitInput[i], out result))
                    removeIndices.Add(result);
            }

            if (removeIndices.Count == 0)
            {
                Console.WriteLine("Wrong argument, specify one or more IDs separated by commas");
                return;
            }

            if (removeIndices.Max() < taskList.Count && removeIndices.Min() >= 0)
            {
                foreach (int i in removeIndices)
                    taskList[i].CompleteTask();
            }
            else
            {
                Console.WriteLine("ID or IDs not found in database");
            }
        }

        public void Remove(string input)
        {
            char[] delimiters = new char[] { ' ', ',' };
            string[] splitInput = input.Split(delimiters);

            if (splitInput.Length == 1)
            {
                Console.WriteLine("Wrong argument, specify one or more IDs separated by commas");
                return;
            }

            HashSet<int> removeIndices = new HashSet<int>();

            //create indices from input string
            for (int i = 1; i < splitInput.Length; i++)
            {
                int result;
                if (int.TryParse(splitInput[i], out result))
                    removeIndices.Add(result);
            }

            if (removeIndices.Count == 0)
            {
                Console.WriteLine("Wrong argument, specify one or more IDs separated by commas");
                return;
            }

            if (removeIndices.Max() < taskList.Count && removeIndices.Min() >= 0)
            {
                foreach (int i in removeIndices.OrderByDescending(x => x))
                    taskList.RemoveAt(i);
            }
            else
            {
                Console.WriteLine("ID or IDs not found in database");
            }
        }

        /// <summary>
        /// Returns string which contains selected tasks depending on type
        /// </summary>
        /// <param name="type">1 for all tasks, 2 for incomplete tasks, 3 for completed tasks</param>
        /// <returns>String containing formatted text with selected tasks</returns>
        public void Display(string type)
        {
            Console.WriteLine();

            switch (type)
            {
                case "all":
                    if (taskList.Count == 0)
                        Console.WriteLine("No tasks to show.");
                    foreach (Task task in taskList)
                        Console.WriteLine(TaskToString(task));
                    break;
                case "incomplete":
                    List<Task> incompleteList = taskList.Where(x => !x.Completed).ToList();
                    if (incompleteList.Count == 0)
                        Console.WriteLine("No tasks to show.");
                    foreach (Task task in incompleteList)
                        Console.WriteLine(TaskToString(task));
                    break;
                case "completed":
                    List<Task> completedList = taskList.Where(x => x.Completed).ToList();
                    if (completedList.Count == 0)
                        Console.WriteLine("No tasks to show.");
                    foreach (Task task in completedList)
                        Console.WriteLine(TaskToString(task));
                    break;
                default:
                    Console.WriteLine("Wrong argument, use list all, list incomplete or list complete");
                    break;
            }

            Console.WriteLine();
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

            output += outDesc;

            return output;
        }
    }
}
