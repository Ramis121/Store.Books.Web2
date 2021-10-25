using Microsoft.EntityFrameworkCore;
using Store.Books.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Books.Infrastructure.Data
{
    public class BookDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Busket> Buskets { get; set; }
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }

        // Конструктор для разработки, остальные конструкторы отключить
        //public BookDbContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          => optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=books;Trusted_Connection=True;");
    }
}
