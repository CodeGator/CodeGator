
#pragma warning disable IDE0130
namespace System;
#pragma warning restore IDE0130

/// <summary>
/// This class contains extension methods related to the <see cref="AppDomain"/>
/// type.
/// </summary>
public static partial class AppDomainExtensions
{
    /// <summary>
    /// This method gets the much friendlier name of the app-domain.
    /// </summary>
    /// <param name="appDomain">The app-domain to use for the operation.</param>
    /// <param name="stripTrailingExtension">True to strip any trailing file
    /// extension in the friendly name; false otherwise.</param>
    /// <returns>The friendly name of this application domain.</returns>
    /// <exception cref="AppDomainUnloadedException">The operation was 
    /// attempted on an unloaded app-domain.</exception>
    public static string FriendlyNameEx(
        this AppDomain appDomain,
        bool stripTrailingExtension = false
        )
    {
        Guard.Instance().ThrowIfNull(appDomain, nameof(appDomain));

        var friendlyName = AppDomain.CurrentDomain.FriendlyName;

        if (friendlyName.Contains("Enumerating source"))
        {
            // If we get here then it most likely means we are actually running
            //   inside a unit test environment. For some reason, the standard
            //   unit test environment changes the friendly name to something 
            //   that's not so very friendly. So, let's fix that now.

            var match = Regex.Match(friendlyName, @": Enumerating source \((?<path>.*)\)");
            if (match.Groups.Count >= 2)
            {
                friendlyName = Path.GetFileName(match.Groups[1].Value);
            }
            else
            {
                // If we get here then we've got no friendly name because something 
                //   we didn't anticipate has happened. So, we'll need to invent a 
                //   reasonable friendly name now.

                friendlyName = Path.GetFileName(
                    Process.GetCurrentProcess().MainModule?.FileName ?? ""
                    );
            }
        }

        if (stripTrailingExtension)
        {
            if (friendlyName.ToLower().EndsWith(".dll") ||
                friendlyName.ToLower().EndsWith(".exe"))
            {
                friendlyName = Path.GetFileNameWithoutExtension(friendlyName);
            }
        }

        return friendlyName;
    }
}
