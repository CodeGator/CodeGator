
#pragma warning disable IDE0130
namespace System.Text;
#pragma warning restore IDE0130

/// <summary>
/// This class contains extension methods related to the <see cref="StringBuilder"/>
/// type.
/// </summary>
public static partial class StringBuilderExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method reverses the characters in a string.
    /// </summary>
    /// <param name="builder">The string builder to use for the operation.</param>
    /// <returns>A reversed version of the specified string.</returns>
    public static StringBuilder Reverse(
        [NotNull] this StringBuilder builder
        )
    {
        Guard.Instance().ThrowIfNull(builder, nameof(builder));

        for (int x = 0; x < builder.Length / 2; x++)
        {
            var temp = builder[builder.Length - x - 1];
            builder[builder.Length - x - 1] = builder[x];
            builder[x] = temp;
        }

        return builder;
    }

    #endregion
}
