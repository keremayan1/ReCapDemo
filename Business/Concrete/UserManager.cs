using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult();
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult();
        }

        public IDataResult<List<User>> GetAll()
        {
            _userDal.GetAll();
            return new SuccessDataResult<List<User>>();
        }

        public IDataResult<List<User>> GetByUserId(int id)
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(u => u.Id == id));
        }

        public List<OperationClaim> GetClaims(User user)
        {
           return _userDal.GetClaims(user);
            
        }

        public User GetUserByEmail(string email)
        {
           return _userDal.Get(p => p.Email == email);
          
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult();
        }
    }
}
