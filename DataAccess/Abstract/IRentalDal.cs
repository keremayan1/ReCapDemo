using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Concrete.Dto;

namespace DataAccess.Abstract
{
   public interface IRentalDal:IEntityRepository<Rental>
   {
       List<CarRentalDetailDto> CarRentalDetails();

   }
}
