using HR.Application.Service.Enums;
using HR.Application.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Service.Abstraction
{
     public interface IUserService
    {
        ResponseDto<int?> UserRegister(UserRegistrationDto model);
        string UserLogin(UserLoginDto model);
        
    }
}
