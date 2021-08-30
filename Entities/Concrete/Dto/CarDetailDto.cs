using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Concrete.Dto
{
   public class CarDetailDto:IDto
    {
        public int CarId { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public string BrandName { get; set; }

        public string ColorName { get; set; }
        public decimal DailyPrice { get; set; }
        public int ModelYear { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public bool IsRentable { get; set; }

    }
}
