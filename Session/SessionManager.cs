using System;
using System.Collections.Generic;

namespace BotFramework.Session
{
    public class SessionManager : ISessionManager
    {
        private readonly IBotSessionDriver _driver;

        public SessionManager(IBotSessionDriver driver)
        {
            _driver = driver;
        }

        public Session RetrieveSessionState(IncomingMessage message)
        {
            var key = message.BotDriver.GetName() + message.SessionId;
            var session = _driver.Get(key) ?? new Session
            {
                Key = key
            };
            session.IncomingMessage = message;
            return session;
        }

        public void SaveSessionState(Session session)
        {
            _driver.Store(session);
        }
    }

    public interface ISessionManager
    {
        Session RetrieveSessionState(IncomingMessage sessionId);
        void SaveSessionState(Session session);
    }

    [Serializable]
    public class Session
    {
        public string Key { get; set; }
        public IncomingMessage IncomingMessage { get; set; }
        public Action<Answer> Next { get; set; }
        public Message OutgoingMessage { get; set; }
        public IDictionary<string, dynamic> Data { get; set; }
    }

    public interface IBotSessionDriver
    {
        Session Get(string key);
        void Store(Session session);
    }
}