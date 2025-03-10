using System.ComponentModel.DataAnnotations;

namespace Library.API.DTOs
{
    public class UpdateBookDto
    {
        [Required]
        public string NewTitle { get; set; }

        [Required]
        public string NewAuthor { get; set; }
    }
}
