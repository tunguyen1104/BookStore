using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;

namespace BookStore.Application.Services.Impl
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Book> GetByIdAsync(long id)
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
            });
            return bookList;
        }

        public IEnumerable<BookDto> find(string search, int page, int pageSize)
        {
            // Start with an empty queryable
            if (page < 1) page = 1;
            IQueryable<Book> booksQuery = _unitOfWork.Books.GetAll(); // Assuming this returns an IQueryable<Book>

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

            return bookDtos;
        }
    }
}
