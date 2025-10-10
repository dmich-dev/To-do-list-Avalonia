using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using To_do_list_Avalonia.ViewModels;

namespace To_do_list_Avalonia.Views;

public partial class StickyNoteWindow : Window
{
    private bool _isDragging;
    private Point _dragStartPoint;

    public StickyNoteWindow()
    {
        InitializeComponent();
    }

    public StickyNoteWindow(StickyNoteViewModel viewModel) : this()
    {
        DataContext = viewModel;
        
        // Restore position if saved
        if (viewModel.PositionX > 0 || viewModel.PositionY > 0)
        {
            Position = new PixelPoint((int)viewModel.PositionX, (int)viewModel.PositionY);
        }

        // Restore size if saved
        if (viewModel.Width > 0)
        {
            Width = viewModel.Width;
        }
        if (viewModel.Height > 0)
        {
            Height = viewModel.Height;
        }

        // Save position when window is moved
        PositionChanged += (s, e) =>
        {
            if (DataContext is StickyNoteViewModel vm)
            {
                vm.PositionX = Position.X;
                vm.PositionY = Position.Y;
            }
        };

        // Save size when window is resized
        PropertyChanged += (s, e) =>
        {
            if (e.Property.Name == nameof(Width) || e.Property.Name == nameof(Height))
            {
                if (DataContext is StickyNoteViewModel vm)
                {
                    vm.Width = Width;
                    vm.Height = Height;
                }
            }
        };
    }

    private void HeaderBar_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            _isDragging = true;
            _dragStartPoint = e.GetPosition(this);
            e.Pointer.Capture((IInputElement?)sender);
        }
    }

    private void HeaderBar_PointerMoved(object? sender, PointerEventArgs e)
    {
        if (_isDragging && e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            var currentPosition = e.GetPosition(this);
            var offset = currentPosition - _dragStartPoint;
            
            Position = new PixelPoint(
                Position.X + (int)offset.X,
                Position.Y + (int)offset.Y
            );
        }
    }

    private void HeaderBar_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (_isDragging)
        {
            _isDragging = false;
            e.Pointer.Capture(null);
        }
    }

    private void Content_TextChanged(object? sender, TextChangedEventArgs e)
    {
        // Auto-save is handled by the data binding and the main view model's save logic
    }
}
