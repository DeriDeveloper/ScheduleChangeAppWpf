using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleChangeAppWpf
{
    internal class Config
    {
        static protected internal string UrlWebApi { get; set; }
        static protected internal string UrlWeb { get; set; }


        public static string TokenChangeSchedule { get; } = "dXNlci1jaGFuZ2Utc2NoZWR1bGU=";

        internal static void RestartUrlWeb(bool typeServer)
        {
			if (typeServer)
			{
				UrlWeb = "http://192.168.1.112:7040";
				//UrlWeb = "http://192.168.0.121:7040";
			}
			else
			{
				UrlWeb = "http://derideveloper.ru:7040";
			}

			UrlWebApi = $"{UrlWeb}/api/";
		}

        static Config()
        {
			RestartUrlWeb(false);
		}
    }
}
