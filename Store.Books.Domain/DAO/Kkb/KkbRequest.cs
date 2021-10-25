using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Store.Books.Domain.DAO.Kkb
{
    public class KkbRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Sign { get; set; }
    }
}
