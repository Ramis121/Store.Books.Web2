using System.ComponentModel.DataAnnotations;

namespace Store.Books.Localization.Model
{
    public class Locale
    {
        [Key]
        public int Id { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
    }
}
