using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;

namespace Toolkit
{
    public static class DateUtility
    {
        public static long CurrentTimestamp(bool millisecond = false)
        {
            return DateTimeToTimestamp(DateTime.Now, millisecond);
        }

        public static long DateTimeToTimestamp(DateTime dateTime, bool millisecond)
        {
            return (dateTime.ToUniversalTime().Ticks - 621355968000000000) / (millisecond ? 10000 : 10000000);
        }

        public static DateTime TimestampToDateTime(long timestamp, bool millisecond = false)
        {
            DateTime dateTimeStart = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            TimeSpan toNow = new TimeSpan(timestamp * (millisecond ? 10000 : 10000000));
            return dateTimeStart.Add(toNow);
        }
    }
}