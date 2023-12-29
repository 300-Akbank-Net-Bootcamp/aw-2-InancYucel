using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vb.Base.Entity;

namespace Vb.Data.Entity;

[Table("AccountTransaction", Schema = "dbo")]
public class AccountTransaction : BaseEntity
{
    public int AccountId { get; set; }
    public virtual Account Account { get; set; }
    public string ReferenceNumber { get; set; }
    public DateTime TransactionDate { get; set; }
    [Precision(18, 2)] public decimal Amount { get; set; }
    public string Description { get; set; }
    public string TransferType { get; set; }
}

public class AccountTransactionConfiguration : IEntityTypeConfiguration<AccountTransaction>
{
    public void Configure(EntityTypeBuilder<AccountTransaction> builder)
    {
        builder.Property(z => z.InsertDate).IsRequired();
        builder.Property(z => z.InsertUserId).IsRequired();
        builder.Property(z => z.UpdateDate).IsRequired(false);
        builder.Property(z => z.UpdateUserId).IsRequired(false);
        builder.Property(z => z.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(z => z.AccountId).IsRequired();
        builder.Property(z => z.TransactionDate).IsRequired();
        builder.Property(z => z.Amount).IsRequired().HasPrecision(18, 4);
        builder.Property(z => z.Description).IsRequired(false).HasMaxLength(300);
        builder.Property(z => z.TransferType).IsRequired().HasMaxLength(10);
        builder.Property(z => z.ReferenceNumber).IsRequired().HasMaxLength(50);

        builder.HasIndex(z => z.ReferenceNumber);
    }
}