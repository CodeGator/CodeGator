namespace CodeGator.UnitTests;

[TestClass]
public sealed class ListExtensionsTests
{
    [TestMethod]
    public void AddRange_appends_to_list()
    {
        var list = new List<int> { 1 };
        list.AddRange(new[] { 2, 3 });

        CollectionAssert.AreEqual(new[] { 1, 2, 3 }, list);
    }

    [TestMethod]
    public void RemoveRange_removes_slice()
    {
        var list = new List<int> { 1, 2, 3, 4 };
        list.RemoveRange(1, 2);

        CollectionAssert.AreEqual(new[] { 1, 4 }, list);
    }

    [TestMethod]
    public void ForEach_on_list_delegates()
    {
        var sum = 0;
        new List<int> { 1, 2 }.ForEach(x => sum += x);

        Assert.AreEqual(3, sum);
    }

    [TestMethod]
    public void Shuffle_empty_returns_same_instance()
    {
        var list = new List<int>();
        var returned = list.Shuffle();

        Assert.AreSame(list, returned);
    }

    [TestMethod]
    public void Shuffle_preserves_multiset()
    {
        var list = new List<int> { 3, 1, 2 };
        list.Shuffle();

        CollectionAssert.AreEqual(new[] { 1, 2, 3 }, list.OrderBy(x => x).ToArray());
    }
}
