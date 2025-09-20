using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiDomain;

namespace WebApiData.EmployeeRepo;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly DataContext _context;

    public EmployeeRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Employee> CreateEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee?> GetByEmail(string email)
    {

        return await _context.Employees
            .FirstOrDefaultAsync(x => x.Email == email);
    }


    public async Task<Employee?> LoginAsync(string email, string password)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(x => x.Email == email
        && x.PasswordHash == password);

    }
}
