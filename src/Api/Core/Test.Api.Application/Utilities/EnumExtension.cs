using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Test.Api.Application.Utilities
{
    public static class EnumExtension
    {
        

        public static string GetEnumDescription(this System.Enum value)
        {
            var type = value.GetType();
            if (!type.IsEnum) throw new ArgumentException(string.Format("Type {0} is not an enum", type));


            // get attributes  
            var field = value.GetType().GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            // return description
            return attributes.Any()
                ? ((DescriptionAttribute)attributes.ElementAt(0)).Description
                : "Description Not Found";
        }

      
    }
}
