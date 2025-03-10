using FluentAssertions;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.ValueObjects;
using Library.Infrastructure.Repositories;
using Xunit;

namespace Library.UnitTests.Infrastrucure
{
    public class InMemoryBookRepositoryTests
    {
        private readonly InMemoryBookRepository _repository;

        public InMemoryBookRepositoryTests()
        {
            _repository = new InMemoryBookRepository();
        }

        [Fact]
        public async Task GetByISBNAsync_ShouldReturnBook_WhenBookExists()
        {
            // Arrange
            var isbn = new ISBN("9780316769488");

            // Act
            var result = await _repository.GetByISBNAsync(isbn);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be("The Catcher in the Rye");
        }

        [Fact]
        public async Task GetByISBNAsync_ShouldReturnNull_WhenBookDoesNotExist()
        {
            // Arrange
            var isbn = new ISBN("9999999999999");

            // Act
            var result = await _repository.GetByISBNAsync(isbn);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetFilteredAndPagedAsync_ShouldReturnFilteredBooks_ByTitle()
        {
            // Act
            var result = await _repository.GetFilteredAndPagedAsync(titleFilter: "1984");

            // Assert
            result.Should().HaveCount(1);
            result.First().Title.Should().Be("1984");
        }

        [Fact]
        public async Task GetFilteredAndPagedAsync_ShouldReturnPagedResults()
        {
            // Act
            var result = await _repository.GetFilteredAndPagedAsync(pageNumber: 1, pageSize: 5);

            // Assert
            result.Should().HaveCount(5);
        }

        [Fact]
        public async Task AddAsync_ShouldAddBookToRepository()
        {
            // Arrange
            var newBook = new Book(new ISBN("9781234567897"), "New Book", "New Author", BookStatus.OnShelf);

            // Act
            await _repository.AddAsync(newBook);
            var result = await _repository.GetByISBNAsync(newBook.ISBN);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be("New Book");
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateExistingBook()
        {
            // Arrange
            var isbn = new ISBN("9780316769488");
            var updatedBook = new Book(isbn, "Updated Title", "Updated Author", BookStatus.OnShelf);

            // Act
            await _repository.UpdateAsync(updatedBook);
            var result = await _repository.GetByISBNAsync(isbn);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be("Updated Title");
            result.Author.Should().Be("Updated Author");
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveBookFromRepository()
        {
            // Arrange
            var isbn = new ISBN("9780316769488");
            var bookToDelete = await _repository.GetByISBNAsync(isbn);

            // Act
            await _repository.DeleteAsync(bookToDelete);
            var result = await _repository.GetByISBNAsync(isbn);

            // Assert
            result.Should().BeNull();
        }
    }
}
