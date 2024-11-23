using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QRMenu.Core.Entities;
using QRMenu.Core.ValueObjects;
using System.Text.Json;

namespace QRMenu.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<CompanyTheme> CompanyThemes { get; set; }
        public DbSet<Log> Logs { get; set; }  // Log entity'sini ekledik

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>(builder =>
            {
                builder.ToTable("Companies");
                builder.HasKey(e => e.Id);

                // BusinessHours - Value Object Configuration
                builder.Property<List<BusinessHours>>("_businessHours")
                    .HasColumnName("BusinessHours")
                    .HasColumnType("nvarchar(max)")
                    .HasConversion(
                        list => JsonSerializer.Serialize(list ?? new List<BusinessHours>(), new JsonSerializerOptions()),
                        str => JsonSerializer.Deserialize<List<BusinessHours>>(str ?? "[]", new JsonSerializerOptions()) ?? new List<BusinessHours>()
                    )
                    .Metadata.SetValueComparer(new ValueComparer<List<BusinessHours>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                    ));

                // Location - Value Object Configuration
                builder.Property(e => e.Location)
                    .HasColumnType("nvarchar(max)")
                    .HasConversion(
                        loc => JsonSerializer.Serialize(loc, new JsonSerializerOptions()),
                        str => JsonSerializer.Deserialize<Location>(str ?? "{}", new JsonSerializerOptions())
                    )
                    .Metadata.SetValueComparer(new ValueComparer<Location>(
                        (c1, c2) => c1.Equals(c2),
                        c => c.GetHashCode(),
                        c => c
                    ));

                // Basic Properties
                builder.Property(e => e.LanguagesSupported)
                    .HasColumnType("nvarchar(max)");

                // Relationships
                builder.HasOne(e => e.Dealer)
                    .WithMany(e => e.Companies)
                    .HasForeignKey(e => e.DealerId);

                builder.HasMany(e => e.Branches)
                    .WithOne(e => e.Company)
                    .HasForeignKey(e => e.CompanyId);

                builder.HasOne(e => e.CompanyTheme)
                    .WithOne(e => e.Company)
                    .HasForeignKey<CompanyTheme>("CompanyId");

                builder.Ignore(e => e.BusinessHours);
            });


            // Branch Configuration
            modelBuilder.Entity<Branch>(builder =>
            {
                builder.ToTable("Branches");
                builder.HasKey(e => e.Id);

                builder.Property(e => e.LanguagesSupported)
                    .HasColumnType("nvarchar(max)");
            });

            // Business Hours değil bir entity olmadığını belirt
            modelBuilder.Entity<Company>()
                .Ignore(c => c.BusinessHours);

            // Branch Configuration
            modelBuilder.Entity<Branch>(builder =>
            {
                builder.ToTable("Branches");
                builder.HasKey(x => x.Id);

                builder.Property(c => c.LanguagesSupported)
                    .HasColumnType("nvarchar(max)");
            });

            // User Configuration
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users");
                builder.HasKey(x => x.Id);
            });

            // Dealer Configuration
            modelBuilder.Entity<Dealer>(builder =>
            {
                builder.ToTable("Dealers");
                builder.HasKey(x => x.Id);
            });

            // Theme Configuration
            modelBuilder.Entity<Theme>(builder =>
            {
                builder.ToTable("Themes");
                builder.HasKey(x => x.Id);
            });

            // Template Configuration
            modelBuilder.Entity<Template>(builder =>
            {
                builder.ToTable("Templates");
                builder.HasKey(x => x.Id);
            });

            // CompanyTheme Configuration
            modelBuilder.Entity<CompanyTheme>(builder =>
            {
                builder.ToTable("CompanyThemes");
                builder.HasKey(x => x.Id);

                builder.HasOne(ct => ct.Company)
                    .WithOne(c => c.CompanyTheme)
                    .HasForeignKey<CompanyTheme>(ct => ct.CompanyId);
            });



            modelBuilder.Entity<Log>(builder =>
            {
                builder.ToTable("Logs");
                builder.HasKey(x => x.Id);

                builder.Property(e => e.Module)
                    .IsRequired();

                builder.Property(e => e.Action)
                    .IsRequired();

                builder.Property(e => e.Details);

                builder.Property(e => e.LogLevel)
                    .IsRequired();

                // İlişkiler
                builder.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}