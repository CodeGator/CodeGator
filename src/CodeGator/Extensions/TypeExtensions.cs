
#pragma warning disable IDE0130
namespace System;
#pragma warning restore IDE0130

/// <summary>
/// This class contains extension methods related to the <see cref="Type"/>
/// type.
/// </summary>
public static partial class TypeExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method returns a list of all the public, concrete types that 
    /// are derived from the specified type.
    /// </summary>
    /// <param name="type">The type to use for the operation.</param>
    /// <param name="assemblyWhiteList">An optional white list, for filtering
    /// the assemblies used in the operation.</param>
    /// <param name="assemblyBlackList">An optional black list, for filtering
    /// the assemblies used in the operation.</param>
    /// <returns>An array of matching types.</returns>
    public static Type[] DerivedTypes(
        [NotNull] this Type type,
        string assemblyWhiteList = "",
        string assemblyBlackList = "Microsoft.*,System.*,netstandard"
        )
    {
        Guard.Instance().ThrowIfNull(type, nameof(type));

        var asmList = AppDomain.CurrentDomain.GetAssemblies()
            .Where(x => !x.IsDynamic);

        if (!string.IsNullOrEmpty(assemblyWhiteList))
        {
            asmList = asmList.ApplyWhiteList(x =>
                x.GetName().Name ?? "",
                assemblyWhiteList
                );
        }

        if (!string.IsNullOrEmpty(assemblyBlackList))
        {
            asmList = asmList.ApplyBlackList(x =>
                x.GetName().Name ?? "",
                assemblyBlackList
                );
        }

        var types = new List<Type>();
        foreach (var asm in asmList)
        {
            var assemblyTypes = asm.GetTypes().Where(x =>
                x.IsSubclassOf(type) &&
                !x.IsAbstract
                );

            types.AddRange(assemblyTypes);
        }

        return [..types];
    }

    #endregion
}
