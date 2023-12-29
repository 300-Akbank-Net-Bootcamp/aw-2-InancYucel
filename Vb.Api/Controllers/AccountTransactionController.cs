using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Data;
using Vb.Data.DtoS;
using Vb.Data.Entity;

namespace Week2_Task_Controllers.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountTransactionController : ControllerBase
{
    private readonly VbDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public AccountTransactionController(VbDbContext dbContext, IMapper mapper)
    {
        this._dbContext = dbContext;
        this._mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<AccountTransactionDto>> Get()
    {
        var dbDatas = await _dbContext.Set<AccountTransaction>().ToListAsync();
        return dbDatas.Select(accountTransaction => _mapper.Map<AccountTransactionDto>(accountTransaction)); //AutoMapper 
    }
    
    [HttpGet("{id:int}")]
    public async Task<AccountTransactionDto> Get(int id)
    {
        var dbData = await _dbContext.Set<AccountTransaction>().FirstOrDefaultAsync(z => z.Id == id);
        return _mapper.Map<AccountTransactionDto>(dbData); //AutoMapper 
    }
    
    [HttpPost]
    public async Task Post([FromBody] AccountTransactionDto transaction)
    {
        var thatAccountTransaction = _mapper.Map<AccountTransaction>(transaction); //AutoMapper 
        await _dbContext.AccountTransactions.AddAsync(thatAccountTransaction);
        await _dbContext.SaveChangesAsync();
    }
    
    [HttpPut("{id:int}")]
    public async Task Put(int id, [FromBody] AccountTransactionDto accountTransaction)
    {
        var fromDb = await _dbContext.Set<AccountTransaction>().FirstOrDefaultAsync(z => z.Id == id);

        if (fromDb != null)
        {
            fromDb.ReferenceNumber = accountTransaction.ReferenceNumber;
            fromDb.TransactionDate = accountTransaction.TransactionDate;
            fromDb.Amount = accountTransaction.Amount;
            fromDb.Description = accountTransaction.Description;
            fromDb.TransferType = accountTransaction.TransferType;
        }
        await _dbContext.SaveChangesAsync();
    }
    
    [HttpDelete("{id:int}")]
    public async Task Delete(int id)
    {
        var fromDb = await _dbContext.Set<AccountTransaction>().FirstOrDefaultAsync(z => z.Id == id);
        if (fromDb != null) fromDb.IsActive = false;
        await _dbContext.SaveChangesAsync();
    }
}