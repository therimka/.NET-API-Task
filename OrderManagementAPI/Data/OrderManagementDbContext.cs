using Microsoft.EntityFrameworkCore; 
using OrderManagementAPI.Models; 

namespace OrderManagementAPI.Data 
{
    
    public class OrderManagementDbContext : DbContext
    {
        
        public OrderManagementDbContext(DbContextOptions<OrderManagementDbContext> options)
            : base(options) 
        {
        }

      
        public DbSet<Customer> Customer { get; set; } 
        public DbSet<Order> Order { get; set; } 
        public DbSet<OrderItem> OrderItem { get; set; } 
        public DbSet<Product> Product { get; set; } 

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Customer>().ToTable("Customer"); 
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItem"); 
            modelBuilder.Entity<Product>().ToTable("Product"); 

            
            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.OrderItemID)
                .ValueGeneratedOnAdd(); 

            
            modelBuilder.Entity<Order>()
                .HasKey(o => o.OrderID); 

           
            modelBuilder.Entity<Order>()
                .HasOne<Customer>() 
                .WithMany() 
                .HasForeignKey(o => o.CustomerID) 
                .OnDelete(DeleteBehavior.Cascade); 

          
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order) 
                .WithMany(o => o.OrderItems) 
                .HasForeignKey(oi => oi.OrderID) 
                .OnDelete(DeleteBehavior.Cascade); 

           
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product) 
                .WithMany() 
                .HasForeignKey(oi => oi.ProductID) 
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
