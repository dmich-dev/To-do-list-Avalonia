using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using To_do_list_Avalonia.Models;
using To_do_list_Avalonia.Infrastructure;
using To_do_list_Avalonia.Services;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace To_do_list_Avalonia.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private string _newTitle = string.Empty;
    private readonly TodoDataService _dataService;
    private readonly StickyNoteDataService _stickyNoteDataService;
    private readonly Dictionary<Guid, StickyNoteViewModel> _activeStickyNotes = new();

    public ObservableCollection<TodoItem> Items { get; } = new();
    public ObservableCollection<StickyNote> StickyNotes { get; } = new();

    public string NewTitle
    {
        get => _newTitle;
        set
        {
            if (_newTitle != value)
            {
                _newTitle = value;
                OnPropertyChanged();
                AddCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public string Summary 
    {
        get
        {
            var total = Items.Count;
            var completed = Items.Count(i => i.IsCompleted);
            var pending = total - completed;
            
            if (total == 0) return "No tasks yet";
            if (completed == total) return $"All done! ({total} tasks completed)";
            
            return $"{pending} pending • {completed} completed • {total} total";
        }
    }

    public RelayCommand AddCommand { get; }
    public RelayCommand RemoveCommand { get; }
    public RelayCommand ClearCompletedCommand { get; }
    public RelayCommand StartEditCommand { get; }
    public RelayCommand SaveEditCommand { get; }
    public RelayCommand CancelEditCommand { get; }
    public RelayCommand CreateStickyNoteCommand { get; }

    public MainViewModel()
    {
        _dataService = new TodoDataService();
        _stickyNoteDataService = new StickyNoteDataService();
        
        AddCommand = new RelayCommand(_ => Add(), _ => !string.IsNullOrWhiteSpace(NewTitle));
        RemoveCommand = new RelayCommand(item => Remove(item as TodoItem));
        ClearCompletedCommand = new RelayCommand(_ => ClearCompleted(), _ => Items.Any(i => i.IsCompleted));
        StartEditCommand = new RelayCommand(item => StartEdit(item as TodoItem));
        SaveEditCommand = new RelayCommand(item => SaveEdit(item as TodoItem));
        CancelEditCommand = new RelayCommand(item => CancelEdit(item as TodoItem));
        CreateStickyNoteCommand = new RelayCommand(_ => CreateStickyNote());

        Items.CollectionChanged += async (_, _) => 
        { 
            OnPropertyChanged(nameof(Summary)); 
            ClearCompletedCommand.RaiseCanExecuteChanged();
            await SaveDataAsync();
        };

        StickyNotes.CollectionChanged += async (_, _) =>
        {
            await SaveStickyNotesAsync();
        };

        // Load existing data when the view model is created
        _ = LoadDataAsync();
        _ = LoadStickyNotesAsync();
    }

    private async Task LoadDataAsync()
    {
        try
        {
            var savedTodos = await _dataService.LoadTodosAsync();
            
            // Clear existing items and add loaded ones
            Items.Clear();
            foreach (var item in savedTodos)
            {
                item.PropertyChanged += OnItemPropertyChanged;
                Items.Add(item);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading data: {ex.Message}");
        }
    }

    public async Task SaveDataAsync()
    {
        try
        {
            await _dataService.SaveTodosAsync(Items);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving data: {ex.Message}");
        }
    }

    private async Task LoadStickyNotesAsync()
    {
        try
        {
            var savedNotes = await _stickyNoteDataService.LoadNotesAsync();
            
            StickyNotes.Clear();
            foreach (var note in savedNotes)
            {
                note.PropertyChanged += OnStickyNotePropertyChanged;
                StickyNotes.Add(note);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading sticky notes: {ex.Message}");
        }
    }

    public async Task SaveStickyNotesAsync()
    {
        try
        {
            await _stickyNoteDataService.SaveNotesAsync(StickyNotes);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving sticky notes: {ex.Message}");
        }
    }

    private void CreateStickyNote()
    {
        var note = new StickyNote();
        note.PropertyChanged += OnStickyNotePropertyChanged;
        StickyNotes.Add(note);
        OnStickyNoteCreated?.Invoke(this, note);
    }

    private void Add()
    {
        if (string.IsNullOrWhiteSpace(NewTitle)) return;
        
        var newItem = new TodoItem { Title = NewTitle.Trim() };
        newItem.PropertyChanged += OnItemPropertyChanged;
        Items.Insert(0, newItem); // Add to top for better UX
        NewTitle = string.Empty;
        OnPropertyChanged(nameof(Summary));
        // Save is triggered by Items.CollectionChanged event
    }

    private void Remove(TodoItem? item)
    {
        if (item == null) return;
        item.PropertyChanged -= OnItemPropertyChanged;
        Items.Remove(item);
        OnPropertyChanged(nameof(Summary));
        // Save is triggered by Items.CollectionChanged event
    }

    private void ClearCompleted()
    {
        var completed = Items.Where(i => i.IsCompleted).ToList();
        foreach (var item in completed)
        {
            item.PropertyChanged -= OnItemPropertyChanged;
            Items.Remove(item);
        }
        OnPropertyChanged(nameof(Summary));
        // Save is triggered by Items.CollectionChanged event
    }

    private void StartEdit(TodoItem? item)
    {
        if (item == null) return;
        
        // Cancel any other items being edited
        foreach (var otherItem in Items.Where(i => i.IsEditing))
        {
            CancelEdit(otherItem);
        }
        
        // Start editing this item
        item.EditTitle = item.Title; // Store current title in edit field
        item.IsEditing = true;
    }

    private async void SaveEdit(TodoItem? item)
    {
        if (item == null || !item.IsEditing) return;
        
        // Validate the edited title
        if (!string.IsNullOrWhiteSpace(item.EditTitle))
        {
            item.Title = item.EditTitle.Trim();
        }
        
        item.IsEditing = false;
        item.EditTitle = string.Empty;
        
        // Save after editing
        await SaveDataAsync();
    }

    private void CancelEdit(TodoItem? item)
    {
        if (item == null || !item.IsEditing) return;
        
        item.IsEditing = false;
        item.EditTitle = string.Empty;
    }

    private async void OnItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TodoItem.IsCompleted))
        {
            OnPropertyChanged(nameof(Summary));
            ClearCompletedCommand.RaiseCanExecuteChanged();
            
            // Save when completion status changes
            await SaveDataAsync();
        }
    }

    public void CloseStickyNote(StickyNoteViewModel viewModel)
    {
        var note = StickyNotes.FirstOrDefault(n => n.Id == viewModel.Id);
        if (note != null)
        {
            note.PropertyChanged -= OnStickyNotePropertyChanged;
            StickyNotes.Remove(note);
        }
        
        _activeStickyNotes.Remove(viewModel.Id);
    }

    public void RegisterStickyNoteViewModel(StickyNoteViewModel viewModel)
    {
        _activeStickyNotes[viewModel.Id] = viewModel;
    }

    private void OnStickyNotePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // Auto-save when sticky note properties change
        _ = SaveStickyNotesAsync();
    }

    public event EventHandler<StickyNote>? OnStickyNoteCreated;

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null) 
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
