using FacebookLeadAdsWebhooks.Model;
using MongoDB.Driver;

namespace FacebookLeadAdsWebhooks.Helpers
{
    public static class MongoDBManager
    {
        public static void Insert(string logType, string message)
        {
            var connectionString = "<MongoDB Connection String>";
            var databaseName = "fb_webhooks";
            var collectionName = "logs";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<LogModel>(collectionName);
            collection.InsertOne(new LogModel
            {
                LogType = logType,
                Details = message
            });
        }
    }
}