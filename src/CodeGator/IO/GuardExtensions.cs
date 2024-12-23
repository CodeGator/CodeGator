
#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.IO;
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
    /// argument contains a value that is not a readable stream.
    /// </summary>
    /// <param name="guard">The guard instance to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a stream that 
    /// is not readable.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfNotReadable(Guard, Stream, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = new FileStream("test.doc", FileMode.Open, FileAccess.Write);
    /// 
    ///         // throws an exception, since the stream is not readable.
    ///         Guard.Instance().ThrowIfNotReadable(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfNotReadable(
        this Guard guard,
        Stream argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (!argValue.CanRead)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: "The argument is not a readable stream!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }

        return guard;
    }

    // ******************************************************************

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/>
    /// argument contains an invalid file path.
    /// </summary>
    /// <param name="guard">The guard instance to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains an invalid file
    /// path.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfInvalidFilePath(Guard, string, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = "*";
    /// 
    ///         // throws an exception, since the file path is invalid.
    ///         Guard.Instance().ThrowIfInvalidFilePath(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfInvalidFilePath(
        this Guard guard,
        string argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (Path.GetFileName(argValue).IndexOfAny(Path.GetInvalidFileNameChars(), 0) != -1 ||
            Path.GetDirectoryName(argValue)?.IndexOfAny(Path.GetInvalidPathChars(), 0) != -1)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: "The argument contains an invalid file path!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }

        return guard;
    }

    // ******************************************************************

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/> 
    /// argument contains an invalid folder path.
    /// </summary>
    /// <param name="guard">The guard instance to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains an invalid folder
    /// path.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfInvalidFolderPath(Guard, string, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = "*";
    /// 
    ///         // throws an exception, since the folder path is invalid.
    ///         Guard.Instance().ThrowIfInvalidFolderPath(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfInvalidFolderPath(
        this Guard guard,
        string argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue.IndexOfAny(Path.GetInvalidPathChars(), 0) != -1)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: "The argument contains an invalid folder path!"                
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }

        return guard;
    }

    // ******************************************************************

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/>
    /// argument contains an invalid file extension.
    /// </summary>
    /// <param name="guard">The guard instance to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains an invalid file
    /// extension.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfInvalidFileExtension(Guard, string, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = "*";
    /// 
    ///         // throws an exception, since the file extension is invalid.
    ///         Guard.Instance().ThrowIfInvalidFolderPath(arg, nameof(arg));
    ///    }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfInvalidFileExtension(
        this Guard guard,
        string argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (!argValue.Contains('.') ||
            argValue.Trim().Length <= 1 ||
            Path.GetExtension(argValue).IndexOfAny(Path.GetInvalidFileNameChars(), 0) != -1)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: "The argument contains an invalid file extension!"                
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }

        return guard;
    }

    // *******************************************************************

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/>
    /// argument contains a value that is not a writable stream.
    /// </summary>
    /// <param name="guard">The guard instance to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a stream that 
    /// is not writable.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfNotWritable(Guard, Stream, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = new FileStream("test.doc", FileMode.Open, FileAccess.Read);
    /// 
    ///         // throws an exception, since the stream is not writeable.
    ///         Guard.Instance().ThrowIfNotWritable(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfNotWritable(
        this Guard guard,
        Stream argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (!argValue.CanWrite)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: "The argument is not a writable stream!"
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
