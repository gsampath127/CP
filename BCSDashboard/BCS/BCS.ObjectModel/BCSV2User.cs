using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel
{
   public class BCSV2User
    {
       public string Email { get; set; }
       public string PasswordHash { get; set; }
       public string PhoneNumber { get; set; }
       public string UserName { get; set; }
       public string FirstName { get; set; }
       public string LastName { get; set; }
       public string SecurityQuestion { get; set; }
       public string SecurityAnswer { get; set; }
       public string UserSecurityAnswer { get; set; }
       public string Password { get; set;}
       public string NewPassword { get; set;}
       public string SecurityStamp { get; set; }
       public int UserId { get; set; }
       public int SecurityQuestionId { get; set; }
       public int ModifiedBy { get; set; }
       public bool? LockoutEnabled{get;set;}
       public bool? EmailConfirmed { get; set; }
    }
}