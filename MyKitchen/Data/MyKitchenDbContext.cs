namespace MyKitchen.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using MyKitchen.Data.Models;

    public class MyKitchenDbContext : IdentityDbContext<ApplicationUser>
    {
        public MyKitchenDbContext(DbContextOptions<MyKitchenDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; init; }
        public DbSet<Color> Colors { get; init; }
        public DbSet<Dimensions> Dimensions { get; init; }
        public DbSet<Image> Images { get; init; }
        public DbSet<Kitchen> Kitchens { get; init; }
        public DbSet<KitchensColors> KitchensColors { get; init; }
        public DbSet<Manufacturer> Manufacturers { get; init; }
        public DbSet<Country> Countries { get; init; }
        public DbSet<City> Cities { get; init; }
        public DbSet<Address> Addresses { get; init; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
               .Entity<Kitchen>()
               .HasOne(k => k.Мanufacturer)
               .WithMany(m => m.Kitchens)
               .HasForeignKey(k => k.МanufacturerId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
              .Entity<Kitchen>()
              .HasOne(k => k.User)
              .WithMany(u => u.Kitchens)
              .HasForeignKey(k => k.UserId)
              .OnDelete(DeleteBehavior.Restrict);

            builder
             .Entity<Kitchen>()
             .HasOne(k => k.Category)
             .WithMany(c => c.Kitchens)
             .HasForeignKey(k => k.CategoryId)
             .OnDelete(DeleteBehavior.Restrict);

            builder
            .Entity<Image>()
            .HasOne(i => i.Kitchen)
            .WithMany(k => k.Images)
            .HasForeignKey(i => i.KitchenId)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .Entity<Kitchen>()
            .HasOne(k => k.Dimensions)
            .WithOne(d => d.Kitchen)
            .HasForeignKey<Kitchen>(k => k.DimensionId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<KitchensColors>()
                    .HasKey(kc => new { kc.KitchenId, kc.ColorId });
            builder.Entity<KitchensColors>()
                .HasOne(kc => kc.Kitchen)
                .WithMany(k => k.KitchensColors)
                .HasForeignKey(kc => kc.KitchenId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<KitchensColors>()
                .HasOne(kc => kc.Color)
                .WithMany(c => c.KitchensColors)
                .HasForeignKey(kc => kc.ColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Image>()
                .HasOne(i => i.AddedByUser)
                .WithMany(u => u.Images)
                .HasForeignKey(i => i.AddedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Manufacturer>()
                .HasOne(m => m.AddedByUser)
                .WithMany(u => u.Manufacturers)
                .HasForeignKey(m => m.AddedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Manufacturer>()
                .HasOne(m => m.Address)
                .WithMany(a => a.Manufacturers)
                .HasForeignKey(m => m.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<City>()
                .HasOne(c => c.Country)
                .WithMany(c => c.Cities)
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Address>()
                .HasOne(a => a.City)
                .WithMany(c => c.Addresses)
                .HasForeignKey(c => c.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}