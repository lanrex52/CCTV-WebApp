using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCTV_App.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public  string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
        public string Location { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }




    }
}
