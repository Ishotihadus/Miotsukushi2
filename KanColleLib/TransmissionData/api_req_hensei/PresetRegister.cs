namespace KanColleLib.TransmissionData.api_req_hensei
{
    public class PresetRegister
    {
        public values.PresetDeckValue data;

        public static PresetRegister fromDynamic(dynamic json)
        {
            return new PresetRegister() {data = values.PresetDeckValue.fromDynamic(json)};
        }
    }
}
