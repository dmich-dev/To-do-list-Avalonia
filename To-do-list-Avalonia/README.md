# ?? To-Do List Application with Sticky Notes

A modern, feature-rich desktop application built with Avalonia UI and .NET 8. This application combines a traditional todo list with floating sticky notes, providing a complete task management and note-taking solution.

## ?? Project Overview

This project demonstrates a complete desktop application architecture using the **MVVM (Model-View-ViewModel)** pattern, implementing **SOLID principles**, and featuring persistent data storage. Built as a cross-platform application using Avalonia UI framework, it runs on Windows, macOS, and Linux.

### Key Technologies
- **.NET 8.0** - Latest .NET framework
- **Avalonia UI 11.3.5** - Cross-platform UI framework
- **C# 12.0** - Modern C# language features
- **JSON** - Data persistence format
- **MVVM Pattern** - Clean separation of concerns
- **SOLID Principles** - Professional code architecture

---

## ? Features

### Todo List Management
- ? **Create Tasks** - Add new todo items with a simple input field
- ?? **Edit Tasks** - Double-click or click edit to modify existing tasks
- ?? **Complete Tasks** - Check off completed items
- ??? **Delete Tasks** - Remove individual tasks or bulk clear completed items
- ?? **Summary Statistics** - Real-time count of pending, completed, and total tasks
- ?? **Auto-Save** - All changes are automatically saved to disk

### Sticky Notes
- ?? **Floating Notes** - Create independent sticky note windows
- ?? **Color Customization** - Choose from multiple color themes (Yellow, Blue, Pink, Green, Purple)
- ?? **Resizable** - Drag corners to resize notes
- ??? **Draggable** - Move notes anywhere on screen
- ?? **Auto-Save with Debouncing** - Changes saved automatically after 1 second of inactivity
- ?? **Position Persistence** - Notes remember their position and size
- ? **Save Status Indicator** - Visual feedback showing save status

### Data Persistence
- ?? **Automatic Saving** - All data saved automatically
- ?? **JSON Format** - Human-readable data storage
- ?? **User AppData Location** - Data stored in appropriate system folders
- ?? **Crash Recovery** - Data persists even if application crashes

---

## ?? File Structure

```
To-do-list-Avalonia/
?
??? Models/                           # Data models (SOLID: Single Responsibility)
?   ??? TodoItem.cs                   # Todo item entity
?   ??? StickyNote.cs                 # Sticky note entity
?
??? ViewModels/                       # MVVM ViewModels
?   ??? MainViewModel.cs              # Main window view model
?   ??? StickyNoteViewModel.cs        # Sticky note window view model
?   ??? IViewModelFactory.cs          # Factory interface (SOLID: Dependency Inversion)
?   ??? ViewModelFactory.cs           # Factory implementation (Creational Pattern)
?
??? Views/                            # UI Views (XAML)
?   ??? StickyNoteWindow.axaml[.cs]   # Sticky note window UI
?
??? Services/                         # Business logic and data services
?   ??? IDataService.cs               # Generic data service interface (SOLID: DIP)
?   ??? TodoDataService.cs            # Todo persistence service
?   ??? StickyNoteDataService.cs      # Sticky note persistence service
?
??? Infrastructure/                   # Shared utilities and base classes
?   ??? RelayCommand.cs               # ICommand implementation for MVVM
?
??? MainWindow.axaml[.cs]             # Main application window
??? App.axaml[.cs]                    # Application entry point and resources
??? Program.cs                        # Application bootstrapper
??? To-do-list-Avalonia.csproj        # Project configuration

??? To-do-list-Avalonia.Tests/        # Unit test project
?   ??? UnitTests.cs                  # 32+ comprehensive unit tests

Data Files (Auto-generated in AppData):
%AppData%/TodoListAvalonia/
??? todos.json                        # Serialized todo items
??? stickynotes.json                  # Serialized sticky notes
```

### Architecture Explanation

#### **Models** (Data Layer)
- Pure data classes representing application entities
- Implement `INotifyPropertyChanged` for data binding
- No business logic - just data and property change notifications

#### **ViewModels** (Presentation Logic)
- Mediate between Views and Models
- Handle user commands via `RelayCommand`
- Manage application state
- Coordinate with Services for data operations

#### **Views** (UI Layer)
- XAML files defining the user interface
- Data-bound to ViewModels
- No business logic - pure presentation

#### **Services** (Business Logic)
- Handle data persistence (JSON serialization)
- Implement `IDataService<T>` interface
- Separate concerns from ViewModels

#### **Infrastructure** (Shared Code)
- `RelayCommand` - Standard ICommand implementation
- Factory patterns for creating ViewModels
- Reusable components

---

## ?? Installation Instructions

