using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Entities.Concrete.Dto;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car>()
            {
                new Car{Id=1,BrandId=1,ColorId=1,ModelYear=2017,DailyPrice=15000,Description="Nissan GT-R "},
                 new Car{Id=2,BrandId=2,ColorId=4,ModelYear=2012,DailyPrice=15000,Description="Ford Shelby 500"},
                  new Car{Id=3,BrandId=2,ColorId=2,ModelYear=2021,DailyPrice=15000,Description="Bugatti Chiron"},
                   new Car{Id=4,BrandId=3,ColorId=3,ModelYear=2015,DailyPrice=15000,Description="Mclaren MP-4"},
            };
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDeleted = _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars.Remove(carToDeleted);

        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetCarDetail()
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _cars;
        }
        public List<Car> GetById(int id)
        {
            return _cars.Where(c => c.Id == id).ToList();
        }
        public void Update(Car car)
        {
            Car carToUpdated = _cars.SingleOrDefault(c => c.Id == car.Id);
            carToUpdated.Id = car.Id;
            carToUpdated.ColorId = car.ColorId;
            carToUpdated.BrandId = car.BrandId;
            carToUpdated.DailyPrice = car.DailyPrice;
            carToUpdated.Description = car.Description;
            carToUpdated.ModelYear = car.ModelYear;
        }

        public void MultipleAdd(Car[] entity)
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetCarDetail(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public bool Any(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
