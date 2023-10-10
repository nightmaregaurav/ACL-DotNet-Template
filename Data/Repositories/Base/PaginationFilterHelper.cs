using System.Linq.Expressions;
using System.Reflection;
using Data.Repositories.Base.PaginationModels;

namespace Data.Repositories.Base
{
    internal static class PaginationFilterHelper
    {
        public static IEnumerable<FilterType> GetValidFiltersForType(Type type)
        {
            FilterType[] validFilterTypes;

            if (type == typeof(int)) validFilterTypes = FilterTypeValidator.NumberFilters;
            else if (type == typeof(long)) validFilterTypes = FilterTypeValidator.NumberFilters;
            else if (type == typeof(DateOnly)) validFilterTypes = FilterTypeValidator.DateTimeFilters;
            else if (type == typeof(TimeOnly)) validFilterTypes = FilterTypeValidator.DateTimeFilters;
            else if (type == typeof(DateTime)) validFilterTypes = FilterTypeValidator.DateTimeFilters;
            else if (type == typeof(bool)) validFilterTypes = FilterTypeValidator.BooleanFilters;
            else validFilterTypes = FilterTypeValidator.StringFilters;

            return validFilterTypes;
        }

        public static IQueryable<T> FilterBasedOnFilterTypeAndPropertyType<T>(this IQueryable<T> queryable, Type propertyType, Expression<Func<T, object?>> propertyAccessor, FilterType filterType, string filterQuery)
        {
            Expression<Func<T, bool>> predicate;
            switch (filterType)
            {
                case FilterType.Contains:
                    predicate = t => ((string)(propertyAccessor.Call()(t) ?? "")).ToUpperInvariant().Contains(filterQuery.ToUpperInvariant());
                    queryable = queryable.Where(predicate.AsTranslatableExpression());
                    break;
                case FilterType.NotContains:
                    predicate = t => !((string)(propertyAccessor.Call()(t) ?? "")).ToUpperInvariant().Contains(filterQuery.ToUpperInvariant());
                    queryable = queryable.Where(predicate.AsTranslatableExpression());
                    break;
                case FilterType.StartsWith:
                    predicate = t => ((string)(propertyAccessor.Call()(t) ?? "")).ToUpperInvariant().StartsWith(filterQuery.ToUpperInvariant());
                    queryable = queryable.Where(predicate.AsTranslatableExpression());
                    break;
                case FilterType.EndsWith:
                    predicate = t => ((string)(propertyAccessor.Call()(t) ?? "")).ToUpperInvariant().EndsWith(filterQuery.ToUpperInvariant());
                    queryable = queryable.Where(predicate.AsTranslatableExpression());
                    break;
                case FilterType.Equals:
                    if (propertyType == typeof(int))
                    {
                        int? intQuery = int.TryParse(filterQuery, out var intValue) ? intValue : null;
                        predicate = t => (int?)(propertyAccessor.Call()(t) ?? null) == intQuery;
                    }
                    else if (propertyType == typeof(long))
                    {
                        long? longQuery = long.TryParse(filterQuery, out var longValue) ? longValue : null;
                        predicate = t => (long?)(propertyAccessor.Call()(t) ?? null) == longQuery;
                    }
                    else if (propertyType == typeof(DateOnly)) {
                        DateOnly? dateQuery = DateOnly.TryParse(filterQuery, out var dateValue) ? dateValue : null;
                        predicate = t => (DateOnly?)(propertyAccessor.Call()(t) ?? null) == dateQuery;
                    }
                    else if (propertyType == typeof(TimeOnly))
                    {
                        TimeOnly? timeQuery = TimeOnly.TryParse(filterQuery, out var timeValue) ? timeValue : null;
                        predicate = t => (TimeOnly?)(propertyAccessor.Call()(t) ?? null) == timeQuery;
                    }
                    else if (propertyType == typeof(DateTime))
                    {
                        DateTime? dateTimeQuery = DateTime.TryParse(filterQuery, out var dateTimeValue) ? dateTimeValue : null;
                        predicate = t => (DateTime?)(propertyAccessor.Call()(t) ?? null) == dateTimeQuery;
                    }
                    else if (propertyType == typeof(bool))
                    {
                        bool? booleanQuery = bool.TryParse(filterQuery, out var booleanValue) ? booleanValue : null;
                        predicate = t => (bool?)(propertyAccessor.Call()(t) ?? null) == booleanQuery;
                    }
                    else
                        predicate = t => ((string)(propertyAccessor.Call()(t) ?? "")).ToUpperInvariant() == filterQuery.ToUpperInvariant();

                    queryable = queryable.Where(predicate.AsTranslatableExpression());
                    break;
                case FilterType.NotEquals:
                    if (propertyType == typeof(int))
                    {
                        int? intQuery = int.TryParse(filterQuery, out var intValue) ? intValue : null;
                        predicate = t => (int?)(propertyAccessor.Call()(t) ?? null) != intQuery;
                    }
                    else if (propertyType == typeof(long))
                    {
                        long? longQuery = long.TryParse(filterQuery, out var longValue) ? longValue : null;
                        predicate = t => (long?)(propertyAccessor.Call()(t) ?? null) != longQuery;
                    }
                    else if (propertyType == typeof(DateOnly)) {
                        DateOnly? dateQuery = DateOnly.TryParse(filterQuery, out var dateValue) ? dateValue : null;
                        predicate = t => (DateOnly?)(propertyAccessor.Call()(t) ?? null) != dateQuery;
                    }
                    else if (propertyType == typeof(TimeOnly))
                    {
                        TimeOnly? timeQuery = TimeOnly.TryParse(filterQuery, out var timeValue) ? timeValue : null;
                        predicate = t => (TimeOnly?)(propertyAccessor.Call()(t) ?? null) != timeQuery;
                    }
                    else if (propertyType == typeof(DateTime))
                    {
                        DateTime? dateTimeQuery = DateTime.TryParse(filterQuery, out var dateTimeValue) ? dateTimeValue : null;
                        predicate = t => (DateTime?)(propertyAccessor.Call()(t) ?? null) != dateTimeQuery;
                    }
                    else if (propertyType == typeof(bool))
                    {
                        bool? booleanQuery = bool.TryParse(filterQuery, out var booleanValue) ? booleanValue : null;
                        predicate = t => (bool?)(propertyAccessor.Call()(t) ?? null) != booleanQuery;
                    }
                    else
                        predicate = t => ((string)(propertyAccessor.Call()(t) ?? "")).ToUpperInvariant() != filterQuery.ToUpperInvariant();

                    queryable = queryable.Where(predicate.AsTranslatableExpression());
                    break;
                case FilterType.GreaterThan when double.TryParse(filterQuery, out var number):
                    predicate = t => (double)(propertyAccessor.Call()(t) ?? 0) > number;
                    queryable = queryable.Where(predicate.AsTranslatableExpression());
                    break;
                case FilterType.GreaterThan:
                    return queryable;
                case FilterType.GreaterThanOrEqual when double.TryParse(filterQuery, out var number):
                    predicate = t => (double)(propertyAccessor.Call()(t) ?? 0) >= number;
                    queryable = queryable.Where(predicate.AsTranslatableExpression());
                    break;
                case FilterType.GreaterThanOrEqual:
                    return queryable;
                case FilterType.LessThan when double.TryParse(filterQuery, out var number):
                    predicate = t => (double)(propertyAccessor.Call()(t) ?? 0) < number;
                    queryable = queryable.Where(predicate.AsTranslatableExpression());
                    break;
                case FilterType.LessThan:
                    return queryable;
                case FilterType.LessThanOrEqual when double.TryParse(filterQuery, out var number):
                    predicate = t => (double)(propertyAccessor.Call()(t) ?? 0) <= number;
                    queryable = queryable.Where(predicate.AsTranslatableExpression());
                    break;
                case FilterType.LessThanOrEqual:
                    return queryable;
                case FilterType.In:
                {
                    var split = filterQuery.Split(',');
                    if (split.Length == 0) return queryable;

                    split = split.Select(x => x.Trim()).ToArray();
                    predicate = t => split.Contains((string)(propertyAccessor.Call()(t) ?? ""));
                    queryable = queryable.Where(predicate.AsTranslatableExpression());
                    break;
                }
                case FilterType.NotIn:
                {
                    var split = filterQuery.Split(',');
                    if (split.Length == 0) return queryable;

                    split = split.Select(x => x.Trim()).ToArray();
                    predicate = t => !split.Contains((string)(propertyAccessor.Call()(t) ?? ""));
                    queryable = queryable.Where(predicate.AsTranslatableExpression());
                    break;
                }
                case FilterType.Between:
                {
                    var split = filterQuery.Split(',');
                    if (split.Length != 2) return queryable;

                    if (double.TryParse(split[0].Trim(), out var first) && double.TryParse(split[1].Trim(), out var second))
                    {
                        predicate = t => (double)(propertyAccessor.Call()(t) ?? 0) >= first && (double)(propertyAccessor.Call()(t) ?? 0) <= second;
                        queryable = queryable.Where(predicate.AsTranslatableExpression());
                    }
                    else
                        return queryable;

                    break;
                }
                case FilterType.NotBetween:
                {
                    var split = filterQuery.Split(',');
                    if (split.Length != 2) return queryable;

                    if (double.TryParse(split[0].Trim(), out var first) && double.TryParse(split[1].Trim(), out var second))
                    {
                        predicate = t => (double)(propertyAccessor.Call()(t) ?? 0) < first || (double)(propertyAccessor.Call()(t) ?? 0) > second;
                        queryable = queryable.Where(predicate.AsTranslatableExpression());
                    }
                    else
                        return queryable;

                    break;
                }
                case FilterType.IsTrue:
                    predicate = t => ((bool?)propertyAccessor.Call()(t) ?? null) == true;
                    queryable = queryable.Where(predicate.AsTranslatableExpression());
                    break;
                case FilterType.IsFalse:
                    predicate = t => ((bool?)propertyAccessor.Call()(t) ?? null) == false;
                    queryable = queryable.Where(predicate.AsTranslatableExpression());
                    break;
                case FilterType.IsTrueOrNull:
                    predicate = t => ((bool?)propertyAccessor.Call()(t) ?? true) == true;
                    queryable = queryable.Where(predicate.AsTranslatableExpression());
                    break;
                case FilterType.IsFalseOrNull:
                    predicate = t => ((bool?)propertyAccessor.Call()(t) ?? false) == false;
                    queryable = queryable.Where(predicate.AsTranslatableExpression());
                    break;
                case FilterType.IsNull:
                    predicate = t => propertyAccessor.Call()(t) == null;
                    queryable = queryable.Where(predicate.AsTranslatableExpression());
                    break;
                case FilterType.IsNotNull:
                    predicate = t => propertyAccessor.Call()(t) != null;
                    queryable = queryable.Where(predicate.AsTranslatableExpression());
                    break;
                default:
                    throw new PaginationFilterNotSupportedException(filterType.ToString());
            }

            return queryable;
        }

