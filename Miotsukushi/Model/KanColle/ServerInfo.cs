using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;

namespace Miotsukushi.Model.KanColle
{
    class ServerInfo
    {
        static readonly Dictionary<IPAddress, string> address_dic = new Dictionary<IPAddress, string>()
        {
            { IPAddress.Parse("127.0.0.1"), "localhost" },
            { IPAddress.Parse("203.104.209.71"), "横須賀鎮守府" },
            { IPAddress.Parse("203.104.105.167"), "横須賀鎮守府" },
            { IPAddress.Parse("125.6.184.15"), "呉鎮守府" },
            { IPAddress.Parse("125.6.184.16"), "佐世保鎮守府" },
            { IPAddress.Parse("125.6.187.205"), "舞鶴鎮守府" },
            { IPAddress.Parse("125.6.187.229"), "大湊警備府" },
            { IPAddress.Parse("125.6.187.253"), "トラック泊地" },
            { IPAddress.Parse("125.6.188.25"), "リンガ泊地" },
            { IPAddress.Parse("203.104.248.135"), "ラバウル基地" },
            { IPAddress.Parse("125.6.189.7"), "ショートランド泊地" },
            { IPAddress.Parse("125.6.189.39"), "ブイン基地" },
            { IPAddress.Parse("125.6.189.71"), "タウイタウイ泊地" },
            { IPAddress.Parse("125.6.189.103"), "パラオ泊地" },
            { IPAddress.Parse("125.6.189.135"), "ブルネイ泊地" },
            { IPAddress.Parse("125.6.189.167"), "単冠湾泊地" },
            { IPAddress.Parse("125.6.189.215"), "幌筵泊地" },
            { IPAddress.Parse("125.6.189.247"), "宿毛湾泊地" },
            { IPAddress.Parse("203.104.209.23"), "鹿屋基地" },
            { IPAddress.Parse("203.104.209.39"), "岩川基地" },
            { IPAddress.Parse("203.104.209.55"), "佐伯湾泊地" },
            { IPAddress.Parse("203.104.209.102"), "柱島泊地" }
        };

        public IPAddress address;

        public string GetServerNameFromString(string ipString)
        {
            IPAddress address;
            if (!IPAddress.TryParse(ipString, out address))
                return null;
            return GetServerNameFromAddress(address);
        }

        public string GetServerNameFromAddress(IPAddress address)
        {
            if (address != null && address_dic.ContainsKey(address))
                return address_dic[address];
            else
                return null;
        }

        public string GetServerName()
        {
            return GetServerNameFromAddress(address);
        }

        public bool SetServerAddress(string url)
        {
            if (Regex.IsMatch(url, @"^(https?|ftp)://\d+\.\d+\.\d+\.\d+.*$"))
            {
                string address = Regex.Match(url, @"\d+\.\d+\.\d+\.\d+").Value;
                return IPAddress.TryParse(address, out this.address);
            }
            else
            {
                return false;
            }
        }
    }
}
