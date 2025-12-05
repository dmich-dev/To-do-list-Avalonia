using System;
using To_do_list_Avalonia.Models;

namespace To_do_list_Avalonia.ViewModels;

/// <summary>
/// Factory interface for creating ViewModels.
/// Implements the Factory pattern (Creational Design Pattern).
/// Follows Open/Closed Principle (SOLID) - open for extension, closed for modification.
/// </summary>
public interface IViewModelFactory
{
    /// <summary>
    /// Creates a StickyNoteViewModel for a given StickyNote model.
    /// </summary>
    /// <param name="note">The sticky note model</param>
    /// <param name="onClose">Callback when the note is closed</param>
    /// <param name="onSaveRequested">Callback when save is requested</param>
    /// <returns>A new StickyNoteViewModel instance</returns>
    StickyNoteViewModel CreateStickyNoteViewModel(
        StickyNote note, 
        Action<StickyNoteViewModel> onClose, 
        Action onSaveRequested);
}
