using System.ComponentModel.DataAnnotations;

namespace Store.Books.Localization.Model
{
    public class Translation
    {
        [Key]
        public int Id { get; set; }
        public Locale Locale { get; set; }
        public string Lang { get; set; }
        public string Translate { get; set; }
    }
}
