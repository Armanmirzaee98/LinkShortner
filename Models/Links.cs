using System.ComponentModel.DataAnnotations;

namespace My_LinkShortener_App.Models
{
    public class Links
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public string ShortKey { get; set; }
        public string? ShortLink { get; set; }
    }
}