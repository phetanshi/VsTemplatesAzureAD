using $ext_projectname$.Data.DbModels;
using $ext_projectname$.Dtos;

namespace $safeprojectname$.Services
{
    public interface ISampleService
    {
        Task<bool> DeleteEmployeeAsync(int id);
        Task<List<Employee>> Get();
        Task<EmployeeDto> InsertEmployeeAsync(EmployeeDto empDto);
        Task<EmployeeDto> UpdateEmployeeAsync(EmployeeDto empDto);
    }
}
