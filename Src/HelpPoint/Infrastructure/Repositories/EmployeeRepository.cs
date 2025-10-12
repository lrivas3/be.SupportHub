using HelpPoint.Features.Employees;
using HelpPoint.Infrastructure.DataBase;
using HelpPoint.Infrastructure.Models.Support;
using Microsoft.EntityFrameworkCore;

namespace HelpPoint.Infrastructure.Repositories;

public class EmployeeRepository(HelpPointDbContext context) : Repository<Empleado>(context), IEmployeeRepository
{
    public async Task<Empleado?> GetByEmailAsync(string email) => await context.Empleados.FirstOrDefaultAsync(x => x.Email == email);
}
