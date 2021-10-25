using Store.Books.Domain.Base;

namespace Store.Books.Domain
{
    public class Author : BaseEntity
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
    }
}
