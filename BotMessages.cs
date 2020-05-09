using BotFramework.Drivers;

namespace BotFramework
{
    public class Answer
    {
        public string Value { get; set; }
    }

    public class Message
    {
        public string Text { get; set; }
    }

    public class IncomingMessage : Message
    {
        public string SessionId { get; set; }
        public IBotDriver BotDriver { get; set; }
    }

    public class Question : Message
    {
    }
}