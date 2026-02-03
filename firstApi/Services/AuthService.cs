using firstApi.Models;
using firstApi.UserModel;
using firstApi.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace firstApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly DatabaseContext _context;

        public AuthService(DatabaseContext context)
        {
            _context = context;
        }

        public bool CheckIsExistMobile(string mobile)
        {
            var user = _context.Users.FirstOrDefault(u => u.Mobile == mobile);
            if (user == null)
                return false;
            return true;
        }

        public User GetUserByMobile(string mobile)
        {
            var user = _context.Users.FirstOrDefault(u => u.Mobile == mobile);
            return user;
        }

        public string Login(UserModelBinding model)
        {
            // var hashPassword = Hasing.Decryptdata(model.Password);
            var getUserByMobile = GetUserByMobile(model.Mobile);
            var decryptPassword = Hasing.Decryptdata(getUserByMobile.Password);
            if (decryptPassword != model.Password) 
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();

            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,getUserByMobile.FullName),
                    new Claim(ClaimTypes.Role,getUserByMobile.Role)
                }),
                Expires = DateTime.Now.AddDays(7),
                Issuer = "first api",
                Audience = "customers",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("this is testthis is testthis is testthis is testthis is testthis is testthis is test")),SecurityAlgorithms.Aes256CbcHmacSha512)
            };
            var token = tokenHandler.CreateToken(descriptor);
            var result = tokenHandler.WriteToken(token);
            return result;
           
        }

        public bool Register(RegisterModel model)
        {
            var hashPassword = Hasing.PasswordHasher(model.Password);
            var result = _context.Users.Add(new User() {FullName = model.FullName,Role="user",  Mobile = model.Mobile,Password = hashPassword});
            if (result == null)
                return false;
            _context.SaveChanges();
            return true;
        }
    }
}
