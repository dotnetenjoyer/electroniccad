using System.Globalization;

namespace ElectronicCad.Domain.Storage;

/// <summary>
/// The dictionary for storage entities data.
/// </summary>
public class StorageDictionary : SortedDictionary<string, object?>
{
    private readonly CultureInfo _defaultCulture = CultureInfo.InvariantCulture;

    /// <summary>
    /// Find value by key and try cast to a given type.
    /// </summary>
    /// <typeparam name="T">Type for cast.</typeparam>
    /// <param name="key">Key.</param>
    /// <returns>Casted value.</returns>
    public T Parse<T>(string key)
    {
        if(!TryGetValue(key, out var value))
        {
            throw new KeyNotFoundException();
        }

        if (value == null)
        {
            return default;
        }

        var type = GetTargetType(typeof(T));

        if(type == value.GetType())
        {
            return (T)value;
        }

        if (!IsPrimitive(type))
        {
            throw new InvalidOperationException("Storage dictionary contains only primitive types.");
        }

        if (typeof(Guid).IsAssignableFrom(typeof(T)))
        {
            var stringValue = value.ToString();
            return (T)(object)Guid.Parse(stringValue);
        }

        // Converts to enum.
        if (type.IsEnum)
        {
            return (T)Enum.Parse(type, value.ToString()!, ignoreCase: true);
        }

        // Converts to primitive types.
        if(value is IConvertible)
        {
            return (T)((IConvertible)value).ToType(type, _defaultCulture);
        }

        // Try convert with system converter.
        return (T)Convert.ChangeType(value, type);
    }

    private Type GetTargetType(Type type)
    {
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            var underlyingType = Nullable.GetUnderlyingType(type);

            if (underlyingType != null)
            {
                return underlyingType;
            }
        }

        return type;
    }

    private bool IsPrimitive(Type type)
    {
        Type[] primitiveTypes = new[]
        {
            typeof(string),
            typeof(decimal),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
            typeof(Guid)
        };

        return type.IsPrimitive
            || type.IsEnum
            || primitiveTypes.Contains(type)
            || Convert.GetTypeCode(type) != TypeCode.Object
            || (type.IsGenericType
                && type.GetGenericTypeDefinition() == typeof(Nullable<>)
                && IsPrimitive(Nullable.GetUnderlyingType(type)));
    }
}
