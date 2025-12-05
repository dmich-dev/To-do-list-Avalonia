using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using To_do_list_Avalonia.Models;
using To_do_list_Avalonia.Services;
using To_do_list_Avalonia.ViewModels;
using To_do_list_Avalonia.Infrastructure;
using Xunit;

namespace To_do_list_Avalonia.Tests;

/// <summary>
/// Unit tests for TodoItem model.
/// Tests Single Responsibility Principle - model only handles data.
/// </summary>
public class TodoItemTests
{
    [Fact]
    public void TodoItem_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var todoItem = new TodoItem();

        // Assert
        Assert.NotEqual(Guid.Empty, todoItem.Id);
        Assert.NotEqual(default(DateTime), todoItem.CreatedAt);
        Assert.Equal(string.Empty, todoItem.Title);
        Assert.False(todoItem.IsCompleted);
        Assert.False(todoItem.IsEditing);
    }

    [Fact]
    public void TodoItem_ShouldSetAndGetTitle()
    {
        // Arrange
        var todoItem = new TodoItem();
        var expectedTitle = "Test Todo Item";

        // Act
        todoItem.Title = expectedTitle;

        // Assert
        Assert.Equal(expectedTitle, todoItem.Title);
    }

    [Fact]
    public void TodoItem_ShouldSetAndGetCompletedStatus()
    {
        // Arrange
        var todoItem = new TodoItem();

        // Act
        todoItem.IsCompleted = true;

        // Assert
        Assert.True(todoItem.IsCompleted);
    }

    [Fact]
    public void TodoItem_ShouldRaisePropertyChangedEvent_WhenTitleChanges()
    {
        // Arrange
        var todoItem = new TodoItem();
        var propertyChangedRaised = false;
        todoItem.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(TodoItem.Title))
                propertyChangedRaised = true;
        };

        // Act
        todoItem.Title = "New Title";

        // Assert
        Assert.True(propertyChangedRaised);
    }
}

/// <summary>
/// Unit tests for StickyNote model.
/// Tests Single Responsibility Principle - model only handles data.
/// </summary>
public class StickyNoteTests
{
    [Fact]
    public void StickyNote_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var note = new StickyNote();

        // Assert
        Assert.NotEqual(Guid.Empty, note.Id);
        Assert.NotEqual(default(DateTime), note.CreatedAt);
        Assert.Equal(string.Empty, note.Content);
        Assert.Equal("#FFF9C4", note.Color); // Default yellow
        Assert.Equal(300, note.Width);
        Assert.Equal(300, note.Height);
    }

    [Fact]
    public void StickyNote_ShouldSetAndGetContent()
    {
        // Arrange
        var note = new StickyNote();
        var expectedContent = "Test note content";

        // Act
        note.Content = expectedContent;

        // Assert
        Assert.Equal(expectedContent, note.Content);
    }

    [Fact]
    public void StickyNote_ShouldSetAndGetColor()
    {
        // Arrange
        var note = new StickyNote();
        var expectedColor = "#FFB3E5";

        // Act
        note.Color = expectedColor;

        // Assert
        Assert.Equal(expectedColor, note.Color);
    }

    [Fact]
    public void StickyNote_ShouldSetAndGetPosition()
    {
        // Arrange
        var note = new StickyNote();

        // Act
        note.PositionX = 100;
        note.PositionY = 200;

        // Assert
        Assert.Equal(100, note.PositionX);
        Assert.Equal(200, note.PositionY);
    }

    [Fact]
    public void StickyNote_ShouldRaisePropertyChangedEvent_WhenContentChanges()
    {
        // Arrange
        var note = new StickyNote();
        var propertyChangedRaised = false;
        note.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(StickyNote.Content))
                propertyChangedRaised = true;
        };

        // Act
        note.Content = "New content";

        // Assert
        Assert.True(propertyChangedRaised);
    }
}

/// <summary>
/// Unit tests for RelayCommand.
/// Tests Command pattern implementation.
/// </summary>
public class RelayCommandTests
{
    [Fact]
    public void RelayCommand_ShouldExecuteAction()
    {
        // Arrange
        var executed = false;
        var command = new RelayCommand(_ => executed = true);

        // Act
        command.Execute(null);

        // Assert
        Assert.True(executed);
    }

    [Fact]
    public void RelayCommand_ShouldRespectCanExecute()
    {
        // Arrange
        var canExecute = false;
        var command = new RelayCommand(_ => { }, _ => canExecute);

        // Act & Assert
        Assert.False(command.CanExecute(null));

        canExecute = true;
        Assert.True(command.CanExecute(null));
    }

