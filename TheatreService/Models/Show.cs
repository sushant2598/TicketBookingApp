using System.Collections.Generic;

namespace TheatreService.Models
{
    public class Show
    {
        public string ShowDate { get; set; }
        public List<ShowDetails> ShowsDetails { get; set; }
    }
}
