using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    // Define DbSet properties for your entities (tables)
    public DbSet<Customer> Customers { get; set; } 
}