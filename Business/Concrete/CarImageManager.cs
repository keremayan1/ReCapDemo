using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }
        public IResult Add(IFormFile formFile, CarImage carImage)
        {
            var result = BusinessRules.Run(CheckIfCarImages(carImage.ImagePath));
            if (result!=null)
            {
                return new ErrorResult();
            }

            var uploadFile =
                FileHelper.Upload(formFile, FileConstants.FileImageExtensions, FileConstants.ImageFolderName);
            if (!uploadFile.Item1.Success)
            {
                return uploadFile.Item1;
            }

            carImage.ImagePath = uploadFile.dbPath;
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult("Islem Basarili");
        }

        public IResult Update(IFormFile formFile, CarImage carImage)
        {
            _carImageDal.Update(carImage);
            return new SuccessResult();
        }

        public IResult Delete(IFormFile formFile, CarImage carImage)
        {
           _carImageDal.Delete(carImage);
           return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<List<CarImage>> GetCarImageById(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId);
            if (result.Count==0)
            {
               result.Add(new CarImage{ImagePath = FileConstants.DefaultImagePath});
            }

            return new SuccessDataResult<List<CarImage>>();
        }



        private IResult CheckIfCarImages(string imagePath)
        {
            var result = _carImageDal.GetAll(c => c.ImagePath == imagePath).Count;
            if (result > 5)
            {
                return new ErrorResult("Arabanin Resmi besten fazla olamaz");
            }

            return new SuccessResult();
        }
    }
}
