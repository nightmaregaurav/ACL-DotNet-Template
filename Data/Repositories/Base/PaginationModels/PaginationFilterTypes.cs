namespace Data.Repositories.Base.PaginationModels
{
    public enum SortType
    {
        Ascending,
        Descending,
    }

    public enum FilterType
    {
        Contains,
        NotContains,

        StartsWith,
        EndsWith,

        Equals,
        NotEquals,

        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,

        In,
        NotIn,

        Between,
        NotBetween,

        IsNull,
        IsNotNull,

        IsTrue,
        IsFalse,
        IsTrueOrNull,
        IsFalseOrNull
    }
}
