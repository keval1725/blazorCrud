using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp1.Data
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; } = DateTime.UtcNow;
        public bool? IsDeleted { get; set; }
    }
}
