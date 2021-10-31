using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
   public class CardManager:ICardService
   {
       private ICardDal _cardDal;

       public CardManager(ICardDal cardDal)
       {
           _cardDal = cardDal;
       }

       public IResult Add(Card card)
       {
           var result =
               BusinessRules.Run(CheckIfCardExists(card.CreditCardNumber, card.ExpirationDate, card.SecurityCode));
           if (result!=null)
           {
               return result;
           }
            _cardDal.Add(card);
            return new SuccessResult();
        }

        public IResult Update(Card card)
        {
            _cardDal.Update(card);
            return new SuccessResult();
        } 

        public IResult Delete(Card card)
        {
           _cardDal.Delete(card);
           return new SuccessResult();
        }

        public IDataResult<List<Card>> GetAllCards()
        {
            return new SuccessDataResult<List<Card>>(_cardDal.GetAll());
        }

        public IDataResult<Card> GetByCustomerId(int customerId)
        {
            var result = BusinessRules.Run(IsCustomerExists(customerId));
            if (result != null)
            {
                return new ErrorDataResult<Card>(result.Message);
            }
            return new SuccessDataResult<Card>(_cardDal.Get(c => c.CustomerId == customerId));
        }

        public IDataResult<Card> GetByCardNumber(string cardNumber)
        {
            return new SuccessDataResult<Card>(_cardDal.Get(c => c.CreditCardNumber == cardNumber));
        }

        public IDataResult<List<Card>> GetCardListByCustomerId(int customerId)
        {
            var result = BusinessRules.Run(IsCustomerExists(customerId));
            if (result!=null)
            {
                return new ErrorDataResult<List<Card>>(result.Message);
            }
            return new SuccessDataResult<List<Card>>(_cardDal.GetAll(c => c.CustomerId == customerId));
        }

        public IResult CheckIfCardExists(string cardNumber,string expirationDate,string securityCode)
        {
            //1. Yontem
            if (!_cardDal.Any(c=>c.CreditCardNumber==cardNumber&& c.ExpirationDate==expirationDate && c.SecurityCode==securityCode))
            {
                return new ErrorResult("Kredi Karti Mevcut");
            }
            //2. Yontem
            //var result = !_cardDal.Any(c =>
            //    c.CreditCardNumber == cardNumber && c.ExpirationDate == expirationDate &&
            //    c.SecurityCode == securityCode);
            //if (result)
            //{
            //    return new ErrorResult("Kredi Karti Mevcut");
            //}
            return new SuccessResult();
        }

        public IResult IsCustomerExists(int customerId)
        {
            var result = _cardDal.Any(x => x.CustomerId == customerId);
            if (result==false)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }
    }
}
