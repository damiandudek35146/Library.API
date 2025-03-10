using Library.Application.Books.Command.ChangeBookStatus;
using Library.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Books.Command.DeleteBook
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly IBookRepository _repository;

        public DeleteBookHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetByISBNAsync(request.ISBN);
            if (book == null)
            {
                throw new Exception("Book not found.");
            }

            await _repository.DeleteAsync(book);

            return true;
        }
    }
}
