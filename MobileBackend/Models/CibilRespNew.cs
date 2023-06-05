using System.Collections.Generic;
using System.Xml.Serialization;

namespace MobileBackend.Models.CIBILResponseNew
{
    [XmlRoot(ElementName = "Header")]
    public class Header
    {

        [XmlElement(ElementName = "SegmentTag")]
        public string SegmentTag { get; set; }

        [XmlElement(ElementName = "Version")]
        public string Version { get; set; }

        [XmlElement(ElementName = "ReferenceNumber")]
        public string ReferenceNumber { get; set; }

        [XmlElement(ElementName = "MemberCode")]
        public string MemberCode { get; set; }

        [XmlElement(ElementName = "SubjectReturnCode")]
        public string SubjectReturnCode { get; set; }

        [XmlElement(ElementName = "EnquiryControlNumber")]
        public double EnquiryControlNumber { get; set; }

        [XmlElement(ElementName = "DateProcessed")]
        public string DateProcessed { get; set; }

        [XmlElement(ElementName = "TimeProcessed")]
        public string TimeProcessed { get; set; }
    }

    [XmlRoot(ElementName = "NameSegment")]
    public class NameSegment
    {

        [XmlElement(ElementName = "Length")]
        public string Length { get; set; }

        [XmlElement(ElementName = "SegmentTag")]
        public string SegmentTag { get; set; }

        [XmlElement(ElementName = "ConsumerName1FieldLength")]
        public string ConsumerName1FieldLength { get; set; }

        [XmlElement(ElementName = "ConsumerName1")]
        public string ConsumerName1 { get; set; }

        [XmlElement(ElementName = "ConsumerName2FieldLength")]
        public string ConsumerName2FieldLength { get; set; }

        [XmlElement(ElementName = "ConsumerName2")]
        public string ConsumerName2 { get; set; }

        [XmlElement(ElementName = "DateOfBirthFieldLength")]
        public string DateOfBirthFieldLength { get; set; }

        [XmlElement(ElementName = "DateOfBirth")]
        public string DateOfBirth { get; set; }

        [XmlElement(ElementName = "GenderFieldLength")]
        public string GenderFieldLength { get; set; }

        [XmlElement(ElementName = "Gender")]
        public string Gender { get; set; }
    }

    [XmlRoot(ElementName = "IDSegment")]
    public class IDSegment
    {

        [XmlElement(ElementName = "Length")]
        public string Length { get; set; }

        [XmlElement(ElementName = "SegmentTag")]
        public string SegmentTag { get; set; }

        [XmlElement(ElementName = "IDType")]
        public string IDType { get; set; }

        [XmlElement(ElementName = "IDNumberFieldLength")]
        public string IDNumberFieldLength { get; set; }

        [XmlElement(ElementName = "IDNumber")]
        public string IDNumber { get; set; }

        [XmlElement(ElementName = "EnrichedThroughEnquiry")]
        public string EnrichedThroughEnquiry { get; set; }
    }

    [XmlRoot(ElementName = "TelephoneSegment")]
    public class TelephoneSegment
    {

        [XmlElement(ElementName = "Length")]
        public string Length { get; set; }

        [XmlElement(ElementName = "SegmentTag")]
        public string SegmentTag { get; set; }

        [XmlElement(ElementName = "TelephoneNumberFieldLength")]
        public string TelephoneNumberFieldLength { get; set; }

        [XmlElement(ElementName = "TelephoneNumber")]
        public string TelephoneNumber { get; set; }

        [XmlElement(ElementName = "TelephoneType")]
        public string TelephoneType { get; set; }

        [XmlElement(ElementName = "EnrichedThroughEnquiry")]
        public string EnrichedThroughEnquiry { get; set; }
    }

    [XmlRoot(ElementName = "EmailContactSegment")]
    public class EmailContactSegment
    {

        [XmlElement(ElementName = "Length")]
        public string Length { get; set; }

