﻿using System;
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
            var result = BusinessRules.Run(CheckIfBrandName(brand.Name));
            if (result!=null)
            {
                return result;
            }
            _brandDal.Add(brand);
            return new SuccessResult(BrandMessages.BrandAdded);
        }

        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult(BrandMessages.BrandDeleted);
        }

        public IResult Update(Brand brand)
        {
            _brandDal.Update(brand);
            return new SuccessResult(BrandMessages.BrandUpdated);
        }

        public IDataResult<List<Brand>> GetById(int id)
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(p => p.Id == id),BrandMessages.BrandListed);
        }

        public IResult CheckIfBrandName(string name)
        {
            var result = _brandDal.Any(b => b.Name.ToLower() == name.ToLower());
            if (result)
            {
                return new ErrorResult("Eklemek Istediginiz Araba Sistemde Vardir!");
            }

            return new SuccessResult();
        }
    }
}
