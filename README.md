# ? My Tasks - Avalonia To-Do List

A modern, cross-platform to-do list application built with Avalonia UI and .NET 8, featuring a beautiful gradient-themed interface with comprehensive task management capabilities.

![Screenshot](https://via.placeholder.com/800x500/667eea/white?text=My+Tasks+Screenshot)

## ?? Features

- **?? Task Management**
  - Add new tasks with a clean, modern input interface
  - Mark tasks as completed with visual feedback
  - In-line editing of existing tasks
  - Delete individual tasks or clear all completed tasks

- **?? Beautiful UI**
  - Modern gradient design with smooth animations
  - Responsive layout that works on different screen sizes
  - Custom styled components with hover effects
  - Emoji-enhanced interface for better visual appeal

- **?? Smart Statistics**
  - Real-time task summary showing pending, completed, and total tasks
  - Empty state with helpful guidance
  - Task timestamps with creation date and time

- **? Performance**
  - MVVM architecture with proper data binding
  - Observable collections for efficient UI updates
  - Command pattern implementation for clean separation of concerns

## ??? Technology Stack

- **Framework**: [Avalonia UI 11.3.5](https://avaloniaui.net/)
- **Runtime**: .NET 8
- **Language**: C# 12
- **Architecture**: MVVM (Model-View-ViewModel)
- **Platform**: Cross-platform (Windows, macOS, Linux)

## ??? Project Structure

```
To-do-list-Avalonia/
??? Infrastructure/
?   ??? RelayCommand.cs          # Command implementation for MVVM
??? Models/
?   ??? TodoItem.cs              # Task data model
??? ViewModels/
?   ??? MainViewModel.cs         # Main view model with business logic
??? Views/
?   ??? App.axaml               # Application configuration
?   ??? App.axaml.cs            # Application code-behind
?   ??? MainWindow.axaml        # Main window UI definition
?   ??? MainWindow.axaml.cs     # Main window code-behind
??? Program.cs                   # Application entry point
```

## ?? Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Any IDE that supports .NET development (Visual Studio, VS Code, JetBrains Rider)

### Installation & Running

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/To-do-list-Avalonia.git
   cd To-do-list-Avalonia
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Run the application**
   ```bash
   dotnet run --project To-do-list-Avalonia
   ```

### Development

To run the project in development mode with hot reload:
```bash
dotnet watch --project To-do-list-Avalonia
```

## ?? Usage

1. **Adding Tasks**: Type your task in the input field and click "? Add Task" or press Enter
2. **Completing Tasks**: Click the checkbox next to any task to mark it as completed
3. **Editing Tasks**: Click the edit button (??) to modify a task title inline
4. **Deleting Tasks**: Click the delete button (???) to remove individual tasks
5. **Bulk Operations**: Use "?? Clear Completed" to remove all finished tasks at once

## ?? Configuration

The application includes several customizable features:

- **Themes**: Modern gradient design with customizable colors in `MainWindow.axaml`
- **Sample Data**: Pre-loaded sample tasks for demonstration (can be removed in `MainViewModel.cs`)
- **Window Settings**: Configurable minimum size and startup position

## ?? Dependencies

- **Avalonia**: Cross-platform .NET UI framework
- **Avalonia.Desktop**: Desktop-specific Avalonia features
- **Avalonia.Themes.Fluent**: Modern Fluent design system
- **Avalonia.Fonts.Inter**: Inter font family for modern typography
- **Avalonia.Diagnostics**: Development tools (Debug builds only)

## ??? Architecture Details

### MVVM Pattern
- **Model**: `TodoItem` - Represents individual tasks with properties for title, completion status, and editing state
- **View**: `MainWindow.axaml` - XAML-based UI with data binding
- **ViewModel**: `MainViewModel` - Business logic, commands, and observable collections

### Key Components
- **RelayCommand**: Custom ICommand implementation for handling user actions
- **ObservableCollection**: Automatic UI updates when tasks are added/removed
- **Property Change Notification**: Real-time updates for task status and statistics

## ?? Contributing

Contributions are welcome! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## ?? License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## ?? Acknowledgments

- [Avalonia UI](https://avaloniaui.net/) - For the amazing cross-platform UI framework
- [Inter Font](https://rsms.me/inter/) - For the beautiful typography
- [Fluent Design System](https://www.microsoft.com/design/fluent/) - For design inspiration

## ?? Screenshots

### Main Interface
![Main Interface](https://via.placeholder.com/600x400/667eea/white?text=Main+Interface)

### Task Management
![Task Management](https://via.placeholder.com/600x400/48CAE4/white?text=Task+Management)

### Empty State
![Empty State](https://via.placeholder.com/600x400/F8FAFC/2D3748?text=Empty+State)

---

Made with ?? using Avalonia UI