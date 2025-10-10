using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace To_do_list_Avalonia.Models;

public class StickyNote : INotifyPropertyChanged
{
    private string _content = string.Empty;
    private string _color = "#FFF9C4"; // Default yellow color
    private double _positionX;
    private double _positionY;
    private double _width = 300;
    private double _height = 300;

    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string Content
    {
        get => _content;
        set
        {
            if (_content != value)
            {
                _content = value;
                OnPropertyChanged();
            }
        }
    }

    public string Color
    {
        get => _color;
        set
        {
            if (_color != value)
            {
                _color = value;
                OnPropertyChanged();
            }
        }
    }

    public double PositionX
    {
        get => _positionX;
        set
        {
            if (_positionX != value)
            {
                _positionX = value;
                OnPropertyChanged();
            }
        }
    }

    public double PositionY
    {
        get => _positionY;
        set
        {
            if (_positionY != value)
            {
                _positionY = value;
                OnPropertyChanged();
            }
        }
    }

    public double Width
    {
        get => _width;
        set
        {
            if (_width != value)
            {
                _width = value;
                OnPropertyChanged();
            }
        }
    }

    public double Height
    {
        get => _height;
        set
        {
            if (_height != value)
            {
                _height = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
