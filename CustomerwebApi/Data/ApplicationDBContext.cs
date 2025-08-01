using Microsoft.EntityFrameworkCore;
using MyApp.Model;
using System.Collections.Generic;


namespace MyApp.Data

{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }

    }
}
