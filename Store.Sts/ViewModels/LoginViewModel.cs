using System.ComponentModel.DataAnnotations;

namespace Store.Sts.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string PhoneNumber { get; set; }
    }
}
