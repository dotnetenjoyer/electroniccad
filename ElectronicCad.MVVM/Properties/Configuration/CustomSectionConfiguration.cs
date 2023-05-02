using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.Properties.Configuration;

/// <summary>
/// Configuration of custom section.
/// </summary>
public abstract class CustomSectionConfiguration
{
    /// <summary>
    /// Specifies the type of custom section.
    /// </summary>
    public abstract Type CustomSectionType { get; }
}

/// <summary>
/// Generic configuration of custom section.
/// </summary>
/// <typeparam name="T">Specified the type of custom section.</typeparam>
public class CustomSectionConfiguration<T> : CustomSectionConfiguration where T : ICustomSection
{
    /// <inheritdoc />
    public override Type CustomSectionType => typeof(T);
}
