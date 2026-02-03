using System.ComponentModel.DataAnnotations;

namespace firstApi.UserModel
{
    public class UserModelBinding
    {

        [Required(ErrorMessage = "اجباری است")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "اجباری است")]
        public string Password { get; set; }
    }
}
