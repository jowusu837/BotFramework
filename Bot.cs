using System;
using System.Threading.Tasks;
using BotFramework.Drivers;
using BotFramework.Session;
using BotFramework.Webhooks;
using Microsoft.Extensions.DependencyInjection;

namespace BotFramework
{
    public class Bot : IBot
    {
        private readonly ISessionManager _sessionManager;
        private readonly ConversationProvider _conversationProvider;
        private Session.Session _session;

        public Bot(ISessionManager sessionManager, ConversationProvider conversationProvider)
        {
            _sessionManager = sessionManager;
            _conversationProvider = conversationProvider;
        }

        public Message ProcessMessage(IncomingMessage message)
        {
            _session = _sessionManager.RetrieveSessionState(message);
            var nextAction = _session.Next;
            if (nextAction == null)
            {
                _conversationProvider.GetEntryPointConversation().Run();
            }
            else
            {
                nextAction(new Answer
                {
                    Value = message.Text
                });
            }

            return _session.OutgoingMessage;
        }

        public void Ask(Message outgoingMessage, Action<Answer> next)
        {
            _session.OutgoingMessage = outgoingMessage;
            _session.Next = next;
            _sessionManager.SaveSessionState(_session);
        }

        public void Reply(string message)
        {
            Ask(new Message
            {
                Text = message
            }, null);
        }

        public void StoreSessionData<T>(string key, T value)
        {
            Task.Run(() =>
            {
                _session.Data[key] = value;
                _sessionManager.SaveSessionState(_session);
            });
        }
    }

    public interface IBot
    {
        Message ProcessMessage(IncomingMessage message);
        void Ask(Message outgoingMessage, Action<Answer> next);
        void Reply(string message);
        void StoreSessionData<T>(string key, T value);
    }

    public static class BotExtensions
    {
        public static void AddBot(this IServiceCollection services, Action<BotBuilder> botBuilder)
        {
            services.AddScoped<IWebhookHandler, WebhookHandler>();
            services.AddSingleton<ISessionManager, SessionManager>();
            services.AddScoped<ConversationProvider>();
            services.AddScoped<IBot, Bot>();
            botBuilder(new BotBuilder(services));
        }
    }

    public class BotBuilder
    {
        private readonly IServiceCollection _serviceCollection;

        public BotBuilder(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public void UseSessionDriver<T>() where T : class, IBotSessionDriver
        {
            _serviceCollection.AddSingleton<IBotSessionDriver, T>();
        }

        public void AddDriver<T>() where T : class, IBotDriver
        {
            _serviceCollection.AddScoped<IBotDriver, T>();
        }

        public void SetEntryPointConversation<T>() where T : class, IEntryPointConversation
        {
            _serviceCollection.AddScoped<IEntryPointConversation, T>();
        }
    }
}