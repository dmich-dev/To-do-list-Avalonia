using System;
using To_do_list_Avalonia.Models;

namespace To_do_list_Avalonia.ViewModels;

/// <summary>
/// Factory for creating ViewModel instances.
/// Implements the Factory pattern (Creational Design Pattern).
/// This centralizes ViewModel creation logic and makes it easier to:
/// - Add dependency injection
/// - Mock ViewModels for testing
/// - Change ViewModel construction logic in one place
/// </summary>
public class ViewModelFactory : IViewModelFactory
{
    /// <summary>
    /// Creates a StickyNoteViewModel for a given StickyNote model.
    /// </summary>
    public StickyNoteViewModel CreateStickyNoteViewModel(
        StickyNote note, 
        Action<StickyNoteViewModel> onClose, 
        Action onSaveRequested)
    {
        return new StickyNoteViewModel(note, onClose, onSaveRequested);
    }
}