    [Fact]
    public void RelayCommand_ShouldRaiseCanExecuteChanged()
    {
        // Arrange
        var command = new RelayCommand(_ => { });
        var eventRaised = false;
        command.CanExecuteChanged += (sender, args) => eventRaised = true;

        // Act
        command.RaiseCanExecuteChanged();

        // Assert
        Assert.True(eventRaised);
    }

    [Fact]
    public void RelayCommand_ShouldThrowException_WhenExecuteIsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new RelayCommand(null!));
    }
}

/// <summary>
/// Unit tests for ViewModelFactory.
/// Tests Factory Pattern (Creational Design Pattern).
/// Tests Open/Closed Principle - can extend without modification.
/// </summary>
public class ViewModelFactoryTests
{
    [Fact]
    public void ViewModelFactory_ShouldCreateStickyNoteViewModel()
    {
        // Arrange
        var factory = new ViewModelFactory();
        var note = new StickyNote { Content = "Test note" };
        Action<StickyNoteViewModel> onClose = _ => { };
        Action onSave = () => { };

        // Act
        var viewModel = factory.CreateStickyNoteViewModel(note, onClose, onSave);

        // Assert
        Assert.NotNull(viewModel);
        Assert.Equal(note.Id, viewModel.Id);
        Assert.Equal(note.Content, viewModel.Content);
    }

    [Fact]
    public void ViewModelFactory_ShouldImplementIViewModelFactory()
    {
        // Arrange & Act
        IViewModelFactory factory = new ViewModelFactory();

        // Assert - Tests Liskov Substitution Principle
        Assert.NotNull(factory);
        Assert.IsAssignableFrom<IViewModelFactory>(factory);
    }
}

/// <summary>
/// Unit tests for StickyNoteViewModel.
/// Tests MVVM pattern and ViewModel behavior.
/// </summary>
public class StickyNoteViewModelTests
{
    [Fact]
    public void StickyNoteViewModel_ShouldInitializeWithNoteData()
    {
        // Arrange
        var note = new StickyNote
        {
            Content = "Test content",
            Color = "#FFB3E5",
            PositionX = 100,
            PositionY = 200
        };

        // Act
        var viewModel = new StickyNoteViewModel(note, _ => { }, () => { });

        // Assert
        Assert.Equal(note.Content, viewModel.Content);
        Assert.Equal(note.Color, viewModel.Color);
        Assert.Equal(note.PositionX, viewModel.PositionX);
        Assert.Equal(note.PositionY, viewModel.PositionY);
    }

    [Fact]
    public void StickyNoteViewModel_ShouldUpdateContent()
    {
        // Arrange
        var note = new StickyNote();
        var viewModel = new StickyNoteViewModel(note, _ => { }, () => { });
        var newContent = "Updated content";

        // Act
        viewModel.Content = newContent;

        // Assert
        Assert.Equal(newContent, viewModel.Content);
        Assert.Equal(newContent, note.Content); // Model should be updated too
    }

    [Fact]
    public void StickyNoteViewModel_ShouldUpdateColor()
    {
        // Arrange
        var note = new StickyNote();
        var viewModel = new StickyNoteViewModel(note, _ => { }, () => { });
        var newColor = "#B3E5FC";

        // Act
        viewModel.Color = newColor;

        // Assert
        Assert.Equal(newColor, viewModel.Color);
        Assert.Equal(newColor, note.Color);
    }

    [Fact]
    public void StickyNoteViewModel_ShouldHaveCloseCommand()
    {
        // Arrange
        var note = new StickyNote();
        var viewModel = new StickyNoteViewModel(note, _ => { }, () => { });

        // Act & Assert
        Assert.NotNull(viewModel.CloseCommand);
        Assert.True(viewModel.CloseCommand.CanExecute(null));
    }

    [Fact]
    public void StickyNoteViewModel_ShouldHaveChangeColorCommand()
    {
        // Arrange
        var note = new StickyNote();
        var viewModel = new StickyNoteViewModel(note, _ => { }, () => { });

        // Act & Assert
        Assert.NotNull(viewModel.ChangeColorCommand);
        Assert.True(viewModel.ChangeColorCommand.CanExecute("#FFB3E5"));
    }

    [Fact]
    public void StickyNoteViewModel_CloseCommand_ShouldCallOnClose()
    {
        // Arrange
        var note = new StickyNote();
        var closeCalled = false;
        StickyNoteViewModel? closedViewModel = null;
        Action<StickyNoteViewModel> onClose = vm =>
        {
            closeCalled = true;
            closedViewModel = vm;
        };
        var viewModel = new StickyNoteViewModel(note, onClose, () => { });

        // Act
        viewModel.CloseCommand.Execute(null);

        // Assert
        Assert.True(closeCalled);
        Assert.Equal(viewModel, closedViewModel);
    }

