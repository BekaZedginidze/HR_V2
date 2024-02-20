using HR.Application.Service.Abstraction;
using HR.Application.Service.Models;
using HR.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static HR.Application.Service.Implementation.CustomerService;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Cryptography;
using HR.Application.Infrastructure;
using HR.Application.Service.Helpers;

namespace HR.Application.Service.Implementation
{
    public class UserService : IUserService
    {

        private readonly HrDbContext hrDbContext;
        private readonly GenerateJwtToken _generateToken;
      //  private readonly PasswordHash _paasswordHash;

        public UserService(HrDbContext hrDbContext)
        {
            this.hrDbContext = hrDbContext;
            this._generateToken = new GenerateJwtToken();
        //    this._paasswordHash = new PasswordHash();
        }

       


        public ResponseDto<int?> UserRegister(UserRegistrationDto model)
        {
           
            string hashedPassword = PasswordHash.HashPassword(model.Password);

         //   string hashedPassword = HashPassword(model.Password);
            string hashedRePassword = PasswordHash.HashPassword(model.RePassword);


            if (model.Password != model.RePassword)
            {
                ResponseDto<int?> responseDto = new ResponseDto<int?>
                {
                    Status = Enums.ErrorCodeEnum.PasswordDoNotMatch,
                    Value = null
                };

                return responseDto;
              //  return BadRequest("Passwords do not match.");
            }


            var user = new Users()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = hashedPassword,
                RePassword = hashedRePassword,
                Email = model.Email,
                CreateOnDate = DateTime.Now,
                IsActive = true


                
            };

            var response = hrDbContext.Users.Add(user);

            hrDbContext.SaveChanges();

            ResponseDto<int?> responseDto1 = new ResponseDto<int?>
            {
                Status = Enums.ErrorCodeEnum.Success,
                Value = user.Id
            };
            return responseDto1;
         //   return Ok(response.Data);
        }



         

        public string UserLogin(UserLoginDto model) 
        {
            string hashedPassword = PasswordHash.HashPassword(model.Password);
         
           // string password = HashPassword(model.Password);




            var user = hrDbContext.Users.SingleOrDefault(u => u.Email == model.Email);

            
           
            if (user == null || user.Password != hashedPassword)
            {
               return "Invalid email or password.";
            }
            string token = _generateToken.GenerateToken(user);

            return token;
        }

    }
}
