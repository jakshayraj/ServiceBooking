using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;
using BusinessEntities;
using Data.Repository;
using Data.Repository.Interface;

namespace Business
{
    public class LoginManager : ILoginManager
    {
        private readonly ILoginRepository _AdminLoginRepository;

        public LoginManager(ILoginRepository AdminLoginRepository)
        {
            _AdminLoginRepository = AdminLoginRepository;
        }

      
        public int validUser(LoginViewModel objUser)
        {
            return _AdminLoginRepository.validUser(objUser);
        }
    }
}
