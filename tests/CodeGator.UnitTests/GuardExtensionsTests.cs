namespace CodeGator.UnitTests;

[TestClass]
public sealed class GuardExtensionsTests
{
    private sealed class NonReadableStream : Stream
    {
        public override bool CanRead => false;
        public override bool CanSeek => false;
        public override bool CanWrite => true;
        public override long Length => 0;
        public override long Position { get => 0; set { } }
        public override void Flush() { }
        public override int Read(byte[] buffer, int offset, int count) => 0;
        public override long Seek(long offset, SeekOrigin origin) => 0;
        public override void SetLength(long value) { }
        public override void Write(byte[] buffer, int offset, int count) { }
    }

    [TestMethod]
    public void ThrowIfEmpty_throws_on_empty_sequence()
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
            Guard.Instance().ThrowIfEmpty(Array.Empty<int>(), "values"));
    }

    [TestMethod]
    public void ThrowIfNotReadable_throws_when_cannot_read()
    {
        using var stream = new NonReadableStream();

        Assert.ThrowsExactly<ArgumentException>(() =>
            Guard.Instance().ThrowIfNotReadable(stream, "stream"));
    }

    [TestMethod]
    public void ThrowIfNotWritable_throws_when_cannot_write()
    {
        using var ms = new MemoryStream(new byte[1], writable: false);

        Assert.ThrowsExactly<ArgumentException>(() =>
            Guard.Instance().ThrowIfNotWritable(ms, "stream"));
    }

    [TestMethod]
    public void ThrowIfInvalidFilePath_throws_on_invalid_characters()
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
            Guard.Instance().ThrowIfInvalidFilePath("a|b.txt", "path"));
    }

    [TestMethod]
    public void ThrowIfInvalidFolderPath_throws_on_invalid_characters()
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
            Guard.Instance().ThrowIfInvalidFolderPath("a|b", "path"));
    }

    [TestMethod]
    public void ThrowIfInvalidFileExtension_throws_on_missing_dot()
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
            Guard.Instance().ThrowIfInvalidFileExtension("tmp", "ext"));
    }
}
