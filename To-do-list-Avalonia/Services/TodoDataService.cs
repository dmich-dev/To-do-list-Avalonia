using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using To_do_list_Avalonia.Models;

namespace To_do_list_Avalonia.Services;

/// <summary>
/// Service for persisting TodoItem data to JSON files.
/// Implements Single Responsibility Principle (SOLID) - only handles todo data persistence.
/// </summary>
public class TodoDataService : IDataService<TodoItem>
{
    private readonly string _dataFilePath;

    public TodoDataService()
    {
        // Store the data file in the user's AppData folder
        var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var appFolder = Path.Combine(appDataFolder, "TodoListAvalonia");
        Directory.CreateDirectory(appFolder); // Create the folder if it doesn't exist
        _dataFilePath = Path.Combine(appFolder, "todos.json");
    }

    /// <summary>
    /// Loads todo items from persistent storage.
    /// </summary>
    public async Task<List<TodoItem>> LoadAsync()
    {
        try
        {
            if (!File.Exists(_dataFilePath))
            {
                return new List<TodoItem>();
            }

            var json = await File.ReadAllTextAsync(_dataFilePath);
            var todos = JsonSerializer.Deserialize<List<TodoItem>>(json);
            return todos ?? new List<TodoItem>();
        }
        catch (Exception ex)
        {
            // Log the error if needed, for now just return empty list
            Console.WriteLine($"Error loading todos: {ex.Message}");
            return new List<TodoItem>();
        }
    }

    /// <summary>
    /// Saves todo items to persistent storage.
    /// </summary>
    public async Task SaveAsync(IEnumerable<TodoItem> todos)
    {
        try
        {
            var json = JsonSerializer.Serialize(todos, new JsonSerializerOptions 
            { 
                WriteIndented = true 
            });
            await File.WriteAllTextAsync(_dataFilePath, json);
        }
        catch (Exception ex)
        {
            // Log the error if needed
            Console.WriteLine($"Error saving todos: {ex.Message}");
        }
    }

    // Legacy method names for backward compatibility
    public Task<List<TodoItem>> LoadTodosAsync() => LoadAsync();
    public Task SaveTodosAsync(IEnumerable<TodoItem> todos) => SaveAsync(todos);
}