namespace CodeGator.UnitTests;

/// <summary>
/// This class verifies StringStream UTF-8 read behavior.
/// </summary>
/// <remarks>See <see cref="global::System.IO.StringStream"/>.</remarks>
[TestClass]
public sealed class StringStreamTests
{
    /// <summary>
    /// This method verifies StreamReader round-trips UTF-8 text from a string stream.
    /// </summary>
    [TestMethod]
    public void Read_roundtrip_utf8()
    {
        using var stream = new StringStream("hello");

        using var reader = new StreamReader(stream, Encoding.UTF8, leaveOpen: true);
        var text = reader.ReadToEnd();

        Assert.AreEqual("hello", text);
    }
}
