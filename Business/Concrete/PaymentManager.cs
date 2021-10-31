using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
  public  class PaymentManager:IPaymentService
  {
      private IPaymentDal _paymentDal;

      public PaymentManager(IPaymentDal paymentDal)
      {
          _paymentDal = paymentDal;
      }

      public IResult Add(Payment payment)
        {
          _paymentDal.Add(payment);
          return new SuccessResult();
        }

        public IResult Delete(Payment payment)
        {
            _paymentDal.Delete(payment);
            return new SuccessResult();
        }

        public IResult Update(Payment payment)
        {
           _paymentDal.Update(payment);
           return new SuccessResult();
        }

        public IDataResult<List<Payment>> GetAll()
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetAll());
        }

        public IDataResult<Payment> GetByPaymentId(int paymentId)
        {
            return new SuccessDataResult<Payment>(_paymentDal.Get(p => p.PaymentId == paymentId));
        }
    }
}
