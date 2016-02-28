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

            Task task1 = new Task("Tristique suspendisse sollicitudin imperdiet tincidunt tempus vitae enim. Eget ante nullam hendrerit. Mattis molestie pulvinar lectus aliquet luctus hendrerit justo auctor dui volutpat scelerisque platea Rutrum porta. Litora porttitor, iaculis metus Sociosqu et malesuada conubia ut sollicitudin. Velit euismod sit tortor lectus consectetuer Sociosqu lacinia Primis. Lacinia fringilla tortor nascetur cras sociis tempus sapien senectus eros semper pretium. Et congue primis dui vitae ultrices velit nisl metus. Nec lectus dapibus quam mattis potenti malesuada cras conubia sit eros neque mollis, sociosqu ultrices hendrerit nibh convallis ornare magna porttitor erat ornare. Augue orci eu magnis odio. Fermentum vulputate nibh, in nunc.");
            task1.CompleteTask();
            manager.AddTask(task1);
            Task task2 = new Task("asd");
            task2.CompleteTask();
            manager.AddTask(task2);
            Task task3 = new Task("Sollicitudin ipsum velit curae; dapibus arcu ultricies natoque parturient magnis. Odio in maecenas, habitant inceptos nascetur ante rhoncus gravida. Penatibus mollis congue rutrum taciti potenti pulvinar sit. Habitant nunc nostra tempor diam lectus morbi enim libero fusce natoque fusce nam diam ullamcorper porta. Fringilla nibh. Sem mauris a nascetur mattis phasellus tempor in quisque dui vehicula aenean elementum id. Facilisis curae;, arcu lobortis nulla nulla magna lorem quis netus, cum per dictumst neque, lobortis aliquam felis primis malesuada mauris. Commodo. Luctus pharetra hymenaeos dui erat Natoque senectus. Imperdiet mattis augue ligula sit fusce dui aptent lobortis mus gravida. Ad aptent.");
            //task3.CompleteTask();
            manager.AddTask(task3);
            Task task4 = new Task("asd");
            task4.CompleteTask();
            manager.AddTask(task4);
            

            Console.WriteLine(manager.DisplayTasks("completed"));

            manager.CreateTask();

            Console.ReadKey();
        }

        static void displayMenu()
        {
        }
    }
}
