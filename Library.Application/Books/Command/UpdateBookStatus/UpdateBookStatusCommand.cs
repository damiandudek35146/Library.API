using Library.Domain.Enums;
using Library.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Books.Command.ChangeBookStatus
{
    public class UpdateBookStatusCommand : IRequest<bool>
    {
        public ISBN ISBN { get; }
        public BookStatus NewStatus { get; }

        public UpdateBookStatusCommand(ISBN isbn, BookStatus newStatus)
        {
            ISBN = isbn;
            NewStatus = newStatus;
        }
    }
}
