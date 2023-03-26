namespace ElectronicCad.Domain.Storage;

/// <summary>
/// Entity serealizer.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
public interface IEntitySerializer<TEntity>
{
    /// <summary>
    /// Serialize the entity to the storage dictionary.
    /// </summary>
    /// <param name="dictionary">Storage dictionary.</param>
    /// <param name="entity">Entity to serialization.</param>
    void Serialize(StorageDictionary dictionary, TEntity entity);

    /// <summary>
    /// Deserialize from the storage dictionary.
    /// </summary>
    /// <param name="dictionary">Storage dictionary.</param>
    TEntity Deserialize(StorageDictionary dictionary);
}