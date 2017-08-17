using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel
{
    public class UserDetails
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public List<Role> Roles { get; set; }
        public int ApplicationId { get; set; }
        public string Application { get; set; }
    }

    public class TokenDetails
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime UtcIssuedOn { get; set; }
        public DateTime UtcExpiresOn { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
