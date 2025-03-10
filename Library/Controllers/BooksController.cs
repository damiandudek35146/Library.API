using Library.API.DTOs;
using Library.Application.Books.Command.ChangeBookStatus;
using Library.Application.Books.Command.CreateBook;
using Library.Application.Books.Command.DeleteBook;
using Library.Application.Books.Command.UpdateBook;
using Library.Application.Books.Queries.GetAllBooks;
using Library.Domain.Enums;
using Library.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Get all books with optional filters and ordering
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(IEnumerable<BookDto>))]
        public async Task<IActionResult> GetBooks(
            int page = 1,
            int pageSize = 10,
            string titleFilter = "",
            string authorTitle = "",
            BookOrderBy orderBy = BookOrderBy.ISBN,
            bool asc = true)
        {
            var query = new GetAllBooksQuery(page, pageSize, titleFilter, authorTitle)
            {
                Asc = asc,
                OrderBy = orderBy
            };
            var books = await _mediator.Send(query);
            return Ok(books);
        }

        [HttpPost]
        [SwaggerResponse(200, Type = typeof(bool))]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand createBookCommand)
        {
            var result = await _mediator.Send(createBookCommand);
            return Ok(result);
        }

        [HttpPost("{isbn}/status")]
        [SwaggerResponse(200, Type = typeof(bool))]
        public async Task<IActionResult> ChangeBookStatus([FromRoute] string isbn, BookStatus newStatus)
        {
            var command = new UpdateBookStatusCommand(new ISBN(isbn), newStatus);
            await _mediator.Send(command);
            return NoContent();
        }
        [HttpPost("{isbn}")]
        [SwaggerResponse(200, Type = typeof(bool))]
        public async Task<IActionResult> UpdateBook([FromRoute] string isbn, [FromBody] UpdateBookDto dto)
        {
            var command = new UpdateBookCommand()
            {
                ISBN = new ISBN(isbn),
                NewTitle = dto.NewTitle,
                NewAuthor = dto.NewAuthor,
            };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        [SwaggerResponse(200, Type = typeof(bool))]
        public async Task<IActionResult> DeleteBook(string ISBN)
        {
            var command = new DeleteBookCommand(new ISBN(ISBN));
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
