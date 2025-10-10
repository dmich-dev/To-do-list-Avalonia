using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using To_do_list_Avalonia.Models;

namespace To_do_list_Avalonia.Services;

public class StickyNoteDataService
{
    private readonly string _dataFilePath;

    public StickyNoteDataService()
    {
        var appDataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "TodoListAvalonia"
        );
        
        Directory.CreateDirectory(appDataFolder);
        _dataFilePath = Path.Combine(appDataFolder, "stickynotes.json");
    }

    public async Task<List<StickyNote>> LoadNotesAsync()
    {
        try
        {
            if (!File.Exists(_dataFilePath))
            {
                return new List<StickyNote>();
            }

            var json = await File.ReadAllTextAsync(_dataFilePath);
            var notes = JsonSerializer.Deserialize<List<StickyNote>>(json);
            return notes ?? new List<StickyNote>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading sticky notes: {ex.Message}");
            return new List<StickyNote>();
        }
    }

    public async Task SaveNotesAsync(IEnumerable<StickyNote> notes)
    {
        try
        {
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true 
            };
            
            var json = JsonSerializer.Serialize(notes.ToList(), options);
            await File.WriteAllTextAsync(_dataFilePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving sticky notes: {ex.Message}");
        }
    }
}
