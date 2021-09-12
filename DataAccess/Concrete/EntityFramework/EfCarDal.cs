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
        

        public List<CarDetailDto> GetCarDetail(Expression<Func<Car, bool>> filter = null)
        {
            using (var context = new CarContext())
            {
                var result = from car in filter is null ? context.Cars : context.Cars.Where(filter)
                             join brand in context.Brands on car.BrandId equals brand.BrandId
                             join color in context.Colors on car.ColorId equals color.ColorId
                             select new CarDetailDto
                             {
                                 CarId = car.CarId,
                                 BrandId = brand.BrandId,
                                 ColorId = color.ColorId,
                                 BrandName = brand.BrandName,
                                 ColorName = color.ColorName,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 ModelYear = car.ModelYear,
                                 ImagePath = (from image in context.CarImages where car.CarId == image.CarId select image.ImagePath).FirstOrDefault(),
                                 IsRentable = !context.Rentals.Any(r => r.CarId == car.CarId) || !context.Rentals.Any(r => r.CarId == car.CarId && (r.ReturnDate == null || (r.ReturnDate.HasValue && r.ReturnDate > DateTime.Now)))
                             };
                return result.ToList();

            }
        }
    }
}
