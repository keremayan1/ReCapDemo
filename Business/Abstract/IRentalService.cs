using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();
        IDataResult<List<Rental>> GetById(int rentalId);
        IResult Add(Rental rental);
        IResult Update(Rental rental);
        IResult Delete(Rental rental);
        IDataResult<List<Rental>> GetRentalDetails(int id);
        IDataResult<Rental> IsForRent(int id);
        IResult IsForRentCompany(Rental rental);
        IResult ReturnDateNull(Rental rental);


    }
}
