using System.Collections.Generic;
using System.Threading.Tasks;
using Framework;
using Framework.Conversations;

namespace Demo.Conversations
{
    public class WelcomeConversation : Conversation, IEntryPointConversation
    {
        public WelcomeConversation(IBot bot) : base(bot)
        {
        }

        public override async Task Run()
        {
            var question = new Question
            {
                Text = "Hi there! How may I help you?",
                Options = new List<Option>
                {
                    new Option
                    {
                        Text = "Check system status",
                        Next = answer => new CheckSystemStatusConversation(Bot).Run()
                    },
                    new Option
                    {
                        Text = "Set system date and time",
                        Next = answer => SetDateTime()
                    },
                    new Option
                    {
                        Text = "Logoff",
                        Next = answer => Logoff()
                    }
                }
            };
            await Bot.Ask(question, null);
        }

        private Task Logoff()
        {
            return Bot.Reply("Hope to see you again soon!");
        }

        private Task SetDateTime()
        {
            return Bot.Ask("What date is today?", answer => Bot.Reply($"System date updated to {answer.Value}"));
        }
    }
}