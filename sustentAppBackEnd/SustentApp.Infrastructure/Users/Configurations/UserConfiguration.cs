using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SustentApp.Domain.Users.Entities;

namespace SustentApp.Infrastructure.Users.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Document)
            .IsRequired()
            .HasMaxLength(14);

        builder.OwnsOne(u => u.Address, a =>
        {
            a.Property(ad => ad.Street)
                .IsRequired()
                .HasMaxLength(100);

            a.Property(ad => ad.Number)
                .IsRequired()
                .HasMaxLength(10);

            a.Property(ad => ad.Complement)
                .HasMaxLength(100);

            a.Property(ad => ad.Neighborhood)
                .IsRequired()
                .HasMaxLength(100);

            a.Property(ad => ad.City)
                .IsRequired()
                .HasMaxLength(100);

            a.Property(ad => ad.State)
                .IsRequired()
                .HasMaxLength(2);

            a.Property(ad => ad.ZipCode)
                .IsRequired()
                .HasMaxLength(8);
        });

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Phone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property("Password")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(u => u.Role)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(u => u.ConfirmedEmail)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(u => u.SecurityStamp)
            .IsRequired()
            .HasMaxLength(36)
            .HasDefaultValueSql("(uuid())");

        builder.Property(u => u.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("(now())");

        builder.Property(u => u.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("(now())");
    }
}
