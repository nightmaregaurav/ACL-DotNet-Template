using System.Linq.Expressions;

namespace Data.Repositories.Base.PaginationModels
{
    public class PaginationRequestFilterDescriptor<T>
    {
        public string PropertyName { get; set; } = string.Empty;
        public Expression<Func<T, object?>> PropertyAccessor { get; set; }
        public Type PropertyDataType { get; set; }
    }

    public class PaginationResponseFilterDescriptor
    {
        public string PropertyName { get; set; } = string.Empty;
        public IEnumerable<FilterType> AllowedFilterTypes { get; set; } = new List<FilterType>();
    }
}