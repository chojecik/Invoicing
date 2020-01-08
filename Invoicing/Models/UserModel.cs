using System;
using System.Collections.Generic;
using System.Text;

namespace Invoicing.Web.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
