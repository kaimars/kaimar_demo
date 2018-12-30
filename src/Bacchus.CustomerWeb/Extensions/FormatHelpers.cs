using System;

namespace Bacchus.CustomerWeb.Extensions
{
    public static class FormatHelpers
    {
        public static string ToUiString(this TimeSpan value)
        {
            return value.ToString(value.Days == 0 ? @"hh\:mm" : 
                (value.Days == 1 ? @"d' day, 'hh\:mm" : @"d' days, 'hh\:mm"));
        }
    }

}