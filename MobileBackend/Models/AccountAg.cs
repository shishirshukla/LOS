using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileBackend.Models
{
    public class AccountView
    {
        public string AccountNumber { get; set; }
        public string BranchId { get; set; }
        public string AccountName { get; set; }
        public Single OS { get; set; }
        public Single Limit { get; set; }
        public string AccountType { get; set; }
        public string Cibil { get; set; }
        public string Bhuiyan { get; set; }
        public string KYCVerified { get; set; }
        public string ISS { get; set; }

    }

    public class CibilAccounts
    {
        public string member_ref { get; set; }
        public string member_name { get; set; }
        public string acc_number { get; set; }
        public string acc_type { get; set; }
        public string date_open { get; set; }
        public string date_close { get; set; }
        public Single high_credit { get; set; }
        public Single current_bal { get; set; }
        public Single amt_overdue { get; set; }
        public Single tenure { get; set; }
        public Single emi { get; set; }
        public string hist1 { get; set; }
        public string hist1_date { get; set; }
        //public Single writeoff { get; set; }

    }
    public class MandateCheckResponse
    {
        public string status { get; set; }
        public string mandate_status { get; set; }
        public string reference_id { get; set; }
        public string error { get; set; }
        public string error_code { get; set; }
        public string umrn { get; set; }

        public DateTime response_time_stamp { get; set; }

    }
    public class CheckMandate
    {
        public CheckMandate()
        {
            type = "Create";
        }
        public string emandate_id { get; set; }
        public string type { get; set; }
    }

    public class ExistingAcMandate
    {

        
        public ExistingAcMandate()
        {
            reference_id = Guid.NewGuid().ToString().Replace("-", "");
            occurance_sequence_type = "RCUR";
            occurance_frequency_type = "MNTH";

            final_collection_date = DateTime.Now.AddYears(30).ToString("yyyy-MM-dd");
            collection_amount_type = "FIXED";

            mandate_type_category_code = "L001";
            untill_30_years = true;
            quick_invite = true;
            authentication_mode = "netBanking";

        }
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public string LoanAc { get; set; }
        [NotMapped]
        public string final_collection_date { get; set; }
        [NotMapped]
        public string instructed_agent_id_type { get; set; }
        [NotMapped]
        public string instructed_agent_id { get; set; }
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
        [JsonIgnore]
        public bool untill_30_years { get; set; }
       // public bool is_until_cancel { get; set; }
        public bool quick_invite { get; set; }
        public string authentication_mode { get; set; }
        public string instructed_agent_code { get; set; }
        [JsonIgnore]
        public DateTime createDate { get; set; }
        [JsonIgnore]
        public string api_response_id { get; set; }
        [JsonIgnore]
        public string emandate_id { get; set; }
        [JsonIgnore]
        public DateTime? last_run_date { get; set; }
        [JsonIgnore]
        public string last_run_status { get; set; }
        [JsonIgnore]
        public string is_cancelled { get; set; }
        [JsonIgnore]
        public string mandate_status { get; set; }
        [JsonIgnore]
        public string created_by { get; set; }
        [JsonIgnore]
        public string branch { get; set; }
        [JsonIgnore]
        public string umrn_mandate { get; set; }
    }
    public class DataReq
    {
        public string consentID { get; set; }
       
    }
    public class DatumR
    {
        public string consentID { get; set; }
        public string consentHandle { get; set; }
        public string status { get; set; }
        public string productID { get; set; }
        public string accountID { get; set; }
        public string aaId { get; set; }
        public string vua { get; set; }
        public DateTime consentCreationData { get; set; }
        public List<object> accounts { get; set; }
    }

    public class AACResponse
    {
        public string ver { get; set; }
        public string status { get; set; }
        public List<DatumR> data { get; set; }
    }

    public class AARequest
    {
        public string partyIdentifierType { get; set; }
        public string partyIdentifierValue { get; set; }
        public string productID { get; set; }
        public string accountID { get; set; }
        public string vua { get; set; }
    }
    public class AACRequest
    {
        public string partyIdentifierType { get; set; }
        public string partyIdentifierValue { get; set; }
        public string productID { get; set; }
        public string accountID { get; set; }
       
    }
    public class AccountStatments
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int ApplicantId { get; set; }
        public string MobileNumber { get; set; }
        public string status_request { get; set; }
        public string Consent_Handle { get; set; }
        public string request_status { get; set; }
        public string statement { get; set; }
        public DateTime requestDate { get; set; }
        public string UserId { get; set; }
        public Application Application { get; set; }
    }
    public class ConsentResquestData
    {
        public string status { get; set; }
        public string consent_handle { get; set; }
    }
    public class AAResponse
    {
        public string status { get; set; }
        public string ver { get; set; }
        public ConsentResquestData data { get; set; }

    }
    public class Datum
    {
        public string linkReferenceNumber { get; set; }
        public string maskedAccountNumber { get; set; }
        public string fiType { get; set; }
        public string bank { get; set; }
        public Summary Summary { get; set; }
        public Profile Profile { get; set; }
        public Transactions Transactions { get; set; }
    }

    public class Holder
    {
        public string name { get; set; }
        public string dob { get; set; }
        public string mobile { get; set; }
        public string nominee { get; set; }
        public string landline { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string pan { get; set; }
        public string ckycCompliance { get; set; }
    }

    public class Holders
    {
        public string type { get; set; }
        public List<Holder> Holder { get; set; }
    }

    public class Pending
    {
        public int amount { get; set; }
        public string transactionType { get; set; }
    }

    public class Profile
    {
        public Holders Holders { get; set; }
    }

    public class AAStatement
    {
        public string ver { get; set; }
        public string status { get; set; }
        public List<Datum> data { get; set; }
    }

    public class Summary
    {
        public string currentBalance { get; set; }
        public string currency { get; set; }
        public string exchgeRate { get; set; }
        public DateTime balanceDateTime { get; set; }
        public string type { get; set; }
        public string branch { get; set; }
        public string facility { get; set; }
        public string ifscCode { get; set; }
        public string micrCode { get; set; }
        public string openingDate { get; set; }
        public string currentODLimit { get; set; }
        public string drawingLimit { get; set; }
        public string status { get; set; }
        public List<Pending> Pending { get; set; }
    }

    public class Transaction
    {
        public string type { get; set; }
        public string mode { get; set; }
        public string amount { get; set; }
        public string currentBalance { get; set; }
        public DateTime transactionTimestamp { get; set; }
        public string valueDate { get; set; }
        public string txnId { get; set; }
        public string narration { get; set; }
        public string reference { get; set; }
    }

    public class Transactions
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
        public List<Transaction> Transaction { get; set; }
    }

}
