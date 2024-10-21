using BookStore.Application.DTOs;

namespace BookStore.Application.Services
{
    public interface IBookService
    {
        public IEnumerable<BookDto> findAll();
        public IEnumerable<BookDto> find(string search, int page, int pageSize);
        public int getTotal();
    }
}
