using Library.Domain.Enums;
using Library.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Books.Queries.GetAllBooks
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<BookDto>>
    {
        private readonly IBookRepository _repository;

        public GetAllBooksQueryHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _repository.GetFilteredAndPagedAsync(request.TitleFilter, request.AuthorFilter, request.Page, request.PageSize);

            bool asc = request.Asc ?? false;
            switch (request.OrderBy)
            {
                case BookOrderBy.ISBN:
                    {
                        if (asc)
                        {
                            books = books.OrderBy(x => x.ISBN.Value);
                            break;
                        }

                        books = books.OrderByDescending(x => x.ISBN.Value);
                    }
                    break;
                case BookOrderBy.Title:
                    {
                        if (asc)
                        {
                            books = books.OrderBy(x => x.Title);
                            break;
                        }

                        books = books.OrderByDescending(x => x.Title);
                    }
                    break;
                case BookOrderBy.Status:
                    {
                        if (asc)
                        {
                            books = books.OrderBy(x => x.Status);
                            break;
                        }

                        books = books.OrderByDescending(x => x.Status);
                    }
                    break;

                case BookOrderBy.Author:
                    {
                        if (asc)
                        {
                            books = books.OrderBy(x => x.Author);
                            break;
                        }

                        books = books.OrderByDescending(x => x.Author);
                    }
                    break;
            }
            return books.Select(b => new BookDto(b.ISBN, b.Title, b.Author, b.Status));
        }
    }
}
