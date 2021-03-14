using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
  public  class CarManager:ICarService,IRuleService<Car>
    {
        ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public List<Car> GetCarsByColorId(int id)
        {
            return _carDal.GetAll(p => p.ColorId == id);
        }

        public void Add(Car car)
        {
            minDailyPrice(car);
            minCarName(car);
            _carDal.Add(car);
        }

        public void Delete(Car car)
        {
            _carDal.Delete(car);
        }

        public List<Car> GetAll()
        {
            return _carDal.GetAll();
        }

        public List<Car> GetById(int id)
        {
            return _carDal.GetAll(p => p.Id == id);
        }

        public List<Car> GetCarsByBrandId(int id)
        {
            return _carDal.GetAll(p => p.BrandId == id);
        }

        public void Update(Car car)
        {
            _carDal.Update(car);
        }
        public void minCarName(Car entity)
        {
            if (entity.Description.Length<=2)
            {
                throw new Exception("Araba İsmi En Az 2 Haneli olmalı");
            }
        }
        public void minDailyPrice(Car entity)
        {
            if (entity.DailyPrice <= 0)
            {
                throw new Exception("Günlük Fiyat 0'dan büyük olmalı");
            }
        }
    }
}
