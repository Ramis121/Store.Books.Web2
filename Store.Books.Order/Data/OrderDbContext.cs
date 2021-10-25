using Microsoft.EntityFrameworkCore;
using Store.Books.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Books.Order.Data
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Domain.Busket> Buskets { get; set; }
        public DbSet<Domain.Order> Orders { get; set; }
        public DbSet<Domain.Payment> Payments { get; set; }
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

        // Конструктор для разработки, остальные конструкторы отключить
        //public OrderDbContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          => optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=orders;Trusted_Connection=True;");
    }
}
