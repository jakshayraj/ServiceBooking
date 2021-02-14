using BusinessEntities;
using Data.Repository.Interface;
using System;
using System.Collections.Generic;
using Data.Model;
using AutoMapper;
using System.Data.Entity;
using System.Linq;

namespace Data.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly VehiclesEntities _dbContext;
        public LoginRepository()
        {
            _dbContext = new VehiclesEntities();
        }

       

        public int validUser(LoginViewModel objUser)
        {
            var obj = _dbContext.Customers.Where(a => a.EmailId.Equals(objUser.emailid) && a.Password.Equals(objUser.password)).FirstOrDefault();
            if (obj != null)
            {
                return obj.CustomerId;
            }
            return 0;
        }
    }
}
