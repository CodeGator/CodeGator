namespace CodeGator.UnitTests;

/// <summary>
/// This class verifies Guard extension methods for collection and file checks.
/// </summary>
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

    /// <summary>
    /// This method verifies ThrowIfEmpty rejects empty sequences.
    /// </summary>
    [TestMethod]
    public void ThrowIfEmpty_throws_on_empty_sequence()
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
            Guard.Instance().ThrowIfEmpty(Array.Empty<int>(), "values"));
    }

    /// <summary>
    /// This method verifies ThrowIfNotReadable rejects non-readable streams.
    /// </summary>
    [TestMethod]
    public void ThrowIfNotReadable_throws_when_cannot_read()
    {
        using var stream = new NonReadableStream();

        Assert.ThrowsExactly<ArgumentException>(() =>
            Guard.Instance().ThrowIfNotReadable(stream, "stream"));
    }

    /// <summary>
    /// This method verifies ThrowIfNotWritable rejects non-writable streams.
    /// </summary>
    [TestMethod]
    public void ThrowIfNotWritable_throws_when_cannot_write()
    {
        using var ms = new MemoryStream(new byte[1], writable: false);

        Assert.ThrowsExactly<ArgumentException>(() =>
            Guard.Instance().ThrowIfNotWritable(ms, "stream"));
    }

    /// <summary>
    /// This method verifies ThrowIfInvalidFilePath rejects invalid characters.
    /// </summary>
    [TestMethod]
    public void ThrowIfInvalidFilePath_throws_on_invalid_characters()
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
            Guard.Instance().ThrowIfInvalidFilePath("a|b.txt", "path"));
    }

    /// <summary>
    /// This method verifies ThrowIfInvalidFolderPath rejects invalid characters.
    /// </summary>
    [TestMethod]
    public void ThrowIfInvalidFolderPath_throws_on_invalid_characters()
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
            Guard.Instance().ThrowIfInvalidFolderPath("a|b", "path"));
    }

    /// <summary>
    /// This method verifies ThrowIfInvalidFileExtension rejects values without a dot.
    /// </summary>
    [TestMethod]
    public void ThrowIfInvalidFileExtension_throws_on_missing_dot()
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
            Guard.Instance().ThrowIfInvalidFileExtension("tmp", "ext"));
    }
}
