using Sample.api.Data;
using Repository.Interfaces;
using Service.Helper;
using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implements
{
    public class UserService : IUserService
    {
        private readonly IRepository<Users> _userRepository;
        public UserService(IRepository<Users> userRepository)
        {
            _userRepository = userRepository;
        }

        public Users Login(string username, string Password)
        {
            string encrypt = EncryptHelper.ComputeMd5Hash(Password);
            return _userRepository.Query().FirstOrDefault(u => u.Username == username && u.Password == encrypt);
        }

        public bool Register(RegisterModel registerModel)
        {
            using (var transition = _userRepository.BeginTransaction())
            {
                try
                {
                    string hashedPassword = EncryptHelper.ComputeMd5Hash(registerModel.Password);

                    var user = new Users
                    {
                        FullName = registerModel.FullName,
                        Username = registerModel.UserName,
                        Password = hashedPassword,
                        PhoneNumber = registerModel.Phone,
                        Email = $"{registerModel.UserName}@example.com", // Generate email from username
                        IsActive = true,
                        CreatedDate = DateTime.Now
                    };

                    _userRepository.Insert(user);

                    if (_userRepository.SaveChanges() > 0)
                    {
                        transition.Commit();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    new LoggingHelper().WriteLog("Exception: " + ex.ToString());
                    transition.Dispose();
                    return false;
                }
            }
        }

        public IEnumerable<AccountQuery> GetListAccounts(string searchString)
        {
            try
            {
                return _userRepository.ExecuteStoredProcedure<AccountQuery>("dbo.GetListAccounts",
                                new
                                {
                                    SearchString = searchString
                                }
                            );
            }
            catch (Exception ex)
            {
                new LoggingHelper().WriteLog("Exception: " + ex.ToString());
                return null;
            }

        }
    }

}
