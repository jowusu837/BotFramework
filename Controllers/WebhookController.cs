using System.Threading.Tasks;
using BotFramework.Webhooks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BotFramework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly ILogger<WebhookController> _logger;
        private readonly IWebhookHandler _handler;

        public WebhookController(ILogger<WebhookController> logger, IWebhookHandler handler)
        {
            _logger = logger;
            _handler = handler;
        }

        [HttpPost]
        [Route("{driverName}")]
        public async Task<JsonResult> Post(string driverName)
        {
            return await _handler.HandleRequest(HttpContext.Request, driverName);
        }
    }
}