using PhoneWebShop.Domain.Entities;
using System.Threading.Tasks;

namespace PhoneWebShop.Domain.Interfaces
{
    public interface IBrandService
    {
        Task<Brand> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves the ID of a brand by name
        /// </summary>
        /// <param name="name">Name of the brand</param>
        /// <returns>The ID of the brand</returns>
        Task<Brand> GetByName(string name);

        /// <summary>
        /// Returns whether a brand already exists
        /// </summary>
        /// <param name="brand">The brand to match (except Id)</param>
        /// <returns>True if brand exists, false if not</returns>
        bool Exists(Brand brand);

        /// <summary>
        /// Add a brand to the database, and set the Id of the input brand to match Id in DB.
        /// </summary>
        /// <param name="brand">The name of the brand</param>
        Task Add(Brand brand);

        /// <summary>
        /// Check if a brand is in the database. If not, then add it
        /// </summary>
        /// <param name="brand">The name of the brand</param>
        /// <returns>The newly added brand, if it already existed, returns the existing brand.</returns>
        Task<Brand> AddIfNotExists(Brand brand);

        Task DeleteAsync(int id);
    }
}
