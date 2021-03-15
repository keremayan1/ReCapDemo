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
                var result = from c in carContext.Cars
                    join b in carContext.Brands on c.BrandId equals b.Id
                    join crs in carContext.Colors on c.ColorId equals crs.Id
                    select new CarDetailDto
                    {
                        CarName = c.Description,
                        BrandName = b.Name,
                        ColorName = crs.Name,
                        DailyPrice = c.DailyPrice
                    };
                return result.ToList();

            }
        }
    }
}
