using Library.Domain.Entities;
using Library.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> GetByISBNAsync(ISBN isbn);
        Task<IEnumerable<Book>> GetAllAsync();
        Task<bool> AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Book book);
        Task<IEnumerable<Book>> GetFilteredAndPagedAsync(string? titleFilter = null, string? authorFilter = null, int pageNumber = 1, int pageSize = 10);
    }
}
