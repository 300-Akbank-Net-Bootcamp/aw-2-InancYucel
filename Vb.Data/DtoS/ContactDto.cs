namespace Vb.Data.DtoS;

public class ContactDto //Data Transfer Object
{
    public int CustomerId { get; set; }
    public string ContactType { get; set; } = string.Empty;
    public string Information { get; set; } = string.Empty;
}