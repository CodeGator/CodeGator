
namespace System.IO;

/// <summary>
/// This class wraps a temp file-backed stream for short-lived binary I/O.
/// </summary>
public class TemporaryStream : Stream
{

    /// <summary>
    /// This property holds the file path of the underlying temporary file.
    /// </summary>
    public string Name
    {
        get { return BaseStream.Name; }
    }


    /// <summary>
    /// This property holds the backing file stream used for read and write operations.
    /// </summary>
    protected FileStream BaseStream { get; private set; }


    /// <summary>
    /// This constructor initializes a new instance of the TemporaryStream class.
    /// </summary>
    /// <param name="ext">Optional extension applied to the temporary file name.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="ext"/> is not a valid file extension.</exception>
    public TemporaryStream(
        string ext = ".tmp"
        )
    {
        Guard.Instance().ThrowIfInvalidFileExtension(ext, nameof(ext));

        BaseStream = File.Create(
            Path.ChangeExtension(
                Path.GetTempFileName(),
                ext
                )
            );
    }


    /// <summary>
    /// This property indicates whether the stream supports read operations.
    /// </summary>
    public override bool CanRead
    {
        get { return BaseStream.CanRead; }
    }


    /// <summary>
    /// This property indicates whether the stream supports seek operations.
    /// </summary>
    public override bool CanSeek
    {
        get { return BaseStream.CanSeek; }
    }


    /// <summary>
    /// This property indicates whether the stream supports write operations.
    /// </summary>
    public override bool CanWrite
    {
        get { return BaseStream.CanWrite; }
    }


    /// <summary>
    /// This property holds the length of the stream in bytes.
    /// </summary>
    public override long Length
    {
        get { return BaseStream.Length; }
    }


    /// <summary>
    /// This property gets or sets the current byte offset within the stream.
    /// </summary>
    public override long Position
    {
        get { return BaseStream.Position; }
        set { BaseStream.Position = value; }
    }


    /// <summary>
    /// This method clears buffers and commits buffered data to the temp file.
    /// </summary>
    public override void Flush()
    {
        BaseStream.Flush();
    }


    /// <summary>
    /// This method reads bytes from the stream into the supplied buffer.
    /// </summary>
    /// <param name="buffer">When this method returns, contains the specified 
    /// byte array with the values between offset and (offset + count - 1) 
    /// replaced by the bytes read from the current source.</param>
    /// <param name="offset">The byte offset in array at which the read bytes 
    /// will be placed.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <returns>The total number of bytes read into the buffer. This might 
    /// be less than the number of bytes requested if that number of bytes 
    /// are not currently available, or zero if the end of the stream is 
    /// reached.</returns>
    public override int Read(
        byte[] buffer,
        int offset,
        int count
        )
    {
        return BaseStream.Read(
            buffer,
            offset,
            count
            );
    }


    /// <summary>
    /// This method sets the stream position relative to the specified origin.
    /// </summary>
    /// <param name="offset">The point relative to origin from which to 
    /// begin seeking.</param>
    /// <param name="origin">Specifies the beginning, the end, or the current 
    /// position as a reference point for offset, using a value of type 
    /// System.IO.SeekOrigin.</param>
    /// <returns>The new position in the stream.</returns>
    public override long Seek(
        long offset,
        SeekOrigin origin
        )
    {
        return BaseStream.Seek(
            offset,
            origin
            );
    }


    /// <summary>
    /// This method sets the length of the stream to the specified byte count.
    /// </summary>
    /// <param name="value">The new length of the stream.</param>
    public override void SetLength(
        long value
        )
    {
        BaseStream.SetLength(value);
    }


    /// <summary>
    /// This method writes bytes from the buffer to the underlying file stream.
    /// </summary>
    /// <param name="buffer">The buffer containing data to write to the 
    /// stream.</param>
    /// <param name="offset">The zero-based byte offset in array from which 
    /// to begin copying bytes to the stream.</param>
    /// <param name="count">The maximum number of bytes to write.</param>
    public override void Write(
        byte[] buffer,
        int offset,
        int count
        )
    {
        BaseStream.Write(
            buffer,
            offset,
            count
            );
    }


    /// <summary>
    /// This method releases the temporary file and underlying stream resources.
    /// </summary>
    /// <param name="disposing">True to release both managed and unmanaged 
    /// resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            var tempPath = BaseStream.Name;

            BaseStream.Dispose();

            if (File.Exists(tempPath))
                File.Delete(tempPath);
        }
        base.Dispose(disposing);
    }
}
