namespace Data.Repositories.Base.PaginationModels
{
    public class PaginatedDataResponseModel<T>
    {
        public long PageNumber { get; set; }
        public long PageSize { get; set; }
        public long TotalRecords { get; set; }
        public long TotalFilteredRecords { get; set; }
        public IEnumerable<T> Data { get; set; } = new List<T>();
        public IEnumerable<PaginationResponseFilterDescriptor> FilterDescriptors { get; set; } = new List<PaginationResponseFilterDescriptor>();
    }
}
