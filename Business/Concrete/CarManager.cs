using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Business.Constants;
using Core.Utilities.Results;
using Entities.Concrete.Dto;

namespace Business.Concrete
{
    public class CarManager : ICarService, IRuleService<Car>
    {
        ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.ColorId == id),CarMessages.CarListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetail()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetail(),CarMessages.CarListed);
        }

        public IResult Add(Car car)
        {
            minDailyPrice(car);
            minCarName(car);
            _carDal.Add(car);
            return new SuccessResult(CarMessages.CarAdded);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(CarMessages.CarDeleted);
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(),CarMessages.CarListed);
        }

        public IDataResult<List<Car>> GetById(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.Id == id),CarMessages.CarListed);
        }

        public IDataResult<List<Car> > GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.BrandId == id),CarMessages.CarListed)  ;
        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(CarMessages.CarUpdated);
        }
        public void minCarName(Car entity)
        {
            if (entity.Description.Length <= 2)
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
