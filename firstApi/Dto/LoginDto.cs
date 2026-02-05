namespace firstApi.Dto
{
    public class LoginDto
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string SecretKey { get; set; }
    }
}
