using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersApi.Dtos
{
  public class CreateCustomerDto
  {
    [Required(ErrorMessage ="El nombre debe estar especificado")]
    public string? FirstName { get; set; }
    [Required(ErrorMessage ="El apellido debe estar especificado")]
    public string? LastName { get; set; }
    [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage ="El email NO es valido") ]
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
  }
}