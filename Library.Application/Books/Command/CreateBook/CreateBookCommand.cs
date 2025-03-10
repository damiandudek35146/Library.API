using Library.Domain.Enums;
using Library.Domain.ValueObjects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Library.Application.Books.Command.CreateBook
{
    public class CreateBookCommand : IRequest<bool>
    {
        public ISBN ISBN { get; set; }
        public BookStatus NewStatus { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
    }
}