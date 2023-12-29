namespace Vb.Data.DtoS;

public class AccountDto //Data Transfer Object
{
    public int CustomerId { get; set; }
    public int AccountNumber { get; set; } 
    public string IBAN { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string CurrencyType { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime OpenDate { get; set; }
}