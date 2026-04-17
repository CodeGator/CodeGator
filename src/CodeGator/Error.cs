
namespace CodeGator;

// I found this code here: https://www.milanjovanovic.tech/blog/functional-error-handling-in-dotnet-with-the-result-pattern

/// <summary>
/// This class represents a structured error with code and description.
/// </summary>
/// <param name="Code">This property holds the machine-readable error code.</param>
/// <param name="Description">This property holds the human-readable error text.</param>
public sealed record Error(
    string Code,
    string Description
    )
{
    /// <summary>
    /// This field holds the sentinel value that indicates no error occurred.
    /// </summary>
    public static readonly Error None = new("No error.", string.Empty);
}
