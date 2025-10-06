using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Infrastructure.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");
            builder.HasKey(p => p.PaymentId);
            builder.Property(p => p.PaymentId).ValueGeneratedOnAdd().HasColumnType("INT");

            builder.Property(p => p.OrderId).HasMaxLength(20).IsRequired(true);
            builder.Property(p => p.Status).IsRequired().HasColumnType("INT");
            builder.Property(p => p.PaymentMethod).IsRequired().HasColumnType("INT");
            builder.Property(p => p.Amount).IsRequired().HasColumnType("DECIMAL(18,2)");
            builder.Property(p => p.CardNumber).IsRequired().HasMaxLength(19); 
            builder.Property(p => p.CardHolder).IsRequired().HasMaxLength(100);
            builder.Property(p => p.ExpiryDate).IsRequired().HasMaxLength(7); 
            builder.Property(p => p.Cvv).IsRequired().HasMaxLength(3);

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()")
                .HasConversion(
                    v => v, // Grava no banco normalmente
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // Força Kind como UTC ao ler
                );
            builder.Property(p => p.UpdatedAt)
                .IsRequired(false)
                .HasConversion(
                    v => v, // Grava no banco normalmente  
                    v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : null // Força Kind como UTC ao ler  
                );
        }
    }
}
