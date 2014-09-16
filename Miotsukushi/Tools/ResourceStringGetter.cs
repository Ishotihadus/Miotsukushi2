using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;

namespace Miotsukushi.Tools
{
    class ResourceStringGetter
    {
        const string _not_found_message = "### String Not Found ###";
        public static CultureInfo culture = Thread.CurrentThread.CurrentCulture;

        public static string GetResourceString(string key)
        {
            if (string.IsNullOrEmpty(key))
                return _not_found_message;

            return GetString(key) ?? _not_found_message;
        }

        public static string GetShipTypeNameResourceString(string key)
        {
            return GetNameResourceString("ShipType_" + key) ?? key;
        }

        public static string GetShipNameResourceString(string key)
        {
            if (string.IsNullOrEmpty(key) || IsCultureJP())
                return key;

            if (key.Length >= 2 && key.Substring(key.Length - 2) == "改二")
            {
                return (GetNameResourceString("ShipName_" + key.Substring(0, key.Length - 2)) ?? key.Substring(0, key.Length - 2)) + (GetNameResourceString("改二") ?? "改二");
            }
            else if (key.Length >= 1 && key[key.Length - 1] == '改')
            {
                return (GetNameResourceString("ShipName_" + key.Substring(0, key.Length - 1)) ?? key.Substring(0, key.Length - 1)) + (GetNameResourceString("改") ?? "改");
            }
            else
            {
                return GetNameResourceString("ShipName_" + key) ?? key;
            }
        }

        private static bool IsCultureJP()
        {
            return culture.Name == "ja-JP";
        }

        private static string GetNameResourceString(string key)
        {
            if (string.IsNullOrEmpty(key) || IsCultureJP())
                return null;

            return GetString(key);
        }

        /// <summary>
        /// Threadがごちゃごちゃになるとアレなので、リソースからの文字列の取得は必ずこれで行う
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetString(string key)
        {
            if (Thread.CurrentThread.CurrentCulture != culture)
                Thread.CurrentThread.CurrentCulture = culture;
            if (Thread.CurrentThread.CurrentUICulture != culture)
                Thread.CurrentThread.CurrentUICulture = culture;
            return Properties.Resources.ResourceManager.GetString(key);
        }
    }
}
