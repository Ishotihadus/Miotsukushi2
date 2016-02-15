namespace KanColleLib.TransmissionRequest.api_req_sortie
{
    public class LdAirbattleRequest : RequestBase
    {
        public int recovery_type;
        public int formation;

        public LdAirbattleRequest(string request) : base(request)
        {
            recovery_type = _Get_Request_int("api_recovery_type").Value;
            formation = _Get_Request_int("api_formation").Value;
        }
    }
}