        [XmlElement(ElementName = "SegmentTag")]
        public string SegmentTag { get; set; }

        [XmlElement(ElementName = "EmailIDFieldLength")]
        public string EmailIDFieldLength { get; set; }

        [XmlElement(ElementName = "EmailID")]
        public string EmailID { get; set; }
    }

    [XmlRoot(ElementName = "EmploymentSegment")]
    public class EmploymentSegment
    {

        [XmlElement(ElementName = "Length")]
        public string Length { get; set; }

        [XmlElement(ElementName = "SegmentTag")]
        public string SegmentTag { get; set; }

        [XmlElement(ElementName = "AccountType")]
        public string AccountType { get; set; }

        [XmlElement(ElementName = "DateReportedCertified")]
        public string DateReportedCertified { get; set; }

        [XmlElement(ElementName = "IncomeFieldLength")]
        public string IncomeFieldLength { get; set; }

        [XmlElement(ElementName = "Income")]
        public string Income { get; set; }

        [XmlElement(ElementName = "NetGrossIndicator")]
        public string NetGrossIndicator { get; set; }

        [XmlElement(ElementName = "MonthlyAnnualIndicator")]
        public string MonthlyAnnualIndicator { get; set; }
    }

    [XmlRoot(ElementName = "ScoreSegment")]
    public class ScoreSegment
    {

        [XmlElement(ElementName = "Length")]
        public string Length { get; set; }

        [XmlElement(ElementName = "ScoreName")]
        public string ScoreName { get; set; }

        [XmlElement(ElementName = "ScoreCardName")]
        public string ScoreCardName { get; set; }

        [XmlElement(ElementName = "ScoreCardVersion")]
        public string ScoreCardVersion { get; set; }

        [XmlElement(ElementName = "ScoreDate")]
        public string ScoreDate { get; set; }

        [XmlElement(ElementName = "Score")]
        public string Score { get; set; }

        [XmlElement(ElementName = "ReasonCode1FieldLength")]
        public string ReasonCode1FieldLength { get; set; }

        [XmlElement(ElementName = "ReasonCode1")]
        public string ReasonCode1 { get; set; }

        [XmlElement(ElementName = "BureauCharacterstics")]
        public object BureauCharacterstics { get; set; }
    }

    [XmlRoot(ElementName = "Address")]
    public class ResponseAddress
    {

        [XmlElement(ElementName = "AddressSegmentTag")]
        public string AddressSegmentTag { get; set; }

        [XmlElement(ElementName = "Length")]
        public string Length { get; set; }

        [XmlElement(ElementName = "SegmentTag")]
        public string SegmentTag { get; set; }

        [XmlElement(ElementName = "AddressLine1FieldLength")]
        public string AddressLine1FieldLength { get; set; }

        [XmlElement(ElementName = "AddressLine1")]
        public string AddressLine1 { get; set; }

        [XmlElement(ElementName = "AddressLine2FieldLength")]
        public string AddressLine2FieldLength { get; set; }

        [XmlElement(ElementName = "AddressLine2")]
        public string AddressLine2 { get; set; }

        [XmlElement(ElementName = "AddressLine3FieldLength")]
        public string AddressLine3FieldLength { get; set; }

        [XmlElement(ElementName = "AddressLine3")]
        public string AddressLine3 { get; set; }

        [XmlElement(ElementName = "StateCode")]
        public string StateCode { get; set; }

        [XmlElement(ElementName = "PinCodeFieldLength")]
        public string PinCodeFieldLength { get; set; }

        [XmlElement(ElementName = "PinCode")]
        public string PinCode { get; set; }

        [XmlElement(ElementName = "AddressCategory")]
        public string AddressCategory { get; set; }

        [XmlElement(ElementName = "ResidenceCode")]
        public string ResidenceCode { get; set; }

        [XmlElement(ElementName = "DateReported")]
        public string DateReported { get; set; }

        [XmlElement(ElementName = "EnrichedThroughEnquiry")]
        public string EnrichedThroughEnquiry { get; set; }

