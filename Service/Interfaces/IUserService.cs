using Sample.api.Data;
using Service.Models;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IUserService
    {
        Users Login(string username, string password);
        bool Register(RegisterModel registerModel);
        IEnumerable<AccountQuery> GetListAccounts(string searchString);
    }
}
