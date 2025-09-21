using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using To_do_list_Avalonia.Models;
using To_do_list_Avalonia.Infrastructure;
using System.Linq;
using System;

namespace To_do_list_Avalonia.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private string _newTitle = string.Empty;

    public ObservableCollection<TodoItem> Items { get; } = new();

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
            if (completed == total) return $"All done! ?? ({total} tasks completed)";
            
            return $"{pending} pending • {completed} completed • {total} total";
        }
    }

    public RelayCommand AddCommand { get; }
    public RelayCommand RemoveCommand { get; }
    public RelayCommand ClearCompletedCommand { get; }
    public RelayCommand StartEditCommand { get; }
    public RelayCommand SaveEditCommand { get; }
    public RelayCommand CancelEditCommand { get; }

    public MainViewModel()
    {
        AddCommand = new RelayCommand(_ => Add(), _ => !string.IsNullOrWhiteSpace(NewTitle));
        RemoveCommand = new RelayCommand(item => Remove(item as TodoItem));
        ClearCompletedCommand = new RelayCommand(_ => ClearCompleted(), _ => Items.Any(i => i.IsCompleted));
        StartEditCommand = new RelayCommand(item => StartEdit(item as TodoItem));
        SaveEditCommand = new RelayCommand(item => SaveEdit(item as TodoItem));
        CancelEditCommand = new RelayCommand(item => CancelEdit(item as TodoItem));

        Items.CollectionChanged += (_, _) => 
        { 
            OnPropertyChanged(nameof(Summary)); 
            ClearCompletedCommand.RaiseCanExecuteChanged(); 
        };

        // Add some sample data for demonstration
        LoadSampleData();
    }

    private void LoadSampleData()
    {
        var sampleItems = new[]
        {
            new TodoItem { Title = "Review quarterly goals", CreatedAt = DateTime.Now.AddHours(-2) },
            new TodoItem { Title = "Prepare presentation slides", CreatedAt = DateTime.Now.AddHours(-1) },
            new TodoItem { Title = "Schedule team meeting", IsCompleted = true, CreatedAt = DateTime.Now.AddMinutes(-30) },
            new TodoItem { Title = "Update project documentation", CreatedAt = DateTime.Now.AddMinutes(-15) }
        };

        foreach (var item in sampleItems)
        {
            item.PropertyChanged += OnItemPropertyChanged;
            Items.Add(item);
        }
    }

    private void Add()
    {
        if (string.IsNullOrWhiteSpace(NewTitle)) return;
        
        var newItem = new TodoItem { Title = NewTitle.Trim() };
        newItem.PropertyChanged += OnItemPropertyChanged;
        Items.Insert(0, newItem); // Add to top for better UX
        NewTitle = string.Empty;
        OnPropertyChanged(nameof(Summary));
    }

    private void Remove(TodoItem? item)
    {
        if (item == null) return;
        item.PropertyChanged -= OnItemPropertyChanged;
        Items.Remove(item);
        OnPropertyChanged(nameof(Summary));
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

    private void SaveEdit(TodoItem? item)
    {
        if (item == null || !item.IsEditing) return;
        
        // Validate the edited title
        if (!string.IsNullOrWhiteSpace(item.EditTitle))
        {
            item.Title = item.EditTitle.Trim();
        }
        
        item.IsEditing = false;
        item.EditTitle = string.Empty;
    }

    private void CancelEdit(TodoItem? item)
    {
        if (item == null || !item.IsEditing) return;
        
        item.IsEditing = false;
        item.EditTitle = string.Empty;
    }

    private void OnItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TodoItem.IsCompleted))
        {
            OnPropertyChanged(nameof(Summary));
            ClearCompletedCommand.RaiseCanExecuteChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null) 
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
