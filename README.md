# My Tasks - Avalonia To-Do List with Sticky Notes

A modern, feature-rich cross-platform productivity application built with Avalonia UI and .NET 8. Combine powerful task management with flexible sticky notes in a beautiful gradient-themed interface.

![Screenshot](https://via.placeholder.com/800x500/667eea/white?text=My+Tasks+Screenshot)

## ✨ Features

### 📝 Task Management
- **Add & Organize Tasks**: Clean, modern input interface with instant feedback
- **Drag & Drop Reordering**: Easily reorganize tasks by dragging them to new positions
- **Mark as Complete**: Visual feedback with checkbox completion status
- **In-line Editing**: Click the edit button to modify task titles on the fly
- **Smart Deletion**: Delete individual tasks or bulk clear all completed items
- **Real-time Statistics**: Live summary showing pending, completed, and total task counts
- **Persistent Storage**: Automatic saving to local storage with async operations

### 📌 Sticky Notes
- **Create Multiple Notes**: Launch unlimited sticky note windows for quick thoughts
- **Customizable Colors**: Choose from 6 vibrant color themes (Yellow, Blue, Green, Pink, Purple, Orange)
- **Draggable Windows**: Move notes anywhere on your screen
- **Resizable**: Adjust note size to fit your content
- **Auto-save**: Changes are automatically saved with debounced persistence (1-second delay)
- **Position Memory**: Notes remember their last position and size between sessions
- **Close & Restore**: Closed notes are removed, new notes can be created anytime

### 🎨 Beautiful UI
- **Modern Gradient Design**: Eye-catching color schemes with smooth transitions
- **Smooth Animations**: Polished hover effects and state transitions
- **Responsive Layout**: Adapts to different screen sizes and window states
- **Emoji Integration**: Enhanced visual communication throughout the interface
- **Custom Styling**: Carefully crafted components with attention to detail
- **Empty State Guidance**: Helpful prompts when getting started

### ⚡ Performance & Architecture
- **MVVM Pattern**: Clean separation of concerns with proper data binding
- **Observable Collections**: Efficient UI updates without manual refresh
- **Command Pattern**: Testable, reusable command implementations
- **Async/Await**: Non-blocking I/O operations for smooth performance
- **Debounced Saving**: Smart auto-save prevents excessive disk writes
- **Memory Management**: Proper disposal and event unsubscription

## 🛠️ Technology Stack

- **Framework**: [Avalonia UI 11.3.5](https://avaloniaui.net/)
- **Runtime**: .NET 8
- **Language**: C# 12
- **Architecture**: MVVM (Model-View-ViewModel)
- **Platform**: Cross-platform (Windows, macOS, Linux)
- **Storage**: JSON-based local persistence

## 📂 Project Structure

```
To-do-list-Avalonia/
├── Infrastructure/
│   └── RelayCommand.cs          # Command implementation for MVVM
├── Models/
│   └── TodoItem.cs              # Task data model
├── ViewModels/
│   └── MainViewModel.cs         # Main view model with business logic
├── Views/
│   ├── App.axaml               # Application configuration
│   ├── App.axaml.cs            # Application code-behind
│   ├── MainWindow.axaml        # Main window UI definition
│   └── MainWindow.axaml.cs     # Main window code-behind
├── Notes/                        # Sticky notes implementation
│   ├── StickyNote.xaml          # Sticky note UI definition
│   ├── StickyNote.xaml.cs       # Sticky note code-behind
│   ├── StickyNotesViewModel.cs  # ViewModel for sticky notes
└── Program.cs                   # Application entry point
```

## 🚀 Getting Started

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

## 📝 Usage

### Task Management

1. **Adding Tasks**: Type your task in the input field and click "Add Task" or press Enter
2. **Completing Tasks**: Click the checkbox next to any task to mark it as completed
3. **Editing Tasks**: Click the edit button to modify a task title inline
4. **Deleting Tasks**: Click the delete button to remove individual tasks
5. **Bulk Operations**: Use "Clear Completed" to remove all finished tasks at once

### Sticky Notes

1. **Creating Notes**: Click on "New Note" to open a blank sticky note
2. **Entering Text**: Type directly onto the sticky note
3. **Moving & Resizing**: Drag the note by the header or resize using the corners
4. **Changing Color**: Select a color from the color palette menu
5. **Closing Notes**: Click the close button to remove a note (it will be restored on next run)

## ⚙️ Configuration

The application includes several customizable features:

- **Themes**: Modern gradient design with customizable colors in `MainWindow.axaml`
- **Sample Data**: Pre-loaded sample tasks for demonstration (can be removed in `MainViewModel.cs`)
- **Window Settings**: Configurable minimum size and startup position
- **Sticky Note Colors**: Change sticky note colors in `StickyNote.xaml`

## 📚 Dependencies

- **Avalonia**: Cross-platform .NET UI framework
- **Avalonia.Desktop**: Desktop-specific Avalonia features
- **Avalonia.Themes.Fluent**: Modern Fluent design system
- **Avalonia.Fonts.Inter**: Inter font family for modern typography
- **Avalonia.Diagnostics**: Development tools (Debug builds only)
- **Newtonsoft.Json**: Popular high-performance JSON framework for .NET

## 📐 Architecture Details

### MVVM Pattern
- **Model**: `TodoItem` - Represents individual tasks with properties for title, completion status, and editing state
- **View**: `MainWindow.axaml` - XAML-based UI with data binding
- **ViewModel**: `MainViewModel` - Business logic, commands, and observable collections

### Sticky Notes Architecture
- **Model**: `StickyNoteModel` - Represents a sticky note with properties for content, position, size, and color
- **View**: `StickyNote.xaml` - XAML-based UI for sticky notes
- **ViewModel**: `StickyNotesViewModel` - Manages a collection of sticky notes, handles creation, deletion, and persistence

### Key Components
- **RelayCommand**: Custom ICommand implementation for handling user actions
- **ObservableCollection**: Automatic UI updates when tasks are added/removed
- **Property Change Notification**: Real-time updates for task status and statistics
- **JSON Persistence**: Async reading and writing of tasks and notes to local JSON files

## 👥 Contributing

Contributions are welcome! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📜 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- [Avalonia UI](https://avaloniaui.net/) - For the amazing cross-platform UI framework
- [Inter Font](https://rsms.me/inter/) - For the beautiful typography
- [Fluent Design System](https://www.microsoft.com/design/fluent/) - For design inspiration

## 🖼️ Screenshots

### Main Interface
![Main Interface](https://via.placeholder.com/600x400/667eea/white?text=Main+Interface)

### Task Management
![Task Management](https://via.placeholder.com/600x400/48CAE4/white?text=Task+Management)

### Empty State
![Empty State](https://via.placeholder.com/600x400/F8FAFC/2D3748?text=Empty+State)

### Sticky Notes
![Sticky Notes](https://via.placeholder.com/600x400/FFD700/2D3748?text=Sticky+Notes)

---

Made with ❤️ using Avalonia UI