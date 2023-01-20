using CustomersApi.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CustomersApi.Repositories
{
  public class CustomerDbContext : DbContext
  {

    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options){}

    public DbSet<CustomerEntity>? Customers { get; set; }


    public async Task<CustomerEntity> Get(long id)
    {
      return await Customers!.FirstAsync(x => x.Id == id);
    }


    public async Task<CustomerEntity> Add(CreateCustomerDto customerDto)
    {
      CustomerEntity Entity = new CustomerEntity()
      {
        Id = null,
        FirstName = customerDto.FirstName,
        LastName = customerDto.LastName,
        Email = customerDto.Email,
        Phone = customerDto.Phone,
        Address = customerDto.Address
      };

      EntityEntry<CustomerEntity> response = await Customers!.AddAsync(Entity);
      await SaveChangesAsync();

      return await  Get(response.Entity.Id ?? throw new Exception("No se ha podido guardar el cliente"));
    }

    public async Task<bool> Update(CustomerEntity customerEntity)
    {
      Customers!.Update(customerEntity);
      await SaveChangesAsync();

      return true;
    }

    public async Task<bool> Delete(long id)
        {
            CustomerEntity entity = await Get(id);
            Customers!.Remove(entity);
            SaveChanges();
            return true;
        }
  }

  public class CustomerEntity
  {
    public long? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }


     public CustomerDto ToDto()
    {
      return new CustomerDto()
      {
          Address = Address,
          Email = Email,
          FirstName = FirstName,
          LastName = LastName,
          Phone = Phone,
          Id = Id ?? throw new Exception("el id no puede ser null")
      };
    }
  }

 
}
