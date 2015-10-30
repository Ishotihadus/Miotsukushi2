using KanColleLib.TransmissionData.api_get_member.values;

namespace KanColleLib.TransmissionData.api_req_hensei
{
    public class PresetSelect
    {
        public DeckValue data;

        public static PresetSelect fromDynamic(dynamic json)
        {
            return new PresetSelect() {data = DeckValue.fromDynamic(json)};
        }
    }
}
