using System.Text.Json;
using Task = TaskTrackerCLI.Models.Task;

namespace TaskTrackerCLI.Services
{
    public class TaskManager
    {
        private const string FilePath = "Tasks.json";
        public List<Task> Tasks { get; set; }

        public TaskManager()
        {
            Tasks = LoadTasks();
        }

        /// <summary>
        /// Loads tasks from the JSON file.
        /// </summary>
        /// <returns>A list of tasks.</returns>
        private List<Task> LoadTasks()
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    using FileStream fs = File.OpenRead(FilePath);
                    return JsonSerializer.Deserialize<List<Task>>(fs) ?? new List<Task>();
                }
                catch (JsonException)
                {
                    // Handle JSON deserialization errors if necessary
                    Console.WriteLine("Error reading tasks from file. Starting with an empty task list.");
                    return new List<Task>();
                }
                catch (Exception ex)
                {
                    // Handle other potential exceptions
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                    return new List<Task>();
                }
            }
            return new List<Task>();
        }

        /// <summary>
        /// Saves tasks to the JSON file.
        /// </summary>
        private void SaveTasks()
        {
            try
            {
                using FileStream fs = File.Create(FilePath);
                JsonSerializer.Serialize(fs, Tasks);
            }
            catch (Exception ex)
            {
                // Handle potential exceptions during file writing
                Console.WriteLine($"Error saving tasks to file: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a new task to the list and saves it.
        /// </summary>
        /// <param name="task">The task to add.</param>
        public void AddTask(Task task)
        {
            Tasks.Add(task);
            SaveTasks();
        }

        /// <summary>
        /// Removes a task by its ID and saves the changes.
        /// </summary>
        /// <param name="id">The ID of the task to remove.</param>
        public void RemoveTask(int id)
        {
            Task task = Tasks.Find(t => t.Id == id);
            if (task != null)
            {
                Tasks.Remove(task);
                SaveTasks();
            }
            else
            {
                Console.WriteLine($"Task with ID {id} not found.");
            }
        }

        /// <summary>
        /// Updates an existing task and saves the changes.
        /// </summary>
        /// <param name="id">The ID of the task to update.</param>
        /// <param name="updatedTask">The updated task details.</param>
        public void UpdateTask(int id, Task updatedTask)
        {
            Task task = Tasks.Find(t => t.Id == id);
            if (task != null)
            {
                task.Description = updatedTask.Description;
                task.Status = updatedTask.Status;
                task.UpdatedAt = DateTime.Now;
                SaveTasks();
            }
            else
            {
                Console.WriteLine($"Task with ID {id} not found.");
            }
        }

        /// <summary>
        /// Updates the status of a task and saves the changes.
        /// </summary>
        /// <param name="id">The ID of the task to update.</param>
        /// <param name="status">The new status of the task.</param>
        public void UpdateTaskStatus(int id, string status)
        {
            Task? task = Tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.Status = status;
                task.UpdatedAt = DateTime.Now;
                SaveTasks();
            }
            else
            {
                Console.WriteLine($"Task with ID {id} not found.");
            }
        }

        /// <summary>
        /// Prints tasks to the console, optionally filtered by status.
        /// </summary>
        /// <param name="status">The status to filter tasks by (optional).</param>
        public void PrintTasks(string? status)
        {
            var tasksToPrint = string.IsNullOrEmpty(status) ? Tasks : Tasks.Where(t => t.Status == status).ToList();
            if (tasksToPrint.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            foreach (Task task in tasksToPrint)
            {
                Console.WriteLine($"Id: {task.Id}");
                Console.WriteLine($"Description: {task.Description}");
                Console.WriteLine($"Status: {task.Status}");
                Console.WriteLine($"Created At: {task.CreatedAt}");
                Console.WriteLine($"Updated At: {task.UpdatedAt}");
                Console.WriteLine();
            }
        }
    }
}
