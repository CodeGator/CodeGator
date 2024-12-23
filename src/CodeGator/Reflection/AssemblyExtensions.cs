
#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.Reflection;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// This class contains extension methods related to the <see cref="Assembly"/>
/// type.
/// </summary>
public static partial class AssemblyExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// Reads the value of the <see cref="AssemblyFileVersionAttribute"/>
    /// attribute for the given assembly.
    /// </summary>
    /// <param name="assembly">The assembly to read from.</param>
    /// <returns>The value of the given assembly's file version attribute.</returns>
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

    #endregion
}