        [XmlElement(ElementName = "AddressLine5FieldLength")]
        public string AddressLine5FieldLength { get; set; }

        [XmlElement(ElementName = "AddressLine5")]
        public string AddressLine5 { get; set; }

        [XmlElement(ElementName = "AddressLine4FieldLength")]
        public string AddressLine4FieldLength { get; set; }

        [XmlElement(ElementName = "AddressLine4")]
        public string AddressLine4 { get; set; }
    }

    [XmlRoot(ElementName = "Account_Summary_Segment_Fields")]
    public class AccountSummarySegmentFields
    {

        [XmlElement(ElementName = "ReportingMemberShortNameFieldLength")]
        public string ReportingMemberShortNameFieldLength { get; set; }
    }

    [XmlRoot(ElementName = "Account_NonSummary_Segment_Fields")]
    public class AccountNonSummarySegmentFields
    {

        [XmlElement(ElementName = "ReportingMemberShortNameFieldLength")]
        public string ReportingMemberShortNameFieldLength { get; set; }

        [XmlElement(ElementName = "ReportingMemberShortName")]
        public string ReportingMemberShortName { get; set; }

        [XmlElement(ElementName = "AccountType")]
        public string AccountType { get; set; }

        [XmlElement(ElementName = "OwenershipIndicator")]
        public string OwenershipIndicator { get; set; }

        [XmlElement(ElementName = "DateOpenedOrDisbursed")]
        public string DateOpenedOrDisbursed { get; set; }

        [XmlElement(ElementName = "DateOfLastPayment")]
        public string DateOfLastPayment { get; set; }

        [XmlElement(ElementName = "DateReportedAndCertified")]
        public string DateReportedAndCertified { get; set; }

        [XmlElement(ElementName = "HighCreditOrSanctionedAmountFieldLength")]
        public string HighCreditOrSanctionedAmountFieldLength { get; set; }

        [XmlElement(ElementName = "HighCreditOrSanctionedAmount")]
        public string HighCreditOrSanctionedAmount { get; set; }

        [XmlElement(ElementName = "CurrentBalanceFieldLength")]
        public string CurrentBalanceFieldLength { get; set; }

        [XmlElement(ElementName = "CurrentBalance")]
        public string CurrentBalance { get; set; }

        [XmlElement(ElementName = "PaymentHistory1FieldLength")]
        public string PaymentHistory1FieldLength { get; set; }

        [XmlElement(ElementName = "PaymentHistory1")]
        public string PaymentHistory1 { get; set; }

        [XmlElement(ElementName = "PaymentHistoryStartDate")]
        public string PaymentHistoryStartDate { get; set; }

        [XmlElement(ElementName = "PaymentHistoryEndDate")]
        public string PaymentHistoryEndDate { get; set; }

        [XmlElement(ElementName = "CreditLimitFieldLength")]
        public string CreditLimitFieldLength { get; set; }

        [XmlElement(ElementName = "CreditLimit")]
        public string CreditLimit { get; set; }

        [XmlElement(ElementName = "CashLimitFieldLength")]
        public string CashLimitFieldLength { get; set; }

        [XmlElement(ElementName = "CashLimit")]
        public string CashLimit { get; set; }

        [XmlElement(ElementName = "RateOfstringerestFieldLength")]
        public string RateOfstringerestFieldLength { get; set; }

        [XmlElement(ElementName = "RateOfstringerest")]
        public double RateOfstringerest { get; set; }

        [XmlElement(ElementName = "PaymentFrequency")]
        public string PaymentFrequency { get; set; }

        [XmlElement(ElementName = "ActualPaymentAmountFieldLength")]
        public string ActualPaymentAmountFieldLength { get; set; }

        [XmlElement(ElementName = "ActualPaymentAmount")]
        public string ActualPaymentAmount { get; set; }

        [XmlElement(ElementName = "AmountOverdueFieldLength")]
        public string AmountOverdueFieldLength { get; set; }

