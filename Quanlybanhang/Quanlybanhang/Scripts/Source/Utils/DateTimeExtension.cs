using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quanlybanhang.Scripts.Source.Utils
{
    public static class DateTimeExtension
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private const string InvalidUnixEpochErrorMessage = "Unix epoc starts January 1st, 1970";

        public static string ToMySQLDateTimeString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        ///   Convert a DateTime into a long
        /// </summary>
        public static long ToUnixTime(this DateTime self)
        {
            if (self < UnixEpoch)
            {
                if (self == DateTime.MinValue)
                {
                    return 0;
                }

                throw new ArgumentOutOfRangeException(InvalidUnixEpochErrorMessage);
            }

            TimeSpan delta = self.Subtract(UnixEpoch);
            var result = (long)delta.TotalSeconds;
            return result;
        }
    }
}