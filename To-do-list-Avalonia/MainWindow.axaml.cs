using Avalonia.Controls;
using Avalonia.Input;
using Avalonia;
using System;
using To_do_list_Avalonia.ViewModels;
using To_do_list_Avalonia.Views;
using To_do_list_Avalonia.Models;
using System.Collections.Generic;
using System.Linq;

namespace To_do_list_Avalonia
{
    public partial class MainWindow : Window
    {
        private readonly Dictionary<System.Guid, StickyNoteWindow> _openStickyNotes = new();
        private TodoItem? _draggedItem;
        private bool _isDragging;
        private Point _dragStartPoint;
        private Border? _lastDropTarget;

        public MainWindow()
        {
            InitializeComponent();
            var viewModel = new MainViewModel();
            DataContext = viewModel;

            // Subscribe to sticky note creation event
            viewModel.OnStickyNoteCreated += OnStickyNoteCreated;

            // Restore existing sticky notes
            foreach (var note in viewModel.StickyNotes)
            {
                OpenStickyNote(viewModel, note);
            }
        }

        private void OnStickyNoteCreated(object? sender, StickyNote note)
        {
            if (DataContext is MainViewModel viewModel)
            {
                OpenStickyNote(viewModel, note);
            }
        }

        private void OpenStickyNote(MainViewModel mainViewModel, StickyNote note)
        {
            // Don't open if already open
            if (_openStickyNotes.ContainsKey(note.Id))
            {
                _openStickyNotes[note.Id].Activate();
                return;
            }

            var stickyViewModel = new StickyNoteViewModel(note, 
                vm =>
                {
                    mainViewModel.CloseStickyNote(vm);
                    if (_openStickyNotes.ContainsKey(vm.Id))
                    {
                        _openStickyNotes[vm.Id].Close();
                        _openStickyNotes.Remove(vm.Id);
                    }
                },
                async () => await mainViewModel.SaveStickyNotesAsync());

            mainViewModel.RegisterStickyNoteViewModel(stickyViewModel);

            var stickyWindow = new StickyNoteWindow(stickyViewModel);
            
            stickyWindow.Closed += (s, e) =>
            {
                _openStickyNotes.Remove(note.Id);
            };

            _openStickyNotes[note.Id] = stickyWindow;
            stickyWindow.Show();
        }

        // Drag and Drop Implementation
        private void Task_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (sender is Border border && border.DataContext is TodoItem item)
            {
                // Only start drag from the drag handle area (left side)
                var position = e.GetPosition(border);
                if (position.X < 50) // Within drag handle area
                {
                    _draggedItem = item;
                    _dragStartPoint = e.GetPosition(this);
                    _isDragging = false;
                }
            }
        }

        private async void Task_PointerMoved(object? sender, PointerEventArgs e)
        {
            if (_draggedItem != null && e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                var currentPoint = e.GetPosition(this);
                var diff = currentPoint - _dragStartPoint;

                // Start drag operation if moved more than 5 pixels
                if (!_isDragging && (Math.Abs(diff.X) > 5 || Math.Abs(diff.Y) > 5))
                {
                    _isDragging = true;
                    
                    if (sender is Border border)
                    {
                        border.Classes.Add("dragging");
                        
                        var dragData = new DataObject();
                        dragData.Set("TodoItem", _draggedItem);
                        
                        await DragDrop.DoDragDrop(e, dragData, DragDropEffects.Move);
                        
                        border.Classes.Remove("dragging");
                    }
                }
            }
        }

        private void Task_PointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            if (sender is Border border)
            {
                border.Classes.Remove("dragging");
            }
            
            _draggedItem = null;
            _isDragging = false;
        }

        private void Task_DragOver(object? sender, DragEventArgs e)
        {
            if (sender is Border border)
            {
                // Highlight drop target
                if (_lastDropTarget != border)
                {
                    _lastDropTarget?.Classes.Remove("drop-target");
                    border.Classes.Add("drop-target");
                    _lastDropTarget = border;
                }

                e.DragEffects = DragDropEffects.Move;
            }
        }

        private void Task_DragLeave(object? sender, DragEventArgs e)
        {
            if (sender is Border border)
            {
                border.Classes.Remove("drop-target");
                if (_lastDropTarget == border)
                {
                    _lastDropTarget = null;
                }
            }
        }

        private void Task_Drop(object? sender, DragEventArgs e)
        {
            if (sender is Border border && border.DataContext is TodoItem targetItem)
            {
                border.Classes.Remove("drop-target");
                _lastDropTarget = null;

                if (e.Data.Get("TodoItem") is TodoItem draggedItem && 
                    DataContext is MainViewModel viewModel)
                {
                    if (draggedItem != targetItem)
                    {
                        // Get current indices
                        var oldIndex = viewModel.Items.IndexOf(draggedItem);
                        var newIndex = viewModel.Items.IndexOf(targetItem);

                        if (oldIndex != -1 && newIndex != -1)
                        {
                            // Remove and reinsert at new position
                            viewModel.Items.Move(oldIndex, newIndex);
                        }
                    }
                }
            }

            _draggedItem = null;
            _isDragging = false;
        }
    }
}