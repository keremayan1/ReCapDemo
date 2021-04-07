using Business.Concrete;
using DataAccess.Concrete.InMemory;
using System;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

            CarManager carManager = new CarManager(new EfCarDal());
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            //   carManager.Add(new Car{Id = 2,BrandId = 1,ColorId = 1,ModelYear = 2000,DailyPrice = 100,Description = "audi",});
            foreach (var VARIABLE in rentalManager.GetAll().Data) 
            {
                
            }
        }
    }
}
