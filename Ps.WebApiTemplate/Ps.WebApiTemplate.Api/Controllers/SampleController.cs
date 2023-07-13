using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ps.WebApiTemplate.Api.Services;

namespace Ps.WebApiTemplate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SampleController : AppBaseController
    {
        private readonly ISampleService _sampleService;

        public SampleController(ISampleService sampleService, IConfiguration config, ILogger<SampleController> logger) : base(config, logger)
        {
            _sampleService = sampleService;
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetUsers()
        {
            await _sampleService.Get();
            return OkWrapper();
        }
    }
}
