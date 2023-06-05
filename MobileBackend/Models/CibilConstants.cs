using System.Collections.Generic;

namespace MobileBackend.Models
{
    public class CibilConstants
    {
        public Dictionary<string,string> AccountType { get; set; }
        public CibilConstants()
        {
            AccountType = new Dictionary<string, string>();
            AccountType.Add("01", "Auto Loan (Personal)");
            AccountType.Add("02", "Housing Loan");
            AccountType.Add("03", "Property Loan");
            AccountType.Add("04", "Loan Against Shares/Securities");
            AccountType.Add("05", "Personal Loan");
            AccountType.Add("06", "Consumer Loan");
            AccountType.Add("07", "Gold Loan");
            AccountType.Add("08", "Education Loan");
            AccountType.Add("09", "Loan to Professional");
            AccountType.Add("10", "Credit Card");
            AccountType.Add("11", "Leasing");
            AccountType.Add("12", "Overdraft");
            AccountType.Add("13", "Two-wheeler Loan");
            AccountType.Add("14", "Non-Funded Credit Facility (Acronym - NFCF)");
            AccountType.Add("15", "Loan Against Bank Deposits  (Acronym - LABD)");
            AccountType.Add("16", "Fleet Card");
            AccountType.Add("17", "Commercial Vehicle Loan");
            AccountType.Add("21", "Seller Financing");
            AccountType.Add("22", "Seller Financing Soft (Applicable to Enquiry Purpose only)");
            AccountType.Add("23", "GECL Loan Secured");
            AccountType.Add("24", "GECL Loan Unsecured");
            AccountType.Add("31", "Secured Credit Card");
            AccountType.Add("32", "Used Car Loan");
            AccountType.Add("33", "Construction Equipment Loan");
            AccountType.Add("34", "Tractor Loan");
            AccountType.Add("35", "Corporate Credit Card");
            AccountType.Add("36", "Kisan Credit Card");
            AccountType.Add("37", "Loan on Credit Card");
            AccountType.Add("38", "Prime Minister Jaan Dhan Yojana – Overdraft");
            AccountType.Add("39", "Mudra Loans – Shishu / Kishor / Tarun");
            AccountType.Add("40", "Microfinance – Business Loan");
            AccountType.Add("41", "Microfinance – Personal Loan");
            AccountType.Add("42", "Microfinance – Housing Loan");
            AccountType.Add("43", "Microfinance – Other");
            AccountType.Add("44", "Pradhan Mantri Awas Yojana - Credit Link Subsidy Scheme MAY CLSS");
            AccountType.Add("45", "P2P Personal Loan");
            AccountType.Add("46", "P2P Auto Loan");
            AccountType.Add("47", "P2P Education Loan");
            AccountType.Add("50", "Business Loan – Secured");
            AccountType.Add("51", "Business Loan – General");
            AccountType.Add("52", "Business Loan – Priority Sector – Small Business (Acronym - BLPS-SB )");
            AccountType.Add("53", "Business Loan – Priority Sector – Agriculture (Acronym - BLPS-AGR)");
            AccountType.Add("54", "Business Loan – Priority Sector – Others (Acronym - BLPS-OTH )");
            AccountType.Add("55", "Business Non-Funded Credit Facility – General (Acronym - BNFCF-GEN)");
            AccountType.Add("56", "Business Non-Funded Credit Facility – Priority Sector – Small Business (Acronym - BNFCF-PS-SB)");
            AccountType.Add("57", "Business Non-Funded Credit Facility – Priority Sector – Agriculture (Acronym - BNFCF-PS-AGR)");
            AccountType.Add("58", "Business Non-Funded Credit Facility – Priority Sector-Others (Acronym - BNFCF-PS-OTH )");
            AccountType.Add("59", "Business Loan Against Bank Deposits (Acronym - BLABD )");
            AccountType.Add("61", "Business Loan – Unsecured");
            AccountType.Add("64", "Insurance (Acronym - INSURANCE )");
            AccountType.Add("88", "Locate Plus for Insurance (Applicable to Enquiry Purpose only)");
            AccountType.Add("90", "Account Review (Applicable to Enquiry Purpose only)");
            AccountType.Add("91", "Retro Enquiry (Applicable to Enquiry Purpose only)");
            AccountType.Add("92", "Locate Plus (Applicable to Enquiry Purpose only)");
            AccountType.Add("97", "Adviser Liability (Applicable to Enquiry Purpose only)");
            AccountType.Add("00", "Other");
            AccountType.Add("98", "Secured (Account Group for Portfolio Review response)");
            AccountType.Add("99", "Unsecured (Account Group for Portfolio Review response)");
        }
    }
}
