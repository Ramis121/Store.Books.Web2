using Store.Books.Domain.Base;

namespace Store.Books.Domain
{
    public class Busket : BaseEntity
    {
        public Order Order { get; set; }
        public int BookId { get; set; }
        public int PriceId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
