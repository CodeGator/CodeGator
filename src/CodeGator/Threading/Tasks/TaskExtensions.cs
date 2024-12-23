
#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.Threading.Tasks;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// This class contains extension methods related to the <see cref="Task"/>
/// type.
/// </summary>
public static partial class TaskExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method runs the collection of tasks while limiting the number
    /// that run concurrently to, at most, <paramref name="maxConcurrency"/>.
    /// </summary>
    /// <param name="tasks">The collection of tasks to run.</param>
    /// <param name="maxConcurrency">The maximum number of tasks to run 
    /// concurrently. A positive value limits the number of concurrent 
    /// operations to the set value. If it is -1, there is no limit on the 
    /// number of concurrently running operations.</param>
    /// <param name="maxTimeout">The maximum number of milliseconds to wait
    /// for the operations to finish.</param>
    /// <param name="token">An optional cancellation token.</param>
    public static void WaitAll(
        this IEnumerable<Task> tasks,
        int maxConcurrency,
        int maxTimeout = -1,
        CancellationToken token = default
        )
    {
        Guard.Instance().ThrowIfNull(tasks, nameof(tasks))
            .ThrowIfLessThan(maxConcurrency, -1, nameof(maxConcurrency))
            .ThrowIfLessThan(maxTimeout, -1, nameof(maxTimeout))
            .ThrowIfNull(token, nameof(token));

        if (maxConcurrency <= 0)
        {
            maxConcurrency = int.MaxValue;
        }

#pragma warning disable IDE0063 // Use simple 'using' statement
        using (var sync = new SemaphoreSlim(maxConcurrency))
        {
            var postTaskTasks = new List<Task>();

            tasks.ForEach(t => postTaskTasks.Add(
                t.ContinueWith(
                    tsk => sync.Release()
                    )
                ));

            foreach (var task in tasks)
            {
                sync.Wait(maxTimeout, token);

                token.ThrowIfCancellationRequested();

                if (task.Status != TaskStatus.WaitingToRun)
                {
                    task.Start();
                }
            }

            Task.WaitAll(
                [.. postTaskTasks],
                maxTimeout,
                token
                );
        }
#pragma warning restore IDE0063 // Use simple 'using' statement
    }

    // *******************************************************************

    /// <summary>
    /// This method runs the collection of tasks while limiting the number
    /// that run concurrently to, at most, <paramref name="maxConcurrency"/>.
    /// </summary>
    /// <param name="tasks">The collection of tasks to run.</param>
    /// <param name="maxConcurrency">The maximum number of tasks to run 
    /// concurrently. A positive value limits the number of concurrent 
    /// operations to the set value. If it is -1, there is no limit on the 
    /// number of concurrently running operations.</param>
    /// <param name="maxTimeout">The maximum number of milliseconds to wait
    /// for the operations to finish.</param>
    /// <param name="token">An optional cancellation token.</param>
    /// <remarks>A task to perform the operation.</remarks>
    public static async Task WhenAll(
        this IEnumerable<Task> tasks,
        int maxConcurrency,
        int maxTimeout = -1,
        CancellationToken token = default
        )
    {
        Guard.Instance().ThrowIfNull(tasks, nameof(tasks));

        await Task.Run(() =>
        {
            WaitAll(
                tasks,
                maxConcurrency,
                maxTimeout,
                token
                );
        },
        cancellationToken: token
        ).ConfigureAwait(false);
    }

    #endregion
}
