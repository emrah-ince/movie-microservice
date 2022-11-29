using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TMDB.Services.Movie.Model
{
    public class Movie
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string OriginalTitle { get; set; }
    }
}