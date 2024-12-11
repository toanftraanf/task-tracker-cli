using TaskTrackerCLI.Services;
using Task = TaskTrackerCLI.Models.Task;

TaskManager taskManager = new TaskManager();
Console.WriteLine("\nTask Tracker CLI");
Console.WriteLine("Enter command: ");

while (true)
{
    string? input = Console.ReadLine();
    if (string.IsNullOrEmpty(input))
    {
        Console.WriteLine("Please enter a command.");
        continue;
    }

    string[] inputParts = input.Split(' ', 2);
    string command = inputParts[0].ToLower();
    string arguments = inputParts.Length > 1 ? inputParts[1].Trim() : string.Empty;

    switch (command)
    {
        case "add":
            if (string.IsNullOrEmpty(arguments))
            {
                Console.WriteLine("Task description cannot be empty.");
                continue;
            }
            string description = arguments.Trim('"');
            Task task = new Task
            {
                Id = taskManager.Tasks.Count + 1,
                Description = description,
                Status = "todo",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            taskManager.AddTask(task);
            Console.WriteLine("Task added successfully.");
            break;

        case "delete":
            if (int.TryParse(arguments, out int deleteId))
            {
                taskManager.RemoveTask(deleteId);
                Console.WriteLine("Task deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid task ID.");
            }
            break;

        case "update":
            string[] updateParts = arguments.Split(' ', 2);
            if (updateParts.Length < 2 || !int.TryParse(updateParts[0], out int updateId))
            {
                Console.WriteLine("Invalid update command. Usage: update <id> \"<description>\"");
                continue;
            }
            string updatedDescription = updateParts[1].Trim('"');
            Task updatedTask = new Task
            {
                Description = updatedDescription,
                Status = "todo",
                UpdatedAt = DateTime.Now
            };
            taskManager.UpdateTask(updateId, updatedTask);
            Console.WriteLine("Task updated successfully.");
            break;

        case "list":
            string? status = !string.IsNullOrEmpty(arguments) ? arguments : null;
            taskManager.PrintTasks(status);
            break;

        case "mark-in-progress":
            if (int.TryParse(arguments, out int inProgressId))
            {
                taskManager.UpdateTaskStatus(inProgressId, "in progress");
                Console.WriteLine("Task marked as in progress.");
            }
            else
            {
                Console.WriteLine("Invalid task ID.");
            }
            break;

        case "mark-done":
            if (int.TryParse(arguments, out int doneId))
            {
                taskManager.UpdateTaskStatus(doneId, "done");
                Console.WriteLine("Task marked as done.");
            }
            else
            {
                Console.WriteLine("Invalid task ID.");
            }
            break;

        case "exit":
            Environment.Exit(0);
            break;

        default:
            Console.WriteLine("Invalid command. Please try again.");
            break;
    }
}
