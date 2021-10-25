using Microsoft.EntityFrameworkCore;
using Store.Books.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Product.Data
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        // Конструктор для разработки, остальные конструкторы отключить
        //public ProductDbContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          => optionsBuilder.UseSqlServer(@"Server=DESKTOP-NB24L70;Database=productsApi;Trusted_Connection=True;");
    }
}
