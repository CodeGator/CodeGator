
#pragma warning disable IDE0130
namespace System;
#pragma warning restore IDE0130

/// <summary>
/// This class contains extension methods related to the <see cref="Object"/>
/// type.
/// </summary>
public static partial class ObjectExtensions
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains cached JSON serialization options.
    /// </summary>
    internal static readonly JsonSerializerOptions _jsonSerializerOptions =
        new()
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method performs a quick clone of the specified object.
    /// </summary>
    /// <param name="source">The object to be cloned.</param>
    /// <param name="sourceType">The type of the object to be cloned.</param>
    /// <returns>The cloned object.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more arguments are invalid, or missing.</exception>
    public static object? QuickClone(
        [NotNull] this object source,
        [NotNull] Type sourceType
        )
    {
        Guard.Instance().ThrowIfNull(source, nameof(source));

        var json = JsonSerializer.Serialize(
            source, 
            sourceType,
            _jsonSerializerOptions
            );

        var obj = JsonSerializer.Deserialize(
            json, 
            sourceType,
            _jsonSerializerOptions
            );

        return obj;
    }

    // *******************************************************************

    /// <summary>
    /// This method performs a quick clone of the specified object.
    /// </summary>
    /// <typeparam name="T">The type of object to be cloned.</typeparam>
    /// <param name="source">The object to be cloned.</param>
    /// <returns>The cloned object.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more arguments are invalid, or missing.</exception>
    public static T? QuickClone<T>(
        [NotNull] this T source
        ) where T : class
    {
        Guard.Instance().ThrowIfNull(source, nameof(source));

        if (typeof(T) == typeof(object) && source.GetType() != typeof(object))
        {
            // The problem here is, 'source' is a non object type but it's 
            //   been passed to us as an object reference. That makes the T
            //   type parameter = object. That's not right, so let's call
            //   GetType ourselves and pass the correct type into the
            //   overload. Then we can cast the results manually.

            var obj = source.QuickClone(source.GetType()) as T;
            return obj;
        }
        else
        {
            var json = JsonSerializer.Serialize<T>(
                source,
                _jsonSerializerOptions
                );

            var obj = JsonSerializer.Deserialize<T>(
                json,
                _jsonSerializerOptions
                );

            return obj;
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method performs a quick copy from the source object to the 
    /// destination object.
    /// </summary>
    /// <param name="source">The object to read from.</param>
    /// <param name="dest">The object to write to.</param>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more arguments are invalid, or missing.</exception>
    public static void QuickCopyTo(
        [NotNull] this object source,
        [NotNull] object dest
        )
    {
        Guard.Instance().ThrowIfNull(source, nameof(source));

        var sourceProps = source.GetType().GetProperties()
            .Where(x => x.CanWrite && x.CanRead);

        foreach (var pi in sourceProps)
        {
            var sourcePropValue = pi.GetValue(source, null);
            if (null != sourcePropValue)
            {
                if (pi.PropertyType == typeof(string) ||
                    pi.PropertyType == typeof(decimal) ||
                    pi.PropertyType == typeof(int) ||
                    pi.PropertyType == typeof(double) ||
                    pi.PropertyType == typeof(float) ||
                    pi.PropertyType == typeof(DateTime) ||
                    pi.PropertyType == typeof(TimeSpan) ||
                    pi.PropertyType.IsEnum
                    )
                {
                    pi.SetValue(dest, sourcePropValue, null);
                }
                else
                {
                    var destPropValue = pi.GetValue(dest, null);
                    if (null != destPropValue)
                    {
                        sourcePropValue.QuickCopyTo(
                            destPropValue
                            );
                    }
                    else
                    {
                        pi.SetValue(dest, null, null);
                    }
                }
            }
            else
            {
                pi.SetValue(dest, null, null);
            }
        }
    }

    #endregion
}