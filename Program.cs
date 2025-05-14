using System;
using System.Collections.Generic;
using System.IO

class TaskItem
{
    // Sets the properties for the task item
    public string Description { get; set; }
    public bool IsDone { get; set; } = false;

    // Override the ToString method to display the task item
    public override string ToString()
    {
        return (IsDone ? "[✓]" : "[ ]") + Description;
    }
}
class Program
{
    // List to hold the task items
    static List<string> todoList = new List<TaskItem>();

    static void Main()
    {
        // Load existing tasks from file
        LoadTasks();

        // Main loop for the console to-do list
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== TO-DO LIST ===");
            ShowList();
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. Add new task");
            Console.WriteLine("2. Mark task as done");
            Console.WriteLine("3. Delete task");
            Console.WriteLine("4. Exit");
            Console.Write("\nChoose an option: ");
            string choice = Console.ReadLine();

            if (choice == "1")
                AddTask();
            else if (choice == "2")
                MarkDone();
            else if (choice == "3")
                DeleteTask();
            else if (choice == "4")
                break;
        }
    }

    // Method to display the list of tasks with the status
    static void ShowList()
    {
        if (todoList.Count == 0)
        {
            Console.WriteLine("Your list is empty!");
        }
        else
        {
            for (int i = 0; i < todoList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {todoList[i]}");
            }
        }
    }

    // Method to add a new task
    static void AddTask()
    {
        Console.Write("Enter new task: ");
        string task = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(task))
        {
            todoList.Add(new TaskItem { Description = task });
            SaveTasks(); // Save to file after adding
        }
    }

    // Methods to load tasks to a file
    static void LoadTasks()
    {
        todoList.Clear();
        if (File.Exists("tasks.txt"))
        {
            var lines = File.ReadAllLines("tasks.txt");
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length == 2)
                {
                    bool isDone = parts[0] == "True";
                    todoList.Add(new TaskItem { IsDone = isDone, Description = parts[1] });
                }
            }
        }
    }

    // Method to save tasks to a file
    static void SaveTasks()
    {
        var lines = new List<string>();
        foreach (var task in todoList)
        {
            lines.Add($"{task.IsDone}|{task.Description}");
        }
        File.WriteAllLines("tasks.txt", lines);
    }

    // Method to mark a task as done
    static void MarkDone()
    {
        Console.Write("Enter task number to mark as done: ");
        if (int.TryParse(Console.Readline(), out int number) && number >= 1 && number <= todoList.Count)
        {
            todoList[number - 1].IsDone = true;
            SaveTasks(); // Save to file after marking as done
        }
    }

    // Method to delete a task
    static void DeleteTask()
    {
        Console.Write("Enter task number to delete: ");
        if (int.TryParse(Console.ReadLine(), out int number) && number >= 1 && number <= todoList.Count)
        {
            todoList.RemoveAt(number - 1);
            SaveTasks(); // Save to file after deleting
        }
    }
}