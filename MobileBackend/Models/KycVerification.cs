namespace MobileBackend.Models
{
    public class AdhaarReqOTP
    {
        public string aadhaar_number { get; set; }
    }
    public class AdhaarRespOTP
    {
        public string ref_id { get; set; }
        public string message { get; set; }
    }
    public class Token
    {
        public string access_token { get; set; }
    }
    public class PanrespData
    {
        public string pan { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string status { get; set; }
        public string aadhaar_seeding_status { get; set; }
        public string category { get; set; }
        public string last_updated { get; set; }
    }

    public class PANResponse
    {
        public int code { get; set; }
        public long timestamp { get; set; }
        public string transaction_id { get; set; }
        public PanrespData data { get; set; }
    }

    public class PANReq
    {
        public PANReq()
        {
            consent = "Y";
            reason = "For KYC of Customer";
        }
        public string pan { get; set; }
        public string consent { get; set; }
        public string reason { get; set; }
    }
}
