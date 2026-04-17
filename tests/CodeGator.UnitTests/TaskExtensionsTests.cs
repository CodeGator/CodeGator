using System.Threading.Tasks;

namespace CodeGator.UnitTests;

/// <summary>
/// This class verifies <see cref="global::System.Threading.Tasks.TaskExtensions"/> helpers.
/// </summary>
[TestClass]
public sealed class TaskExtensionsTests
{
    /// <summary>
    /// This method verifies WaitAll runs every unstarted task under a concurrency cap.
    /// </summary>
    [TestMethod]
    public void WaitAll_runs_all_unstarted_tasks()
    {
        var n = 0;
        var tasks = Enumerable.Range(0, 6)
            .Select(_ => new Task(() => Interlocked.Increment(ref n)))
            .ToArray();

        tasks.WaitAll(maxConcurrency: 2);

        Assert.AreEqual(6, n);
    }

    /// <summary>
    /// This method verifies WaitAll treats zero max concurrency as unbounded.
    /// </summary>
    [TestMethod]
    public void WaitAll_maxConcurrency_zero_acts_unbounded()
    {
        var n = 0;
        var tasks = Enumerable.Range(0, 4)
            .Select(_ => new Task(() => Interlocked.Increment(ref n)))
            .ToArray();

        tasks.WaitAll(maxConcurrency: 0);

        Assert.AreEqual(4, n);
    }

    /// <summary>
    /// This method verifies WhenAll schedules every unstarted task asynchronously.
    /// </summary>
    /// <returns>A task that completes when assertions finish.</returns>
    [TestMethod]
    public async Task WhenAll_runs_all_unstarted_tasks()
    {
        var n = 0;
        var tasks = Enumerable.Range(0, 5)
            .Select(_ => new Task(() => Interlocked.Increment(ref n)))
            .ToArray();

        await tasks.WhenAll(maxConcurrency: 3);

        Assert.AreEqual(5, n);
    }

    /// <summary>
    /// This method verifies WaitAll throws when the task sequence reference is null.
    /// </summary>
    [TestMethod]
    public void WaitAll_throws_when_tasks_null()
    {
        IEnumerable<Task>? tasks = null;

        Assert.ThrowsExactly<ArgumentNullException>(() =>
            tasks!.WaitAll(maxConcurrency: 1));
    }
}
