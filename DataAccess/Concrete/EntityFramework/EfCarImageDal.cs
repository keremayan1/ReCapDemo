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
  public  class EfCarImageDal:EfEntityRepository<CarImage,CarContext>,ICarImageDal
    {
        public List<CarImageDetailDto> GetCarImageDetail()
        {
            using (CarContext context = new CarContext())
            {
                var result = from c in context.Cars
                    join carimages in context.CarImages on c.Id equals carimages.CarId
                    select new CarImageDetailDto
                    {
                        Id = carimages.Id,
                        CarId = c.Id,
                        ImagePath = carimages.ImagePath,
                        Date = carimages.Date,
                    };
                return result.ToList();
            }
        }
    }
}