        private class PaginationFilterNotSupportedException(string filter) : Exception("The filter " + filter + " is not supported.");
    }

    public abstract class FilterTypeValidator
    {
        public static readonly FilterType[] StringFilters = {
            FilterType.Contains,
            FilterType.NotContains,
            FilterType.StartsWith,
            FilterType.EndsWith,
            FilterType.Equals,
            FilterType.NotEquals,
            FilterType.In,
            FilterType.NotIn,
            FilterType.IsNull,
            FilterType.IsNotNull
        };

        public static readonly FilterType[] NumberFilters = {
            FilterType.Equals,
            FilterType.NotEquals,
            FilterType.GreaterThan,
            FilterType.GreaterThanOrEqual,
            FilterType.LessThan,
            FilterType.LessThanOrEqual,
            FilterType.In,
            FilterType.NotIn,
            FilterType.Between,
            FilterType.NotBetween,
            FilterType.IsNull,
            FilterType.IsNotNull
        };

        public static readonly FilterType[] DateTimeFilters = {
            FilterType.Equals,
            FilterType.NotEquals,
            FilterType.GreaterThan,
            FilterType.GreaterThanOrEqual,
            FilterType.LessThan,
            FilterType.LessThanOrEqual,
            FilterType.Between,
            FilterType.NotBetween,
            FilterType.IsNull,
            FilterType.IsNotNull
        };

