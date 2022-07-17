using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FacebookLeadAdsWebhooks.Model
{
    public class LogModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public string LogType { get; set; }

        public string Details { get; set; }
    }
}