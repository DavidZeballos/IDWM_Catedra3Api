using System.ComponentModel.DataAnnotations;

namespace Cat3_API.Src.Models
{
    public class Post
    {
        public int Id { get; set; }
    
        [MinLength(5)]
        public required string Title { get; set; }

        public required string ImageUrl { get; set; }

        public DateTime PublishDate { get; set; }

        public required string UserId { get; set; }
    }
}
