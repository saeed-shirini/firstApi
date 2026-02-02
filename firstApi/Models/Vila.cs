using System.ComponentModel.DataAnnotations;

namespace firstApi.Models
{
    public class Vila
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string  City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Mobile { get; set; }
    }
}
