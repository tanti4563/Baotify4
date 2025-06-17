//------------------------------------------------------------------------------
// Ferry Booking System - Entity Framework DbContext
// Production-ready database context for ferry booking operations
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Sample.api.Data
{
    public partial class ShipManageEntities : DbContext
    {
        public ShipManageEntities()
            : base("name=ShipManageEntities")
        {
            // Disable initializer to prevent automatic database creation
            Database.SetInitializer<ShipManageEntities>(null);
        }

        public ShipManageEntities(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Database.SetInitializer<ShipManageEntities>(null);
        }

        // Alternative constructor for simple connection
        public static ShipManageEntities CreateWithSimpleConnection()
        {
            var connectionString = "data source=LAPTOP-70K0HA84\\KIN;initial catalog=test;integrated security=True;MultipleActiveResultSets=True";
            return new ShipManageEntities(connectionString);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure entity relationships and constraints

            // Users entity configuration
            modelBuilder.Entity<Users>()
                .HasKey(e => e.UserId)
                .Property(e => e.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Users>()
                .Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Users>()
                .Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Users>()
                .Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Users>()
                .Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(100);

            // Orders entity configuration
            modelBuilder.Entity<Orders>()
                .HasKey(e => e.OrderId)
                .Property(e => e.OrderId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Configure the Users-Orders relationship
            modelBuilder.Entity<Orders>()
                .HasOptional(o => o.Users)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .WillCascadeOnDelete(false);

            // UserRoles
            modelBuilder.Entity<UserRoles>()
                .HasKey(e => e.RoleId);

            // UserRoleAssignments
            modelBuilder.Entity<UserRoleAssignments>()
                .HasKey(e => new { e.UserId, e.RoleId });

            modelBuilder.Entity<UserRoleAssignments>()
                .HasRequired(e => e.Users)
                .WithMany(e => e.UserRoleAssignments)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserRoleAssignments>()
                .HasRequired(e => e.UserRoles)
                .WithMany(e => e.UserRoleAssignments)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            // Tickets
            modelBuilder.Entity<Tickets>()
                .HasKey(e => e.TicketId);

            modelBuilder.Entity<Tickets>()
                .HasRequired(e => e.Orders)
                .WithMany(e => e.Tickets)
                .HasForeignKey(e => e.OrderId);

            // Payments
            modelBuilder.Entity<Payments>()
                .HasKey(e => e.PaymentId);

            modelBuilder.Entity<Payments>()
                .HasRequired(e => e.Orders)
                .WithMany(e => e.Payments)
                .HasForeignKey(e => e.OrderId);

            // PaymentLogs
            modelBuilder.Entity<PaymentLogs>()
                .HasKey(e => e.LogId);

            modelBuilder.Entity<PaymentLogs>()
                .HasRequired(e => e.Payments)
                .WithMany(e => e.PaymentLogs)
                .HasForeignKey(e => e.PaymentId);

            // Configure decimal precision
            modelBuilder.Entity<Orders>()
                .Property(e => e.SubTotal)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Orders>()
                .Property(e => e.AdditionalFees)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Orders>()
                .Property(e => e.TotalAmount)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Orders>()
                .Property(e => e.PaidAmount)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Tickets>()
                .Property(e => e.UnitPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Payments>()
                .Property(e => e.PaymentAmount)
                .HasPrecision(12, 2);
        }

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<UserRoleAssignments> UserRoleAssignments { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Tickets> Tickets { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<PaymentLogs> PaymentLogs { get; set; }
    }
}