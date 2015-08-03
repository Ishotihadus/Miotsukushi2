using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_sortie.values
{
    public class KoukuStage2AirFire
    {
        // http://ai.2ch.sc/test/read.cgi/gameswf/1418029405/674

        /// <summary>
        /// 対空砲火をした艦
        /// </summary>
        public int idx;

        /// <summary>
        /// 対空砲火の種類
        /// 1　高角砲/高角砲/電探
        /// 2　高角砲/電探
        /// 3　高角砲/高角砲
        /// 4　大口径主砲/三式弾/高射装置/（電探　※表示されない）
        /// 5　高角砲+高射装置/高角砲+高射装置/電探
        /// 6　大口径主砲/三式弾/高射装置
        /// 7　高角砲/高射装置/電探
        /// 8　高角砲+高射装置/電探
        /// 9　高角砲/高射装置
        /// 11 高角砲/摩耶機銃?
        /// </summary>
        public int kind;

        /// <summary>
        /// 使用した装備
        /// </summary>
        public int[] use_items;
    }
}
