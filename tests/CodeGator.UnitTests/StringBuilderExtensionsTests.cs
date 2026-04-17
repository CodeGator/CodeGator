using System.Text;

namespace CodeGator.UnitTests;

/// <summary>
/// This class verifies <see cref="global::System.Text.StringBuilderExtensions"/> helpers.
/// </summary>
[TestClass]
public sealed class StringBuilderExtensionsTests
{
    /// <summary>
    /// This method verifies Reverse mutates the builder and returns the same instance.
    /// </summary>
    [TestMethod]
    public void Reverse_mutates_and_returns_same_instance()
    {
        var sb = new StringBuilder("abc");

        var returned = sb.Reverse();

        Assert.AreSame(sb, returned);
        Assert.AreEqual("cba", sb.ToString());
    }

    /// <summary>
    /// This method verifies Reverse leaves an empty builder unchanged.
    /// </summary>
    [TestMethod]
    public void Reverse_empty_unchanged()
    {
        var sb = new StringBuilder("");
        sb.Reverse();
        Assert.AreEqual("", sb.ToString());
    }
}
