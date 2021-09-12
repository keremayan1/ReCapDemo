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
            var result = BusinessRules.Run(CheckIfColorNameExists(color.ColorName));
            if (result!=null)
            {
                return result;
            }
            _colorDal.Add(color);
            return new SuccessResult(ColorMessages.ColorAdded);
        }
        public IResult Delete(Color color)
        {
            var result = BusinessRules.Run(CheckIfColorIdExists(color.ColorId));
            if (result != null)
            {
                return result;
            }

            var deletedColor = _colorDal.Get(c => c.ColorId == color.ColorId);
            _colorDal.Delete(deletedColor);
            return new SuccessResult(ColorMessages.ColorDeleted);
        }

        public IResult Update(Color color)
        {
            var colors = new Color
            {
              ColorId = color.ColorId,
                ColorName = color.ColorName
            };
            _colorDal.Update(colors);
            return new SuccessResult(ColorMessages.ColorUpdated);
        }

        public IDataResult<Color> GetByColorId(int colorId)
        {
            var result = BusinessRules.Run(CheckIfColorIdExists(colorId));
            if (result!=null)
            {
                return new ErrorDataResult<Color>(result.Message);
            }
            return new SuccessDataResult<Color>(_colorDal.Get(p => p.ColorId == colorId), ColorMessages.ColorListed);
        }

        public IResult CheckIfColorNameExists(string colorName)
        {
            var result = _colorDal.Any(c => c.ColorName.ToLower() == colorName.ToLower());
            if (result)
            {
                return new ErrorResult("Eklemek Istediginiz Renk Sistemde Vardir");
            }

            return new SuccessResult();
        }

        public IResult CheckIfColorIdExists(int colorId)
        {
            var result = _colorDal.Any(c => c.ColorId == colorId);
            if (!result)
            {
                return new ErrorResult("Hata");
            }

            return new SuccessResult();
        }
    }
}
