using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle.BattleModels
{
    class MapModel : NotifyModelBase
    {
        public int maparea_id;
        public string maparea_name;

        public int map_no;
        public string map_name;

        public int level;

        public string ope_title;
        public string ope_info;

        public bool is_cleared;

        public int? required_defeat_count;
        public int? now_defeat_count;

        public int? max_hp;
        public int? now_hp;
        public int? selected_rank;
    }
}
