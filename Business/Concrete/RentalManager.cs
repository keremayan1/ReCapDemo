using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<List<Rental>> GetById(int rentalId)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(p => p.Id == rentalId));
        }

        public IResult Add(Rental rental)
        {
            var result = CheckReturnDate(rental.CarId);
            if (!result.Success)
            {
                return new ErrorResult();
            }

            _rentalDal.Add(rental);
            return new SuccessResult();
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult();
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetRentalDetails(int id)
        {
            _rentalDal.CarRentalDetails();
            return new SuccessDataResult<List<Rental>>();
        }

        public IResult CheckReturnDate(int id)
        {
            var result = _rentalDal.GetAll(p => p.CarId == id && p.ReturnDate == null);
            if (result.Count > 0)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

        public IResult UpdateReturnDate(Rental rental)
        {
            var result = _rentalDal.GetAll(p => p.Id == rental.Id);
            var result2 = result.LastOrDefault();
            if (result2.ReturnDate!=null)
            {
                return new ErrorResult();
            }

            result2.ReturnDate = rental.ReturnDate;
             _rentalDal.Update(result2);
            return new SuccessResult();
        }
    }
}
