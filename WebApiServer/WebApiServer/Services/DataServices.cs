using LiteDB;
using System.Collections.Generic;
using WebApiServer.Models;

namespace WebApiServer.Services
{
    public interface IDataServices
    {
        void Add(Message message);
        Message Get(string messageId);
        IEnumerable<Message> GetMessages();
        void Update(Message message);
        void Delete(Message message);
    }
    public class DataServices : IDataServices
    {
        string connectionString = null;
        public DataServices(string connString)
        {
            connectionString = connString;
        }
        public void Add(Message message)
        {
            using (LiteDatabase database = new LiteDatabase(connectionString))
            {
                database.GetCollection<Message>("Messages").Insert(message);
                database.Commit();
            }
        }
        public Message Get(string messageId)
        {
            using (LiteDatabase database = new LiteDatabase(connectionString))
            {
                return database.GetCollection<Message>().FindById(messageId);
            }
        }
        public IEnumerable<Message> GetMessages()
        {
            using (LiteDatabase database = new LiteDatabase(connectionString))
            {
                return database.GetCollection<Message>("Messages").FindAll();
            }
        }
        public void Update(Message message)
        {
            using (LiteDatabase database = new LiteDatabase(connectionString))
            {
                database.GetCollection<Message>().Update(message);
            }
        }
        public void Delete(Message message)
        {
            using (LiteDatabase database = new LiteDatabase(connectionString))
            {
                database.GetCollection<Message>().Delete(message.id);
            }
        }
    }
}
