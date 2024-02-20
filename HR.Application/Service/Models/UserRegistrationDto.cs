using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Service.Models
{
    public class UserRegistrationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }

        public string Email { get; set; }
        //public int Id { get; set; }
    }
}
