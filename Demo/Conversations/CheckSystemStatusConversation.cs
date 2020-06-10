using System.Threading.Tasks;
using Framework;
using Framework.Conversations;

namespace Demo.Conversations
{
    public class CheckSystemStatusConversation : Conversation
    {
        public CheckSystemStatusConversation(IBot bot) : base(bot)
        {
        }

        public override async Task Run()
        {
            await Bot.Reply("All systems functioning at 100%!");
        }
    }
}