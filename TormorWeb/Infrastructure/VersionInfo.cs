using System;
using System.IO;
using System.Globalization;

namespace NNS.Config
{
    public static class VersionInfo
    {
        // from http://www.codinghorror.com/blog/archives/000264.html
        private static DateTime getLinkerTimeStamp(string filepath)
        {
            const int peHeaderOffset = 60;
            const int linkerTimestampOffset = 8;

            byte[] b = new byte[2048];
            var s = (Stream)null;

            try
            {
                s = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally
            {
                if (s != null)
                {
                    s.Close();
                }
            }

            int i = BitConverter.ToInt32(b, peHeaderOffset);
            int secondsSince1970 = BitConverter.ToInt32(b, i + linkerTimestampOffset);
            var dt = new DateTime(1970, 1, 1, 0, 0, 0);
            dt = dt.AddSeconds(secondsSince1970);
            dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
            return dt;
        }

        public static string GetBuildString()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var majorVersion = String.Format("{0}.{1}", assembly.GetName().Version.Major,assembly.GetName().Version.Minor);
            var subRevision = getLinkerTimeStamp(assembly.Location).ToString("yyMMdd.HHmm", new CultureInfo("en-US"));
            return String.Format("{0}.{1}", majorVersion, subRevision); 
        }
    }
}