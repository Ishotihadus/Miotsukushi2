using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle
{
    class MapInfoData
    {

        public string name;
        public int area_id;
        public int map_id;

        public enum MapDefeatType
        {
            normal, count_of_defeat, max_hp
        }
        
        public int map_level;
        public string opename;
        public string ope_info;
        public MapDefeatType defeat_type;
        public int defeat_count;
    }
}
