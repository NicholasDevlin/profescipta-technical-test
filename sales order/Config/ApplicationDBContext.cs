using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using sales_order.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    // Define DbSet properties for your entities (tables)
    public DbSet<COM_CUSTOMER> COM_CUSTOMER { get; set; }
    public DbSet<SO_ORDER> SO_ORDER { get; set; }
    public DbSet<SO_ITEM> SO_ITEM { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SO_ORDER>()
            .HasOne(s => s.Customer)
            .WithMany(c => c.SalesOrders)
            .HasForeignKey(s => s.COM_CUSTOMER_ID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SO_ITEM>()
            .HasOne(si => si.SalesOrder)
            .WithMany(so => so.SoItems)
            .HasForeignKey(si => si.SO_ORDER_ID)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}