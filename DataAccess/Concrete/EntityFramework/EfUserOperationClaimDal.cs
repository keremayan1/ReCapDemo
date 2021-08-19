using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;

namespace DataAccess.Concrete.EntityFramework
{
   public class EfUserOperationClaimDal:EfEntityRepository<UserOperationClaim,CarContext>,IUserOperationClaimDal
    {
    }
}
