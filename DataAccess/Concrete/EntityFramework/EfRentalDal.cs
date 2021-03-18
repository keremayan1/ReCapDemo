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
  public  class EfRentalDal:EfEntityRepository<Rental,CarContext>,IRentalDal
    {
        public List<CarRentalDetailDto> CarRentalDetails()
        {
            using (CarContext context = new CarContext())
            {
                var result = from rent in context.Rentals
                    join car in context.Cars on rent.CarId equals car.Id
                    join color in context.Colors on car.ColorId equals color.Id
                    join brand in context.Brands on car.BrandId equals brand.Id
                    join customer in context.Customers on rent.CustomerId equals customer.UserId
                    join user in context.Users on customer.UserId equals user.Id
                    select new CarRentalDetailDto
                    {
                        RentalID = rent.Id,
                        CustomerName = user.FirstName,
                        CustomerLastName = user.LastName,
                        BrandName = brand.Name,
                        CarName = car.Description,
                        ColorName = color.Name,
                        DailyPrice = car.DailyPrice,
                        TotalPrice = Convert.ToDecimal(rent.ReturnDate.Day - rent.RentDate.Day) * car.DailyPrice,
                        RentDate = rent.RentDate,
                        ReturnDate = rent.ReturnDate

                    };
                return result.ToList();
            }
        }
    }
}
