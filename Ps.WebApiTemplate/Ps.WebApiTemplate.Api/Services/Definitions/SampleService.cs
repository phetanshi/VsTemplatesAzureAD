using Ps.WebApiTemplate.Data;

namespace Ps.WebApiTemplate.Api.Services.Definitions
{
    public class SampleService : ServiceBase, ISampleService
    {
        public SampleService(IRepository repository, ILogger<SampleService> logger, IConfiguration config) : base(repository, logger, config)
        {
        }

        public async Task Get()
        {
            await Task.FromResult(0);
        }
    }
}
