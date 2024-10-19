using BookStore.Application.Services;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Client.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly IService<Category> _categoryService;

        public CategoriesViewComponent(IService<Category> categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Category> categories = await _categoryService.GetAllAsync();
            return View(categories);
        }
    }
}
