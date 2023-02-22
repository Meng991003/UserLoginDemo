using UserLoginDemo.Models;

namespace UserLoginDemo.Repository
{
    public interface IUserLogin
    {
        Task<String> AuthenticateUser(UserLoginInfo user);

        List<UserInfo> GetUser();
        void Logout(string userId);
    }
}
