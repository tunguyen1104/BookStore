using BookStore.Application.Services;
using BookStore.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilterController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IUnitOfWork _supplierRepository;
        public FilterController(ICategoryService categoryService, IUnitOfWork supplierRepository)
        {
            _categoryService = categoryService;
            _supplierRepository = supplierRepository;
        }
        [HttpGet("genres")]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _categoryService.GetAllAsync();
            return Ok(genres);
        }
        [HttpGet("factories")]
        public async Task<IActionResult> GetFactory()
        {
            var factories = await _supplierRepository.Suppliers.GetAllAsync();
            return Ok(factories);
        }
    }
}


