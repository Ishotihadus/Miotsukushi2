using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.EventArgs
{
    public class GetKcsAPIDataEventArgs : System.EventArgs
    {
        public TransmissionRequest.RequestBase request;
        public object response;
        public TransmissionData.SvdataHeader response_header;
        public string kcsapiurl;
        public bool IsSuccess { get { return (response_header != null && response_header.result == 1); } }
        public Type responseType { get { return response.GetType(); } }

        public GetKcsAPIDataEventArgs(string kcsapiurl, TransmissionRequest.RequestBase request, TransmissionData.SvdataHeader response_header, object response)
        {
            this.kcsapiurl = kcsapiurl;
            this.request = request;
            this.response_header = response_header;
            this.response = response;
        }
    }
}
