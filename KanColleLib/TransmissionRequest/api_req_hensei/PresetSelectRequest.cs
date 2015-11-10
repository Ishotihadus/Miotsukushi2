namespace KanColleLib.TransmissionRequest.api_req_hensei
{
    public class PresetSelectRequest : RequestBase
    {
        public int deck_id;
        public int preset_no;

        public PresetSelectRequest(string request)
            : base(request)
        {
            deck_id = _Get_Request_int("api_deck_id").Value;
            preset_no = _Get_Request_int("api_preset_no").Value;
        }
    }
}
