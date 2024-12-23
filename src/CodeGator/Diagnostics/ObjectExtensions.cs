
#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.Diagnostics;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// This class contains extension methods related to the <see cref="object"/>
/// type.
/// </summary>
public static partial class ObjectExtensions
{
    // *******************************************************************
    // Types.
    // *******************************************************************

    #region Types

    /// <summary>
    /// This class is a custom expression visitor, for de-constructing LINQ
    /// expressions at runtime.
    /// </summary>
#pragma warning disable IDE1006 // Naming Styles
    private class _ExpressionVisitor : ExpressionVisitor
#pragma warning restore IDE1006 // Naming Styles
    {
        /// <summary>
        /// This property contains a stack of expression parts.
        /// </summary>
        public List<Expression> Expressions { get; } = [];

        /// <summary>
        /// This method is called for each method call in the expression.
        /// </summary>
        /// <param name="node">The node to use for the operation.</param>
        /// <returns>The LINQ expression.</returns>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            Expressions.Insert(0, node);
            return base.VisitMethodCall(node);
        }

        /// <summary>
        /// This method is called for each member access in the expression.
        /// </summary>
        /// <param name="node">The node to use for the operation.</param>
        /// <returns>The LINQ expression.</returns>
        protected override Expression VisitMember(MemberExpression node)
        {
            Expressions.Insert(0, node);
            return base.VisitMember(node);
        }
    }

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method reads a field specified by a LINQ expression and returns 
    /// the value. The linq expression can have nested elements and limited
    /// inline method calls, such as: x => x.A.B.C[2]._myField;
    /// </summary>
    /// <typeparam name="TSource">The type of source object.</typeparam>
    /// <typeparam name="TFld">The type of field.</typeparam>
    /// <param name="source">The source object to use for the operation.</param>
    /// <param name="selector">The LINQ expression to use for the operation.</param>
    /// <param name="value">The value from the specified field.</param>
    /// <returns>True if the field was read; false otherwise.</returns>
    /// <remarks>
    /// <para>The idea, with this method, is to use reflection to go find
    /// and return a field value from an object at runtime. The intent is 
    /// to use this sparingly because the performance isn't great. I see
    /// this approach as something useful for things like unit testing.</para>
    /// </remarks>
    public static bool TryGetFieldValue<TSource, TFld>(
        this TSource source,
        Expression<Func<TSource, TFld>> selector,
        out TFld? value
        )
    {
        Guard.Instance().ThrowIfNull(source, nameof(source))
            .ThrowIfNull(selector, nameof(selector));

        var visitor = new _ExpressionVisitor();
        var e = visitor.Visit(selector.Body);

        object? obj = source;
        foreach (var exp in visitor.Expressions)
        {
            switch (exp.NodeType)
            {
                case ExpressionType.MemberAccess:

                    var fi = (exp as MemberExpression)?.Member as FieldInfo;
                    if (fi != null)
                    {
                        obj = fi.GetValue(obj);
                    }
                    else
                    {
                        value = default;
                        return false;
                    }
                    break;

                case ExpressionType.Call:

                    var mi = (exp as MethodCallExpression)?.Method as MethodInfo;
                    if (mi != null)
                    {
                        var args = (exp as MethodCallExpression)?.Arguments.OfType<ConstantExpression>()
                            .Select(x => x.Value).ToArray();

                        obj = mi.Invoke(obj, args);
                    }
                    break;
            }
        }

        value = (TFld?)obj;

        return true;
    }

    // *******************************************************************

    /// <summary>
    /// This method reads a property specified by a LINQ expression and returns 
    /// the value. The linq expression can have nested elements and limited
    /// inline method calls, such as: x => x.A.B.C[2].MyProperty;
    /// </summary>
    /// <typeparam name="TSource">The type of source object.</typeparam>
    /// <typeparam name="TProp">The type of property.</typeparam>
    /// <param name="source">The source object to use for the operation.</param>
    /// <param name="selector">The LINQ expression to use for the operation.</param>
    /// <param name="value">The value from the specified property.</param>
    /// <returns>True if the property was read; false otherwise.</returns>
    /// <remarks>
    /// <para>The idea, with this method, is to use reflection to go find
    /// and return a property value from an object at runtime. The intent is 
    /// to use this sparingly because the performance isn't great. I see
    /// this approach as something useful for things like unit testing.</para>
    /// </remarks>
    public static bool TryGetPropertyValue<TSource, TProp>(
        this TSource source,
        Expression<Func<TSource, TProp>> selector,
        out TProp? value
        )
    {
        Guard.Instance().ThrowIfNull(source, nameof(source))
            .ThrowIfNull(selector, nameof(selector));

        var visitor = new _ExpressionVisitor();
        var e = visitor.Visit(selector.Body);

        object? obj = source;
        foreach (var exp in visitor.Expressions)
        {
            switch (exp.NodeType)
            {
                case ExpressionType.MemberAccess:

                    var pi = (exp as MemberExpression)?.Member as PropertyInfo;
                    if (pi != null)
                    {
                        obj = pi.GetValue(obj, null);
                    }
                    else
                    {
                        value = default;
                        return false;
                    }
                    break;

                case ExpressionType.Call:

                    var mi = (exp as MethodCallExpression)?.Method as MethodInfo;
                    if (mi != null)
                    {
                        var args = (exp as MethodCallExpression)?.Arguments.OfType<ConstantExpression>()
                            .Select(x => x.Value).ToArray();

                        obj = mi.Invoke(obj, args);
                    }
                    break;
            }
        }

        value = (TProp?)obj;

        return true;
    }

    // *******************************************************************

    /// <summary>
    /// This method reads a field value from the specified object.
    /// </summary>
    /// <param name="obj">The object to use for the operation.</param>
    /// <param name="fieldName">The field to use for the operation.</param>
    /// <param name="includeProtected">Determines if protected fields are included 
    /// in the search.</param>
    /// <returns>The value of the field.</returns>
    /// <remarks>
    /// <para>The idea, with this method, is to use reflection to go find
    /// and return a field value from an object at runtime. The intent is 
    /// to use this sparingly because the performance isn't great. I see
    /// this approach as something useful for things like unit testing.</para>
    /// </remarks>
    public static object? GetFieldValue(
        this object obj,
        string fieldName,
        bool includeProtected = false
        )
    {
        Guard.Instance().ThrowIfNull(obj, nameof(obj))
            .ThrowIfNullOrEmpty(fieldName, nameof(fieldName));

        var type = obj.GetType();

        var fi = type.GetField(
            fieldName,
            BindingFlags.Static |
            BindingFlags.Instance |
            BindingFlags.Public |
            (includeProtected ? BindingFlags.NonPublic : BindingFlags.Public)
            );

        if (fi == null)
        {
            return null;
        }

        var value = fi.GetValue(obj);
        var valType = value?.GetType();

        if (valType == typeof(WeakReference))
        {
            return (value as WeakReference)?.Target;
        }

        return value;
    }

    // *******************************************************************

    /// <summary>
    /// This method reads a field value from the specified object.
    /// </summary>
    /// <typeparam name="T">The type of the field.</typeparam>
    /// <param name="obj">The object to use for the operation.</param>
    /// <param name="fieldName">The field to use for the operation.</param>
    /// <param name="includeProtected">Determines if protected fields are included 
    /// in the search.</param>
    /// <returns>The value of the field.</returns>
    /// <remarks>
    /// <para>The idea, with this method, is to use reflection to go find
    /// and return a field value from an object at runtime. The intent is 
    /// to use this sparingly because the performance isn't great. I see
    /// this approach as something useful for things like unit testing.</para>
    /// </remarks>
    public static T? GetFieldValue<T>(
        this object obj,
        string fieldName,
        bool includeProtected = false
        ) where T : class
    {
        Guard.Instance().ThrowIfNull(obj, nameof(obj))
            .ThrowIfNullOrEmpty(fieldName, nameof(fieldName));

        var type = obj.GetType();

        var fi = type.GetField(
            fieldName,
            BindingFlags.Static |
            BindingFlags.Instance |
            BindingFlags.Public |
            (includeProtected ? BindingFlags.NonPublic : BindingFlags.Public)
            );

        if (fi == null)
        {
            return default;
        }

        var value = fi?.GetValue(obj);
        var valType = value?.GetType();

        if (valType == typeof(WeakReference))
        {
            return (T?)(value as WeakReference)?.Target;
        }

        if (valType == typeof(WeakReference<T>))
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if ((value as WeakReference<T>).TryGetTarget(out T? target))
            {
                return target;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        return (T?)value;
    }

    // *******************************************************************

    /// <summary>
    /// This method writes a field value for the specified object.
    /// </summary>
    /// <param name="obj">The object to use for the operation.</param>
    /// <param name="fieldName">The field to use for the operation.</param>
    /// <param name="value">The value to use for the operation.</param>
    /// <param name="includeProtected">Determines if protected fields are 
    /// included in the search.</param>
    /// <remarks>
    /// <para>The idea, with this method, is to use reflection to go find
    /// a field on an object and set the value at runtime. The intent is 
    /// to use this sparingly because the performance isn't great. I see
    /// this approach as something useful for things like unit testing.</para>
    /// </remarks>
    public static void SetFieldValue(
        this object obj,
        string fieldName,
        object value,
        bool includeProtected = false
        )
    {
        Guard.Instance().ThrowIfNull(obj, nameof(obj))
            .ThrowIfNullOrEmpty(fieldName, nameof(fieldName));

        var type = obj.GetType();

        var fi = type.GetField(
            fieldName,
            BindingFlags.Static |
            BindingFlags.Instance |
            BindingFlags.Public |
            (includeProtected ? BindingFlags.NonPublic : BindingFlags.Public)
            );

        if (fi == null)
        {
            return;
        }

        fi.SetValue(obj, value);
    }

    // *******************************************************************

    /// <summary>
    /// This method reads a property value from the specified object.
    /// </summary>
    /// <param name="obj">The object to use for the operation.</param>
    /// <param name="propertyName">The property to use for the operation.</param>
    /// <param name="includeProtected">Determines if protected properties are 
    /// included in the search.</param>
    /// <returns>The value of the property.</returns>
    /// <remarks>
    /// <para>The idea, with this method, is to use reflection to go find
    /// and return a property value from an object at runtime. The intent is 
    /// to use this sparingly because the performance isn't great. I see
    /// this approach as something useful for things like unit testing.</para>
    /// </remarks>
    public static object? GetPropertyValue(
        this object obj,
        string propertyName,
        bool includeProtected = false
        )
    {
        Guard.Instance().ThrowIfNull(obj, nameof(obj))
            .ThrowIfNullOrEmpty(propertyName, nameof(propertyName));

        var type = obj.GetType();

        var pi = type.GetProperty(
            propertyName,
            BindingFlags.Static |
            BindingFlags.Instance |
            BindingFlags.Public |
            (includeProtected ? BindingFlags.NonPublic : BindingFlags.Public) 
            );

        if (pi is null)
        {
            return null;
        }

        if (!pi.CanRead)
        {
            return null;
        }

        var value = pi?.GetValue(obj, []);

        if (value is null)
        {
            return null;
        }

        var valType = value.GetType();

        if (valType == typeof(WeakReference))
        {
            return (value as WeakReference)?.Target;
        }

        return value;
    }

    // *******************************************************************

    /// <summary>
    /// This method reads a property value from the specified object.
    /// </summary>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="obj">The object to use for the operation.</param>
    /// <param name="propertyName">The property to use for the operation.</param>
    /// <param name="includeProtected">Determines if protected properties are 
    /// included in the search.</param>
    /// <returns>The value of the property.</returns>
    /// <remarks>
    /// <para>The idea, with this method, is to use reflection to go find
    /// and return a property value from an object at runtime. The intent is 
    /// to use this sparingly because the performance isn't great. I see
    /// this approach as something useful for things like unit testing.</para>
    /// </remarks>
    public static T? GetPropertyValue<T>(
        this object obj,
        string propertyName,
        bool includeProtected = false
        ) where T : class
    {
        Guard.Instance().ThrowIfNull(obj, nameof(obj))
            .ThrowIfNullOrEmpty(propertyName, nameof(propertyName));

        var type = obj.GetType();

        var pi = type.GetProperty(
            propertyName,
            BindingFlags.Static |
            BindingFlags.Instance |
            BindingFlags.Public |
            (includeProtected ? BindingFlags.NonPublic : BindingFlags.Public)
            );

        if (pi == null)
        {
            return default;
        }

        if (!pi.CanRead)
        {
            return default;
        }

        var value = pi.GetValue(obj, []);

        var valType = value?.GetType();

        if (valType == typeof(WeakReference))
        {
            return (value as WeakReference)?.Target as T;
        }

        if (valType == typeof(WeakReference<T>))
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if ((value as WeakReference<T>).TryGetTarget(out T? target))
            {
                return target;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        return (T?)value;
    }

    // *******************************************************************

    /// <summary>
    /// This method writes a property value on the specified object.
    /// </summary>
    /// <param name="obj">The object to use for the operation.</param>
    /// <param name="propertyName">The property to use for the operation.</param>
    /// <param name="value">The value to use for the operation.</param>
    /// <param name="includeProtected">Determines if protected properties are 
    /// included in the search.</param>
    /// <remarks>
    /// <para>The idea, with this method, is to use reflection to go find
    /// a property on an object and set the value at runtime. The intent is 
    /// to use this sparingly because the performance isn't great. I see
    /// this approach as something useful for things like unit testing.</para>
    /// </remarks>
    /// <remarks>
    /// <para>The idea, with this method, is to use reflection to go find
    /// a property on an object and set the value at runtime. The intent is 
    /// to use this sparingly because the performance isn't great. I see
    /// this approach as something useful for things like unit testing.</para>
    /// </remarks>
    public static void SetPropertyValue(
        this object obj,
        string propertyName,
        object value,
        bool includeProtected = false
        )
    {
        Guard.Instance().ThrowIfNull(obj, nameof(obj))
            .ThrowIfNullOrEmpty(propertyName, nameof(propertyName));

        var type = obj.GetType();

        var pi = type.GetProperty(
            propertyName,
            BindingFlags.Static |
            BindingFlags.Instance |
            BindingFlags.Public |
            (includeProtected ? BindingFlags.NonPublic : BindingFlags.Public)
            );

        if (pi == null)
        {
            return;
        }

        if (!pi.CanWrite)
        {
            return;
        }

        pi.SetValue(obj, (object)value);
    }

    #endregion
}
