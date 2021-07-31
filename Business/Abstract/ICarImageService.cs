using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult Add(List<IFormFile>formFile, CarImage carImage);
        IResult Update(List<IFormFile> formFile, CarImage carImage);
        IResult Delete(List<IFormFile> formFile, CarImage carImage);
        IDataResult<List<CarImage>> GetAll();
        IDataResult<List<CarImage>> GetCarImageById(int id);

    }
}
