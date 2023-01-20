
using CustomersApi.Dtos;
using CustomersApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using CustomersApi.CasosDeUso;


namespace CustomersApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CustomerController : Controller
  {
    private readonly CustomerDbContext _customerDbContext;
    private readonly IUpdateCustomerUseCase _updateCustomerUseCase;

        public CustomerController(CustomerDbContext customerDatabaseContext, IUpdateCustomerUseCase updateCustomerUseCase)
        {
            _customerDbContext = customerDatabaseContext;
            _updateCustomerUseCase = updateCustomerUseCase; 
        }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(List<CustomerDto>))]
    public async Task<IActionResult> GetCustomers()
    {
      var result = _customerDbContext!.Customers!.Select(c => c.ToDto()).ToList();

      return new OkObjectResult(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(CustomerDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomer(long id)
    {
    
      CustomerEntity result = await _customerDbContext.Get(id);
      return new OkObjectResult(result.ToDto());

    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type=typeof(CustomerDto))]
    public async Task<IActionResult> CreateCustomer(CreateCustomerDto customer)
    {
      CustomerEntity result = await _customerDbContext.Add(customer);

      return new CreatedResult($"https://localhost:7099/api/customer/{result.Id}", null);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(CustomerDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCustomer(CustomerDto customer)
    {
      CustomerDto? result = await _updateCustomerUseCase.Execute(customer);

        if (result == null)
            return new NotFoundResult();

        return new OkObjectResult(result);
    }

    // [HttpPut("{id}")]
    // public async Task<CustomerDto> UpdateCustomer(long id, CustomerDto customer)
    // {
    //   throw new NotImplementedException();
    // }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCustomer(long id)
    {
      var result = await _customerDbContext.Delete(id);

      return new OkObjectResult(result);
    }

  }
}