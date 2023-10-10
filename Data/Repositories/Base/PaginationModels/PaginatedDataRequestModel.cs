namespace Data.Repositories.Base.PaginationModels
{
    public class PaginatedDataRequestModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public IEnumerable<PaginationFilterModel>? Filters { get; set; }
    }
}
