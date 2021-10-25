using Store.Books.Domain.Base;

namespace Store.Books.Domain
{
    public class BookAuthor : BaseEntity
    {
        public Book Book { get; set; }
        public Author Author { get; set; }
    }
}
