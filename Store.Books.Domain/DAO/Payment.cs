using Store.Books.Domain.Base;
using System;

namespace Store.Books.Domain
{
    public class Payment : BaseDateEntity
    {
        public string Command { get; set; }
        public string Account { get; set; }
        public string TxnId { get; set; }
        public string TxnDate { get; set; }
        public decimal Sum { get; set; }
    }
}
