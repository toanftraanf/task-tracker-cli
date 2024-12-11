using Task = TaskTrackerCLI.Models.Task;
namespace TaskTrackerCLI.Services
{
    public class TaskManager
    {
        public List<Task> Tasks { get; set; }

        public TaskManager()
        {
            Tasks = new List<Task>();
        }

        public void AddTask(Task task)
        {
            Tasks.Add(task);
        }

        public void RemoveTask(int id)
        {
            Task task = Tasks.Find(t => t.Id == id);
            if (task != null)
            {
                Tasks.Remove(task);
            }
        }

        public void UpdateTask(int id, Task updatedTask)
        {
            Task task = Tasks.Find(t => t.Id == id);
            if (task != null)
            {
                task.Description = updatedTask.Description;
                task.Status = updatedTask.Status;
                task.UpdatedAt = DateTime.Now;
            }
        }
        public void UpdateTaskStatus(int id, string status)
        {
            Task? task = Tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.Status = status;
                task.UpdatedAt = DateTime.Now;
                //Console.WriteLine($"Task {id} marked as {status}.");
            }
            else
            {
                Console.WriteLine($"Task with ID {id} not found.");
            }
        }
        public void PrintTasks(string? status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                Tasks = Tasks.Where(t => t.Status == status).ToList();
            }
            foreach (Task task in Tasks)
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
