using FluentAssertions;
using Library.API.Controllers;
using Library.API.DTOs;
using Library.Application.Books.Command.ChangeBookStatus;
using Library.Application.Books.Command.CreateBook;
using Library.Application.Books.Command.DeleteBook;
using Library.Application.Books.Command.UpdateBook;
using Library.Application.Books.Queries.GetAllBooks;
using Library.Domain.Enums;
using Library.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.UnitTests.Controllers
{
    public class BooksControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly BooksController _controller;

        public BooksControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new BooksController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetBooks_ShouldReturnOkResult_WhenBooksAreFound()
        {
            // Arrange
            var books = new List<BookDto>
            {
                new BookDto (new ISBN("9780316769488"),"The Catcher in the Rye","J.D. Salinger", BookStatus.OnShelf ),
                new BookDto (new ISBN("9780451524935"),"1984","George Orwell", BookStatus.OnShelf ),
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllBooksQuery>(), default)).ReturnsAsync(books);

            // Act
            var result = await _controller.GetBooks(1, 10, "", "", BookOrderBy.ISBN, true);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(books);
        }

        [Fact]
        public async Task CreateBook_ShouldReturnOkResult_WhenBookIsCreated()
        {
            // Arrange
            var createCommand = new CreateBookCommand
            {
                Title = "New Book",
                Author = "New Author",
                ISBN = new ISBN("9781234567897")
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateBookCommand>(), default)).ReturnsAsync(true);

            // Act
            var result = await _controller.CreateBook(createCommand);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().Be(true);
        }

        [Fact]
        public async Task ChangeBookStatus_ShouldReturnNoContent_WhenStatusIsChanged()
        {
            // Arrange
            var isbn = "9780316769488";
            var newStatus = BookStatus.OnShelf;

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateBookStatusCommand>(), default)).ReturnsAsync(true);

            // Act
            var result = await _controller.ChangeBookStatus(isbn, newStatus);

            // Assert
            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task UpdateBook_ShouldReturnNoContent_WhenBookIsUpdated()
        {
            // Arrange
            var isbn = "9780316769488";
            var updateDto = new UpdateBookDto
            {
                NewTitle = "Updated Title",
                NewAuthor = "Updated Author"
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateBookCommand>(), default)).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateBook(isbn, updateDto);

            // Assert
            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteBook_ShouldReturnOkResult_WhenBookIsDeleted()
        {
            // Arrange
            var isbn = "9780316769488";

            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteBookCommand>(), default)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteBook(isbn);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().Be(true);
        }
    }
}
