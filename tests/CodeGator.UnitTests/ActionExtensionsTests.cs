namespace CodeGator.UnitTests;

[TestClass]
public sealed class ActionExtensionsTests
{
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
