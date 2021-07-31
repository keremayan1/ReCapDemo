using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.Concrete.Dto;

namespace DataAccess.Concrete.EntityFramework
{
   public class EfCustomerDal:EfEntityRepository<Customer,CarContext>,ICustomerDal
    {
        public List<CarCustomerDetailDto> CustomerDetails()
        {
            using (CarContext context = new CarContext())
            {
                var result = from c in context.Customers
                    join u in context.Users on c.CustomerId equals u.Id
                    select new CarCustomerDetailDto
                    {
                        Id = u.Id,
                        CustomerId = c.CustomerId,
                         FirstName = u.FirstName,
                         LastName = u.LastName,
                         EMail = u.Email,
                        
                         CompanyName = c.CompanyName

                    };
                return result.ToList();
            }
        }
    }
}
