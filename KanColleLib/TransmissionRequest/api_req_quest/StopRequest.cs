﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_req_quest
{
    public class StopRequest : RequestBase
    {
        /// <summary>
        /// 任務のID
        /// </summary>
        public int quest_id;

        public StopRequest(string request)
            : base(request)
        {
            quest_id = int.Parse(_Get_Request("api_quest_id"));
        }
    }
}
