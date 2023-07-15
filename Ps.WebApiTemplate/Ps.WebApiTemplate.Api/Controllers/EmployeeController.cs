using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ps.WebApiTemplate.Api.Constants;
using Ps.WebApiTemplate.Api.Services;
using Ps.WebApiTemplate.Dtos;

namespace Ps.WebApiTemplate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : AppBaseController
    {
        private readonly ISampleService _sampleService;

        public EmployeeController(ISampleService sampleService, IConfiguration config, ILogger<EmployeeController> logger) : base(config, logger)
        {
            _sampleService = sampleService;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get()
        {
            var result = await _sampleService.Get();
            return OkWrapper(result);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddEmployee(EmployeeDto emp)
        {
            var result = await _sampleService.InsertEmployeeAsync(emp);
            return OkWrapper(result);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDto emp)
        {
            var result = await _sampleService.UpdateEmployeeAsync(emp);
            return OkWrapper(result);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteEmployee([FromBody]int empId)
        {
            var isSuccess = await _sampleService.DeleteEmployeeAsync(empId);
            return OkWrapper(isSuccess, isSuccess ? AppConstants.SUCCESS : AppConstants.FAIL);
        }
    }
}
