namespace CodeGator.UnitTests;

[TestClass]
public sealed class TemporaryStreamTests
{
    [TestMethod]
    public void Write_read_delete_on_dispose()
    {
        var path = "";
        using (var stream = new TemporaryStream(".tmp"))
        {
            path = stream.Name;
            Assert.IsTrue(File.Exists(path));

            var bytes = Encoding.UTF8.GetBytes("data");
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
            stream.Position = 0;

            var buffer = new byte[bytes.Length];
            var read = stream.Read(buffer, 0, buffer.Length);

            Assert.AreEqual(bytes.Length, read);
            CollectionAssert.AreEqual(bytes, buffer);
        }

        Assert.IsFalse(string.IsNullOrEmpty(path));
        Assert.IsFalse(File.Exists(path));
    }
}
