using Store.Books.Domain.Enums;
using Store.Books.Domain.Base;
using System;

namespace Store.Books.Domain
{
    public class Order : BaseDateEntity
    {
        public string UserName { get; set; }
        public OrderStatusEnum Status { get; set; }
        public decimal Total { get; set; }
    }
}
