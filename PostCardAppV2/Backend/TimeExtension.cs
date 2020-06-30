using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostCardAppV2.Backend
{
    public static class TimeExtension
    {
        public static string ToReadableString(this TimeSpan span)
        {
            if (span.Duration().Days > 30) { return string.Format("{0:0} month{1} ", span.Days/30, span.Days == 1 ? string.Empty : "s") + "ago..."; }
            if (span.Duration().Days > 0) { return string.Format("{0:0} day{1} ", span.Days, span.Days == 1 ? string.Empty : "s") + "ago..."; }
            if (span.Duration().Hours > 0) { return string.Format("{0:0} hour{1} ", span.Hours, span.Hours == 1 ? string.Empty : "s") + "ago..."; }
            if (span.Duration().Minutes > 0) { return string.Format("{0:0} minute{1} ", span.Minutes, span.Minutes == 1 ? string.Empty : "s") + "ago..."; }
            return "Just Now";
        }
    }
}
