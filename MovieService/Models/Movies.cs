using MongoDB.Bson.Serialization.Attributes;

namespace MovieService.Models
{
    public class Movies
    {
        [BsonId]
        public string ImdbId { get; set; }
        public string MovieName { get; set; }
        public string DateOfRelease { get; set; }
        public string Duration { get; set; }
        public string Rating { get; set; }
        public string Actors { get; set; }
        public string ImagePath { get; set; }
    }
}
