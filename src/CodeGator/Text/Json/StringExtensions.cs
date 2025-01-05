
// I found this code here: https://stackoverflow.com/questions/59313256/deserialize-anonymous-type-with-system-text-json
// Any bugs in my version are probably my fault.

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.Text.Json;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// This class utility contains extensions to the <see cref="string"/>
/// type.
/// </summary>
public static partial class StringExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method deserialize the given JSON into an anonymous type, 
    /// defined by the <typeparamref name="T"/> parameter.
    /// </summary>
    /// <typeparam name="T">The type to use for the operation</typeparam>
    /// <param name="json">The JSON to use for the operation.</param>
    /// <param name="anonymousTypeObject">The instance of <typeparamref name="T"/></param>
    /// <param name="options">The options to use for the operation.</param>
    /// <returns>The deserialized JSON.</returns>
    public static T? DeserializeAnonymousType<T>(
        [NotNull] this string json,
#pragma warning disable IDE0060 // Remove unused parameter
        T anonymousTypeObject,
#pragma warning restore IDE0060 // Remove unused parameter
        JsonSerializerOptions? options = default
        ) => JsonSerializer.Deserialize<T>(json, options);

    #endregion
}
