using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Miotsukushi.Tools
{
    class ResourceStringGetter
    {
        const string _not_found_message = "### String Not Found ###";

        public static string GetResourceString(string key)
        {
            if (string.IsNullOrEmpty(key))
                return _not_found_message;

            return Properties.Resources.ResourceManager.GetString(key) ?? _not_found_message;
        }

        public static string GetNameResourceString(string key)
        {
            if (string.IsNullOrEmpty(key) || IsCultureJP())
                return key;

            return Properties.Resources.ResourceManager.GetString(key) ?? key;
        }

        public static string GetShipNameResourceString(string key)
        {
            if (string.IsNullOrEmpty(key) || IsCultureJP())
                return key;

            if (key.Length >= 2 && key.Substring(key.Length - 2) == "改二")
            {
                return GetNameResourceString(key.Substring(0, key.Length - 2)) + GetNameResourceString("改二");
            }
            else if (key.Length >= 1 && key.Substring(key.Length - 1) == "改")
            {
                return GetNameResourceString(key.Substring(0, key.Length - 1)) + GetNameResourceString("改");
            }
            else
            {
                return GetNameResourceString(key);
            }
        }

        private static bool IsCultureJP()
        {
            return Thread.CurrentThread.CurrentCulture.Name == "ja-JP" && Thread.CurrentThread.CurrentUICulture.Name == "ja-JP";
        }
    }
}
