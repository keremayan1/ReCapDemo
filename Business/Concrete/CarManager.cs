using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Business.Constants;
using Business.Validation_Rules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Entities.Concrete.Dto;
using Core.Utilities.Business;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        private IColorService _colorService;
        private IBrandService _brandService;
        public CarManager(ICarDal carDal, IColorService colorService, IBrandService brandService)
        {
            _carDal = carDal;
            _colorService = colorService;
            _brandService = brandService;
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.ColorId == id), CarMessages.CarListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailByCarId(int carId)
        {
            var result = BusinessRules.Run(IsCarExists(carId));
            if (result != null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(result.Message);
            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetail(c => c.CarId == carId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetail()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetail(), CarMessages.CarListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailByBrandIdAndColorId(int brandId, int colorId)
        {
            var result = BusinessRules.Run(IsBrandExists(brandId), IsColorExists(colorId));
            if (result != null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(result.Message);
            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetail(c =>
                c.BrandId == brandId && c.ColorId == colorId));
        }

        [ValidationAspect(typeof(CarValidatior))]
        // [CacheRemoveAspect("Get")]
        public IResult Add(Car car)
        {
            var result = BusinessRules.Run(IsBrandExists(car.BrandId), IsColorExists(car.ColorId));
            if (result!=null)
            {
                return result;
            }

            _carDal.Add(car);
            return new SuccessResult(CarMessages.CarAdded);
        }

        public IResult Delete(Car car)
        {
            var deletedCar = _carDal.Get(c => c.CarId == car.CarId);

            _carDal.Delete(deletedCar);
            return new SuccessResult(CarMessages.CarDeleted);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailByColorId(int colorId)
        {
            var result = BusinessRules.Run(IsColorExists(colorId));
            if (result != null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(result.Message);
            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetail(c => c.ColorId == colorId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailByBrandId(int brandId)
        {
            var result = BusinessRules.Run(IsBrandExists(brandId));
            if (result != null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(result.Message);
            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetail(c => c.BrandId == brandId));
        }

       // [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), CarMessages.CarListed);
        }

        public IDataResult<Car> GetByCarId(int carId)
        {
            var result = BusinessRules.Run(IsCarExists(carId));
            if (result != null)
            {
                return new ErrorDataResult<Car>(result.Message);
            }
            return new SuccessDataResult<Car>(_carDal.Get(p => p.CarId == carId), CarMessages.CarListed);
        }
        //[CacheAspect]
        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            var result = BusinessRules.Run(IsBrandExists(brandId));
            if (result != null)
            {
                return new ErrorDataResult<List<Car>>(result.Message);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.BrandId == brandId), CarMessages.CarListed);
        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(CarMessages.CarUpdated);
        }

        public IResult IsColorExists(int colorId)
        {
            var result = _colorService.GetByColorId(colorId);
            if (result == null)
            {
                return new ErrorResult("Hata");
            }

            return new SuccessResult();
        }

        public IResult IsCarExists(int carId)
        {
            var result = _carDal.Get(c => c.CarId == carId);
            if (result == null)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

        public IResult IsBrandExists(int brandId)
        {
            var result = _brandService.GetByBrandId(brandId);
            if (result == null)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

 



    }
}
