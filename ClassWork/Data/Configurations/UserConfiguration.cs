using ClassWork.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassWork.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id");
            builder.Property(u => u.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(50);

            builder.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255);
            });

            builder.OwnsOne(u => u.Password, password =>
            {
                password.Property(p => p.PasswordHash)
                    .IsRequired()
                    .HasColumnName("password_hash");
            });
        }
    }
}
