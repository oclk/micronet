namespace Shared.Models;

/// <summary>
/// Represents a generic result container with success status, data, message, and code.
/// </summary>
/// <typeparam name="T">The type of data associated with the result.</typeparam>
public class Result<T>
{
    /// <summary>
    /// Gets or sets a value indicating whether the operation succeeded.
    /// </summary>
    public bool Succeeded { get; init; }

    /// <summary>
    /// Gets or sets the data associated with the result.
    /// </summary>
    public T Data { get; set; }

    /// <summary>
    /// Gets or sets the message providing additional information about the result.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the code associated with the result.
    /// </summary>
    public string Code { get; set; }
}
