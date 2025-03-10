using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Interfaces;
using Library.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class InMemoryBookRepository : IBookRepository
    {
        private readonly List<Book> _books;

        public InMemoryBookRepository()
        {
            _books = new List<Book>()
            {
                new Book(new ISBN("9780316769488"), "The Catcher in the Rye", "J.D. Salinger", BookStatus.OnShelf),
                new Book(new ISBN("9780743273565"), "The Great Gatsby", "F. Scott Fitzgerald", BookStatus.OnShelf),
                new Book(new ISBN("9780061120084"), "To Kill a Mockingbird", "Harper Lee", BookStatus.OnShelf),
                new Book(new ISBN("9780451524935"), "1984", "George Orwell", BookStatus.OnShelf),
                new Book(new ISBN("9780140449136"), "Crime and Punishment", "Fyodor Dostoevsky", BookStatus.OnShelf),
                new Book(new ISBN("9780141439600"), "Pride and Prejudice", "Jane Austen", BookStatus.OnShelf),
                new Book(new ISBN("9780553212419"), "Moby-Dick", "Herman Melville", BookStatus.OnShelf),
                new Book(new ISBN("9780307474278"), "The Road", "Cormac McCarthy", BookStatus.OnShelf),
                new Book(new ISBN("9780452284234"), "Fahrenheit 451", "Ray Bradbury", BookStatus.OnShelf),
                new Book(new ISBN("9780140449266"), "The Brothers Karamazov", "Fyodor Dostoevsky", BookStatus.OnShelf)
            };
        }

        public Task<Book> GetByISBNAsync(ISBN isbn)
        {
            var book = _books.FirstOrDefault(b => b.ISBN.Equals(isbn));
            return Task.FromResult(book);
        }

        public Task<IEnumerable<Book>> GetAllAsync()
        {
            return Task.FromResult((IEnumerable<Book>)_books);
        }

        public async Task<bool> AddAsync(Book book)
        {
            _books.Add(book);
            return true;
        }

        public Task UpdateAsync(Book book)
        {
            var existingBook = _books.FirstOrDefault(b => b.ISBN.Equals(book.ISBN));
            if (existingBook != null)
            {
                _books.Remove(existingBook);
                _books.Add(book);
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Book book)
        {
            _books.Remove(book);
            return Task.CompletedTask;
        }
        public Task<IEnumerable<Book>> GetFilteredAndPagedAsync(string? titleFilter = null, string? authorFilter = null, int pageNumber = 1, int pageSize = 10)
        {
            var filteredBooks = _books.AsQueryable();

            if (!string.IsNullOrEmpty(titleFilter))
            {
                filteredBooks = filteredBooks.Where(b => b.Title.Contains(titleFilter, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(authorFilter))
            {
                filteredBooks = filteredBooks.Where(b => b.Author.Contains(authorFilter, StringComparison.OrdinalIgnoreCase));
            }

            var pagedBooks = filteredBooks
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Task.FromResult((IEnumerable<Book>)pagedBooks);
        }
    }
}
