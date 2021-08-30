using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;
        private ICarService _carService;
       

        public RentalManager(IRentalDal rentalDal, ICarService carService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
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
            var result = BusinessRules.Run(CheckIfCarId(rental.CarId), CheckIfRentalId(rental.Id), IsCarAvaliable(rental.CarId));
            if (result != null)
            {
                return result;
            }
            _rentalDal.Add(rental);
            return new SuccessResult();
        }

        public IResult Update(Rental rental)
        {
            var result = BusinessRules.Run(CheckIfRentalId(rental.Id), CheckIfCarId(rental.CarId));
            if (result != null)
            {
                return result;
            }
            _rentalDal.Update(rental);
            return new SuccessResult();
        }

        public IResult Delete(Rental rental)
        {
            var result = BusinessRules.Run(CheckIfRentalId(rental.Id));
            if (result != null)
            {
                return result;
            }
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetRentalDetails(int id)
        {
            _rentalDal.CarRentalDetails();
            return new SuccessDataResult<List<Rental>>();
        }
        public IResult CheckIfCarId(int carId)
        {
            var result = _carService.GetByCarId(carId);
            if (result == null)
            {
                return new ErrorResult("Arac Kiralanamaz!");
            }
            return new SuccessResult();
        }
        public IResult IsCarAvaliable(int carId)
        {
            var result = _rentalDal.Any(r => r.CarId == carId && (r.ReturnDate == null || r.ReturnDate < DateTime.Now));
            if (result)
            {
                return new ErrorResult("Araba Kiralanmaya uygun değil");
            }
            return new SuccessResult();
        }
        public IResult CheckIfRentalId(int rentalId)
        {
            var result = _rentalDal.Any(r => r.Id == rentalId);
            if (!result)
            {
                return new ErrorResult("Araba Kiralanmaya uygun değil");
            }
            return new SuccessResult();
        }



    }
}
