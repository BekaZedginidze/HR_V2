using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HR.Application.Entity
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateOnDate { get; set; }

        public DateTime LastModityDate { get; set;}


    }
}
