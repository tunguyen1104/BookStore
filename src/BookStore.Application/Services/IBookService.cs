using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public interface IBookService
    {
        Task<Book> GetByIdAsync(long id);
        Task<IEnumerable<Book>> GetAllAsync();
    }
}
