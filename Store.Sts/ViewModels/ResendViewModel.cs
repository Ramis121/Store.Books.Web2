using System.ComponentModel.DataAnnotations;

namespace Store.Sts.ViewModels
{
    public class ResendViewModel
    {
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string ResendToken { get; set; }
    }
}
