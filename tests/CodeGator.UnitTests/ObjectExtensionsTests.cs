namespace CodeGator.UnitTests;

/// <summary>
/// This class verifies <see cref="global::System.ObjectExtensions"/> cloning helpers.
/// </summary>
[TestClass]
public sealed class ObjectExtensionsTests
{
    private sealed class Sample
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }

    /// <summary>
    /// This method verifies QuickClone returns a distinct copy with equal fields.
    /// </summary>
    [TestMethod]
    public void QuickClone_copies_values()
    {
        var a = new Sample { Id = 42, Name = "x" };

        var b = a.QuickClone();

        Assert.IsNotNull(b);
        Assert.AreNotSame(a, b);
        Assert.AreEqual(42, b!.Id);
        Assert.AreEqual("x", b.Name);
    }

    /// <summary>
    /// This method verifies QuickClone with an explicit runtime type succeeds.
    /// </summary>
    [TestMethod]
    public void QuickClone_non_generic_with_runtime_type()
    {
        object a = new Sample { Id = 7, Name = "y" };

        var b = a.QuickClone(a.GetType()) as Sample;

        Assert.IsNotNull(b);
        Assert.AreEqual(7, b!.Id);
    }

    /// <summary>
    /// This method verifies QuickCopyTo copies overlapping properties onto a target.
    /// </summary>
    [TestMethod]
    public void QuickCopyTo_copies_simple_properties()
    {
        var src = new Sample { Id = 5, Name = "src" };
        var dst = new Sample { Id = 0, Name = "" };

        src.QuickCopyTo(dst);

        Assert.AreEqual(5, dst.Id);
        Assert.AreEqual("src", dst.Name);
    }
}