    [Fact]
    public void StickyNoteViewModel_ChangeColorCommand_ShouldUpdateColor()
    {
        // Arrange
        var note = new StickyNote();
        var viewModel = new StickyNoteViewModel(note, _ => { }, () => { });
        var newColor = "#C8E6C9";

        // Act
        viewModel.ChangeColorCommand.Execute(newColor);

        // Assert
        Assert.Equal(newColor, viewModel.Color);
    }
}

/// <summary>
/// Unit tests for MainViewModel.
/// Tests MVVM pattern, SOLID principles, and command behavior.
/// </summary>
public class MainViewModelTests
{
    [Fact]
    public void MainViewModel_ShouldInitializeWithEmptyCollections()
    {
        // Arrange & Act
        var viewModel = new MainViewModel();

        // Assert
        Assert.NotNull(viewModel.Items);
        Assert.NotNull(viewModel.StickyNotes);
        Assert.Empty(viewModel.Items);
        Assert.Empty(viewModel.StickyNotes);
    }

    [Fact]
    public void MainViewModel_ShouldHaveAllCommands()
    {
        // Arrange & Act
        var viewModel = new MainViewModel();

        // Assert - Verify all commands are initialized
        Assert.NotNull(viewModel.AddCommand);
        Assert.NotNull(viewModel.RemoveCommand);
        Assert.NotNull(viewModel.ClearCompletedCommand);
        Assert.NotNull(viewModel.StartEditCommand);
        Assert.NotNull(viewModel.SaveEditCommand);
        Assert.NotNull(viewModel.CancelEditCommand);
        Assert.NotNull(viewModel.CreateStickyNoteCommand);
    }

    [Fact]
    public void MainViewModel_AddCommand_ShouldBeDisabled_WhenTitleIsEmpty()
    {
        // Arrange
        var viewModel = new MainViewModel();

        // Act
        viewModel.NewTitle = "";

        // Assert
        Assert.False(viewModel.AddCommand.CanExecute(null));
    }

    [Fact]
    public void MainViewModel_AddCommand_ShouldBeEnabled_WhenTitleIsNotEmpty()
    {
        // Arrange
        var viewModel = new MainViewModel();

        // Act
        viewModel.NewTitle = "New Task";

        // Assert
        Assert.True(viewModel.AddCommand.CanExecute(null));
    }

    [Fact]
    public void MainViewModel_Summary_ShouldReturnCorrectMessage_WhenEmpty()
    {
        // Arrange
        var viewModel = new MainViewModel();

        // Act
        var summary = viewModel.Summary;

        // Assert
        Assert.Equal("No tasks yet", summary);
    }

    [Fact]
    public void MainViewModel_CreateStickyNoteCommand_ShouldBeExecutable()
    {
        // Arrange
        var viewModel = new MainViewModel();

        // Act & Assert
        Assert.True(viewModel.CreateStickyNoteCommand.CanExecute(null));
    }
}

/// <summary>
/// Tests for IDataService interface implementations.
/// Tests Interface Segregation Principle - focused interface.
/// Tests Dependency Inversion Principle - depend on abstractions.
/// </summary>
public class DataServiceInterfaceTests
{
    [Fact]
    public void TodoDataService_ShouldImplementIDataService()
    {
        // Arrange & Act
        IDataService<TodoItem> service = new TodoDataService();

        // Assert - Tests Liskov Substitution Principle
        Assert.NotNull(service);
        Assert.IsAssignableFrom<IDataService<TodoItem>>(service);
    }

    [Fact]
    public void StickyNoteDataService_ShouldImplementIDataService()
    {
        // Arrange & Act
        IDataService<StickyNote> service = new StickyNoteDataService();

        // Assert - Tests Liskov Substitution Principle
        Assert.NotNull(service);
        Assert.IsAssignableFrom<IDataService<StickyNote>>(service);
    }

    [Fact]
    public async Task TodoDataService_ShouldLoadEmptyList_WhenFileDoesNotExist()
    {
        // Arrange
        var service = new TodoDataService();

        // Act
        var result = await service.LoadAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<TodoItem>>(result);
    }

    [Fact]
    public async Task StickyNoteDataService_ShouldLoadEmptyList_WhenFileDoesNotExist()
    {
        // Arrange
        var service = new StickyNoteDataService();

        // Act
        var result = await service.LoadAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<StickyNote>>(result);
    }
}
