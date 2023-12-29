using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Data;
using Vb.Data.DtoS;
using Vb.Data.Entity;

namespace Week2_Task_Controllers.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EftTransactionController : ControllerBase
{
    private readonly VbDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public EftTransactionController(VbDbContext dbContext, IMapper mapper)
    {
        this._dbContext = dbContext;
        this._mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<EftTransactionDto>> Get()
    {
        var dbDatas = await _dbContext.Set<EftTransaction>().ToListAsync();
        return dbDatas.Select(eft => _mapper.Map<EftTransactionDto>(eft)); //AutoMapper 
    }
    
    [HttpGet("{id:int}")]
    public async Task<EftTransactionDto> Get(int id)
    {
        var dbData = await _dbContext.Set<EftTransaction>().FirstOrDefaultAsync(z => z.Id == id);
        return _mapper.Map<EftTransactionDto>(dbData); //AutoMapper 
    }
    
    [HttpPost]
    public async Task Post([FromBody] EftTransactionDto eftTransaction)
    {
        var eftTransactionAccount = _mapper.Map<EftTransaction>(eftTransaction); //AutoMapper 
        await _dbContext.EftTransactions.AddAsync(eftTransactionAccount);
        await _dbContext.SaveChangesAsync();
    }
    
    [HttpPut("{id:int}")]
    public async Task Put(int id, [FromBody] EftTransactionDto eftTransaction)
    {
        var fromDb = await _dbContext.Set<EftTransaction>().FirstOrDefaultAsync(z => z.Id == id);

        if (fromDb != null)
        {
            //Mapping
            fromDb.ReferenceNumber = eftTransaction.ReferenceNumber;
            fromDb.TransactionDate = eftTransaction.TransactionDate;
            fromDb.Amount = eftTransaction.Amount;
            fromDb.Description = eftTransaction.Description;
            fromDb.SenderAccount = eftTransaction.SenderAccount;
            fromDb.SenderBank = eftTransaction.SenderBank;
            fromDb.SenderIban = eftTransaction.SenderIban;
            fromDb.SenderName = eftTransaction.SenderName;
        }
        await _dbContext.SaveChangesAsync();
    }
    
    [HttpDelete("{id:int}")]
    public async Task Delete(int id)
    {
        var fromDb = await _dbContext.Set<EftTransaction>().FirstOrDefaultAsync(z => z.Id == id);
        if (fromDb != null) fromDb.IsActive = false;
        await _dbContext.SaveChangesAsync();
    }
}