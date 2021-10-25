using Store.Books.Domain.Base;

namespace Store.Books.Domain
{
    public class Price : BaseDateEntity
    {
        public Book Book { get; set; }
        public decimal Amount { get; set; }
    }
}
