namespace ElectronicCad.Domain.Storage;

/// <summary>
/// Stores a objects by a reference.
/// </summary>
public interface IStorage
{
    /// <summary>
    /// Saves dictionary with reference 
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="ref"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PutDictionaryAsync(StorageDictionary dictionary, string @ref, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ref"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<StorageDictionary> GetDictionaryAsync(string @ref, CancellationToken cancellationToken);
}