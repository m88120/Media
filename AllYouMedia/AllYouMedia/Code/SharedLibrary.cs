using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AllYouMedia.Code
{
    public static class SharedLibrary
    {
        public static string ToDateTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("dd-MM-yyyy HH:mm");
        }
        public enum MemberTypeEnum
        {
            Customer = 6,
            AllTalent = 2,
            Production = 3,
            MediaPromoter = 4
        }
        public enum enum_AspNetRoles
        {
            Admin = 1,
            Production = 3,
            AllTalent = 2,
            MediaPromoter = 4,
            Customer = 6
        }
        public enum MessageStatus
        {
            New = 1,
            Read = 2
        }
    }
}