### Prerequisites
- **.NET 8.0 SDK or later** - [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Visual Studio 2022** (recommended) or **Visual Studio Code**
- **Git** (optional, for cloning the repository)

### Step-by-Step Installation

#### Option 1: Clone from Repository
```bash
# Clone the repository
git clone https://github.com/dmich-dev/To-do-list-Avalonia.git

# Navigate to project directory
cd To-do-list-Avalonia/To-do-list-Avalonia

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run
```

#### Option 2: Open in Visual Studio
1. Open Visual Studio 2022
2. Click **File ? Open ? Project/Solution**
3. Navigate to and select `To-do-list-Avalonia.csproj`
4. Press **F5** or click **Start** to build and run

#### Option 3: Publish as Standalone Executable
```bash
# Build a self-contained executable (Windows)
dotnet publish -c Release -r win-x64 --self-contained

# Build for macOS
dotnet publish -c Release -r osx-x64 --self-contained

# Build for Linux
dotnet publish -c Release -r linux-x64 --self-contained
```

The executable will be in `bin/Release/net8.0/{runtime}/publish/`

---

## ?? How Data is Stored

### Storage Location
Data is stored in the user's **Application Data** folder:
- **Windows**: `C:\Users\{Username}\AppData\Roaming\TodoListAvalonia\`
- **macOS**: `~/Library/Application Support/TodoListAvalonia/`
- **Linux**: `~/.config/TodoListAvalonia/`

### File Formats

#### `todos.json` - Todo Items
```json
[
  {
    "Id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "CreatedAt": "2024-01-15T10:30:00",
    "Title": "Complete project documentation",
    "IsCompleted": false,
    "IsEditing": false,
    "EditTitle": ""
  }
]
```

#### `stickynotes.json` - Sticky Notes
```json
[
  {
    "Id": "7fa85f64-8920-4562-c4fc-3d074g77bgb7",
    "CreatedAt": "2024-01-15T11:00:00",
    "Content": "Remember to review code",
    "Color": "#FFF9C4",
    "PositionX": 100.0,
    "PositionY": 150.0,
    "Width": 300.0,
    "Height": 300.0
  }
]
```

### Save Mechanism
1. **Auto-save on changes** - Both TodoItems and StickyNotes save automatically
2. **Debouncing** - StickyNotes use 1-second debounce to prevent excessive saves
3. **Async operations** - All file I/O is asynchronous to prevent UI blocking
4. **Error handling** - Graceful fallback to empty lists if files are corrupted

---

## ?? SOLID Principles Implementation

### **S - Single Responsibility Principle**
Each class has one reason to change:
- `TodoItem` - Represents todo data only
- `TodoDataService` - Handles todo persistence only
- `MainViewModel` - Manages main window state only
- `StickyNoteViewModel` - Manages single note state only

### **O - Open/Closed Principle**
- `IDataService<T>` interface allows extending data services without modifying existing code
- `IViewModelFactory` enables new ViewModel types without changing the factory pattern

### **L - Liskov Substitution Principle**
- All implementations of `IDataService<T>` can be substituted without breaking functionality
- ViewModels properly implement `INotifyPropertyChanged`

### **I - Interface Segregation Principle**
- Focused interfaces like `IDataService<T>` instead of monolithic interfaces
- Clients depend only on methods they use

### **D - Dependency Inversion Principle**
- ViewModels depend on `IDataService<T>` abstraction, not concrete implementations
- High-level modules (ViewModels) don't depend on low-level modules (Services)

---

## ?? Design Pattern: Factory Pattern (Creational)

### **Factory Pattern Implementation**
Located in: `ViewModels/ViewModelFactory.cs` and `ViewModels/IViewModelFactory.cs`

**Benefits:**
- Centralizes ViewModel creation logic
- Makes testing easier (can mock the factory)
- Allows dependency injection in the future
- Follows Open/Closed Principle

**Usage Example:**
```csharp
IViewModelFactory factory = new ViewModelFactory();
var stickyNoteVM = factory.CreateStickyNoteViewModel(note, onClose, onSave);
```

This pattern can be extended to create other ViewModels and supports dependency injection frameworks.

---

## ?? Known Issues and Limitations

### Current Limitations
1. **Single User** - No multi-user support or cloud sync
2. **No Undo/Redo** - Changes are immediate and permanent
3. **No Search** - Cannot search through todos or notes
4. **No Categories** - Todos cannot be grouped into projects/categories
5. **No Due Dates** - No reminder or calendar integration
6. **No Export** - Cannot export data to other formats (CSV, PDF)
7. **Limited Styling** - Fixed color palette for sticky notes

### Known Issues
1. **High DPI Scaling** - Sticky note positioning may be off on high-DPI displays
2. **Window Management** - Sticky notes may appear off-screen if screen resolution changes
3. **Performance** - Large numbers of sticky notes (>50) may impact performance
4. **File Locking** - Multiple instances of the app will overwrite each other's data

### Planned Improvements
- Add task categories/projects
- Implement search functionality
- Add due dates and reminders
- Cloud synchronization option
- Import/export features
- More customization options

---

## ?? Debugging Summary

### Debug Configuration
The project includes debug symbols and diagnostic tools:
- **Avalonia DevTools** - Available in Debug builds (F12 in app)
- **Console logging** - Error messages output to console
- **Exception handling** - Graceful degradation on errors

### Unit Tests
The project includes **32+ comprehensive unit tests** covering:
- **Model Tests** - TodoItem and StickyNote data integrity
- **ViewModel Tests** - MainViewModel and StickyNoteViewModel behavior
- **Command Tests** - RelayCommand pattern implementation
- **Service Tests** - Data service interface implementations
- **Factory Tests** - Factory pattern verification
- **SOLID Principle Tests** - Verify Liskov Substitution and Interface Segregation

**Run tests:**
```bash
dotnet test
```

**Test Results:**
- ? 32 tests passed
- ? 0 tests failed
- ? Code coverage across all layers (Models, ViewModels, Services, Infrastructure)

### Common Issues and Solutions

#### **Issue: Application won't start**
**Solution:** 
- Ensure .NET 8.0 SDK is installed: `dotnet --version`
- Clean and rebuild: `dotnet clean && dotnet build`

#### **Issue: Data not saving**
**Solution:**
- Check AppData folder permissions
- Look for exceptions in Output window
- Verify JSON file integrity

#### **Issue: Sticky notes appear off-screen**
**Solution:**
- Delete `stickynotes.json` from AppData folder
- Restart application (notes will reset to default position)

### Debugging Tips
1. **Enable Avalonia DevTools** - Press F12 while app is running (Debug mode only)
2. **Check Output Window** - View console logs in Visual Studio
3. **Inspect JSON Files** - Manually verify data files in AppData
4. **Use Breakpoints** - Set breakpoints in save/load methods
5. **Watch PropertyChanged Events** - Track data binding issues

---

## ?? Credits and Acknowledgements

### Developer
**dmich-dev** - Primary developer and maintainer
- GitHub: [@dmich-dev](https://github.com/dmich-dev)
- Repository: [To-do-list-Avalonia](https://github.com/dmich-dev/To-do-list-Avalonia)

### Frameworks and Libraries
- **[Avalonia UI](https://avaloniaui.net/)** - Cross-platform XAML-based UI framework
- **[.NET 8.0](https://dotnet.microsoft.com/)** - Microsoft's open-source development platform
- **[System.Text.Json](https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-overview)** - High-performance JSON serialization

### Inspiration and Resources
- **MVVM Pattern** - Microsoft's architectural pattern for separating UI and business logic
- **SOLID Principles** - Object-oriented design principles by Robert C. Martin
- **Factory Pattern** - Gang of Four design patterns
- **Avalonia Documentation** - Excellent tutorials and samples

### Educational Context
This project was developed as part of a software development course to demonstrate:
- Modern application architecture
- SOLID principles in practice
- Design patterns implementation
- Cross-platform development
- Professional documentation practices

### License
This project is developed for educational purposes. Check the repository for license details.

---

## ?? Contributing

While this is primarily an educational project, suggestions and feedback are welcome:

1. **Report Issues** - Use GitHub Issues for bug reports
2. **Feature Requests** - Suggest new features via Issues
3. **Pull Requests** - Fork and submit PRs for improvements

---

## ?? Learning Resources

If you're studying this project, here are helpful resources:

### Avalonia UI
- [Official Documentation](https://docs.avaloniaui.net/)
- [Getting Started Tutorial](https://docs.avaloniaui.net/docs/getting-started)
- [MVVM Pattern Guide](https://docs.avaloniaui.net/docs/basics/mvvm)

### SOLID Principles
- [SOLID Principles Explained](https://www.digitalocean.com/community/conceptual_articles/s-o-l-i-d-the-first-five-principles-of-object-oriented-design)
- [Uncle Bob's SOLID Principles](https://blog.cleancoder.com/uncle-bob/2020/10/18/Solid-Relevance.html)

### Design Patterns
- [Factory Pattern](https://refactoring.guru/design-patterns/factory-method)
- [Design Patterns: Elements of Reusable Object-Oriented Software](https://en.wikipedia.org/wiki/Design_Patterns)

### .NET and C#
- [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [C# Programming Guide](https://docs.microsoft.com/en-us/dotnet/csharp/)

---

## ?? Version History

### Version 1.0 (Current)
- ? Todo list with CRUD operations
- ? Floating sticky notes
- ? JSON data persistence
- ? MVVM architecture
- ? SOLID principles
- ? Factory pattern
- ? Auto-save functionality
- ? Cross-platform support

---

**Built with ?? using Avalonia UI and .NET 8**

*Last Updated: January 2024*
