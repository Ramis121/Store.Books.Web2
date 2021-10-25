using Store.Books.Domain.Base;

namespace Store.Books.Domain
{
    public class BookGenre : BaseEntity
    {
        public Book Book { get; set; }
        public Genre Genre { get; set; }
    }
}
