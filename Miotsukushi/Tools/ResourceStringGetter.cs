using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
