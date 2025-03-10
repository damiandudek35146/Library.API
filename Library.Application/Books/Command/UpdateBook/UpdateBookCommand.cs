using Library.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Books.Command.UpdateBook
{
    public class UpdateBookCommand : IRequest<bool>
    {
        public ISBN ISBN { get; set; }
        [Required]
        public string NewTitle { get; set; }
        [Required]
        public string NewAuthor { get; set; }
    }
}
