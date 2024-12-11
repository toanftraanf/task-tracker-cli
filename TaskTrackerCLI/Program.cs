using TaskTrackerCLI.Services;
using Task = TaskTrackerCLI.Models.Task;

TaskManager taskManager = new TaskManager();
Console.WriteLine("\nTask Tracker CLI");
Console.WriteLine("Enter command: ");

while (true)
{
    string command;
    string? input = Console.ReadLine();
    if (!string.IsNullOrEmpty(input))
    {
        command = input.Split(" ")[0];
    }
    else
    {
        Console.WriteLine("Please enter command: ");
        continue;
    }
    switch (command)
    {
        case "add":
            string description = input.Substring(command.Length).Trim();
            if (string.IsNullOrEmpty(description))
            {
                Console.WriteLine("Task description cannot be empty.");
                continue;
            }
            string updatedDescription = description.Trim('"');
            Task task = new Task
            {
                Id = taskManager.Tasks.Count + 1,
                Description = updatedDescription,
                Status = "todo",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            taskManager.AddTask(task);
            break;
        case "delete":
            int id = Convert.ToInt32(input.Substring("delete".Length).Trim());
            taskManager.RemoveTask(id);
            break;
        case "update":
            string[] updateParts = input.Substring(command.Length).Trim().Split(' ', 2);
            if (updateParts.Length < 2 || !int.TryParse(updateParts[0], out int taskId))
            {
                Console.WriteLine("Invalid update command. Usage: update <id> \"<description>\"");
                continue;
            }
            updatedDescription = updateParts[1].Trim('"');
            Task updatedTask = new Task
            {
                Description = updatedDescription,
                Status = "todo",
                UpdatedAt = DateTime.Now
            };
            taskManager.UpdateTask(taskId, updatedTask);
            break;
        case "list":
            string[] listParts = input.Split(' ', 2);
            string? status = listParts.Length > 1 ? listParts[1] : null;
            taskManager.PrintTasks(status);
            break;
        case "mark-in-progress":
            if (int.TryParse(input.Substring(command.Length).Trim(), out int inProgressId))
            {
                taskManager.UpdateTaskStatus(inProgressId, "in progress");
            }
            else
            {
                Console.WriteLine("Invalid task ID.");
            }
            break;
        case "mark-done":
            if (int.TryParse(input.Substring(command.Length).Trim(), out int doneId))
            {
                taskManager.UpdateTaskStatus(doneId, "done");
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
