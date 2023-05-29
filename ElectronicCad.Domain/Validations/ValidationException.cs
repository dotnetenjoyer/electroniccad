namespace ElectronicCad.Domain.Validations;

/// <summary>
/// An exception that occurs during the validation process.
/// </summary>
public class ValidationException : Exception
{
    /// <summary>
    /// Collection of error messages.
    /// </summary>
    public IDictionary<string, string> ErrorMessages { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="result"></param>
    public ValidationException(ValidationResult result)
    {
        ErrorMessages = new Dictionary<string, string>(result.ErrorMessages);
    }
}