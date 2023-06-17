namespace MobileBackend.Models
{
    public class AdhaarReq
    {
        public string otp { get; set; }
        public string ref_id { get; set; }
    }
    public class AdhaarReqOTP
    {
        public string aadhaar_number { get; set; }
    }
    public class AdhaarData
    {
        public string ref_id { get; set; }
        public string message { get; set; }
    }
    public class AdhaarRespOTP
    {
        public string code { get; set; }
        public AdhaarData data { get; set; }
    }
    public class AdhaarSplit
    {
        public string pincode { get; set; }
        public string landmark { get; set; }
        public string street { get; set; }
        public string state { get; set; }
        public string dist { get; set; }
        public string house { get; set; }
    }

    public class AdhaarRespData
    {
        public string ref_id { get; set; }
        public string status { get; set; }
        public string care_of { get; set; }
        public string dob { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string year_of_birth { get; set; }
        public string photo_link { get; set; }
        public AdhaarSplit split_address { get; set; }
    }
    public class AdhaarResp
    {
        public string code { get; set; }
        public AdhaarRespData data { get; set; }
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
