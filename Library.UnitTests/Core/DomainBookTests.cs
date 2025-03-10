using FluentAssertions;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.UnitTests.Domain
{
    public class DomainBookTests
    {
        [Fact]
        public void SetStatus_ShouldThrowException_WhenInvalidStatusChangeIsAttempted()
        {
            // Arrange
            var book = new Book(new ISBN("9780316769488"), "The Catcher in the Rye", "J.D. Salinger", BookStatus.Borrowed);

            // Act
            var act = () => book.ChangeStatus(BookStatus.OnShelf);

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage("Invalid status transition.");
            //book.Status.Should().Be(BookStatus.OnShelf);
        }

        [Fact]
        public void SetStatus_ShouldChangeBookStatus_WhenValidStatusIsSet()
        {
            // Arrange
            var book = new Book(new ISBN("9780316769488"), "The Catcher in the Rye", "J.D. Salinger", BookStatus.Borrowed);

            // Act
            book.ChangeStatus(BookStatus.Returned);

            // Assert
            book.Status.Should().Be(BookStatus.Returned);
        }
    }
}
