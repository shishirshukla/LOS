using System.Collections.Generic;




namespace MobileBackend.Models.CIBILV1
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ControlData
    {
        public bool success { get; set; }
    }

    public class TuefHeader
    {
        public string headerType { get; set; }
        public string version { get; set; }
        public string memberRefNo { get; set; }
        public string enquiryMemberUserId { get; set; }
        public string subjectReturnCode { get; set; }
        public string enquiryControlNumber { get; set; }
        public string dateProcessed { get; set; }
        public string timeProcessed { get; set; }
    }

    public class Name
    {
        public string index { get; set; }
        public string name { get; set; }
        public string birthDate { get; set; }
        public string gender { get; set; }
    }

    public class Id
    {
        public string index { get; set; }
        public string idType { get; set; }
        public string idNumber { get; set; }
        public string enquiryEnriched { get; set; }
    }

    public class Telephone
    {
        public string index { get; set; }
        public string telephoneNumber { get; set; }
        public string telephoneType { get; set; }
        public string enquiryEnriched { get; set; }
    }

    public class Email
    {
        public string index { get; set; }
        public string emailID { get; set; }
    }

    public class Employment
    {
        public string index { get; set; }
        public string accountType { get; set; }
        public string dateReported { get; set; }
        public string occupationCode { get; set; }
        public string income { get; set; }
        public string incomeType { get; set; }
        public string incomeFrequency { get; set; }
    }

    public class ReasonCode
    {
        public string reasonCodeName { get; set; }
        public string reasonCodeValue { get; set; }
    }

    public class Score
    {
        public string scoreName { get; set; }
        public string scoreCardName { get; set; }
        public string scoreCardVersion { get; set; }
        public string scoreDate { get; set; }
        public string score { get; set; }
        public List<ReasonCode> reasonCodes { get; set; }
    }

    public class Address
    {
        public string index { get; set; }
        public string line1 { get; set; }
        public string stateCode { get; set; }
        public string pinCode { get; set; }
        public string addressCategory { get; set; }
        public string dateReported { get; set; }
    }

    public class Account
    {
        public string index { get; set; }
        public string memberShortName { get; set; }
        public string accountType { get; set; }
        public string ownershipIndicator { get; set; }
        public string dateOpened { get; set; }
        public string dateClosed { get; set; }
        public string dateReported { get; set; }
        public string highCreditAmount { get; set; }
        public string currentBalance { get; set; }
        public string amountOverdue { get; set; }
        public string paymentHistory { get; set; }
        public string paymentStartDate { get; set; }
        public string paymentEndDate { get; set; }
        public string collateralType { get; set; }
        public string errorCode { get; set; }
        public string cibilRemarksDate { get; set; }
        public string cibilRemarksCode { get; set; }
        public string errorRemarksCode1 { get; set; }
        public string errorRemarksCode2 { get; set; }
        public string lastPaymentDate { get; set; }
        public string creditLimit { get; set; }
    }

    public class Enquiry
    {
        public string index { get; set; }
        public string enquiryDate { get; set; }
        public string memberShortName { get; set; }
        public string enquiryPurpose { get; set; }
        public string enquiryAmount { get; set; }
    }

    public class ConsumerCreditData
    {
        public TuefHeader tuefHeader { get; set; }
        public List<Name> names { get; set; }
        public List<Id> ids { get; set; }
        public List<Telephone> telephones { get; set; }
        public List<Email> emails { get; set; }
        public List<Employment> employment { get; set; }
        public List<Score> scores { get; set; }
        public List<Address> addresses { get; set; }
        public List<Account> accounts { get; set; }
        public List<Enquiry> enquiries { get; set; }
    }

    public class CibilResponseV1
    {
        public ControlData controlData { get; set; }
        public List<ConsumerCreditData> consumerCreditData { get; set; }
    }


}
