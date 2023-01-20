using CustomersApi.Repositories;
using CustomersApi.Dtos;

namespace CustomersApi.CasosDeUso
{
  public interface IUpdateCustomerUseCase
  {
    Task<Dtos.CustomerDto?> Execute(Dtos.CustomerDto customer);
  }

  public class UpdateCustomerUseCase : IUpdateCustomerUseCase
  {

    private readonly CustomerDbContext _customerDbContext;

    public UpdateCustomerUseCase(CustomerDbContext customerDbContext)
    {
      _customerDbContext = customerDbContext;
    }


    public async Task<CustomerDto?> Execute(CustomerDto customer)
    {
      var entity = await _customerDbContext.Get(customer.Id);

      if (entity == null)
        return null;

      entity.FirstName = customer.FirstName;
      entity.LastName = customer.LastName;   
      entity.Email = customer.Email; 
      entity.Phone = customer.Phone;
      entity.Address = customer.Address;

      await _customerDbContext.Update(entity);
      return entity.ToDto();

    }

  }
}