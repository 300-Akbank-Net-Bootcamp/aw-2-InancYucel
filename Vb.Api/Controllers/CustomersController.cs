using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Data;
using Vb.Data.DtoS;
using Vb.Data.Entity;

namespace Week2_Task_Controllers.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly VbDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public CustomersController(VbDbContext dbContext, IMapper mapper)
    {
        this._dbContext = dbContext;
        this._mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<CustomerDto>> Get()
    {
        var dbDatas = await _dbContext.Set<Customer>().ToListAsync(); 
        return dbDatas.Select(customer => _mapper.Map<CustomerDto>(customer)); //AutoMapper 
    }

    [HttpGet("{id:int}")]
    public async Task<CustomerDto> Get(int id)
    {
        var dbData = await _dbContext.Set<Customer>().FirstOrDefaultAsync(z => z.Id == id); 
        return _mapper.Map<CustomerDto>(dbData); //AutoMapper 
    }

    [HttpPost]
    public async Task Post([FromBody] CustomerDto customer)
    {
        var thatCustomer = _mapper.Map<Customer>(customer);
        await _dbContext.Customers.AddAsync(thatCustomer);
        await _dbContext.SaveChangesAsync();
    }

    [HttpPut("{id:int}")]
    public async Task Put(int id, [FromBody] CustomerDto customer)
    {
        var fromDb = await _dbContext.Set<Customer>().FirstOrDefaultAsync(z => z.Id == id);

        if (fromDb != null)
        {
            //Mapping
            fromDb.FirstName = customer.FirstName;
            fromDb.LastName = customer.LastName;
            fromDb.CustomerNumber = customer.CustomerNumber;
            fromDb.DateOfBirth = customer.DateOfBirth;
            fromDb.LastActivityDate = customer.LastActivityDate;
        }
        await _dbContext.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        var fromDb = await _dbContext.Set<Customer>().FirstOrDefaultAsync(z => z.Id == id);
        if (fromDb != null) fromDb.IsActive = false;
        await _dbContext.SaveChangesAsync();
    }
}