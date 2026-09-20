using Domain.Models.AuthModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.AuthConfigurations;

public class RefreshTokenConfigurations:IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasDefaultValueSql("gen_random_uuid()");
        builder.HasOne(a => a.User)
            .WithMany(a => a.RefreshTokens)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(a => a.CreatedAt)
            .HasDefaultValueSql("now()");

        builder.HasIndex(a => a.Token)
            .IsUnique();
    }
}