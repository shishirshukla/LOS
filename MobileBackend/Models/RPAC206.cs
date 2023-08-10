using System.Collections.Generic;

namespace MobileBackend.Models
{
    public class Datum206
    {
        public string pan { get; set; }
        public string panName { get; set; }
        public long panAlotmentDate { get; set; }
        public string finYear { get; set; }
        public string panAadhaarLinkStatus { get; set; }
        public string panstatus { get; set; }
        public string aplicableFlag { get; set; }
        public string panAlotmentDateString { get; set; }
    }

    public class C206AB
    {
        public string status { get; set; }
        public string responseMessage { get; set; }
        public object errdata { get; set; }
        public List<Datum206> data { get; set; }
        public string requestid { get; set; }
        public string sequenceId { get; set; }
    }
}
