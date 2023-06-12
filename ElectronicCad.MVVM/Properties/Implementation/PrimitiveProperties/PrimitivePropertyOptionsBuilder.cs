namespace ElectronicCad.MVVM.Properties.Implementation.PrimitiveProperties;

/// <summary>
/// Builder for primitive property options.
/// </summary>
public class PrimitivePropertyOptionsBuilder
{
    private readonly PrimitivePropertyOptions configuration;

    /// <summary>
    /// Constructor.
    /// </summary>
    public PrimitivePropertyOptionsBuilder()
    {
        configuration = new();
    }

    /// <summary>
    /// Creates primitive property options.
    /// </summary>
    /// <returns>Primitive proeprty options.</returns>
    public PrimitivePropertyOptions Build()
    { 
        return configuration;
    }

    /// <summary>
    /// Specify primitive property name.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <returns>Builder</returns>
    public PrimitivePropertyOptionsBuilder HasName(string name)
    {
        configuration.Name = name;
        return this;
    }
}