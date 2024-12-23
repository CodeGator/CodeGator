
#pragma warning disable IDE0130
namespace System;
#pragma warning restore IDE0130

/// <summary>
/// This class contains extension methods related to the <see cref="Action"/>
/// type.
/// </summary>
public static partial class ActionExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method runs the collection of actions while limiting the number
    /// that run concurrently to, at most, <paramref name="maxConcurrency"/>.
    /// </summary>
    /// <param name="actions">The collection of actions to run.</param>
    /// <param name="maxConcurrency">The maximum number of actions to run 
    /// concurrently. A positive value limits the number of concurrent 
    /// operations to the set value. If it is -1, there is no limit on the 
    /// number of concurrently running operations.</param>
    /// <param name="token">An optional cancellation token.</param>
    public static void WaitAll(
        this IEnumerable<Action> actions,
        int maxConcurrency,
        CancellationToken token = default
        )
    {
        Guard.Instance().ThrowIfNull(actions, nameof(actions))
            .ThrowIfLessThan(maxConcurrency, -1, nameof(maxConcurrency))
            .ThrowIfNull(token, nameof(token));

        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = maxConcurrency,
            CancellationToken = token
        };

        Parallel.Invoke(
            options,
            actions.ToArray()
            );
    }

    // *******************************************************************

    /// <summary>
    /// This method runs the collection of actions while limiting the number
    /// that run concurrently to, at most, <paramref name="maxConcurrency"/>.
    /// </summary>
    /// <param name="actions">The collection of actions to run.</param>
    /// <param name="maxConcurrency">The maximum number of actions to run 
    /// concurrently. A positive value limits the number of concurrent 
    /// operations to the set value. If it is -1, there is no limit on the 
    /// number of concurrently running operations.</param>
    /// <param name="token">An optional cancellation token.</param>
    /// <remarks>A task to perform the oepration.</remarks>
    public static async Task WhenAll(
        this IEnumerable<Action> actions,
        int maxConcurrency,
        CancellationToken token = default
        )
    {
        Guard.Instance().ThrowIfNull(actions, nameof(actions))
            .ThrowIfLessThan(maxConcurrency, -1, nameof(maxConcurrency));

        await Task.Run(() =>
        {
            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = maxConcurrency,
                CancellationToken = token
            };

            Parallel.Invoke(
                options,
                actions.ToArray()
                );
        }, token
        ).ConfigureAwait(false);
    }

    #endregion
}
