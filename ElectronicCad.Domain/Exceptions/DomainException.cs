namespace ElectronicCad.Domain.Exceptions;

/// <summary>
/// Describe domain exception
/// </summary>
public class DomainException : Exception
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public DomainException(string message) : base(message)
    {
    }
}