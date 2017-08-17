using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Utilities
{
    public class EnumHelper
    {
        /// <summary>
        /// GetEnumDescription
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum value)
        {

            string description = value.ToString();
            FieldInfo fieldInfo = value.GetType().GetField(description);
            DescriptionAttribute[] attributes =
               (DescriptionAttribute[])
             fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }
            return description;
        }

        /// <summary>
        /// GetEnumDescriptionList
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<string> GetEnumDescriptionList(Type type)
        {
            List<string> list = new List<string>();
            Array enumValues = Enum.GetValues(type);

            foreach (Enum value in enumValues)
            {
                list.Add(GetEnumDescription(value));
            }
            return list;
        }
    }
}
