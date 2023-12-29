using Microsoft.EntityFrameworkCore;

namespace Vb.Data.DtoS;

public class AccountTransactionDto //Data transfer object 
{
    public int AccountId { get; set; }
    public string ReferenceNumber { get; set; }
    public DateTime TransactionDate { get; set; }
    [Precision(18, 2)] 
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string TransferType { get; set; }
}