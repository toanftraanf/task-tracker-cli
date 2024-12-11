# Task Tracker CLI

A simple command-line application built with .NET to manage tasks. This application allows users to add tasks, list tasks, mark tasks as done, and delete tasks.

Project URL: [Task Tracker - Roadmap](https://roadmap.sh/projects/task-tracker)

---

## Features

- **Add a New Task**: Add a task with a description and set its initial status.
- **Delete an Existing Task**: Remove a specific task from the list by its ID.
- **Update Task Description**: Modify the description of an existing task.
- **List Tasks by Status**: View tasks filtered by their status (e.g., Pending, In Progress, Done).
- **Mark Task as In Progress**: Update the status of a task to "In Progress".
- **Mark Task as Done**: Update the status of a task to "Done".

---

## Requirements

- [.NET SDK 8.0](https://dotnet.microsoft.com/download)

---

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/TaskTrackerCLI.git
cd TaskTrackerCLI
```

### 2. Build the Project

Run the following command to restore dependencies and build the project:

```bash
dotnet build
```

### 3. Run the Application

Start the application by running:

```bash
dotnet run
```

---

## How to Use

1. **Choose an Option**  
   When the application starts, it displays a menu of actions:

   - Add a new task
   - Delete an existing task
   - Update a task's description
   - List tasks by status
   - Mark a task as "In Progress"
   - Mark a task as "Done"
   - Exit

2. **Follow Prompts**  
   Enter the required information (e.g., task ID or description) as prompted.

3. **Exit the App**  
   Select the `Exit` option to quit.

---

## Folder Structure

```bash
TaskTrackerCLI/
├── Program.cs           # Application entry point
├── Models/              # Contains data models (e.g., Task)
│   └── Task.cs
├── Services/            # Contains business logic (e.g., TaskManager)
│   └── TaskManager.cs
└── README.md            # Project description and instructions
```

---

## Contributing

Contributions are welcome! Feel free to fork this repository and submit pull requests.

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
