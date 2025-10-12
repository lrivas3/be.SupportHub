
using HelpPoint.Common;
using HelpPoint.Infrastructure.Models.Support;

namespace HelpPoint.Features.Employees;

public interface IEmployeeRepository : IRepository<Empleado>
{
    public Task<Empleado?> GetByEmailAsync(string email);
}
