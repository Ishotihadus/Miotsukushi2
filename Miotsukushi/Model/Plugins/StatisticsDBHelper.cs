using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace Miotsukushi.Model.Plugins
{
    class StatisticsDBHelper
    {
        KanColleNotifier kclib;

        readonly string agent = "zF5wd8DwGUTpXbPprprG";

        readonly string[] url =
        {
            "api_port/port",
            "api_get_member/ship2",
            "api_get_member/ship3",
            "api_get_member/slot_item",
            "api_get_member/kdock",
            "api_get_member/mapinfo",
            "api_req_hensei/change",
            "api_req_kousyou/createship",
            "api_req_kousyou/getship",
            "api_req_kousyou/createitem",
            "api_req_map/start",
            "api_req_map/next",
			"api_req_map/select_eventmap_rank",
            "api_req_sortie/battle",
            "api_req_battle_midnight/battle",
            "api_req_battle_midnight/sp_midnight",
            "api_req_sortie/night_to_day",
            "api_req_sortie/battleresult",
            "api_req_combined_battle/battle",
            "api_req_combined_battle/airbattle",
            "api_req_combined_battle/midnight_battle",
            "api_req_combined_battle/battleresult",
            "api_req_sortie/airbattle",
            "api_req_combined_battle/battle_water",
            "api_req_combined_battle/sp_midnight"
        };

        public StatisticsDBHelper(KanColleNotifier kclib)
        {
            this.kclib = kclib;
            kclib.GetSessionData += Kclib_GetSessionData;
        }

        private void Kclib_GetSessionData(object sender, KanColleLib.EventArgs.GetSessionDataEventArgs e)
        {
            var token = Properties.Settings.Default.StatisticsDBToken;
            if (Properties.Settings.Default.StatisticsSendingOn && token != "" && e.MIMEType == "text/plain" && url.Any(_ => e.fullUrl.IndexOf(_) != -1))
            {
                try
                {
                    // api_tokenを送信しないように削除
                    var requestBody = Regex.Replace(HttpUtility.HtmlDecode(e.requestString), @"&api(_|%5F)token=[0-9a-f]+|api(_|%5F)token=[0-9a-f]+&?", "");

                    // svdata= は削除するとのこと（なんでや!）
                    var responseBody = e.responseString.Replace("svdata=", "");

                    var req = WebRequest.Create("http://api.kancolle-db.net/2/");
                    req.Method = "POST";
                    req.ContentType = "application/x-www-form-urlencoded";

                    var enc = Encoding.GetEncoding("utf-8");

                    // ポストデータは以下の通り
                    var postdata =
                          "token=" + HttpUtility.UrlEncode(Properties.Settings.Default.StatisticsDBToken) + "&"
                        + "agent=" + HttpUtility.UrlEncode(agent) + "&"
                        + "url=" + HttpUtility.UrlEncode(e.fullUrl) + "&"
                        + "requestbody=" + HttpUtility.UrlEncode(requestBody) + "&"
                        + "responsebody=" + HttpUtility.UrlEncode(responseBody);
                    var postDataBytes = Encoding.ASCII.GetBytes(postdata);
                    req.ContentLength = postDataBytes.Length;

                    using (var reqStream = req.GetRequestStream())
                        reqStream.Write(postDataBytes, 0, postDataBytes.Length);

                    string response = null;

                    var res = req.GetResponse();
                    var httpRes = (HttpWebResponse)res;

                    var resStream = res.GetResponseStream();
                    using (var sr = new StreamReader(resStream, enc))
                        response = sr.ReadToEnd();
                }
                catch
                {
                }
            }
        }
    }
}
