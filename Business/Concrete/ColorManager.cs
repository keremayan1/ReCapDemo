using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        private IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(), ColorMessages.ColorListed);
        }

        public IResult Add(Color color)
        {
            var result = BusinessRules.Run(CheckIfColorNameExists(color.Name));
            if (result!=null)
            {
                return result;
            }
            _colorDal.Add(color);
            return new SuccessResult(ColorMessages.ColorAdded);
        }
        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult(ColorMessages.ColorDeleted);
        }

        public IResult Update(Color color)
        {
            _colorDal.Update(color);
            return new SuccessResult(ColorMessages.ColorUpdated);
        }

        public IDataResult<List<Color>> GetById(int id)
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(p => p.Id == id), ColorMessages.ColorListed);
        }

        public IResult CheckIfColorNameExists(string colorName)
        {
            var result = _colorDal.Any(c => c.Name.ToLower() == colorName.ToLower());
            if (result)
            {
                return new ErrorResult("Eklemek Istediginiz Renk Sistemde Vardir");
            }

            return new SuccessResult();
        }
    }
}
