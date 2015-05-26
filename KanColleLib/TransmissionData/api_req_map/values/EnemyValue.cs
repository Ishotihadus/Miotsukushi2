using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_map.values
{
    public class EnemyValue
    {
        public int enemy_id;
        public int result;
        public string result_str;

        public static EnemyValue fromDynamic(dynamic json)
        {
            return new EnemyValue()
            {
                enemy_id = (int)json.api_enemy_id,
                result = (int)json.api_result,
                result_str = json.api_result_str as string
            };
        }
    }
}
