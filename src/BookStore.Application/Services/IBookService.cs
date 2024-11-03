using BookStore.Application.DTOs;
using BookStore.Domain.Entities;

namespace BookStore.Application.Services
{
    public interface IBookService
    {
        Task<Book?> GetByIdAsync(long id);
        Task<IEnumerable<Book>> GetAllAsync();
        (IEnumerable<BookDto> Books, int TotalCount) Find(string search, int page, int pageSize);
        IEnumerable<Category> GetAllBookCategories();
        int getTotal();
        Task<CartSummaryDto?> HandleGetCartPageAsync();
        Task AddBookToCartAsync(long bookId, long quantity);
        Task HandleRemoveCartDetail(long cartDetailId);
        Task HandleUpdateCartBeforeCheckout(List<CartDetailDto> cartDetailDtos);
        Task<CheckoutDto?> GetUserCheckoutDataAsync();
    }
}
