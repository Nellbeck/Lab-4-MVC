using Lab_4_MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab_4_MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Library> Librarys { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>().HasData(
                new Customer() { CustomerId = 1, CustomerName = "Bert Karlsson", CustomerEmail = "bert@gmail.com", CustomerAdress = "Bertvägen 10", CustomerAge = 16 },
                new Customer() { CustomerId = 2, CustomerName = "Karl Fredriksson", CustomerEmail = "karl@gmail.com", CustomerAdress = "Karlgatan 4", CustomerAge = 32 },
                new Customer() { CustomerId = 3, CustomerName = "Frida Eriksson", CustomerEmail = "frida@gmail.se", CustomerAdress = "Salavägen 16", CustomerAge = 65 }
                );
            builder.Entity<Book>().HasData(
                new Book() { BookId = 1, BookName = "Pippi Långstrump", BookAuthor = "Astrid Lindgren", BookDescription = ""},
                new Book() { BookId = 2, BookName = "Hur man bygger en app", BookAuthor = "Reidar", BookDescription = "Reidar går igenom bit för bit hur man bygger en simple app." },
                new Book() { BookId = 3, BookName = "Hur man tjänar pengar", BookAuthor = "Max Million", BookDescription = "" },
                new Book() { BookId = 4, BookName = "Hur man brygger öl", BookAuthor = "Sten Hård", BookDescription = "Sten hård lär ut hur man brygger öl." }
                );
            base.OnModelCreating( builder );
        }
    }
}
