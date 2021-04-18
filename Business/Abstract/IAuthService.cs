using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Concrete.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {

        IDataResult<User> Register(UseForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UseForLoginDto useForLoginDto);
        IResult UserExits(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
