namespace CodeGator.UnitTests;

[TestClass]
public sealed class StringStreamTests
{
    [TestMethod]
    public void Read_roundtrip_utf8()
    {
        using var stream = new StringStream("hello");

        using var reader = new StreamReader(stream, Encoding.UTF8, leaveOpen: true);
        var text = reader.ReadToEnd();

        Assert.AreEqual("hello", text);
    }
}
