using CoreStandart.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalindromController: ControllerBase
    {
        private readonly IServicePalindrom _service;
        private readonly ILogger<PalindromController> _logger;

        public PalindromController(IServicePalindrom service, ILogger<PalindromController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> CheckPalindrom([FromQuery(Name = "text")] string text)
        {
            _logger.LogInformation($"Get request: {text} || {DateTime.Now}");
            await Task.Delay(TimeSpan.FromSeconds(3));
            return Ok(await _service.CheckPalindrom(text));
        }
    }
}
