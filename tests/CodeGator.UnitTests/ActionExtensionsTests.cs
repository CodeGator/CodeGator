namespace CodeGator.UnitTests;

/// <summary>
/// This class verifies <see cref="global::System.Action"/> extension helpers.
/// </summary>
[TestClass]
public sealed class ActionExtensionsTests
{
    /// <summary>
    /// This method verifies WaitAll runs every scheduled action.
    /// </summary>
    [TestMethod]
    public void WaitAll_runs_all_actions()
    {
        var n = 0;
        new Action[]
        {
            () => Interlocked.Increment(ref n),
            () => Interlocked.Increment(ref n),
        }.WaitAll(maxConcurrency: 2);

        Assert.AreEqual(2, n);
    }

    /// <summary>
    /// This method verifies WhenAll runs every scheduled action asynchronously.
    /// </summary>
    /// <returns>A task that completes when assertions finish.</returns>
    [TestMethod]
    public async Task WhenAll_runs_all_actions()
    {
        var n = 0;
        await new Action[]
        {
            () => Interlocked.Increment(ref n),
            () => Interlocked.Increment(ref n),
        }.WhenAll(maxConcurrency: 2);

        Assert.AreEqual(2, n);
    }
}
