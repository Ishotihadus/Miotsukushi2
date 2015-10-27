﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_get_member.values
{
    public class WarValue
    {
        /// <summary>
        /// 勝数
        /// </summary>
        public int win;

        /// <summary>
        /// 負数
        /// </summary>
        public int lose;

        /// <summary>
        /// 勝率（1がMax）
        /// </summary>
        public double rate;

        public static WarValue fromDynamic(dynamic json)
        {
            return new WarValue()
            {
                win = int.Parse(json.api_win),
                lose = int.Parse(json.api_lose),
                rate = double.Parse(json.api_rate)
            };
        }
    }
}