using PhoneWebShop.Domain.Exceptions;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Entities;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PhoneWebShop.Business.Services
{
    public class SqlBrandService : IBrandService
    {
        private readonly IRepository<Brand> _repository;
        private readonly ICaching _caching;

        public SqlBrandService(IRepository<Brand> repository, ICaching caching)
        {
            _repository = repository;
            _caching = caching;
        }

        public Task Add(Brand brand)
        {
            if (Exists(brand))
                throw new DatabaseException($"Brand {brand} already exists in the database.");

            return _caching.GetOrAdd($"singlebrand_id_{brand.Id}", () => _repository.CreateAsync(brand));
        }

        public async Task<Brand> AddIfNotExists(Brand brand)
        {
            if (!Exists(brand))
            {
                await Add(brand);
                return brand;
            }
            else
                return await GetByName(brand.Name);
        }

        public async Task DeleteAsync(int id)
        {
            if (await GetByIdAsync(id) == null)
                return;
            await _repository.DeleteAsync(id);
        }

        public bool Exists(Brand brand)
        {
            return _caching.GetOrAdd($"singlebrandexists_id_{brand.Id}", () => Task.FromResult(_repository.GetAll()
                .Where(x => x.Name == brand.Name)
                .Any())).Result;
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            return await _caching.GetOrAdd($"singlebrand_id_{id}", () => _repository.GetByIdAsync(id));
        }

        public async Task<Brand> GetByName(string name)
        {
            return await _caching.GetOrAdd($"singlebrand_name_{name}", () => _repository.GetAll()
                .Where(x => x.Name.Equals(name))
                .FirstOrDefaultAsync());
        }
    }
}
