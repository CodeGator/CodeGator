using System.Diagnostics.CodeAnalysis;

namespace CodeGator;

// I copied this code from our AI overlords and modified it for my needs.

/// <summary>
/// This class represents the outcome of an operation without a typed value.
/// </summary>
public class Result
{
    /// <summary>
    /// This property indicates whether the result represents success.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// This property indicates whether the result represents failure.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// This property holds the error details when the result is a failure.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    /// This constructor initializes a new instance of the Result class.
    /// </summary>
    protected Result()
    {
        IsSuccess = true;
        Error = Error.None;
    }

    /// <summary>
    /// This constructor initializes a new instance of the Result class.
    /// </summary>
    /// <param name="error">The error that describes the failure.</param>
    protected Result(
        [NotNull] Error error
        )
    {
        IsSuccess = false;
        Error = error;
    }

    /// <summary>
    /// This method returns a successful result without a payload value.
    /// </summary>
    /// <returns>A successful <see cref="Result"/> instance.</returns>
    public static Result Success() => new();

    /// <summary>
    /// This method returns a failed result with the specified error.
    /// </summary>
    /// <param name="error">The error that describes the failure.</param>
    /// <returns>A failed <see cref="Result"/> instance.</returns>
    public static Result Failure(
        [NotNull] Error error
        ) => new(error);
}



/// <summary>
/// This class represents the outcome of an operation with an optional value.
/// </summary>
/// <typeparam name="T">The type of the value carried on success.</typeparam>
/// <remarks>
/// Use <see cref="Result"/> for operations without a payload. This type does not inherit
/// <see cref="Result"/> so nullability flows to <see cref="Data"/>; convert with the
/// implicit cast to <see cref="Result"/> when an untyped outcome is required.
/// </remarks>
public class Result<T> where T : notnull
{
    /// <summary>
    /// This property indicates whether the result represents success.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// This property indicates whether the result represents failure.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// This property holds the error details when the result is a failure.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    /// This property holds the value when the result is successful.
    /// </summary>
    /// <remarks>
    /// Callers may attach null on success for reference types; on failure the value is
    /// undefined and callers should use <see cref="IsFailure"/> and <see cref="Error"/>.
    /// </remarks>
    public T? Data { get; }

    /// <summary>
    /// This constructor initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    /// <param name="data">The value to associate with this successful result.</param>
    protected Result(
        T? data
        )
    {
        IsSuccess = true;
        Error = Error.None;
        Data = data;
    }

    /// <summary>
    /// This constructor initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    /// <param name="error">The error that describes the failure.</param>
    protected Result(
        [NotNull] Error error
        )
    {
        IsSuccess = false;
        Error = error;
        Data = default;
    }

    /// <summary>
    /// This method returns a successful result containing the given value.
    /// </summary>
    /// <param name="data">The value to attach to the result.</param>
    /// <returns>A successful <see cref="Result{T}"/> instance.</returns>
    public static Result<T> Success(
        T? data
        ) => new(data);

    /// <summary>
    /// This method returns a failed typed result with the specified error.
    /// </summary>
    /// <param name="error">The error that describes the failure.</param>
    /// <returns>A failed <see cref="Result{T}"/> instance.</returns>
    public static Result<T> Failure(
        [NotNull] Error error
        ) => new(error);

    /// <summary>
    /// This operator converts a typed result to an untyped result for the same outcome.
    /// </summary>
    /// <param name="value">Typed result to convert.</param>
    public static implicit operator Result(
        Result<T> value
        )
    {
        Guard.Instance().ThrowIfNull(value, nameof(value));

        return value.IsSuccess
            ? Result.Success()
            : Result.Failure(value.Error);
    }
}
