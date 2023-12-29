using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vb.Base.Entity;

namespace Vb.Data.Entity;

[Table("Account", Schema = "dbo")]
public class Account : BaseEntity
{
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public int AccountNumber { get; set; }
    public string IBAN { get; set; }
    public decimal Balance { get; set; }
    public string CurrencyType { get; set; }
    public string Name { get; set; }
    public DateTime OpenDate { get; set; }

    public virtual List<AccountTransaction> AccountTransactions { get; set; }
    public virtual List<EftTransaction> EftTransaction { get; set; }
}

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.Property(z => z.InsertDate).IsRequired();
        builder.Property(z => z.InsertUserId).IsRequired();
        builder.Property(z => z.UpdateDate).IsRequired(false);
        builder.Property(z => z.UpdateUserId).IsRequired(false);
        builder.Property(z => z.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(z => z.CustomerId).IsRequired();
        builder.Property(z => z.AccountNumber).IsRequired();
        builder.Property(z => z.IBAN).IsRequired().HasMaxLength(34);
        builder.Property(z => z.Balance).IsRequired().HasPrecision(18, 4);
        builder.Property(z => z.CurrencyType).IsRequired().HasMaxLength(100);
        builder.Property(z => z.Name).IsRequired(false).HasMaxLength(100);
        builder.Property(z => z.OpenDate).IsRequired();

        builder.HasIndex(z => z.CustomerId);
        builder.HasIndex(z => z.AccountNumber).IsUnique();

        builder.HasMany(z => z.AccountTransactions)
            .WithOne(z => z.Account)
            .HasForeignKey(z => z.AccountId).IsRequired();

        builder.HasMany(z => z.EftTransaction)
            .WithOne(z => z.Account)
            .HasForeignKey(z => z.AccountId).IsRequired();
    }
}