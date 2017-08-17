using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel
{
   public class BCSV2Roles
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class BCSV2UserData
    {
        public BCSV2User UserData { get; set; }
        public List<BCSV2Roles> RolesData { get; set; }
    }
}
