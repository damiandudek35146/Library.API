using Library.Application.Books.Command.ChangeBookStatus;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Books.Command.UpdateBook
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, bool>
    {
        private readonly IBookRepository _repository;

        public UpdateBookHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetByISBNAsync(request.ISBN);
            if (book == null)
            {
                throw new Exception("Book not found.");
            }

            var updatedBook = new Book(
                book.ISBN,
                request.NewTitle,  // Nowy tytuł
                request.NewAuthor, // Nowy autor
                book.Status         // Zachowanie istniejącego statusu
            );

            await _repository.UpdateAsync(updatedBook);

            return true;
        }
    }
}
