using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Data
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string? Title { get; set; }
        [Required]
        [MaxLength(15)]
        public string? Director { get; set; }
        [Required]
        [MaxLength(15)]
        public string? Genre { get; set; }
        public int? ReleaseYear { get; set; }
        public string? Rating { get; set; }
        public int? Duration { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime DeletedAt { get; set; } = DateTime.UtcNow;
        public bool? IsDeleted { get; set; }
    }
}
