using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Books.Localization.Model;

namespace Store.Books.Localization.Data
{
    public class StoreBooksLocalizationContext : DbContext
    {
        public StoreBooksLocalizationContext (DbContextOptions<StoreBooksLocalizationContext> options)
            : base(options)
        {
        }

        public DbSet<Store.Books.Localization.Model.Translation> Translation { get; set; }
    }
}
