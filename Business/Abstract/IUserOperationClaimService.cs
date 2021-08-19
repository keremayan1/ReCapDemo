using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
   public interface IUserOperationClaimService
   {
       IDataResult<List<UserOperationClaim>> GetAll();
       IResult Add(UserOperationClaim userOperationClaim);
       IResult Delete(UserOperationClaim userOperationClaim);
       IResult Update(UserOperationClaim userOperationClaim);
   }
}
