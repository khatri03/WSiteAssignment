using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WSiteAssignment.Models.DbEntities
{
    public class Employee : BaseEntity<int>
    {
        public string FirstName
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
    }
}
