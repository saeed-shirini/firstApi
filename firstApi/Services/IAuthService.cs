using firstApi.Models;
using firstApi.UserModel;

namespace firstApi.Services
{
    public interface IAuthService
    {
        bool Register(RegisterModel model);

        string Login(UserModelBinding model);

        bool CheckIsExistMobile(string mobile);

        User GetUserByMobile(string mobile);
    }
}
