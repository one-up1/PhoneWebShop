using Microsoft.EntityFrameworkCore;
using PhoneWebShop.Domain.Events;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PhoneWebShop.Domain.Exceptions;
using System.Threading.Tasks;

namespace PhoneWebShop.Business.Services
{

    public class PhoneService : IPhoneService
    {
        private readonly IRepository<Phone> _repository;
        private readonly ILogger _logger;
        private readonly IBrandService _brandService;
        private readonly ICaching _caching;

        public PhoneService(IRepository<Phone> repository, ILogger logger, IBrandService brandService, ICaching caching)
        {
            _repository = repository;
            _logger = logger;
            _brandService = brandService;
            _caching = caching;
        }

        public async Task<bool> AddAsync(Phone phone)
        {
            if (phone.Brand == null)
                return false;

            if (Exists(phone))
            {
                _logger.Info($"Tried adding {phone.FullName} but it was already present.");
                return false;
            }

            phone.Brand = await _brandService.GetByName(phone.Brand.Name) ?? phone.Brand;
            await _repository.CreateAsync(phone);

            var isSuccessfulAdd = phone.Id != default;
            if (isSuccessfulAdd)
            {
                _logger.Info($"Added {phone.FullName}");
            }
            return isSuccessfulAdd;
        }

        public async Task<Phone> GetAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id cannot be smaller than or equal to 0", nameof(id));

            var phone = await _repository.GetByIdAsync(id);
            if (phone is null)
                return phone;
            if (phone.BrandId == default)
                throw new Exception($"{nameof(phone.BrandId)} was not set on this phone, thus making it invalid.");

            phone.Brand = await _brandService.GetByIdAsync(phone.BrandId);
            return phone;
        }

        public async Task<IEnumerable<Phone>> GetAll()
        {
            return await Task.FromResult(GetAllWithBrandSorted().ToList());
        }

        public async Task<IEnumerable<Phone>> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Enumerable.Empty<Phone>(); //TODO: Crasht dit misschien?

            _logger.Info($"Searching for query: \'{query}\'");


            return await Task.FromResult(GetAllWithBrandSorted()
                .Include(x => x.Brand)
                .Where(x => x.Brand.Name.Contains(query)
                || x.Type.Contains(query)
                || x.Description.Contains(query))
                .ToList());


        }

        public async Task UpdateAsync(Phone phone)
        {
            if (phone.Id == 0)
                throw new ArgumentException(nameof(phone), "Phone Id not set! ");

            await _repository.UpdateAsync(phone);
            _logger.Info($"Modified phone: {phone.FullName}");
        }

        public async Task DeleteAsync(int id)
        {
            var phone = await _repository.GetByIdAsync(id);
            if (phone == null)
                throw new DatabaseException($"Trying to delete a phone which does not exist! Id: {id}");

            var name = phone.FullName;
            await _repository.DeleteAsync(id);
            _logger.Info($"Deleted phone {name}");
        }

        private IQueryable<Phone> GetAllWithBrandSorted()
        {
            return _repository.GetAll()
                .Include(x => x.Brand)
                .OrderBy(x => x.Brand.Name)
                .ThenBy(x => x.Type);
        }

        private bool Exists(Phone phone)
        {
            if (phone.Brand == null || string.IsNullOrEmpty(phone.Type))
                return false;

            return _repository.GetAll()
                .Where(x => x.Brand.Name == phone.Brand.Name)
                .Where(x => x.Type == phone.Type)
                .Any();
        }
    }
}
