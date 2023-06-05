using System;

namespace MobileBackend.Models
{
    public class CertificateParameters
    {
        public string panno { get; set; }
        public string PANFullName { get; set; }
        public string FullName { get; set; }
        public string DOB { get; set; }
        public string GENDER { get; set; }
    }

    public class Consent
    {
        public string consentId { get; set; }
        public DateTime timestamp { get; set; }
        public DataConsumer dataConsumer { get; set; }
        public DataProvider dataProvider { get; set; }
        public Purpose purpose { get; set; }
        public User user { get; set; }
        public Data data { get; set; }
        public Permission permission { get; set; }
    }

    public class ConsentArtifact
    {
        public Consent consent { get; set; }
        public Signature signature { get; set; }
    }

    public class Data
    {
        public string id { get; set; }
    }

    public class DataConsumer
    {
        public string id { get; set; }
    }

    public class DataProvider
    {
        public string id { get; set; }
    }

    public class DateRange
    {
        public DateTime from { get; set; }
        public DateTime to { get; set; }
    }

    public class Frequency
    {
        public string unit { get; set; }
        public int value { get; set; }
        public int repeats { get; set; }
    }

    public class Permission
    {
        public string access { get; set; }
        public DateRange dateRange { get; set; }
        public Frequency frequency { get; set; }
    }

    public class Purpose
    {
        public string description { get; set; }
    }

    public class PANVerification
    {
        public string txnId { get; set; }
        public string format { get; set; }
        public CertificateParameters certificateParameters { get; set; }
        public ConsentArtifact consentArtifact { get; set; }
    }

    public class Signature
    {
        public string signature { get; set; }
    }

    public class User
    {
        public string idType { get; set; }
        public string idNumber { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
    }
}
