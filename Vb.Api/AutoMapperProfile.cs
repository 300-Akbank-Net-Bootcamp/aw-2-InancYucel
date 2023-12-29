using AutoMapper;
using Vb.Data.DtoS;
using Vb.Data.Entity;

namespace Vb.Api;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Contact, ContactDto>(); //Which class's dto is which?
        CreateMap<ContactDto, Contact>();
        
        CreateMap<Account, AccountDto>(); //Which class's dto is which?
        CreateMap<AccountDto, Account>();
        
        CreateMap<Customer, CustomerDto>(); //Which class's dto is which?
        CreateMap<CustomerDto, Customer>();
        
        CreateMap<AccountTransaction, AccountTransactionDto>(); //Which class's dto is which?
        CreateMap<AccountTransactionDto, AccountTransaction>();
        
        CreateMap<Address, AddressDto>(); //Which class's dto is which?
        CreateMap<AddressDto, Address>();
        
        CreateMap<EftTransaction, EftTransactionDto>(); //Which class's dto is which?
        CreateMap<EftTransactionDto, EftTransaction>();
    }
}