using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
   public interface IPaymentService
   {
       IResult Add(Payment payment);
       IResult Delete(Payment payment);
       IResult Update(Payment payment);
       IDataResult<List<Payment>> GetAll();
       IDataResult<Payment> GetByPaymentId(int paymentId);
   }
}
