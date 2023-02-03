using BlazorMealOrdering.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorMealOrdering.Server.Data.Context
{
    public class MealOrderinDbContext : DbContext
    {
        public MealOrderinDbContext(DbContextOptions<MealOrderinDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users", "public");

                entity.HasKey(i => i.Id).HasName("pk_user_id");
                entity.Property(i => i.Id).HasColumnName("id").HasColumnType("uuid").HasDefaultValueSql("public.uuid_generate_v4()").IsRequired();

                entity.Property(i => i.FirstName).HasColumnName("first_name").HasColumnType("character varying").HasMaxLength(100);
                entity.Property(i => i.LastName).HasColumnName("last_name").HasColumnType("character varying").HasMaxLength(100);
                entity.Property(i => i.Email).HasColumnName("email_address").HasColumnType("character varying").HasMaxLength(100);
                //entity.Property(i => i.Password).HasColumnName("password").HasColumnType("character varying").HasMaxLength(250);
                entity.Property(i => i.CreateDate).HasColumnName("create_date").HasColumnType("timestamp without time zone").HasDefaultValueSql("NOW()");
                entity.Property(i => i.IsActive).HasColumnName("isactive").HasColumnType("boolean");
            });


            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("suppliers", "public");

                entity.HasKey(e => e.Id).HasName("pk_supplier_id");
                entity.Property(e => e.Id).HasColumnName("id").HasColumnType("uuid").HasDefaultValueSql("public.uuid_generate_v4()").IsRequired();

                entity.Property(e => e.Name).HasColumnName("name").HasColumnType("character varying").HasMaxLength(100);
                entity.Property(e => e.WebURL).HasColumnName("web_url").HasColumnType("character varying").HasMaxLength(500);
                entity.Property(e => e.CreateDate).HasColumnName("createdate").HasColumnType("timestamp without time zone").HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
                entity.Property(e => e.IsActive).HasColumnName("isactive").HasColumnType("boolean");
            });


            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders", "public");

                entity.HasKey(e => e.Id).HasName("pk_order_id");
                entity.Property(e => e.Id).HasColumnName("id").HasColumnType("uuid").HasDefaultValueSql("public.uuid_generate_v4()");

                entity.Property(e => e.Name).HasColumnName("name").HasColumnType("character varying").HasMaxLength(100);
                entity.Property(e => e.Description).HasColumnName("description").HasColumnType("character varying").HasMaxLength(1000);
                entity.Property(e => e.CreateUserId).HasColumnName("create_user_id").HasColumnType("uuid");
                entity.Property(e => e.SupplierId).HasColumnName("supplier_id").HasColumnType("uuid").IsRequired().ValueGeneratedNever();
                entity.Property(e => e.ExpireDate).HasColumnName("expire_date").HasColumnType("timestamp without time zone").IsRequired();
                entity.Property(e => e.CreateDate).HasColumnName("createdate").HasColumnType("timestamp without time zone").HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();

                entity.HasOne(o => o.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.CreateUserId)
                    .HasConstraintName("fk_user_order_id")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(o => o.Supplier)
                    .WithMany(s => s.Orders)
                    .HasForeignKey(o => o.SupplierId)
                    .HasConstraintName("fk_supplier_order_id")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("order_items", "public");

                entity.HasKey(e => e.Id).HasName("pk_orderItem_id");
                entity.Property(e => e.Id).HasColumnName("id").HasColumnType("uuid").HasDefaultValueSql("public.uuid_generate_v4()");

                entity.Property(e => e.Description).HasColumnName("description").HasColumnType("character varying").HasMaxLength(1000);
                entity.Property(e => e.OrderId).HasColumnName("order_id").HasColumnType("uuid");
                entity.Property(e => e.CreateUserId).HasColumnName("create_user_id").HasColumnType("uuid");
                entity.Property(e => e.CreateDate).HasColumnName("createdate").HasColumnType("timestamp without time zone").HasDefaultValueSql("NOW()");

                entity.HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.OrderId)
                    .HasConstraintName("fk_order_orderitem_id")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(oi => oi.User)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.CreateUserId)
                    .HasConstraintName("fk_user_orderitem_id")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}