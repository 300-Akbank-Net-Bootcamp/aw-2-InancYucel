using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vb.Base.Entity;

namespace Vb.Data.Entity;

[Table("Contact", Schema = "dbo")]
public class Contact : BaseEntity
{
    public int CustomerId { get; set; }
    public string ContactType { get; set; }
    public virtual Customer Customer { get; set; }
    public string Information { get; set; }
    public bool IsDefault { get; set; }
}

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.Property(z => z.InsertDate).IsRequired();
        builder.Property(z => z.InsertUserId).IsRequired();
        builder.Property(z => z.UpdateDate).IsRequired(false);
        builder.Property(z => z.UpdateUserId).IsRequired(false);
        builder.Property(z => z.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(z => z.CustomerId).IsRequired();
        builder.Property(z => z.ContactType).IsRequired().HasMaxLength(10);
        builder.Property(z => z.Information).IsRequired().HasMaxLength(10);
        builder.Property(z => z.IsDefault).IsRequired().HasDefaultValue(false);
        builder.HasIndex(z => z.CustomerId);

        //Bu tabloda Information ve ContactType'dan bir tane olabilir. 
        builder.HasIndex(z => new { z.Information, z.ContactType }).IsUnique();
    }
}