using System.Linq;
using System.Threading.Tasks;

namespace PhoneWebShop.Domain.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<T> CreateAsync(T entity);

        Task<T> GetByIdAsync (int id);

        IQueryable<T> GetAll();

        Task DeleteAsync(int id);

        Task UpdateAsync(T entity);

        Task SaveChanges();
    }
}
