
namespace CodeGator;

// I copied this code from our AI overlords and modified it for my needs.

/// <summary>
/// This class represents the results of an operation.
/// </summary>
public class Result
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property indicates whether the result is a success.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// This property indicates whether the result is a failuer.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// This property contains the error for the result.
    /// </summary>
    public Error Error { get; }

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="Result"/>
    /// class.
    /// </summary>
    protected Result()
    {
        IsSuccess = true;
        Error = Error.None;
    }

    // *******************************************************************

    /// <summary>
    /// This constructor creates a new instance of the <see cref="Result"/>
    /// class.
    /// </summary>
    /// <param name="error">The error to use for the result.</param>
    protected Result(
        [NotNull] Error error
        )
    {
        IsSuccess = false;
        Error = error;
    }

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method creates a success result with no data.
    /// </summary>
    /// <returns>A <see cref="Result"/> instance.</returns>
    public static Result Success() => new();

    // *******************************************************************

    /// <summary>
    /// This method creates a failure result with no data.
    /// </summary>
    /// <param name="error">The error to use with the result.</param>
    /// <returns>A <see cref="Result"/> instance.</returns>
    public static Result Failure(
        [NotNull] Error error
        ) => new(error);
    
    #endregion
}



/// <summary>
/// This class represents the results of an operation.
/// </summary>
/// <typeparam name="T">The type of data for the result</typeparam>
public class Result<T> : Result
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the data associated with the result.
    /// </summary>
    public T Data { get; }

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="Result"/>
    /// class.
    /// </summary>
    /// <param name="data">The data to use with this result.</param>
    protected Result(
        T data
        ) : base()
    {
        Data = data;
    }

    // *******************************************************************

    /// <summary>
    /// This constructor creates a new instance of the <see cref="Result"/>
    /// class.
    /// </summary>
    /// <param name="error">The error to use with the result.</param>
    protected Result(
        [NotNull] Error error
        ) : base(error)
    {
        Data = default!;
    }

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method creates a success result with data.
    /// </summary>
    /// <param name="data">The data to use with this result.</param>
    /// <returns>A <see cref="Result"/> instance.</returns>
    public static Result<T> Success(
        T data
        ) => new(data);

    // *******************************************************************

    /// <summary>
    /// This method creates a success result with default data.
    /// </summary>
    /// <param name="error">The error to use with this result.</param>
    /// <returns>A <see cref="Result"/> instance.</returns>
    public new static Result<T> Failure(
        [NotNull] Error error
        ) => new(error);

    #endregion
}