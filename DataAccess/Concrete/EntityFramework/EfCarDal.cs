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
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal :EfEntityRepository<Car,CarContext>,ICarDal
    {
        public List<CarDetailDto> GetCarDetail()
        {
            using (CarContext carContext = new CarContext())
            {
                var result = from car in carContext.Cars
                             join brand in carContext.Brands
                             on car.BrandId equals brand.Id
                             join color in carContext.Colors
                             on car.ColorId equals color.Id
                             where car.BrandId == brand.Id && car.ColorId == color.Id
                             select new CarDetailDto
                             {
                                 CarId = car.Id,
                                 CarName = brand.Name,
                                 ColorName = color.Name,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 ModelYear = car.ModelYear
                             };
                return result.ToList();

            }
        }
    }
}
