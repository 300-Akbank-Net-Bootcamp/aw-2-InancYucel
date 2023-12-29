using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vb.Base.Entity;

namespace Vb.Data.Entity;

[Table("Address", Schema = "dbo")]
public class Address : BaseEntity
{
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string PostalCode { get; set; }
    public bool IsDefault { get; set; }
}

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(z => z.InsertDate).IsRequired();
        builder.Property(z => z.InsertUserId).IsRequired();
        builder.Property(z => z.UpdateDate).IsRequired(false);
        builder.Property(z => z.UpdateUserId).IsRequired(false);
        builder.Property(z => z.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(z => z.CustomerId).IsRequired();
        builder.Property(z => z.Address1).IsRequired().HasMaxLength(150);
        builder.Property(z => z.Address2).IsRequired(false).HasMaxLength(150);
        builder.Property(z => z.Country).IsRequired().HasMaxLength(100);
        builder.Property(z => z.City).IsRequired().HasMaxLength(100);
        builder.Property(z => z.County).IsRequired(false).HasMaxLength(100);
        builder.Property(z => z.PostalCode).IsRequired(false).HasMaxLength(10);
        builder.Property(z => z.IsDefault).IsRequired().HasDefaultValue(false);

        builder.HasIndex(z => z.CustomerId);
    }
}