using KanColleLib.TransmissionData.api_get_member.values;

namespace KanColleLib.TransmissionData.api_req_hensei
{
    public class PresetRegister
    {
        public PresetDeckValue data;

        public static PresetRegister fromDynamic(dynamic json)
        {
            return new PresetRegister() {data = PresetDeckValue.fromDynamic(json)};
        }
    }
}
