namespace MobileBackend.Models
{
    public class eMandate
    {
        public eMandate()
        {
            occurance_sequence_type = "RCUR";
            occurance_frequency_type = "MNTH";
            scheme_reference_number = "LoanAC";
            collection_amount_type = "FIXED";
           
            mandate_type_category_code = "L001";
            is_until_cancel= true;
            quick_invite=true;
            authentication_mode = "netBanking";
        }
        public string reference_id { get; set; }
        public string debtor_account_type { get; set; }
        public string debtor_account_id { get; set; }
        public string occurance_sequence_type { get; set; }
        public string occurance_frequency_type { get; set; }
        public string scheme_reference_number { get; set; }
        public string consumer_reference_number { get; set; }
        public string debtor_name { get; set; }
        public string email_address { get; set; }
        public string first_collection_date { get; set; }
        public string mobile_number { get; set; }
        public string phone_number { get; set; }
        public string collection_amount_type { get; set; }
        public int amount { get; set; }
        public string mandate_type_category_code { get; set; }
        public bool is_until_cancel { get; set; }
        public bool quick_invite { get; set; }
        public string authentication_mode { get; set; }
        public string instructed_agent_code { get; set; }
    }
    public class Notification
    {
        public string title { get; set; }
        public string body { get; set; }
        public string click_action { get; set; }
        public string priority { get; set; }
        public string image { get; set; }
        public string applicant_id { get; set; }
        public string response { get; set; }
        public string topic { get; set; }
    }
    public class DataG
    {
        public string title { get; set; }
        public string body { get; set; }
        public string applicant_id { get; set; }
        public string response { get; set; }
        public string topic { get; set; }
    }

    public class GCM
    {
        public string to { get; set; }
        public string channel_id { get; set; }
        public Notification notification { get; set; }
        public DataG data { get; set; }
    }

}
