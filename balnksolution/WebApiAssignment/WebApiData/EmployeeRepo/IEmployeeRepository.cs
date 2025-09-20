using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiDomain;

namespace WebApiData.EmployeeRepo;

public interface IEmployeeRepository
{
    Task<Employee> CreateEmployee(Employee employee);

    Task<Employee?> GetByEmail(string email);

    Task<Employee?> LoginAsync(string email, string password);
}
