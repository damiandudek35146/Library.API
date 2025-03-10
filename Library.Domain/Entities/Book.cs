using Library.Domain.Enums;
using Library.Domain.ValueObjects;

namespace Library.Domain.Entities
{
    public class Book
    {
        public ISBN ISBN { get; }
        public string Title { get; }
        public string Author { get; }
        public BookStatus Status { get; private set; }

        public Book(ISBN isbn, string title, string author, BookStatus status)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
            Status = status;
        }

        // Change book status
        public void ChangeStatus(BookStatus newStatus)
        {
            if (Status.Equals(BookStatus.Borrowed) && newStatus.Equals(BookStatus.Returned))
            {
                Status = newStatus;
            }
            else if (Status.Equals(BookStatus.Returned) && newStatus.Equals(BookStatus.OnShelf))
            {
                Status = newStatus;
            }
            else if (Status.Equals(BookStatus.OnShelf) && newStatus.Equals(BookStatus.Damaged))
            {
                Status = newStatus;
            }
            else if (Status.Equals(BookStatus.OnShelf) && newStatus.Equals(BookStatus.Borrowed))
            {
                Status = newStatus;
            }
            else
            {
                throw new InvalidOperationException("Invalid status transition.");
            }
        }
    }
}
