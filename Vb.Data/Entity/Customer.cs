using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vb.Base.Entity;

namespace Vb.Data.Entity;

[Table("Customer", Schema = "dbo")]
public class Customer : BaseEntity
{
    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int CustomerNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime LastActivityDate { get; set; }
    public virtual List<Address> Addresses { get; set; }
    public virtual List<Contact> Contacts { get; set; }
    public virtual List<Account> Accounts { get; set; }
}

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(z => z.InsertDate).IsRequired();
        builder.Property(z => z.InsertUserId).IsRequired();
        builder.Property(z => z.UpdateDate).IsRequired(false);
        builder.Property(z => z.UpdateUserId).IsRequired(false);
        builder.Property(z => z.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(z => z.IdentityNumber).IsRequired().HasMaxLength(11);
        builder.Property(z => z.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(z => z.LastName).IsRequired().HasMaxLength(50);
        builder.Property(z => z.CustomerNumber).IsRequired();
        builder.Property(z => z.DateOfBirth).IsRequired();
        builder.Property(z => z.LastActivityDate).IsRequired();

        builder.HasIndex(z => z.IdentityNumber).IsUnique();
        builder.HasIndex(z => z.CustomerNumber).IsUnique();

        builder.HasMany(z => z.Accounts)
            .WithOne(z => z.Customer)
            .HasForeignKey(z => z.CustomerId).IsRequired();

        builder.HasMany(z => z.Contacts)
            .WithOne(z => z.Customer)
            .HasForeignKey(z => z.CustomerId).IsRequired();

        builder.HasMany(z => z.Addresses)
            .WithOne(z => z.Customer)
            .HasForeignKey(z => z.CustomerId).IsRequired();
    }
}