namespace CodeGator.UnitTests;

[TestClass]
public sealed class EnumerableExtensionsTests
{
    [TestMethod]
    public void DistinctOn_keeps_first_per_key()
    {
        var items = new[] { (1, "a"), (2, "a"), (3, "b") };

        var result = items.DistinctOn(x => x.Item2).ToList();

        CollectionAssert.AreEqual(new[] { (1, "a"), (3, "b") }, result);
    }

    [TestMethod]
    public void ApplyBlackList_filters_matching_names()
    {
        var names = new[] { "MyApp.dll", "System.Private.CoreLib", "Other" };

        var result = names.ApplyBlackList(x => x, "System.*").ToList();

        CollectionAssert.AreEqual(new[] { "MyApp.dll", "Other" }, result);
    }

    [TestMethod]
    public void ApplyWhiteList_keeps_only_matches()
    {
        var names = new[] { "A", "B", "C" };

        var result = names.ApplyWhiteList(x => x, "A,B").ToList();

        CollectionAssert.AreEqual(new[] { "A", "B" }, result);
    }

    [TestMethod]
    public async Task ToDictionaryAsync_builds_dictionary()
    {
        var keys = new[] { 1, 2 };
        var dict = await keys.ToDictionaryAsync(
            x => x,
            async x =>
            {
                await Task.Yield();
                return x * 10;
            });

        Assert.AreEqual(10, dict[1]);
        Assert.AreEqual(20, dict[2]);
    }

    [TestMethod]
    public void ForEach_invokes_action_per_item()
    {
        var sum = 0;
        new[] { 1, 2, 3 }.ForEach(x => sum += x);

        Assert.AreEqual(6, sum);
    }

    [TestMethod]
    public void ForEach_collects_exceptions_in_aggregate()
    {
        var ex = Assert.ThrowsExactly<AggregateException>(() =>
        {
            new[] { 1, 2 }.ForEach(_ => throw new InvalidOperationException("x"));
        });

        Assert.AreEqual(2, ex.InnerExceptions.Count);
    }
}
