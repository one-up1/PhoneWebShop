using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Interfaces;
using System.Threading.Tasks;

namespace PhoneWebShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BrandsController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateBrand(Brand newBrand)
        {
            await _brandService.Add(newBrand);
            
            return Ok(new {success = true, newBrandObject = newBrand});
        }

        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            await _brandService.DeleteAsync(id);
            
            return Ok(new { success = true });
        }

        [HttpGet]
        [Route("Get")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBrand(int id)
        {
            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null)
                return NotFound(new { error = $"No phone found with id = {id}" });
            return Ok(brand);
        }
    }
}
