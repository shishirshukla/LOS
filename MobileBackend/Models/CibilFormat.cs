using System;
using System.Collections.Generic;

namespace MobileBackend.Models
{
    public class CibilHelper
    {
        public CibilHelper()
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
        public Dictionary<string, string> AccountType { get; set; }

        public  string GetAccountType(string x)
        {
            string y = x;
            AccountType.TryGetValue(y, out y);
            return y;
        }

        public static int GetScore(string x)
        {
            int y = 0;
            if (x.Contains('-'))
            {
                y = 0;
            }
            else
            {
                y = int.Parse(x);
            }
            return y;
        }
        public static string GetAccountClassification(string x)
        {
            string y = "NO RECORD";
            if (x.Length > 2)
            {
                y = x.Substring(0, 3);
                if (y == "STD")
                {
                    return y;
                }
                else if (y == "SMA")
                {
                    return y;
                }
                else if (y == "SUB")
                {
                    return y;
                }
                else if (y == "DBT")
                {
                    return y;
                }
                else if (y == "LSS")
                {
                    return y;
                }
                else if (y == "XXX")
                {
                    return "NO RECORD";
                }
                else 
                {
                    int z = 0;
                    int.TryParse(y,out z);
                    if (z > 0 && z <= 90)
                    {
                        return "SMA";
                    }
                    else if (z > 90 && z <= 455)
                    {
                        return "SUB";
                    }
                    else if (z > 455 && z <= 1185)
                    {
                        return "DBT";
                    }
                    else if (z > 1185)
                    {
                        return "LSS";
                    }
                }

            }
            
            return y;
        }

        public static string GetGender(string x)
        {
            if (x == "1" || x == "01")
            {
                return "Female";
            }
            if (x == "2" || x == "02")
            {
                return "Male";
            }
            return x;
        }
        public static string GetScoreName(string x)
        {
            if (x == "1" || x == "01")
            {
                return "Female";
            }
            if (x == "2" || x == "02")
            {
                return "Male";
            }
            return x;
        }
        public static string GetIDType(string x)
        {
            if (x == "1" || x == "01")
            {
                return "PAN No";
            }
            if (x == "2" || x == "02")
            {
                return "Passport";
            }
            if (x == "3" || x == "03")
            {
                return "Voter Id";
            }
            if (x == "4" || x == "04")
            {
                return "Driving License";
            }
            if (x == "5" || x == "05")
            {
                return "Ration Card";
            }
            if (x == "6" || x == "06")
            {
                return "Universal Id";
            }
            return x;
        }
        public static string GetOwnership(string x)
        {
            if (x == "1" || x == "01")
            {
                return "Individual";
            }
            if (x == "2" || x == "02")
            {
                return "Authorised User";
            }
            if (x == "3" || x == "03")
            {
                return "Guarantor";
            }
            if (x == "4" || x == "04")
            {
                return "Joint";
            }
            
            return x;
        }
        public static string GetAddress(string x)
        {

            //01 = Permanent Address, 02 = Residence Address, 03 = Office Address, 04 = Not Categorized
            if (x == "1" || x == "01")
            {
                return "Permanent Address";
            }
            if (x == "2" || x == "02")
            {
                return "Residence Address";
            }
            if (x == "3" || x == "03")
            {
                return "Office Address";
            }
            if (x == "4" || x == "04")
            {
                return "Not Categorized";
            }

            return x;
        }
        public static DateTime ConvertDate(string x) {
            try
            {
                DateTime dt = new DateTime(int.Parse(x.Substring(4, 4)), int.Parse(x.Substring(2, 2)), int.Parse(x.Substring(0, 2)));
                return dt;
            }
            catch (Exception)
            {

                return DateTime.Now;
            }
            
        }
    
    }
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
        public int subjectReturnCode { get; set; }
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
    }

    public class Telephone
    {
        public string index { get; set; }
        public string telephoneNumber { get; set; }
        public string telephoneType { get; set; }
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
        public int income { get; set; }
        public string incomeType { get; set; }
        public string incomeFrequency { get; set; }
    }

    public class ExclusionCode
    {
        public string exclusionCodeName { get; set; }
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
        public List<ExclusionCode> exclusionCodes { get; set; }
        public List<ReasonCode> reasonCodes { get; set; }
    }

    public class BureauCharacteristic
    {
        public string featureName { get; set; }
        public string value { get; set; }
    }

    public class Address
    {
        public string index { get; set; }
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string line3 { get; set; }
        public string line4 { get; set; }
        public string line5 { get; set; }
        public string stateCode { get; set; }
        public string pinCode { get; set; }
        public string addressCategory { get; set; }
        public string residenceCode { get; set; }
        public string dateReported { get; set; }
    }

    public class Account
    {
        public string index { get; set; }
        public string memberShortName { get; set; }
        public string accountNumber { get; set; }
        public string accountType { get; set; }
        public int ownershipIndicator { get; set; }
        public string dateOpened { get; set; }
        public string lastPaymentDate { get; set; }
        public string dateReported { get; set; }
        public int highCreditAmount { get; set; }
        public int currentBalance { get; set; }
        public int amountOverdue { get; set; }
        public string paymentHistory { get; set; }
        public string paymentStartDate { get; set; }
        public string paymentEndDate { get; set; }
        public int emiAmount { get; set; }
        public string paymentFrequency { get; set; }
        public int actualPaymentAmount { get; set; }
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
        public List<BureauCharacteristic> bureauCharacteristics { get; set; }
        public List<Address> addresses { get; set; }
        public List<Account> accounts { get; set; }
    }

    public class CibilFormat
    {
        public ControlData controlData { get; set; }
        public string responseType { get; set; }
        public List<ConsumerCreditData> consumerCreditData { get; set; }
    }
}
