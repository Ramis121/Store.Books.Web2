using System;
using System.ComponentModel.DataAnnotations;

namespace Store.Books.Domain.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
    public abstract class BaseDateEntity : BaseEntity
    {
        public DateTime Created { get; set; }
    }
}
