# Task Tracker CLI

<div align="center">

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=.net&logoColor=white)
![License](https://img.shields.io/badge/License-AGPL%203.0-blue.svg)
![Language](https://img.shields.io/badge/Language-C%23-239120?logo=csharp&logoColor=white)

A lightweight command-line interface (CLI) application for managing personal task records efficiently.

[Features](#features) • [Quick Start](#quick-start) • [Installation](#installation) • [Architecture](#architecture) • [Contributing](#contributing) • [License](#license)

</div>

## Overview

**Task Tracker CLI** is a practice project built following the [Task Tracker specification](https://roadmap.sh/projects/task-tracker) from [Roadmap.sh](https://roadmap.sh). The application provides a simple yet powerful way to manage your daily tasks directly from the command line, helping you stay organized with an intuitive set of commands.

### Key Highlights
- 📝 **Simple Task Management**: Add, update, delete, and list tasks with ease
- ⏱️ **Status Tracking**: Track tasks as `todo`, `in-progress`, or `done`
- 💾 **Data Persistence**: Tasks are automatically saved to a JSON-based storage
- 🎯 **Efficient**: Built with modern C# patterns and dependency injection
- 🚀 **Cross-platform**: Runs on Windows, macOS, and Linux with .NET 10

## Features

The application includes the following command-line operations:

### Add a New Task
```bash
task-cli add "Buy groceries"
# Output: Task added successfully (ID: 1)
```

### Update an Existing Task
```bash
task-cli update 1 "Buy groceries and cook dinner"
```

### Mark Tasks with Status
```bash
# Mark as in progress
task-cli mark-in-progress 1

# Mark as completed
task-cli mark-done 1
```

### Delete a Task
```bash
task-cli delete 1
```

### List All Tasks
```bash
task-cli list
```

### Filter Tasks by Status
```bash
# View all completed tasks
task-cli list done

# View all pending tasks
task-cli list todo

# View all tasks in progress
task-cli list in-progress
```

## Quick Start

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later
- A terminal or command prompt

### Installation

#### Option 1: Install as a Global Tool (from NuGet)
```bash
dotnet tool install -g TaskTracker.CLI
task-cli add "Your first task"
```

#### Option 2: Build from Source
```bash
# Clone the repository
git clone https://github.com/mchomem/TaskTracker.git
cd TaskTracker

# Build the project
dotnet build

# Run the application
dotnet run --project TaskTracker.CLI -- add "Your first task"

# Or build and install locally
dotnet pack
dotnet tool install --global --add-source ./bin/Release TaskTracker.CLI
```

## Technologies Used

- **Runtime**: .NET 10.0
- **Language**: C# 13
- **Dependency Injection**: Microsoft.Extensions.DependencyInjection
- **Data Format**: JSON (for task persistence)
- **IDE**: Visual Studio 2026 Community Edition

## Architecture

### Project Structure
```
TaskTracker.CLI/
├── Models/                 # Data models
│   ├── UserTask.cs        # Task entity with metadata
│   ├── UserTaskStatus.cs  # Task status enumeration
│   └── AppArguments.cs    # CLI argument model
├── Services/              # Business logic
│   └── UserTaskService.cs # Task operations service
├── Repositories/          # Data access layer
│   └── UserTaskRepository.cs # JSON-based task storage
├── Interfaces/            # Contracts
│   ├── IUserTaskService.cs
│   └── IUserTaskRepository.cs
├── Exceptions/            # Custom exceptions
│   ├── EmptyTaskDescriptionException.cs
│   └── TaskAlreadyDoneException.cs
├── CommandProcessor.cs    # CLI command router
├── Program.cs             # Entry point with DI setup
└── Usings.cs             # Global using statements
```

### Design Patterns Used
- **Service Layer Pattern**: Separates business logic from data access
- **Repository Pattern**: Abstracts data persistence (JSON file storage)
- **Dependency Injection**: Loosely coupled components managed by the IoC container
- **Command Pattern**: Routes CLI commands to appropriate handlers

### Data Persistence
Tasks are stored in a JSON file (`tasks.json`) in the application's working directory. Each task includes:
- `id`: Unique identifier (auto-incremented)
- `description`: Task title/description
- `status`: Current status (Todo, InProgress, Done)
- `createdAt`: Creation timestamp
- `updatedAt`: Last update timestamp (if modified)

## Usage Examples

```bash
# Start your day
task-cli add "Review pull requests"
task-cli add "Complete project documentation"

# Mark tasks as you work
task-cli mark-in-progress 1
task-cli list in-progress

# Update task details
task-cli update 1 "Review and approve pull requests"

# Check your progress
task-cli list done
task-cli list todo

# Clean up completed tasks
task-cli delete 5
```

## Development

### Build the Project
```bash
dotnet build
```

### Run in Development Mode
```bash
dotnet run --project TaskTracker.CLI -- [command] [args]
```

### Package as a NuGet Tool
```bash
dotnet pack TaskTracker.CLI
```

## Project Origin

This project is based on the [Task Tracker](https://roadmap.sh/projects/task-tracker) specification from [Roadmap.sh](https://roadmap.sh/backend/projects), a comprehensive resource for learning web development and backend engineering concepts.

## License

This project is licensed under the **GNU Affero General Public License v3.0** - see the [LICENSE.txt](LICENSE.txt) file for details.

## Contributing

Contributions are welcome! To contribute:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/your-feature`)
3. Commit your changes (`git commit -m 'Add your feature'`)
4. Push to the branch (`git push origin feature/your-feature`)
5. Open a Pull Request

## Support

If you encounter any issues or have questions:
- 📬 Open an [GitHub Issue](https://github.com/mchomem/TaskTracker/issues)
- 💬 Share feedback and suggestions

---

<div align="center">

**[⬆ Back to Top](#task-tracker-cli)**

Made with ❤️ as a learning project

</div>