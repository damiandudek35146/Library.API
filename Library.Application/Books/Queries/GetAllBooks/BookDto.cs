using Library.Domain.Enums;
using Library.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Books.Queries.GetAllBooks
{
    public class BookDto
    {
        public ISBN ISBN { get; }
        public string Title { get; }
        public string Author { get; }
        public BookStatus Status { get; }

        public BookDto(ISBN isbn, string title, string author, BookStatus status)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
            Status = status;
        }
    }
}
