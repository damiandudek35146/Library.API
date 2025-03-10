using Library.Domain.Enums;
using Library.Domain.ValueObjects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Library.Application.Books.Command.CreateBook
{
    public class CreateBookCommand : IRequest<bool>
    {
        public ISBN ISBN { get; }
        public BookStatus NewStatus { get; }
        [Required]
        public string Title { get; }
        [Required]
        public string Author { get; }
    }
}