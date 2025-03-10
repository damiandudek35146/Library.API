using Library.Domain.Enums;
using Library.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Books.Command.DeleteBook
{
    public class DeleteBookCommand : IRequest<bool>
    {
        public ISBN ISBN { get; }

        public DeleteBookCommand(ISBN isbn)
        {
            ISBN = isbn;
        }
    }
}
