﻿namespace ElectronicCad.MVVM.Properties.Abstractions;

/// <summary>
/// Abstraction of custom section factory.
/// </summary>
public interface ICustomSectionFactory
{
    /// <summary>
    /// Indicates whether custom section can be created or not
    /// </summary>
    /// <param name="proxy">Proxy elemnent.</param>
    /// <returns>True if can create custom section.</returns>
    bool CanCreate(IProxy proxy);

    /// <summary>
    /// Creates custom section.
    /// </summary>
    /// <param name="proxy">Proxy element.</param>
    /// <returns>Custom section.</returns>
    ICustomSection Create(IProxy proxy);
}