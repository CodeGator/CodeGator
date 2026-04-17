
#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.Reflection;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// This class contains extension methods related to the <see cref="Assembly"/>
/// type.
/// </summary>
public static partial class AssemblyExtensions
{

    /// <summary>
    /// This method reads the assembly file-version attribute value, if present.
    /// </summary>
    /// <param name="assembly">The assembly whose metadata is read.</param>
    /// <returns>The file version string, or an empty string when it is missing.</returns>
    public static string ReadFileVersion(this Assembly assembly)
    {
        object[] attributes = assembly.GetCustomAttributes(
            typeof(AssemblyFileVersionAttribute),
            true
            );

        if (attributes.Length == 0)
        {
            return string.Empty;
        }

        if (attributes[0] is not AssemblyFileVersionAttribute attr || attr.Version.Length == 0)
        {
            return string.Empty;
        }

        return attr.Version;
    }
}
