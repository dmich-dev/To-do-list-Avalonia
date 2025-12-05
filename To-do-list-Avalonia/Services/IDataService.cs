using System.Collections.Generic;
using System.Threading.Tasks;

namespace To_do_list_Avalonia.Services;

/// <summary>
/// Generic interface for data persistence services.
/// Follows the Dependency Inversion Principle (SOLID).
/// </summary>
/// <typeparam name="T">The type of entity to persist</typeparam>
public interface IDataService<T>
{
    /// <summary>
    /// Loads all entities from persistent storage.
    /// </summary>
    /// <returns>A list of entities</returns>
    Task<List<T>> LoadAsync();

    /// <summary>
    /// Saves entities to persistent storage.
    /// </summary>
    /// <param name="items">The entities to save</param>
    Task SaveAsync(IEnumerable<T> items);
}
