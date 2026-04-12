
namespace CodeGator;

// I found this code here: https://www.milanjovanovic.tech/blog/functional-error-handling-in-dotnet-with-the-result-pattern

/// <summary>
/// This record represents an error.
/// </summary>
/// <param name="Code">The machine readable code for the error.</param>
/// <param name="Description">The human readable description for the error.</param>
public sealed record Error(
    string Code,
    string Description
    )
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields
    
    /// <summary>
    /// This field represents no error.
    /// </summary>
    public static readonly Error None = new("No error.", string.Empty);

    #endregion
}
