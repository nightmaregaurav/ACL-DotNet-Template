using System.Linq.Expressions;
using Data.Models.Entities.Base;
using Data.Repositories.Base.PaginationModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Base
{
    internal class BaseRepository<T> where T : class, IEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;

        public IQueryable<T> Queryable { get; }

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
            Queryable = _table.AsQueryable();
        }

        private void SaveChanges() => _context.SaveChanges();
        private async Task SaveChangesAsync() => await _context.SaveChangesAsync().ConfigureAwait(false);

        public void Insert(T entity)
        {
            _table.Add(entity);
            SaveChanges();
        }

        public async Task InsertAsync(T entity)
        {
            await _table.AddAsync(entity).ConfigureAwait(false);
            await SaveChangesAsync().ConfigureAwait(false);
        }

        public void InsertRange(IEnumerable<T> entities)
        {
            _table.AddRange(entities);
            SaveChanges();
        }

        public async Task InsertRangeAsync(IEnumerable<T> entities)
        {
            await _table.AddRangeAsync(entities).ConfigureAwait(false);
            await SaveChangesAsync().ConfigureAwait(false);
        }

        public void Update(T entity)
        {
            _table.Update(entity);
            SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            _table.Update(entity);
            await SaveChangesAsync().ConfigureAwait(false);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _table.UpdateRange(entities);
            SaveChanges();
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _table.UpdateRange(entities);
            await SaveChangesAsync().ConfigureAwait(false);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
            SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            _table.Remove(entity);
            await SaveChangesAsync().ConfigureAwait(false);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _table.RemoveRange(entities);
            SaveChanges();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _table.RemoveRange(entities);
            await SaveChangesAsync().ConfigureAwait(false);
        }

        public long Count() => Queryable.Count();
        public async Task<long> CountAsync() => await Queryable.CountAsync().ConfigureAwait(false);

        public T? GetById(long id) => _table.Find(id);
        public async Task<T?> GetByIdAsync(long id) => await _table.FindAsync(id).ConfigureAwait(false);

        public ICollection<T> GetAll() => Queryable.ToList();
        public async Task<ICollection<T>> GetAllAsync() => await Queryable.ToListAsync().ConfigureAwait(false);

        public async Task<PaginatedDataResponseModel<T>> GetPaginatedDataAsync(
            PaginatedDataRequestModel model,
            List<PaginationRequestFilterDescriptor<T>> filterDescriptors,
            Expression<Func<T, string, bool>>? predicateForAnyFilter = null,
            Expression<Func<T, bool>>? prePaginationFilter = null,
            IQueryable<T>? customQueryable = null
        )
        {
            var initialQueryable = customQueryable ?? Queryable;
            if (prePaginationFilter != null) initialQueryable = initialQueryable.Where(prePaginationFilter);
            var filteredData = initialQueryable;

            foreach (var filter in model.Filters ?? new List<PaginationFilterModel>())
            {
                if (filter.ColumnName == "Any" && predicateForAnyFilter != null)
                {
                    Expression<Func<T, bool>> fixedPredicate = x => predicateForAnyFilter.Call()(x, filter.FilterQuery ?? "");
                    filteredData = filteredData.Where(fixedPredicate.AsTranslatableExpression());
                    continue;
                }

                var filterDescriptor = filterDescriptors.SingleOrDefault(x => x.PropertyName == filter.ColumnName);
                if (filterDescriptor == null) continue;

                var propertyAccessor = filterDescriptor.PropertyAccessor;

                if (filter.FilterType != null)
                {
                    var propertyType = filterDescriptor.PropertyDataType;
                    var validFilterTypes = PaginationFilterHelper.GetValidFiltersForType(propertyType);
                    if (validFilterTypes.All(x => x != filter.FilterType)) continue;
                    filteredData = filteredData.FilterBasedOnFilterTypeAndPropertyType(propertyType, propertyAccessor, filter.FilterType.Value, filter.FilterQuery ?? "");
                }

                if (filter.SortType != null) filteredData = filter.SortType == SortType.Ascending ? filteredData.OrderBy(propertyAccessor) : filteredData.OrderByDescending(propertyAccessor);
            }

            var totalRecords = await initialQueryable.CountAsync().ConfigureAwait(false);
            var filteredRecords = await filteredData.CountAsync().ConfigureAwait(false);

            var skip = model.PageNumber <= 0 ? 0 : (model.PageNumber - 1) * model.PageSize;
            var take = model.PageSize <= 0 ? 0 : model.PageSize;
            filteredData = filteredData.Skip(skip).Take(take);

            var responseFilterDescriptors = filterDescriptors.Select(x => new PaginationResponseFilterDescriptor
            {
                PropertyName = x.PropertyName,
                AllowedFilterTypes = PaginationFilterHelper.GetValidFiltersForType(x.PropertyDataType)
            });
            responseFilterDescriptors = responseFilterDescriptors.Prepend(new PaginationResponseFilterDescriptor
            {
                PropertyName = "Any",
                AllowedFilterTypes = new List<FilterType> { FilterType.Contains }
            });

            return new PaginatedDataResponseModel<T>
            {
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
                TotalRecords = totalRecords,
                TotalFilteredRecords = filteredRecords,
                Data = filteredData.ToList(),
                FilterDescriptors = responseFilterDescriptors
            };
        }
    }
}
