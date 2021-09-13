using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.Concrete.Dto;

namespace DataAccess.Concrete.EntityFramework
{
  public  class EfRentalDal:EfEntityRepository<Rental,CarContext>,IRentalDal
    {
        public List<CarRentalDetailDto> CarRentalDetails()
        {
            using (CarContext context = new CarContext())
            {
                var result = from rent in context.Rentals
                    join car in context.Cars on rent.CarId equals car.CarId
                             join color in context.Colors on car.ColorId equals color.ColorId
                    join brand in context.Brands on car.BrandId equals brand.BrandId
                    join customer in context.Customers on rent.CustomerId equals customer.CustomerId
                    join user in context.Users on customer.CustomerId equals user.Id
                    select new CarRentalDetailDto
                    {
                        RentalID = rent.RentalId,
                        CustomerName = user.FirstName,
                        CustomerLastName = user.LastName,
                        BrandName = brand.BrandName,
                        CarName = car.Description,
                        ColorName = color.ColorName,
                        DailyPrice = car.DailyPrice,
                        TotalPrice = Convert.ToDecimal(rent.ReturnDate.Value.Day - rent.RentDate.Day) * car.DailyPrice,
                        RentDate = rent.RentDate,
                        ReturnDate = rent.ReturnDate.Value

                    };
                return result.ToList();
            }
        }

        public List<CarRentalDetailDto> CarRentalDetails(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }
    }
}
