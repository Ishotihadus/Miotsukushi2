namespace KanColleLib.TransmissionData.api_get_member.values
{
    public class PresetDeckValue
    {
        /// <summary>
        /// プリセットの番号（1から）
        /// </summary>
        public int preset_no;
        public string name;
        public string name_id;

        /// <summary>
        /// shipidの配列（インデックスは0から、入っていない部分は-1）
        /// </summary>
        public int[] ship;
        
        public static PresetDeckValue fromDynamic(dynamic json)
        {
            var deck = new PresetDeckValue
            {
                preset_no = (int) json.api_preset_no,
                name = json.api_name as string,
                name_id = json.api_name_id as string,
                ship = json.api_ship.Deserialize<int[]>()
            };
            
            return deck;
        }

        // "api_preset_no":1,"api_name":"\u96fb\u6c17\u96fb\u5b50\u60c5\u5831\u5b9f\u9a13\u6f14\u7fd2\u7b2c\u4e8c","api_name_id":"140749500",
        // "api_ship":[1490,-1,-1,-1,-1,-1]}
    }
}
