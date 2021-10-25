using Store.Books.Domain.Base;

namespace Store.Books.Domain
{
    public class Genre : BaseEntity
    {
        public Genre Parent { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
