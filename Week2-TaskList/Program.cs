using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static System.Console;

//  Program: Capstone 2 - Create and manage a List of Tasks
//  Author: Jeffrey Wohlfield
//  Date:   01/31/2021

namespace Week2_TaskList
{
    class Program
    {
        public static List<TheTask> tasks = new List<TheTask>();
        static void Main(string[] args)
        {
            // Load some tasks just to have some in the list
            tasks.Add(new TheTask("Dwight", "Fix the water cooler", "12/31/2021"));
            tasks.Add(new TheTask("Pam", "Hide in the bathroom", "04/15/2021"));
            tasks.Add(new TheTask("Michael", "Destroy vendor relationships", "09/15/2021"));
            tasks.Add(new TheTask("Jim", "Prank call Dwight", "06/27/2021"));
            tasks.Add(new TheTask("Kelly", "Hang out in shipping", "02/01/2021"));
            tasks.Add(new TheTask("Stanley", "Get annoyed by co-workers", "07/04/2021"));

            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }

            WriteLine("Goodbye!");
        }

        public static bool MainMenu()
        {
            Clear();
            ForegroundColor = ConsoleColor.Green;
            WriteLine("Welcome to the Task Manager!");
            ResetColor();
            WriteLine("1) List Tasks");
            WriteLine("2) Add Task");
            WriteLine("3) Delete Task");
            WriteLine("4) Mark Task Complete");
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("5) Quit");
            ResetColor();
            Write("\r\nSelect an option: ");

            switch (ReadLine().Trim())
            {
                case "1":
                    ListTasks("List of all Tasks");
                    WriteLine("\nPress any key to continue...");
                    ReadLine();
                    break;
                case "2":
                    AddTask("Add a task");
                    break;
                case "3":
                    DeleteTask("Delete a Task");
                    break;
                case "4":
                    CompleteTask("Mark a Task Complete");
                    break;
                case "5":
                    return false;
                default:
                    WriteLine("Invalid selection!\nPress any key to continue...");
                    ReadLine();
                    break;
                }
            return true;
        }

        public static void ListTasks(string header)
        {
            Clear();
            ForegroundColor = ConsoleColor.Green;
            WriteLine(header);
            WriteLine(String.Format($"   {"Member",-10} {"Due Date",15} {"Complete",8}\t {"Description",-10}"));
            ResetColor();
            int c = 1;
            foreach (var t in tasks)
            {
                t.PrintTasks(c);
                c++;
            }
        }

        public static void AddTask(string header)
        {
            Clear();
            ForegroundColor = ConsoleColor.Green;
            WriteLine($"{header}\n");
            ResetColor();
            Write("Enter a name that will be assigned to the task: ");
            string addMember = ValidateName(ReadLine().Trim());
            Write("\nPlease enter a description of the task: ");
            string addDescription = ValidateDescription(ReadLine().Trim());
            Write("\nPlease enter a due date (mm/dd/yyyy): ");
            string addDueDate = ValidateDate(ReadLine().Trim());

            tasks.Add(new TheTask(addMember, addDescription, addDueDate));
            WriteLine("Task added successfully!");
            WriteLine("Press any key to continue...");
            ReadLine();
        }

        public static void DeleteTask(string header)
        {
            while(true)
            {
                Clear();
                ListTasks(header);
                int i = ValidateSelection($"\nSelect a task to delete (1-{tasks.Count}) ");
                if(i == -1)
                {
                    continue;
                }

                WriteLine($"You selected");
                tasks[i].PrintTasks(i + 1);
                Write($"\nAre you sure you want to delete entry {i + 1}? (y/n) ");
                if(ReadLine().Trim().ToLower() == "y")
                {
                    
                    tasks.RemoveAt(i);
                    WriteLine($"Task {i + 1} successfully removed!");
                }
                else
                {
                    WriteLine("Task deletion aborted!");
                }
                WriteLine("Press any key to continue...");
                ReadLine();
                break;
            }

        }

        public static void CompleteTask(string header)
        {
            while (true)
            {
                Clear();
                ListTasks(header);
                int i = ValidateSelection($"\nSelect a task to mark complete (1-{tasks.Count}) ");
                if (i == -1)
                {
                    continue;
                }

                WriteLine($"You selected");
                tasks[i].PrintTasks(i + 1);
                Write($"\nAre you sure you want to mark entry {i + 1} complete? (y/n) ");
                if (ReadLine().Trim().ToLower() == "y")
                {

                    tasks[i].IsComplete = true;
                    WriteLine($"Task {i + 1} successfully updated!");
                }
                else
                {
                    WriteLine("Task update aborted!");
                }
                WriteLine("Press any key to continue...");
                ReadLine();
                break;
            }
        }
        public static int ValidateSelection(string display)
        {
            while (true)
            {
                try
                {
                    Write(display);
                    int num = int.Parse(ReadLine());
                    if(num >= 1 && num <= tasks.Count)
                    {
                        return num - 1;
                    }
                }
                catch (Exception e)
                {
                    WriteLine(e.Message);
                    WriteLine("Try again");
                    WriteLine("Press any key to continue...");
                    ReadLine();
                    return -1;
                }
            }
        }

        public static string ValidateName(string input)
        {
            while (true)
            {
                if(!String.IsNullOrEmpty(input))
                {
                    return input;
                }
                else
                {
                    WriteLine("Invalid description!");
                    Write("Please enter a description: ");
                    input = ReadLine().Trim();
                }
            }
        }

        public static string ValidateDescription(string input)
        {
            while (true)
            {
                if (Regex.IsMatch(input, "([A-Z])[a-z]+"))
                {
                    return input;
                }
                else
                {
                    WriteLine("Invalid name!");
                    Write("Please enter a name: ");
                    input = ReadLine().Trim();
                }
            }
        }
        public static string ValidateDate(string input)
        {
            while (true)
            {
                if (Regex.IsMatch(input, @"(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$"))
                {
                    return input;
                }
                else
                {
                    WriteLine("Invalid date format!");
                    Write("Please enter a due date (mm/dd/yyyy) ");
                    input = ReadLine().Trim();
                }
            }

        }

    }
}
