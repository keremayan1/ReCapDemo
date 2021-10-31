using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Concrete
{
   public class Payment:IEntity
    {
        public int PaymentId{ get; set; }
        public int CustomerId { get; set; }
        public string CreditCardNumber { get; set; }
        public decimal Price { get; set; }
        public string ExpirationCode { get; set; }
        public string SecurityCode { get; set; }
    }
}
