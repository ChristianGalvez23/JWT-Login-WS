using System;
using login_webapi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace login_webapi.DbContext.EntityConfiguration {
    public class UserConfiguration : IEntityTypeConfiguration<User> {
        public void Configure (EntityTypeBuilder<User> builder) {
            builder.ToTable ("User");
            builder.HasKey (u => u.Id);
            builder.Property (u => u.FullName)
                .HasMaxLength (100)
                .IsRequired ();
            builder.Property (u => u.Password)
                .IsRequired ();
            builder.Property (u => u.Email).IsRequired ();
            builder.Property (u => u.BornDate)
                .HasColumnType ("date")
                .IsRequired ();
            builder.Property (u => u.SignUpDate)
                .HasColumnType ("date")
                .HasDefaultValue (DateTime.Now)
                .IsRequired ();
        }
    }
}