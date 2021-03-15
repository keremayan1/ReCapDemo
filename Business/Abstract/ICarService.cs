using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete.Dto;

namespace Business.Abstract
{
   public interface ICarService
    {
        List<Car> GetAll();
        List<Car> GetById(int id);
        List<Car> GetCarsByBrandId(int id);
        List<Car> GetCarsByColorId(int id);
        List<CarDetailDto> GetCarDetail();
        void Add(Car car);
        void Update(Car car);
        void Delete(Car car);
    }
}
