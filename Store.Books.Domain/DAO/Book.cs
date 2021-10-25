using Store.Books.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Store.Books.Domain
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public int Year { get; set; }
    }
}
