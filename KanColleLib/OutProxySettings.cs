using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib
{
    class OutProxySettings
    {

        public ProxyType type;
        public string servername;
        public int port;
        public bool enabled = false;

        internal string GetGatewayString()
        {
            if (type == ProxyType.socks)
                return "socks=" + servername + ":" + port;
            else
                return servername + ":" + port;
        }
    }

    public enum ProxyType
    {
        http, socks
    }
}
