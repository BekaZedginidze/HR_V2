using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Service.Models
{
    public class CustomerGetDto
    {


        public string Firstname { get; set; }


        public string Lastname { get; set; }


        public int GenderId { get; set; }


        public string GenderName { get; set; }

        // public string Phone { get; set; }


        public DateTime DateOfBirth { get; set; }

        public List<PhoneNumberModel> PhoneNumbers { get; set; }



    }

    public class PhoneNumberModel
    {
        public string PhoneNumber { get; set; }
        public bool IsDefault { get; set; }
    }

}
