using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCTV_App.Models
{
    public class Audit
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string SigninTime { get; set; }
        public string SignoutTime { get; set; }

    }
}
