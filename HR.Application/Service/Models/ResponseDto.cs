using HR.Application.Service.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Service.Models
{
    public class ResponseDto<T>
    {

        public ErrorCodeEnum Status { get; set; }
        public T Value { get; set; }


    }
}
