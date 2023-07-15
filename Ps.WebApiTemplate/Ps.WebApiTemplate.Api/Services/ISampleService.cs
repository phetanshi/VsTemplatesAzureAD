using Ps.WebApiTemplate.Data.DbModels;
using Ps.WebApiTemplate.Dtos;

namespace Ps.WebApiTemplate.Api.Services
{
    public interface ISampleService
    {
        Task<bool> DeleteEmployeeAsync(int id);
        Task<List<Employee>> Get();
        Task<EmployeeDto> InsertEmployeeAsync(EmployeeDto empDto);
        Task<EmployeeDto> UpdateEmployeeAsync(EmployeeDto empDto);
    }
}
