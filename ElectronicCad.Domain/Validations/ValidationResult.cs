namespace ElectronicCad.Domain.Validations;

/// <summary>
/// Describes a result of the object validation.
/// </summary>
public class ValidationResult
{
    /// <summary>
    /// Enum of validation results.
    /// </summary>
    public enum Result
    {
        Success,
        Error
    }

    /// <summary>
    /// Successful validation result.
    /// </summary>
    public static ValidationResult Successed => new ValidationResult();

    /// <summary>
    /// Status of validation result.
    /// </summary>
    public Result Status { get; private set; } = Result.Success;

    /// <summary>
    /// Indicates if validation successed.
    /// </summary>
    public bool IsSuccessed => Status == Result.Success;

    /// <summary>
    /// Collection of error messages.
    /// </summary>
    public IReadOnlyDictionary<string, string> ErrorMessages => errorMessages;

    private Dictionary<string, string> errorMessages = new();

    /// <summary>
    /// Adds new error message.
    /// </summary>
    /// <param name="propertyname"></param>
    /// <param name="message"></param>
    public void AddError(string propertyname, string message)
    {
        Status = Result.Error;
        errorMessages[propertyname] = message;
    }
}
