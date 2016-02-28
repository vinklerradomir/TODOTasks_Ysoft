using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TodoTasks_YSoft
{
    class TasksManager
    {
        private List<Task> taskList;
        private string savePath;

        public TasksManager()
        {
            this.taskList = new List<Task>();
        }

        /// <summary>
        /// Creates a task after entering command create and then entering text
        /// </summary>
        public void Create()
        {
            Console.WriteLine();

            Console.Write("Enter task description: ");
            string taskDesc = Console.ReadLine();

            Task newTask = new Task(taskDesc);
            taskList.Add(newTask);

            Console.WriteLine("Task created");

            Console.WriteLine();
        }

        /// <summary>
        /// Creates a task straight from input command create "text"
        /// </summary>
        /// <param name="input">raw input string from command line</param>
        public void Create(string input)
        {
            string desc = input.Remove(0, 7);
            Task newTask = new Task(desc);
            taskList.Add(newTask);

            Console.WriteLine("Task created");

            Console.WriteLine();
        }

        /// <summary>
        /// Selects tasks from the input numbers that should be completed.
        /// </summary>
        /// <param name="input">raw input string from console, should be in format complete [number(s)] or complete all</param>
        public void Complete(string input)
        {
            char[] delimiters = new char[] { ' ', ',' };
            string[] splitInput = input.Split(delimiters);

            Console.WriteLine();

            if (splitInput.Length == 1)
            {
                Console.WriteLine("Wrong argument, specify one or more IDs separated by commas");
                Console.WriteLine();
                return;
            }

            if (splitInput[1] == "all")
            {
                foreach (Task task in taskList.Where(x => !x.Completed))
                    task.CompleteTask();
                Console.WriteLine("All tasks marked complete");
                Console.WriteLine();
                return;
            }

            HashSet<int> completeIndices = new HashSet<int>();

            //create indices from input string
            for (int i = 1; i < splitInput.Length; i++)
            {
                int result;
                if (int.TryParse(splitInput[i], out result))
                    completeIndices.Add(result);
            }

            if (completeIndices.Count == 0)
            {
                Console.WriteLine("Wrong argument, specify one or more IDs separated by commas");
                return;
            }

            if (completeIndices.Max() < taskList.Count && completeIndices.Min()  >= 0)
            {
                foreach (int i in completeIndices)
                    taskList[i].CompleteTask();
                Console.WriteLine("{0} task(s) completed", completeIndices.Count);
            }
            else
            {
                Console.WriteLine("ID or IDs not found in database");
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Removes selected tasks from the list of tasks based on their ID.
        /// </summary>
        /// <param name="input">raw input string from console, should be in format remove [number(s)] or remove completed</param>
        public void Remove(string input)
        {
            char[] delimiters = new char[] { ' ', ',' };
            string[] splitInput = input.Split(delimiters);

            Console.WriteLine();

            if (splitInput.Length == 1)
            {
                Console.WriteLine("Wrong argument, specify one or more IDs separated by commas");
                Console.WriteLine();
                return;
            }

            if (splitInput[1] == "completed")
            {
                taskList.RemoveAll(x => x.Completed);
                Console.WriteLine("Removed all completed tasks");
                Console.WriteLine();
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
                Console.WriteLine("{0} task(s) removed", removeIndices.Count);
            }
            else
            {
                Console.WriteLine("ID or IDs not found in database");
            }

            Console.WriteLine();
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

        /// <summary>
        /// Creates a new list of tasks, all former tasks will be lost
        /// </summary>
        public void New()
        {
            Console.Write("Are you sure you want to erase all tasks? (y/n): ");
            if (Console.ReadLine() != "y")
            {
                Console.WriteLine();
                return;
            }

            taskList = new List<Task>();
            Console.WriteLine("Tasks erased");
            Console.WriteLine();
        }

        /// <summary>
        /// Saves the tasks to an XML file in the specified path. If no path is specified than it saves it to the last used path. Throws an error if the path is incorrect.
        /// </summary>
        /// <param name="input">raw input string from console, in format save "path"</param>
        public void Save(string input)
        {
            Console.WriteLine();

            string[] splitString = input.Split(new char[]{' '}, 2);
            string path;

            if (splitString.Length == 1)
            {
                if (String.IsNullOrEmpty(savePath))
                {
                    Console.WriteLine("You need to specify a valid file path in the argument.");
                    Console.WriteLine();
                    return;
                }
                else
                    path = savePath;
            }
            else
            {
                path = @splitString[1];
                if (File.Exists(path))
                {
                    Console.Write("Are you sure you want to overwrite file {0}? (y/n): ", Path.GetFileName(path));
                    if (Console.ReadLine() != "y")
                    {
                        Console.WriteLine();
                        return;
                    }
                }
            }

            Console.WriteLine("Saving tasks to {0}", path);

            try
            {
                using (XmlWriter writer = XmlWriter.Create(path))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Tasks");

                    foreach (Task task in taskList)
                    {
                        writer.WriteStartElement("Task");

                        writer.WriteAttributeString("Description", task.Description);
                        writer.WriteAttributeString("Completed", task.Completed.ToString());
                        if (task.Completed)
                            writer.WriteAttributeString("DateCompleted", task.DateCompleted.ToString());
                        else
                            writer.WriteAttributeString("DateCompleted", null);

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }

                Console.WriteLine("Tasks succesfully saved to file \"{0}\"", path);
                savePath = path;
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Path to directory is not correct, please enter a valid path");
            }
            catch (Exception e) 
            {
                Console.WriteLine("An error occurred while saving:");
                Console.WriteLine(e.Message);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Loads the tasks from a specified XML file. If no path is specified then it loads it from the last used path. Throws an error if the path is wrong.
        /// </summary>
        /// <param name="input">raw input string from console, in format load "path"</param>
        public void Load(string input)
        {
            Console.WriteLine();

            string[] splitString = input.Split(new char[] { ' ' }, 2);
            string path;

            if (splitString.Length == 1)
            {
                if (String.IsNullOrEmpty(savePath))
                {
                    Console.WriteLine("You need to specify a valid file path in the argument.");
                    Console.WriteLine();
                    return;
                }
                else
                    path = savePath;
            }
            else
                path = @splitString[1];

            if (File.Exists(path))
            {
                if (taskList.Count != 0)
                {
                    Console.Write("Are you sure you want to load file {0}? Your current tasks will be lost. (y/n): ", Path.GetFileName(path));
                    if (Console.ReadLine() != "y")
                    {
                        Console.WriteLine();
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("Path is not correct. Please enter a valid path.");
                Console.WriteLine();
                return;
            }

            Console.WriteLine("Loading tasks from {0}", path);

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                List<Task> loadedList = new List<Task>();

                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    string desc = node.Attributes["Description"].Value;
                    bool completed = bool.Parse(node.Attributes["Completed"].Value);

                    if (completed)
                    {
                        DateTime dateCompleted = DateTime.Parse(node.Attributes["DateCompleted"].Value);
                        Task newTask = new Task(desc, dateCompleted);
                        loadedList.Add(newTask);
                    }
                    else
                    {
                        Task newTask = new Task(desc);
                        loadedList.Add(newTask);
                    }
                }

                Console.WriteLine("Succesfully loaded {0} task(s)", doc.DocumentElement.ChildNodes.Count);
                taskList = loadedList;
                savePath = path;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while loading:");
                Console.WriteLine(e.Message);
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
