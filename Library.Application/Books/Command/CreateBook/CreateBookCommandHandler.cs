using Library.Domain.Entities;
using Library.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Books.Command.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, bool>
    {
        private readonly IBookRepository _repository;

        public CreateBookCommandHandler(IBookRepository bookRepository)
        {
            _repository = bookRepository;
        }

        public async Task<bool> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book(new Domain.ValueObjects.ISBN(request.ISBN.Value), request.Title, request.Author, Domain.Enums.BookStatus.OnShelf);

            return await _repository.AddAsync(book);
        }
    }
}
