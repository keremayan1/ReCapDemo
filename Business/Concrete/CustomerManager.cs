using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dto;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }

        public IDataResult<List<Customer>> GetByCustomerId(int id)
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(
                p => p.CustomerId == id));
        }

        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult();
        }

        public IResult Delete(Customer customer)
        {
            var deleteCustomer = _customerDal.Get(c => c.CustomerId == customer.CustomerId);
           _customerDal.Delete(deleteCustomer);
           return new SuccessResult();
        }

        public IResult Update(Customer customer)
        {
           _customerDal.Update(customer);
           return new SuccessResult();
        }

        public IDataResult<List<CarCustomerDetailDto>> GetCustomerDetails(Customer customer)
        {
            return new SuccessDataResult<List<CarCustomerDetailDto>>(_customerDal.CustomerDetails());
        }
    }
}
