using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationToken> ApplicationTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationTokenEntityConfiguration());

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }

    class ApplicationTokenEntityConfiguration : IEntityTypeConfiguration<ApplicationToken>
    {
        public void Configure(EntityTypeBuilder<ApplicationToken> builder)
        {
            builder
                .HasKey(t=>t.Id);

            builder
                .Property(t=>t.Id).ValueGeneratedOnAdd();

            builder
                .Property(t => t.ClientId).IsRequired();
            builder
                .Property(t => t.ClientId);

            builder
                .Property(t => t.CreateDate).IsRequired();
            builder
                .Property(t => t.Value).IsRequired();
            builder
                .Property(t => t.UserId).IsRequired();
            builder
                .Property(t => t.UserId);
            builder
                .HasOne(t => t.ApplicationUser)
                .WithMany(u => u.ApplicationTokens)
                .HasForeignKey(t => t.UserId);
        }


    }
}
