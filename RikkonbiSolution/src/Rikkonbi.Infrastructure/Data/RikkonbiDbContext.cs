using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rikkonbi.Core.Aggregates.OrderReport;
using Rikkonbi.Core.Entities;
using Rikkonbi.Core.Interfaces;
using Rikkonbi.Core.SharedKernel;
using Rikkonbi.Infrastructure.Data.EntityTypeConfigs;
using System.Linq;

namespace Rikkonbi.Infrastructure.Data
{
    public class RikkonbiDbContext : IdentityDbContext
    {
        //private readonly IDomainEventDispatcher _dispatcher;

        public RikkonbiDbContext(DbContextOptions<RikkonbiDbContext> options) : base(options)
        {
            //_dispatcher = dispatcher;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbQuery<UnpaidOrder> UnpaidOrders { get; set; }
        public DbSet<PaymentStatus> PaymentStatus { get; set; }

        public DbSet<Device> Devices { set; get; }

        public DbSet<DeviceCategory> DeviceCategories { get; set; }

        public DbSet<Borrow> Borrows { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data source=.;Initial Catalog=RIKKONBI;Integrated Security=true;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationRoleConfiguration());
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderDetailConfiguration());
            builder.ApplyConfiguration(new PaymentStatusConfiguration());

            builder.ApplyConfiguration(new DeviceConfiguration());
            builder.ApplyConfiguration(new DeviceCategoryConfiguration());
            builder.ApplyConfiguration(new BorrowConfiguration());
        }

        //public override int SaveChanges()
        //{
        //    int result = base.SaveChanges();

        //    // dispatch events only if save was successful
        //    var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
        //        .Select(e => e.Entity)
        //        .Where(e => e.Events.Any())
        //        .ToArray();

        //    foreach (var entity in entitiesWithEvents)
        //    {
        //        var events = entity.Events.ToArray();
        //        entity.Events.Clear();
        //        foreach (var domainEvent in events)
        //        {
        //            _dispatcher.Dispatch(domainEvent);
        //        }
        //    }

        //    return result;
        //}
    }
}