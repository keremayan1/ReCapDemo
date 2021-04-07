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

        public IDataResult<Rental> IsForRent(int id)
        {
            var IsForRent1 = _rentalDal.GetAll(p => p.CarId == id).Any(p => p.CarId == id);
            if (IsForRent1)
            {
                var result = _rentalDal.GetAll(p => p.CarId == id && p.ReturnDate != null);
                if (result != null)
                {

                    return new SuccessDataResult<>("Başarılı1");
                }
                return new SuccessDataResult<bool>("Başarılı2");
            }
            return new SuccessDataResult<bool>("Başarılı3");
        }

        public IResult IsForRentCompany(Rental rental)
        {
            var result = _rentalDal.GetAll(p => p.CarId == rental.CarId).Any(p => p.CarId == rental.CarId);
            if (result)
            {
                var result2 = _rentalDal.Get(p =>
                    p.CarId == rental.CarId && p.ReturnDate != null && p.CustomerId == rental.CustomerId);
                if (result2!=null)
                {
                     return new SuccessResult("Başarılı1");
                }

                return new SuccessResult("Başarılı2");
            }
            return new SuccessResult("Başarılı3");
        }
    }
}
