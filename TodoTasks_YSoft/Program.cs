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
            TasksManager manager = new TasksManager();

            Task task1 = new Task(1, "asd");
            task1.CompleteTask();
            manager.AddTask(task1);
            Task task2 = new Task(2, "asd");
            task2.CompleteTask();
            manager.AddTask(task2);
            Task task3 = new Task(3, "asd");
            //task3.CompleteTask();
            manager.AddTask(task3);
            Task task4 = new Task(4, "asd");
            task4.CompleteTask();
            manager.AddTask(task4);
            

            Console.WriteLine(manager.DisplayTasks(1));

            Console.ReadKey();
        }

        static void displayMenu()
        {
        }
    }
}
