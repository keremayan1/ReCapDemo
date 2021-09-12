using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(),BrandMessages.BrandListed);
        }
       // [SecuredOperation("brands.add")]
        public IResult Add(Brand brand)
        {
            var result = BusinessRules.Run(CheckIfBrandNameExists(brand.BrandName));
            if (result!=null)
            {
                return result;
            }
            _brandDal.Add(brand);
            return new SuccessResult(BrandMessages.BrandAdded);
        }

        public IResult Delete(Brand brand)
        {
            var result = BusinessRules.Run(CheckIfBrandIdExists(brand.BrandId));
            if (result!=null)
            {
                return result;
            }
            var deletedBrand = _brandDal.Get(b => b.BrandId == brand.BrandId);
            _brandDal.Delete(deletedBrand);
            return new SuccessResult(BrandMessages.BrandDeleted);
        }

        public IResult Update(Brand brand)
        {
            _brandDal.Update(brand);
            return new SuccessResult(BrandMessages.BrandUpdated);
        }

       public IDataResult<Brand> GetByBrandId(int brandId)
        {
            var result = BusinessRules.Run(CheckIfBrandIdExists(brandId));
            if (result!=null)
            {
                return new ErrorDataResult<Brand>(result.Message);
            }
            return new SuccessDataResult<Brand>(_brandDal.Get(p => p.BrandId == brandId),BrandMessages.BrandListed);
        }

        public IResult CheckIfBrandNameExists(string name)
        {
            var result = _brandDal.Any(b => b.BrandName.ToLower() == name.ToLower());
            if (result)
            {
                return new ErrorResult("Eklemek Istediginiz Araba Sistemde Vardir!");
            }
            return new SuccessResult();
        }

        public IResult CheckIfBrandIdExists(int brandId)
        {
            var result = _brandDal.Any(b => b.BrandId == brandId);
            if (!result)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }
    }
}
