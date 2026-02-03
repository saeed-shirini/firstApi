using System.ComponentModel.DataAnnotations;

namespace firstApi.UserModel
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "اجباری است")]
        public string FullName { get; set; }

        public string Role { get; set; } 

        [Required(ErrorMessage = "اجباری است")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "اجباری است")]
        public string Password { get; set; }
    }
}
