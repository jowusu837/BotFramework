using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BotFramework.Drivers
{
    public abstract class BaseBotDriver : IBotDriver
    {
        protected readonly IBot Bot;

        protected BaseBotDriver(IBot bot)
        {
            Bot = bot;
        }

        public abstract Task<JsonResult> ProcessRequest(HttpRequest request);

        public abstract string GetName();
    }
    public interface IBotDriver
    {
        Task<JsonResult> ProcessRequest(HttpRequest request);
        string GetName();
    }
}