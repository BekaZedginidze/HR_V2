using HR.Application.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Service.Abstraction
{
    
    public interface ICustomerService
    {
        Task<ResponseDto<List<CustomerGetDto>>> GetCustomers();

      //  Task<ResponseDto<int?>> InsertCustomer(CreateCustomerRequestDto data);
        Task<ResponseDto<int?>> InsertCustomer(CreateCustomerRequestDto data);

        Task<ResponseDto<int?>> CustomerDelete(int id);

    }

}
