namespace CodeGator.UnitTests;

/// <summary>
/// This class verifies <see cref="global::System.Collections.Generic.ListExtensions"/> helpers.
/// </summary>
[TestClass]
public sealed class ListExtensionsTests
{
    /// <summary>
    /// This method verifies AddRange appends every element from the source sequence.
    /// </summary>
    [TestMethod]
    public void AddRange_appends_to_list()
    {
        var list = new List<int> { 1 };
        list.AddRange(new[] { 2, 3 });

        CollectionAssert.AreEqual(new[] { 1, 2, 3 }, list);
    }

    /// <summary>
    /// This method verifies RemoveRange deletes the requested contiguous slice.
    /// </summary>
    [TestMethod]
    public void RemoveRange_removes_slice()
    {
        var list = new List<int> { 1, 2, 3, 4 };
        list.RemoveRange(1, 2);

        CollectionAssert.AreEqual(new[] { 1, 4 }, list);
    }

    /// <summary>
    /// This method verifies ForEach on lists invokes the callback per element.
    /// </summary>
    [TestMethod]
    public void ForEach_on_list_delegates()
    {
        var sum = 0;
        new List<int> { 1, 2 }.ForEach(x => sum += x);

        Assert.AreEqual(3, sum);
    }

    /// <summary>
    /// This method verifies Shuffle returns the same instance for an empty list.
    /// </summary>
    [TestMethod]
    public void Shuffle_empty_returns_same_instance()
    {
        var list = new List<int>();
        var returned = list.Shuffle();

        Assert.AreSame(list, returned);
    }

    /// <summary>
    /// This method verifies Shuffle preserves multiset membership after shuffling.
    /// </summary>
    [TestMethod]
    public void Shuffle_preserves_multiset()
    {
        var list = new List<int> { 3, 1, 2 };
        list.Shuffle();

        CollectionAssert.AreEqual(new[] { 1, 2, 3 }, list.OrderBy(x => x).ToArray());
    }
}
