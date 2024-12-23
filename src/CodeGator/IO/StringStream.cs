
#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.IO;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// This class is a string based implementation of <see cref="MemoryStream"/>.
/// </summary>
/// <param name="value">The value for the stream.</param>
public class StringStream(
     string value
    ) : MemoryStream(Encoding.UTF8.GetBytes(value))
{

}
