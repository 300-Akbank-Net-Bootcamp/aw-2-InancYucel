using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vb.Base.Entity;

namespace Vb.Data.Entity;

[Table("EftTransaction", Schema = "dbo")]
public class EftTransaction : BaseEntity
{
    public int AccountId { get; set; }
    public virtual Account Account { get; set; }
    public string ReferenceNumber { get; set; }
    public DateTime TransactionDate { get; set; }

    [Precision(18, 4)] public decimal Amount { get; set; }

    public string Description { get; set; }

    public string SenderAccount { get; set; }
    public string SenderBank { get; set; }
    public string SenderIban { get; set; }
    public string SenderName { get; set; }
}

public class EftTransactionConfiguration : IEntityTypeConfiguration<EftTransaction>
{
    public void Configure(EntityTypeBuilder<EftTransaction> builder)
    {
        builder.Property(z => z.InsertDate).IsRequired();
        builder.Property(z => z.InsertUserId).IsRequired();
        builder.Property(z => z.UpdateDate).IsRequired(false);
        builder.Property(z => z.UpdateUserId).IsRequired(false);
        builder.Property(z => z.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(z => z.AccountId).IsRequired();
        builder.Property(z => z.TransactionDate).IsRequired();
        builder.Property(z => z.Amount).IsRequired().HasColumnType("decimal(18,4)");
        builder.Property(z => z.Description).IsRequired(false).HasMaxLength(300);
        builder.Property(z => z.ReferenceNumber).IsRequired().HasMaxLength(50);
        builder.Property(z => z.SenderAccount).IsRequired().HasMaxLength(50);
        builder.Property(z => z.SenderIban).IsRequired().HasMaxLength(50);
        builder.Property(z => z.SenderName).IsRequired().HasMaxLength(50);


        builder.HasIndex(z => z.ReferenceNumber);
    }
}