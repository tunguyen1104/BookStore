using BookStore.Application.DTOs;
using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;

namespace BookStore.Application.Services.Impl
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        public BookService(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<Book?> GetByIdAsync(long id)
        {
            return await _unitOfWork.Books.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _unitOfWork.Books.GetAllAsync();

        }
        public int getTotal()
        {
            return _unitOfWork.Books.count();
        }

        public IEnumerable<BookDto> findAll()
        {
            var books = _unitOfWork.Books.GetAllAsync().Result;
            var bookList = books.Select(book => new BookDto
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                DetailDesc = book.DetailDesc,
                Factory = book.Factory,
                Image = book.Image,
                Price = book.Price,
                Quantity = book.Quantity,
                ShortDesc = book.ShortDesc,
                Sold = book.Sold,
                Discount = book.Discount,
                CategoryIds = book.Categories.Select(c => c.Id).ToList()
            });
            return bookList;
        }

        public (IEnumerable<BookDto> Books, int TotalCount) Find(string search, int page, int pageSize)
        {
            if (page < 1) page = 1;
            IQueryable<Book> booksQuery = _unitOfWork.Books.GetAll();
            // Apply filtering if search term is provided
            if (!string.IsNullOrWhiteSpace(search))
            {
                booksQuery = booksQuery.Where(book => book.Name.Contains(search) || book.Author.Contains(search));
            }

            // Get the total count of books that match the criteria (for pagination)
            var totalCount = booksQuery.Count();

            // Apply paging
            var books = booksQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Map to BookDto
            var bookDtos = books.Select(book => new BookDto
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                DetailDesc = book.DetailDesc,
                Factory = book.Factory,
                Image = book.Image,
                Price = book.Price,
                Quantity = book.Quantity,
                ShortDesc = book.ShortDesc,
                Sold = book.Sold,
            });

            return (bookDtos, totalCount);
        }

        public IEnumerable<Category> GetAllBookCategories()
        {
            return _unitOfWork.Categories.GetAll();
        }

        public async Task<CartSummaryDto?> HandleGetCartPageAsync()
        {
            User currentUser = _userService.GetCurrentUser();
            if (currentUser != null)
            {
                Cart? cart = await _unitOfWork.Carts.FetchByUserIdAsync(currentUser.Id);
                List<CartDetail> cartDetails = cart == null ? new List<CartDetail>() : cart.CartDetails.ToList();

                double totalPrice = 0;
                double totalDiscount = 0;

                foreach (var cartDetail in cartDetails)
                {
                    totalPrice += (double)(cartDetail.Price * cartDetail.Quantity);
                    var discount = await _unitOfWork.Books.FetchBookDiscountByIdAsync(cartDetail.BookId) / 100;
                    totalDiscount += (double)(cartDetail.Price * discount * cartDetail.Quantity);
                }

                return new CartSummaryDto(totalPrice, totalDiscount, cartDetails);
            }

            return null;
        }
    }
}
