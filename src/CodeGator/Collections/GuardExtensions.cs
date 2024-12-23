
#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.Collections;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// This class contains extension methods related to the <see cref="Guard"/>
/// type.
/// </summary>
public static partial class GuardExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/> 
    /// argument contains an empty collection.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a value that is
    /// less than zero.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfEmpty{T}(Guard, IEnumerable{T}, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = new string[0];
    /// 
    ///         // throws an exception, since the argument is invalid.
    ///         Guard.Instance().ThrowIfEmpty(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfEmpty<T>(
        this Guard guard,
        IEnumerable<T> argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (!argValue.Any())
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: "The argument must contain at least one element!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }

        return guard;
    }

    #endregion
}
