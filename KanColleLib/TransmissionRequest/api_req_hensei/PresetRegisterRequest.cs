namespace KanColleLib.TransmissionRequest.api_req_hensei
{
    public class PresetRegisterRequest : RequestBase
    {
        public string name_id;
        public string name;
        public int deck_id;
        public int preset_no;

        public PresetRegisterRequest(string request)
            : base(request)
        {
            name_id = _Get_Request("api_name_id");
            name = _Get_Request("api_name");
            deck_id = _Get_Request_int("api_deck_id").Value;
            preset_no = _Get_Request_int("api_preset_no").Value;
        }
    }
}
