using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVEmailSender.Models
{
    public class UserData
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ApartmentNum { get; set; }
    }
}
