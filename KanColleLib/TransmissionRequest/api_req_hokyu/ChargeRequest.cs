using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_req_hokyu
{
    public class ChargeRequest : RequestBase
    {
        /// <summary>
        /// 何の値かわからん　常に1?
        /// </summary>
        public int onslot;

        /// <summary>
        /// 補給対象艦
        /// </summary>
        public int?[] id_items;

        /// <summary>
        /// 補給の種類（たぶん）
        /// 1:燃料のみ 2:弾薬のみ 3:両方
        /// </summary>
        public int kind;

        public ChargeRequest(string request)
            : base(request)
        {
            onslot = (int)_Get_Request_int("api_onslot");
            id_items = _Get_Request_int_array("api_id_items");
            kind = (int)_Get_Request_int("api_kind");
        }
    }
}
