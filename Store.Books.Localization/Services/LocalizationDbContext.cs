using Microsoft.EntityFrameworkCore;
using Store.Books.Localization.Model;

namespace Store.Books.Localization.Services
{
    public class LocalizationDbContext : DbContext
    {
        public DbSet<Locale> Locales { get; set; }
        public DbSet<Translation> Translations { get; set; }
        public LocalizationDbContext(DbContextOptions<LocalizationDbContext> options) : base(options) { }
        //public LocalizationDbContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          //=> optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=localizations;Trusted_Connection=True;");
          => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=localization;Username=postgres;Password=gtxtymrf123");
    }
}
