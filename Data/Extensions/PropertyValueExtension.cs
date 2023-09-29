using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data.Extensions
{
    internal static class PropertyValueExtension
    {
        internal static Dictionary<string, object?> ToDictionary(this PropertyValues propertyValues) => propertyValues.Properties.ToDictionary(property => property.Name, property => propertyValues[property.Name]);
    }
}
