using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Concrete.Dto;

namespace DataAccess.Abstract
{
  public  interface ICustomerDal:IEntityRepository<Customer>
  {
      List<CarCustomerDetailDto> CustomerDetails();
  }
}
