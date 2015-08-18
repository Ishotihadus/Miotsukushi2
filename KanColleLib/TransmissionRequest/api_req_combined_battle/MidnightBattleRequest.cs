﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_req_combined_battle
{
    public class MidnightBattleRequest : RequestBase
    {
        public int recovery_type;

        public MidnightBattleRequest(string request) : base(request)
        {
            recovery_type = _Get_Request_int("api_recovery_type").Value;
        }
    }
}
