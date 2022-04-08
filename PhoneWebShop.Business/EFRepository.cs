using Microsoft.EntityFrameworkCore;
using PhoneWebShop.Domain.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneWebShop.Business
{
    [ExcludeFromCodeCoverage]
    public class EFRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly DataContext _dataContext;

        public EFRepository(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dataContext.Set<T>().AddAsync(entity);
            await SaveChanges();

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            T entity = await GetByIdAsync(id);
            _dataContext.Remove(entity);
            await SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return _dataContext.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dataContext.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChanges()
        {
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dataContext.Update(entity);
            await SaveChanges();
        }
    }
}
