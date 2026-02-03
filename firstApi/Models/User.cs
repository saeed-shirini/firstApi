using System.ComponentModel.DataAnnotations;

namespace firstApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Role { get; set; }
        [Required(ErrorMessage ="اجباری است")]
        public string Mobile  { get; set; }

        [Required(ErrorMessage = "اجباری است")]
        public string Password { get; set; }
    }
}
