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
        string error = "";
        List<CarImage> carImages = new List<CarImage>();
        public IResult Add(List<IFormFile> formFile, CarImage carImage)
        {
            var result = BusinessRules.Run(CheckIfCarImageEmpty(carImage.CarId), CheckIfCarImages(carImage.ImagePath), ProductImageUploadCountFile(formFile, carImage));
            if (result != null)
            {
                return result;
            }
            _carImageDal.MultipleAdd(carImages.ToArray());
            return new SuccessResult();

        }

        public IResult ProductImageUploadCountFile(List<IFormFile> formFile, CarImage carImage)
        {
            for (int i = 0; i < formFile.Count; i++)
            {
                var newImage = new CarImage
                {
                    CarId = carImage.CarId,
                    Date = DateTime.Now
                };
                var imageResult = FileHelper.Upload(formFile[i]);
                if (!imageResult.Success)
                {
                    error = imageResult.Message;
                    break;
                }
                else
                {
                    newImage.ImagePath = imageResult.Message;
                    carImages.Add(newImage);
                }

            }
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }
        public IResult CheckIfCarImageEmpty(int id)
        {
            var result = _carImageDal.GetAll(c => c.Id == id);
            if (result.Count == 0)
            {
                result.Add(new CarImage { ImagePath = FileConstants.DefaultImagePath });
            }
            return new SuccessResult();
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

        public IResult Update(List<IFormFile> formFile, CarImage carImage)
        {
            _carImageDal.Update(carImage);
            return new SuccessResult();
        }

        public IResult Delete(List<IFormFile> formFile, CarImage carImage)
        {
            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetCarImageById(int id)
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.Id == id));
        }
    }
}
