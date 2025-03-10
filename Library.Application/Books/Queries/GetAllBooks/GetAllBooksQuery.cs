using Library.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Books.Queries.GetAllBooks
{
    public class GetAllBooksQuery : IRequest<IEnumerable<BookDto>>
    {
        public int Page { get; }
        public int PageSize { get; }
        public string? TitleFilter { get; }
        public string? AuthorFilter { get; }
        public BookOrderBy? OrderBy { get; set; }
        public bool? Asc { get; set; }

        public GetAllBooksQuery(int page, int pageSize, string? titleFilter, string? authorFilter)
        {
            Page = page;
            PageSize = pageSize;
            TitleFilter = titleFilter;
            AuthorFilter = authorFilter;
        }
    }
}
