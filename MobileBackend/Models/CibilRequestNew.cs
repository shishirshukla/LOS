using System.Collections.Generic;

namespace MobileBackend.Models.CIBILNew
{
    public class RequestInfo
    {
        public string SolutionSetName { get; set; }
        public string ExecuteLatestVersion { get; set; }
    }

    public class Identifier
    {
        public string IdNumber { get; set; }
        public string IdType { get; set; }
    }

    public class Identifiers
    {
        public List<Identifier> Identifier { get; set; }
    }

    public class Telephone
    {
        public string TelephoneNumber { get; set; }
        public string TelephoneType { get; set; }
        public string TelephoneCountryCode { get; set; }
    }

    public class Telephones
    {
        public Telephone Telephone { get; set; }
    }

    public class Address
    {
        public string AddressType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string AddressLine5 { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string ResidenceType { get; set; }
        public string StateCode { get; set; }
    }

    public class Addresses
    {
        public Address Address { get; set; }
    }

    public class Param
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class Params
    {
        public List<Param> Param { get; set; }
    }

    public class Operation
    {
        public string Name { get; set; }
        public Params Params { get; set; }
    }

    public class Operations
    {
        public List<Operation> Operation { get; set; }
    }

    public class Service
    {
        public Service()
        {
            Id = "CORE";
            //Operations = new Operations();
            //Operations.Operation = new List<Operation> ();
            //Operation operation = new Operation();
            //operation.Name = "ConsumerCIR";
            //operation.Params = new Params();
            //operation.Params.Param = new List<Param>();
            //operation.Params.Param.Add(new Param() { Name = "CibilBureauFlag", Value = "false" });
            //operation.Params.Param.Add(new Param() { Name = "ScoreType", Value = "08" });
            //operation.Params.Param.Add(new Param() { Name = "Purpose", Value = "10" });
            //operation.Params.Param.Add(new Param() { Name = "Amount", Value = "400000" });
            //operation.Params.Param.Add(new Param() { Name = "MemberCode", Value = "BR04608888_CIRC2CNPE" });
            //operation.Params.Param.Add(new Param() { Name = "Password", Value = "Hx6$Qd6@Nu3$Me" });
            //operation.Params.Param.Add(new Param() { Name = "FormattedReport", Value = "false" });
            //operation.Params.Param.Add(new Param() { Name = "GSTStateCode", Value = "33" });

            //Operation operation1 = new Operation();
            //operation1.Name = "IDV";
            //operation1.Params = new Params();
            //operation1.Params.Param = new List<Param>();
            //operation1.Params.Param.Add(new Param() { Name = "IDVerificationFlag", Value = "false" });
            //operation1.Params.Param.Add(new Param() { Name = "ConsumerConsentForUIDAIAuthentication", Value = "Y" });
            //operation1.Params.Param.Add(new Param() { Name = "GSTStateCode", Value = "33" });

            //Operations.Operation.Add(operation);
            //Operations.Operation.Add(operation1);

        }
        public string Id { get; set; }
        public Operations Operations { get; set; }
        public string Skip { get; set; }
        public string Consent { get; set; }
        public string EnableSimulation { get; set; }
    }

    public class Services
    {
        public Service Service { get; set; }
    }

    public class Applicant
    {
        public Applicant()
        {
            Services = new Services();
            Services.Service = new Service();
            Service service = new Service();
            service.Id = "CORE";
            service.EnableSimulation = "False";
            service.Skip = "N";
            service.Consent = "true";
            service.Operations = new Operations();
            service.Operations.Operation = new List<Operation>();

           

           

        }
        public string ApplicantFirstName { get; set; }
        public string ApplicantMiddleName { get; set; }
        public string ApplicantLastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string EmailAddress { get; set; }
        public Identifiers Identifiers { get; set; }
        public Telephones Telephones { get; set; }
        public Addresses Addresses { get; set; }
        public Services Services { get; set; }
    }

    public class Applicants
    {
        public Applicant Applicant { get; set; }
    }

    public class ApplicationData
    {
        public string GSTStateCode { get; set; }
        public Services Services { get; set; }
    }

    public class Fields
    {
        public Applicants Applicants { get; set; }
        public ApplicationData ApplicationData { get; set; }
        //public string TrailLevel { get; set; }
    }

    public class CibilRequestNew
    {
        public CibilRequestNew()
        {
            Fields = new Fields();
            RequestInfo = new RequestInfo();
            RequestInfo.SolutionSetName = "GO_CHHATTISGARH_GRAMINBANK_AGSS";
            RequestInfo.ExecuteLatestVersion = "true";
            Fields.ApplicationData = new ApplicationData();
            Fields.ApplicationData.GSTStateCode = "33";
            Fields.ApplicationData.Services = new Services();
            Fields.ApplicationData.Services.Service =  new Service();
            Fields.ApplicationData.Services.Service.Id = "CORE";
            Fields.ApplicationData.Services.Service.Skip = "N";
            Fields.ApplicationData.Services.Service.Consent = "true";
            Fields.ApplicationData.Services.Service.EnableSimulation = "False";
            //Fields.TrailLevel = "0";

        }
        public RequestInfo RequestInfo { get; set; }
        public Fields Fields { get; set; }
    }
    
}
