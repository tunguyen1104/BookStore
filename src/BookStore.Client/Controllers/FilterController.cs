using BookStore.Application.DTOs;
using BookStore.Application.Services;
using BookStore.Application.Services.Impl;
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
        private readonly IBookService _bookService;
        public FilterController(ICategoryService categoryService, IUnitOfWork supplierRepository, IBookService bookService)
        {
            _categoryService = categoryService;
            _supplierRepository = supplierRepository;
            _bookService = bookService;
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

        [HttpGet("search")]
        public IActionResult Search([FromQuery] BookFilterDto filter)
        {
            int pageSize = 10;
            if (filter == null)
            {
                return BadRequest("Invalid filter parameters");
            }
            var (books, filterCount) = _bookService.Find(filter, pageSize);
            return Ok(new { books, filterCount, pageSize });
        }
    }
}


