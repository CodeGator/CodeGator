
#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.IO;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// This class exposes in-memory UTF-8 stream bytes for a string value.
/// </summary>
/// <param name="value">This property supplies the string encoded as the stream payload.</param>
public class StringStream(
     string value
    ) : MemoryStream(Encoding.UTF8.GetBytes(value))
{

}
