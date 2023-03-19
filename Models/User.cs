using System.ComponentModel.DataAnnotations;

namespace drop_grungus.Models
{
    public class User
    {
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }
    }
}
