using DevJobsWeb.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevJobsWeb.Areas.Identity.Data;

public class AspDevJobsWebContext : IdentityDbContext<DevJobsWebUser>
{
    public AspDevJobsWebContext(DbContextOptions<AspDevJobsWebContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new DevJobsWebUserEntityConfiguration());

    }

    public class DevJobsWebUserEntityConfiguration : IEntityTypeConfiguration<DevJobsWebUser>
    {
        public void Configure(EntityTypeBuilder<DevJobsWebUser> builder)
        {
            builder.Property(u => u.PreferredName).HasMaxLength(60);
            builder.Property(u => u.PhoneNumber).HasMaxLength(10);
        }
    }

}
