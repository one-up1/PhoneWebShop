using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneWebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        private readonly IPhoneService _phoneService;

        public PhonesController(IPhoneService phoneService)
        {
            _phoneService = phoneService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhone(int id)
        {
            var phone = await _phoneService.GetAsync(id);
            return Ok(phone);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> GetPhones(string query)
        {
            IEnumerable<Phone> phones = string.IsNullOrEmpty(query) ? await _phoneService.GetAll() : await _phoneService.Search(query);
            return Ok(phones);
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> Create(Phone phone)
        {
            return await _phoneService.AddAsync(phone) ? CreatedAtAction(nameof(Create), phone) : Conflict("Phone exists, cannot add!");
        }

        [HttpPost("Delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePhone(int id)
        {
            await _phoneService.DeleteAsync(id);

            return Ok(new { success = true });
        }
    }
}
