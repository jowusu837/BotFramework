using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotFramework.Drivers;
using BotFramework.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BotFramework.Webhooks
{
    public class WebhookHandler : IWebhookHandler
    {
        private readonly IEnumerable<IBotDriver> _drivers;

        public WebhookHandler(IServiceProvider provider)
        {
            _drivers = provider.GetServices<IBotDriver>();
        }

        public async Task<JsonResult> HandleRequest(HttpRequest request, string driverName)
        {
            var driver = _drivers.First(d => d.GetName().Equals(driverName, StringComparison.OrdinalIgnoreCase));
            if (driver == null)
            {
                throw new DriverNotFoundException("No driver was found that can handle this request.");
            }

            var result = await driver.ProcessRequest(request);
            return result;
        }
    }

    public interface IWebhookHandler
    {
        Task<JsonResult> HandleRequest(HttpRequest request, string driverName);
    }
}