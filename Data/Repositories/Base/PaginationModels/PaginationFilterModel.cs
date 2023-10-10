namespace Data.Repositories.Base.PaginationModels
{
    public class PaginationFilterModel
    {
        public string ColumnName { get; set; }
        public SortType? SortType { get; set; }
        public FilterType? FilterType { get; set; }
        public string? FilterQuery { get; set; }
    }
}
