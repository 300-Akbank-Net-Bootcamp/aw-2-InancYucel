using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Data;
using Vb.Data.DtoS;
using Vb.Data.Entity;

namespace Week2_Task_Controllers.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly VbDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public AccountController(VbDbContext dbContext, IMapper mapper)
    {
        this._dbContext = dbContext;
        this._mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<AccountDto>> Get()
    {
        var dbDatas = await _dbContext.Set<Account>().ToListAsync();
        return dbDatas.Select(contact => _mapper.Map<AccountDto>(contact)); //AutoMapper 
    }
    
    [HttpGet("{id:int}")]
    public async Task<AccountDto> Get(int id)
    {
        var dbData = await _dbContext.Set<Account>().FirstOrDefaultAsync(z => z.Id == id);
        return _mapper.Map<AccountDto>(dbData);
    }
    
    [HttpPost]
    public async Task Post([FromBody] AccountDto account)
    {
        var thatAccount = _mapper.Map<Account>(account); //AutoMapper 
        await _dbContext.Accounts.AddAsync(thatAccount);
        await _dbContext.SaveChangesAsync();
    }
    
    [HttpPut("{id:int}")]
    public async Task Put(int id, [FromBody] AccountDto account)
    {
        var fromDb = await _dbContext.Set<Account>().FirstOrDefaultAsync(z => z.Id == id);

        if (fromDb != null)
        {   //Mapping
            fromDb.AccountNumber = account.AccountNumber;
            fromDb.IBAN = account.IBAN;
            fromDb.Balance = account.Balance;
            fromDb.CurrencyType = account.CurrencyType;
            fromDb.Balance = account.Balance;
            fromDb.Name = account.Name;
            fromDb.OpenDate = account.OpenDate;
        }
        await _dbContext.SaveChangesAsync();
    }
    
    [HttpDelete("{id:int}")]
    public async Task Delete(int id)
    {
        var fromDb = await _dbContext.Set<Account>().FirstOrDefaultAsync(z => z.Id == id);
        if (fromDb != null) fromDb.IsActive = false;
        await _dbContext.SaveChangesAsync();
    }
}