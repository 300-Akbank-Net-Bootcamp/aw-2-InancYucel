using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Data;
using Vb.Data.DtoS;
using Vb.Data.Entity;

namespace Week2_Task_Controllers.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly VbDbContext _dbContext;
    private readonly IMapper _mapper;

    public ContactController(VbDbContext dbContext, IMapper mapper)
    {
        this._dbContext = dbContext;
        this._mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<ContactDto>> Get()
    {
        var dbDatas = await _dbContext.Set<Contact>().ToListAsync();
        return dbDatas.Select(contact => _mapper.Map<ContactDto>(contact)); //AutoMapper 
    }
     
    [HttpGet("{id:int}")]
    public async Task<ContactDto> Get(int id)
    {
        var dbData = await _dbContext.Set<Contact>().FirstOrDefaultAsync(z => z.Id == id);
        return _mapper.Map<ContactDto>(dbData);
    }
    
    [HttpPost]
    public async Task Post([FromBody] ContactDto contact)
    {
        var thatContact = _mapper.Map<Contact>(contact); //AutoMapper 
        await _dbContext.Contact.AddAsync(thatContact);
        await _dbContext.SaveChangesAsync();
    }
    
    [HttpPut("{id:int}")]
    public async Task Put(int id, [FromBody] ContactDto contact)
    {
        var fromDb = await _dbContext.Set<Contact>().FirstOrDefaultAsync(z => z.Id == id);

        if (fromDb != null)
        {
            //Mapping
            fromDb.ContactType = contact.ContactType;
            fromDb.Information = contact.Information;
        }
        await _dbContext.SaveChangesAsync();
    }
    
    [HttpDelete("{id:int}")]
    public async Task Delete(int id)
    {
        var fromDb = await _dbContext.Set<Contact>().FirstOrDefaultAsync(z => z.Id == id);
        if (fromDb != null) fromDb.IsActive = false;
        await _dbContext.SaveChangesAsync();
    }
}