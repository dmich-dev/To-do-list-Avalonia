using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using To_do_list_Avalonia.Models;
using To_do_list_Avalonia.Infrastructure;

namespace To_do_list_Avalonia.ViewModels;

public class StickyNoteViewModel : INotifyPropertyChanged
{
    private readonly StickyNote _note;
    private readonly Action<StickyNoteViewModel> _onClose;
    private readonly Action _onSaveRequested;
    private CancellationTokenSource? _saveCancellation;
    private string _saveStatus = "";

    public StickyNoteViewModel(StickyNote note, Action<StickyNoteViewModel> onClose, Action onSaveRequested)
    {
        _note = note;
        _onClose = onClose;
        _onSaveRequested = onSaveRequested;
        
        CloseCommand = new RelayCommand(_ => Close());
        ChangeColorCommand = new RelayCommand(color => ChangeColor(color as string));
    }

    public Guid Id => _note.Id;

    public string Content
    {
        get => _note.Content;
        set
        {
            if (_note.Content != value)
            {
                _note.Content = value;
                OnPropertyChanged();
                DebouncedSave(); // Trigger debounced save on content change
            }
        }
    }

    public string Color
    {
        get => _note.Color;
        set
        {
            if (_note.Color != value)
            {
                _note.Color = value;
                OnPropertyChanged();
                DebouncedSave(); // Trigger debounced save on color change
            }
        }
    }

    public double PositionX
    {
        get => _note.PositionX;
        set
        {
            if (_note.PositionX != value)
            {
                _note.PositionX = value;
                OnPropertyChanged();
                DebouncedSave();
            }
        }
    }

    public double PositionY
    {
        get => _note.PositionY;
        set
        {
            if (_note.PositionY != value)
            {
                _note.PositionY = value;
                OnPropertyChanged();
                DebouncedSave();
            }
        }
    }

    public double Width
    {
        get => _note.Width;
        set
        {
            if (_note.Width != value)
            {
                _note.Width = value;
                OnPropertyChanged();
                DebouncedSave();
            }
        }
    }

    public double Height
    {
        get => _note.Height;
        set
        {
            if (_note.Height != value)
            {
                _note.Height = value;
                OnPropertyChanged();
                DebouncedSave();
            }
        }
    }

    public string SaveStatus
    {
        get => _saveStatus;
        set
        {
            if (_saveStatus != value)
            {
                _saveStatus = value;
                OnPropertyChanged();
            }
        }
    }

    public RelayCommand CloseCommand { get; }
    public RelayCommand ChangeColorCommand { get; }

    public StickyNote GetModel() => _note;

    private void Close()
    {
        // Cancel any pending saves
        _saveCancellation?.Cancel();
        
        // Do an immediate save on close
        _onSaveRequested?.Invoke();
        
        _onClose?.Invoke(this);
    }

    private void ChangeColor(string? color)
    {
        if (!string.IsNullOrEmpty(color))
        {
            Color = color;
        }
    }

    private async void DebouncedSave()
    {
        // Cancel any existing save operation
        _saveCancellation?.Cancel();
        _saveCancellation = new CancellationTokenSource();

        SaveStatus = "Saving...";

        try
        {
            // Wait 1 second after the last change before saving
            await Task.Delay(1000, _saveCancellation.Token);
            
            // Trigger the save
            _onSaveRequested?.Invoke();
            
            // Update status
            SaveStatus = "Saved ✓";
            
            // Clear the status after 2 seconds
            await Task.Delay(2000, _saveCancellation.Token);
            SaveStatus = "";
        }
        catch (TaskCanceledException)
        {
            // Save was cancelled because another change occurred
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
