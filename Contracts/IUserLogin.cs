using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserLogin
    {
        Task<string> login (string userId, string password);
        Task<string> forgotPassword (string email);
        Task<string> resetPassword(string userId, string oldPassword, string newPassword);
        Task<string> changePassword(string userId, string oldPassword, string newPassword);
        Task<string> logout(string userId);
    }
}
