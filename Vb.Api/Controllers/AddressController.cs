using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Data;
using Vb.Data.DtoS;
using Vb.Data.Entity;

namespace Week2_Task_Controllers.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly VbDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public AddressController(VbDbContext dbContext, IMapper mapper)
    {
        this._dbContext = dbContext;
        this._mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<AddressDto>> Get()
    {
        var dbDatas = await _dbContext.Set<Address>().ToListAsync(); //AutoMapper 
        return dbDatas.Select(address => _mapper.Map<AddressDto>(address));
    }
    
    [HttpGet("{id:int}")]
    public async Task<AddressDto> Get(int id)
    {
        var dbData = await _dbContext.Set<Address>().FirstOrDefaultAsync(z => z.Id == id); //AutoMapper 
        return _mapper.Map<AddressDto>(dbData);
    }
    
    [HttpPost]
    public async Task Post([FromBody] AddressDto address)
    {
        var thatAddress = _mapper.Map<Address>(address); //AutoMapper 
        await _dbContext.Address.AddAsync(thatAddress);
        await _dbContext.SaveChangesAsync();
    }
    
    [HttpPut("{id:int}")]
    public async Task Put(int id, [FromBody] AddressDto address)
    {
        var fromDb = await _dbContext.Set<Address>().FirstOrDefaultAsync(z => z.Id == id);

        if (fromDb != null)
        {
            //Mapping
            fromDb.Address1 = address.Address1;
            fromDb.Address2 = address.Address2;
            fromDb.Country = address.Country;
            fromDb.City = address.City;
            fromDb.County = address.County;
            fromDb.PostalCode = address.PostalCode;
        }
        await _dbContext.SaveChangesAsync();
    }
    
    [HttpDelete("{id:int}")]
    public async Task Delete(int id)
    {
        var fromDb = await _dbContext.Set<Address>().FirstOrDefaultAsync(z => z.Id == id);
        if (fromDb != null) fromDb.IsActive = false;
        await _dbContext.SaveChangesAsync();
    }
}