using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }

        public IResult UpdateUserNames(User user)
        {
            var updatedUser = _userDal.Get(u => u.Id == user.Id);
            updatedUser.FirstName = user.FirstName;
            updatedUser.LastName = user.LastName;
            _userDal.Update(updatedUser);
            return new SuccessResult(Messages.UserUpdated);
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

        public IDataResult<User> GetByEmailWithResult(string email)
        {
            var result = GetByMail(email);
            if (result == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            return new SuccessDataResult<User>(result);
        }

        public IDataResult<User> GetById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(u=>u.Id == userId));
        }

        
    }
}
