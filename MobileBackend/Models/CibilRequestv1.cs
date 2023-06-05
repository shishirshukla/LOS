using System.Collections.Generic;

namespace MobileBackend.Models
{
    public class TuefHeaderRequest
    {
        public string headerType { get; set; }
        public string version { get; set; }
        public string memberRefNo { get; set; }
        public string gstStateCode { get; set; }
        public string enquiryMemberUserId { get; set; }
        public string enquiryPassword { get; set; }
        public string enquiryPurpose { get; set; }
        public string enquiryAmount { get; set; }
        public string responseSize { get; set; }
        public string ioMedia { get; set; }
        public string authenticationMethod { get; set; }
    }

    public class RequestName
    {
        public string index { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string birthDate { get; set; }
        public string gender { get; set; }
    }

    
    

    public class EnquiryAccount
    {
        public string index { get; set; }
        public string accountNumber { get; set; }
    }

    public class ConsumerInputSubject
    {
        public TuefHeaderRequest tuefHeader { get; set; }
        public List<RequestName> names { get; set; }
        public List<Id> ids { get; set; }
        public List<Address> addresses { get; set; }
        public List<EnquiryAccount> enquiryAccounts { get; set; }
    }

    public class CibilRequestV1
    {
        public CibilRequestV1()
        {
            serviceCode = "CAS10001";
            monitoringDate = System.DateTime.Today.ToString("ddMMyyyy");
            consumerInputSubject = new ConsumerInputSubject();
            consumerInputSubject.tuefHeader = new TuefHeaderRequest();
            consumerInputSubject.tuefHeader.headerType="TUEF";
            consumerInputSubject.tuefHeader.version= "12";
            consumerInputSubject.tuefHeader.memberRefNo="81186630";
            consumerInputSubject.tuefHeader.gstStateCode="01";
            consumerInputSubject.tuefHeader.enquiryMemberUserId="BR04608888_UATC2CNPE";
            consumerInputSubject.tuefHeader.enquiryPassword="zwcizyflNdooc+z7";
            consumerInputSubject.tuefHeader.enquiryPurpose="05";
            consumerInputSubject.tuefHeader.responseSize="1";
            consumerInputSubject.tuefHeader.ioMedia="CC";
            consumerInputSubject.tuefHeader.authenticationMethod ="L";
            consumerInputSubject.enquiryAccounts = new List<EnquiryAccount>();
            consumerInputSubject.enquiryAccounts.Add(new EnquiryAccount() {index="I01" , accountNumber ="" });
            consumerInputSubject.names = new List<RequestName>();
            consumerInputSubject.ids = new List<Id>();
            consumerInputSubject.addresses = new List<Address>();

        }
        public string serviceCode { get; set; }
        public string monitoringDate { get; set; }
        public ConsumerInputSubject consumerInputSubject { get; set; }
    }
}
