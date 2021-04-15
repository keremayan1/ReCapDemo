using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
   public class CarImageManager:ICarImageService
   {
       private ICarImageDal _carImageDal;

       public CarImageManager(ICarImageDal carImageDal)
       {
           _carImageDal = carImageDal;
       }


       public IResult Add(CarImage carImage)
       {
           var result = BusinessRules.Run(CheckIfCarImages(carImage.ImagePath));
           if (result!=null)
           {
               return new ErrorResult();
           }

           _carImageDal.Add(carImage);
           return new SuccessResult();
        }

        public IResult Update(CarImage carImage)
        {
            var result = BusinessRules.Run(CheckIfCarImages(carImage.ImagePath));
            if (result!=null)
            {
                return new ErrorResult();
            }

           _carImageDal.Update(carImage);
           return new SuccessResult();
        }

        public IResult Delete(CarImage carImage)
        {
           _carImageDal.Delete(carImage);
           return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<List<CarImage>> GetById(int id)
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.Id == id));
        }

        private IResult CheckIfCarImages(string imagePath)
        {
            var result = _carImageDal.GetAll(c => c.ImagePath == imagePath).Count;
            if (result>15)
            {
                return new ErrorResult("Arabanin Resmi besten fazla olamaz");
            }

            return new SuccessResult();
        }
    }
}
