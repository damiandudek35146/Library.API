using Library.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Books.Command.ChangeBookStatus
{
    public class UpdateBookStatusCommandHandler : IRequestHandler<UpdateBookStatusCommand, bool>
    {
        private readonly IBookRepository _repository;

        public UpdateBookStatusCommandHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateBookStatusCommand request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetByISBNAsync(request.ISBN);
            if (book == null)
            {
                throw new Exception("Book not found.");
            }

            book.ChangeStatus(request.NewStatus);
            await _repository.UpdateAsync(book);

            return true;
        }
    }
}
