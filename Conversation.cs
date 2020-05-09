using System;
using Microsoft.Extensions.DependencyInjection;

namespace BotFramework
{
    public abstract class Conversation : IConversation
    {
        protected readonly IBot Bot;

        protected Conversation(IBot bot)
        {
            Bot = bot;
        }

        public abstract void Run();
    }

    public interface IConversation
    {
        void Run();
    }
    
    public class ConversationProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public ConversationProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IConversation GetEntryPointConversation()
        {
            return _serviceProvider.GetRequiredService<IEntryPointConversation>();
        }
    }

    public interface IEntryPointConversation : IConversation
    {
    }
}