using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheatreService.Models
{
    public class Theatre
    {
        [BsonId]
        public int TheatreID { get; set; }
        [Required]
        public string TheatreName { get; set; }
        [Required]
        public string TheatreAddress { get; set; }
        [Required]
        public List<Show> Shows { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
