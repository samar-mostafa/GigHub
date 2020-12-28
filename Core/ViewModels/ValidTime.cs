using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GigHub.ViewModels
{
    public class ValidTime : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;

            bool valid = DateTime.TryParseExact(Convert.ToString(value),
                "HH:MM",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out dateTime);

            return (valid);
        }
    }
}
