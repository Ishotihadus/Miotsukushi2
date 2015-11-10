namespace KanColleLib.TransmissionRequest.api_req_hensei
{
    public class PresetDeleteRequest : RequestBase
    {
        public int preset_no;

        public PresetDeleteRequest(string request) : base(request)
        {
            preset_no = _Get_Request_int("api_preset_no").Value;
        }
    }
}
