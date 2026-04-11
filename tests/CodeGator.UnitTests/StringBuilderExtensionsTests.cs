using System.Text;

namespace CodeGator.UnitTests;

[TestClass]
public sealed class StringBuilderExtensionsTests
{
    [TestMethod]
    public void Reverse_mutates_and_returns_same_instance()
    {
        var sb = new StringBuilder("abc");

        var returned = sb.Reverse();

        Assert.AreSame(sb, returned);
        Assert.AreEqual("cba", sb.ToString());
    }

    [TestMethod]
    public void Reverse_empty_unchanged()
    {
        var sb = new StringBuilder("");
        sb.Reverse();
        Assert.AreEqual("", sb.ToString());
    }
}