        [XmlElement(ElementName = "AmountOverdue")]
        public string AmountOverdue { get; set; }

        [XmlElement(ElementName = "PaymentHistory2FieldLength")]
        public string PaymentHistory2FieldLength { get; set; }

        [XmlElement(ElementName = "PaymentHistory2")]
        public string PaymentHistory2 { get; set; }

        [XmlElement(ElementName = "DateClosed")]
        public string DateClosed { get; set; }
        [XmlElement(ElementName = "CreditFacility")]
        public string CreditFacility { get; set; }
        [XmlElement(ElementName = "WrittenOffAmountTotal")]
        public string WrittenOffAmountTotal { get; set; }
        [XmlElement(ElementName = "SettlementAmount")]
        public string SettlementAmount { get; set; }
    }

    [XmlRoot(ElementName = "Account")]
    public class Account
    {

        [XmlElement(ElementName = "Length")]
        public string Length { get; set; }

        [XmlElement(ElementName = "SegmentTag")]
        public string SegmentTag { get; set; }

        [XmlElement(ElementName = "Account_Summary_Segment_Fields")]
        public AccountSummarySegmentFields AccountSummarySegmentFields { get; set; }

        [XmlElement(ElementName = "Account_NonSummary_Segment_Fields")]
        public AccountNonSummarySegmentFields AccountNonSummarySegmentFields { get; set; }
    }

    [XmlRoot(ElementName = "Enquiry")]
    public class Enquiry
    {

        [XmlElement(ElementName = "Length")]
        public string Length { get; set; }

        [XmlElement(ElementName = "SegmentTag")]
        public string SegmentTag { get; set; }

        [XmlElement(ElementName = "DateOfEnquiryFields")]
        public string DateOfEnquiryFields { get; set; }

        [XmlElement(ElementName = "EnquiringMemberShortNameFieldLength")]
        public string EnquiringMemberShortNameFieldLength { get; set; }

        [XmlElement(ElementName = "EnquiringMemberShortName")]
        public string EnquiringMemberShortName { get; set; }

        [XmlElement(ElementName = "EnquiryPurpose")]
        public string EnquiryPurpose { get; set; }

        [XmlElement(ElementName = "EnquiryAmountFieldLength")]
        public string EnquiryAmountFieldLength { get; set; }

        [XmlElement(ElementName = "EnquiryAmount")]
        public string EnquiryAmount { get; set; }
    }

    [XmlRoot(ElementName = "End")]
    public class End
    {

        [XmlElement(ElementName = "SegmentTag")]
        public string SegmentTag { get; set; }

        [XmlElement(ElementName = "TotalLength")]
        public string TotalLength { get; set; }
    }

    [XmlRoot(ElementName = "CreditReport")]
    public class CreditReport
    {

        [XmlElement(ElementName = "Header")]
        public Header Header { get; set; }

        [XmlElement(ElementName = "NameSegment")]
        public NameSegment NameSegment { get; set; }

        [XmlElement(ElementName = "IDSegment")]
        public List<IDSegment> IDSegment { get; set; }

        [XmlElement(ElementName = "TelephoneSegment")]
        public List<TelephoneSegment> TelephoneSegment { get; set; }

        [XmlElement(ElementName = "EmailContactSegment")]
        public List<EmailContactSegment> EmailContactSegment { get; set; }

        [XmlElement(ElementName = "EmploymentSegment")]
        public EmploymentSegment EmploymentSegment { get; set; }

        [XmlElement(ElementName = "ScoreSegment")]
        public ScoreSegment ScoreSegment { get; set; }

        [XmlElement(ElementName = "Address")]
        public List<Address> Address { get; set; }

        [XmlElement(ElementName = "Account")]
        public List<Account> Account { get; set; }

        [XmlElement(ElementName = "Enquiry")]
        public List<Enquiry> Enquiry { get; set; }

        [XmlElement(ElementName = "End")]
        public End End { get; set; }
    }
}
