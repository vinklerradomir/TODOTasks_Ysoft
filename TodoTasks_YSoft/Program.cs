using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoTasks_YSoft
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup
            TasksManager manager = new TasksManager();
            Console.BufferHeight = 1000;

            Console.WriteLine("Welcome to Task Manager.");
            Console.WriteLine("Possible commands are list (all, complete, incomplete), create, remove(ID, IDs), complete (ID, IDs), save (path), load(path), help");

            string userInput = "";

            do
            {
                userInput = GetInput();
                string[] splitInput = userInput.Split(' ');

                switch (splitInput[0].ToLower())
                {
                    case "create":
                        manager.Create();
                        break;
                    case "list":
                        if (splitInput.Length == 1)
                            manager.Display("all");
                        else
                            manager.Display(splitInput[1]);
                        break;
                    case "remove":
                        manager.Remove(userInput);
                        break;
                    case "complete":
                        manager.Complete(userInput);
                        break;
                    case "help":
                        DisplayHelp();
                        break;
                    case "quit":
                        userInput = splitInput[0].ToLower();
                        break;
                    case "exit":
                        userInput = splitInput[0].ToLower();
                        break;
                    default:
                        Console.WriteLine("Unknown command. Enter \"help\" for a list of commands");
                        break;
                }
            } while (userInput != "quit" && userInput != "exit");
        }

        static public string GetInput()
        {
            Console.Write("Please enter your choice: ");

            string result = Console.ReadLine();
            return result;
        }

        static void DisplayHelp()
        {
            Console.WriteLine();
            Console.WriteLine("list ... Lists all tasks, you can specify incomplete or completed");
            Console.WriteLine("create ... Creates a new task");
            Console.WriteLine("remove ... Removes a task with ID, or IDs separated by commas");
            Console.WriteLine("complete ... Marks a task as complete with ID or IDs separated by commas");
            Console.WriteLine("save ... Saves the tasks in the specified path it an XML file");
            Console.WriteLine("load ... Loads the tasks from the specified XML file");
            Console.WriteLine("help ... Displays this help message");
            Console.WriteLine();
        }
    }
}
