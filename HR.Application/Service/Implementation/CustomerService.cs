using HR.Application.Infrastructure;
using HR.Application.Service.Abstraction;
using HR.Application.Service.Models;
using HR.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Service.Implementation
{
    public class CustomerService : ICustomerService
    {
        public readonly HrDbContext hrDbContext;

        public CustomerService(HrDbContext hrDbContext)
        {
            this.hrDbContext = hrDbContext;
        }
        public class ApiResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
        }

        public async Task<ResponseDto<List<CustomerGetDto>>> GetCustomers()
        {


            List<CustomerGetDto> getCustomer = new List<CustomerGetDto>();
            var response = await hrDbContext.Customers
                .Include(x => x.Gender)
                .Include(x => x.PhoneNumbers)
                .ToListAsync();

         
            foreach (var item in response)
            {

                var phoneNumbers = item.PhoneNumbers.Select(p => new PhoneNumberModel
                {
                    PhoneNumber = p.PhoneNumber,
                    IsDefault = p.IsDefault

                }).ToList();
                getCustomer.Add(new CustomerGetDto()
                {
                    Firstname = item.Firstname,
                    Lastname = item.Lastname,
                    GenderId = (int)item.GenderId,
                    GenderName = item.Gender.Name,
                    DateOfBirth = item.DateOfBirth,
                    PhoneNumbers = phoneNumbers

                });
            }

            ResponseDto<List<CustomerGetDto>> CustomerResponseDto = new ResponseDto<List<CustomerGetDto>>
            {
                Status = Enums.ErrorCodeEnum.Success,
                Value = getCustomer
            };
            return CustomerResponseDto;
        }

        public async Task<ResponseDto<int?>> InsertCustomer(CreateCustomerRequestDto data)
        {


            var phoneNumber = data.PhoneNumbers.Select(p => new PhoneNumbers
            {
                PhoneNumber = p.PhoneNumber,
                IsDefault = p.IsDefault
            }).ToList();





            var customer = new Customer()
            {
                Lastname = data.Lastname,
                GenderId = data.GenderId,
                DateOfBirth = data.DateOfBirth,
                Firstname = data.Firstname,
                PhoneNumbers = phoneNumber,

            };

            var response = hrDbContext.Customers.Add(customer);

            hrDbContext.SaveChanges();

            ResponseDto<int?> CustomerRequestDto = new ResponseDto<int?>
            {
                Status = Enums.ErrorCodeEnum.Success,
                Value = customer.Id
            };

            return CustomerRequestDto;


        }

        public async Task<ResponseDto<int?>> CustomerDelete(int id)
        {

            try
            {
                var resourceToDelete = await hrDbContext.Customers.FindAsync(id);
                resourceToDelete.IsDeleted = true;
                resourceToDelete.LastModityDate = DateTime.UtcNow;
                await hrDbContext.SaveChangesAsync();

                ResponseDto<int?> CustomerDeleteDto = new ResponseDto<int?>
                {
                    Status = Enums.ErrorCodeEnum.Success,
                    Value = null
                };

                return CustomerDeleteDto;
            }
            catch (Exception ex)
            {
                ResponseDto<int?> CustomerDeleteDto = new ResponseDto<int?>
                {
                    Status = Enums.ErrorCodeEnum.CustomerIdDoNotFount,
                    Value = null
                };

                return CustomerDeleteDto;
            }

        }

    }
}
