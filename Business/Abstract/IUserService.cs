using Core.Entities.Concrete;
using Core.Utilities.Results;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<User> GetByEmailWithResult(string email);
        List<OperationClaim>GetClaims(User user);
        User GetByMail(string email);
        IDataResult<User> GetById(int userId);
        void Add(User user);
        IResult Update(User user);
        IResult Delete(User user);
    }
}
