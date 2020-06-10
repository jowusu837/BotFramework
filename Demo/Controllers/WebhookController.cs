using System.Text.Json;
using System.Threading.Tasks;
using Framework;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly IWebhookHandler _handler;

        public WebhookController(IWebhookHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        [Route("{driverName}")]
        public async Task<JsonResult> Post(string driverName)
        {
            var response = await _handler.HandleRequest(driverName);
            return new JsonResult(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            });
        }

    }
}