        public static readonly FilterType[] BooleanFilters = {
            FilterType.Equals,
            FilterType.NotEquals,
            FilterType.IsTrue,
            FilterType.IsFalse,
            FilterType.IsTrueOrNull,
            FilterType.IsFalseOrNull,
            FilterType.IsNull,
            FilterType.IsNotNull
        };
    }


    public static class ExpressionExtension
    {
        public static TFunc Call<TFunc>(this Expression<TFunc> _) => throw new InvalidOperationException("This method should never be called. It is a marker for constructing filter expressions.");
        public static Expression<TFunc> AsTranslatableExpression<TFunc>(this Expression<TFunc> expression)
        {
            var visitor = new SubstituteExpressionCallVisitor();
            return (Expression<TFunc>)visitor.Visit(expression);
        }
    }

    public class SubstituteExpressionCallVisitor : ExpressionVisitor
    {
        private readonly MethodInfo _markerDescription = typeof(ExpressionExtension).GetMethod(nameof(ExpressionExtension.Call))!.GetGenericMethodDefinition();

        protected override Expression VisitInvocation(InvocationExpression node)
        {
            if (node.Expression.NodeType != ExpressionType.Call || !IsMarker((MethodCallExpression)node.Expression)) return base.VisitInvocation(node);

            var parameterReplacer = new SubstituteParameterVisitor(node.Arguments.ToArray(),
                Unwrap((MethodCallExpression)node.Expression));
            var target = parameterReplacer.Replace();
            return Visit(target);
        }

#pragma warning disable CA1822
        private LambdaExpression Unwrap(MethodCallExpression node)
#pragma warning restore CA1822
        {
            var target = node.Arguments[0];
            return (LambdaExpression)Expression.Lambda(target).Compile().DynamicInvoke()!;
        }

        private bool IsMarker(MethodCallExpression node) => node.Method.IsGenericMethod && node.Method.GetGenericMethodDefinition() == _markerDescription;
    }

    public class SubstituteParameterVisitor : ExpressionVisitor
    {
        private readonly LambdaExpression _expressionToVisit;
        private readonly Dictionary<ParameterExpression, Expression> _substitutionByParameter;

        public SubstituteParameterVisitor(Expression[] parameterSubstitutions, LambdaExpression expressionToVisit)
        {
            _expressionToVisit = expressionToVisit;
            _substitutionByParameter = expressionToVisit.Parameters
                .Select(
                    (parameter, index) =>
                        new { Parameter = parameter, Index = index })
                .ToDictionary(pair => pair.Parameter,
                    pair => parameterSubstitutions[pair.Index]);
        }

        public Expression Replace() => Visit(_expressionToVisit.Body);
        protected override Expression VisitParameter(ParameterExpression node) => _substitutionByParameter.TryGetValue(node, out var substitution) ? Visit(substitution) : base.VisitParameter(node);
    }
}
