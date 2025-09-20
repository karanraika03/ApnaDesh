using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiApplication.Employee.Dto;

namespace WebApiApplication.Employee;

public interface IEmployeeApplication
{
    Task<int> CreateEmployee(CreateEmployeeDto input);
    Task<LoginResponseDto> LoginAsync(LoginDto dto);
}
