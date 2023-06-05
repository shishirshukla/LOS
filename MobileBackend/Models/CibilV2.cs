using Newtonsoft.Json;
using System.Collections.Generic;

namespace MobileBackend.CIBILV2
{
    public class CibilResponseV2
    {
         private string _status;
         private ResponseInfo _responseInfo;
         private Fields _fields;

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public ResponseInfo ResponseInfo
        {
            get { return _responseInfo; }
            set { _responseInfo = value; }
        }

        public Fields Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }
    }


    public class ResponseInfo
    {
        private int _applicationId;
        private string _solutionSetInstanceId;

        public int ApplicationId
        {
            get { return _applicationId; }
            set { _applicationId = value; }
        }

        public string SolutionSetInstanceId
        {
            get { return _solutionSetInstanceId; }
            set { _solutionSetInstanceId = value; }
        }
    }


    public class Fields
    {
        private Applicants _applicants;
        private ApplicationData _applicationData;
       private string _decision;
        private string _applicationId;

        public Applicants Applicants
        {
            get { return _applicants; }
            set { _applicants = value; }
        }

        public ApplicationData ApplicationData
        {
            get { return _applicationData; }
            set { _applicationData = value; }
        }

        public string Decision
        {
            get { return _decision; }
            set { _decision = value; }
        }

        public string ApplicationId
        {
            get { return _applicationId; }
            set { _applicationId = value; }
        }
    }


    public class Applicants
    {
        [JsonProperty("Applicant")] private List<Applicant> _applicantList;

        public List<Applicant> ApplicantList
        {
            get { return _applicantList; }
            set { _applicantList = value; }
        }
    }


    public class Applicant
    {
         private string _applicantFirstName;
         private string _applicantMiddleName;
         private string _applicantLastName;
         private string _dateOfBirth;
         private string _gender;
        private string _emailAddress;
        private Identifiers _identifiers;
         private Telephones _telephones;
        private Addresses _addresses;
        private Services _services;
        private string _applicantIdentifier;

        public string ApplicantFirstName
        {
            get { return _applicantFirstName; }
            set { _applicantFirstName = value; }
        }

        public string ApplicantMiddleName
        {
            get { return _applicantMiddleName; }
            set { _applicantMiddleName = value; }
        }

        public string ApplicantLastName
        {
            get { return _applicantLastName; }
            set { _applicantLastName = value; }
        }

        public string DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }

        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public string EmailAddress
        {
            get { return _emailAddress; }
            set { _emailAddress = value; }
        }

        public Identifiers Identifiers
        {
            get { return _identifiers; }
            set { _identifiers = value; }
        }

        public Telephones Telephones
        {
            get { return _telephones; }
            set { _telephones = value; }
        }

        public Addresses Addresses
        {
            get { return _addresses; }
            set { _addresses = value; }
        }

        public Services Services
        {
            get { return _services; }
            set { _services = value; }
        }

        public string ApplicantIdentifier
        {
            get { return _applicantIdentifier; }
            set { _applicantIdentifier = value; }
        }
    }


    public class Identifiers
    {
        private List<Identifier> _identifierList;

        public List<Identifier> IdentifierList
        {
            get { return _identifierList; }
            set { _identifierList = value; }
        }
    }


    public class Identifier
    {
        private string _idNumber;
         private string _idType;

        public string IdNumber
        {
            get { return _idNumber; }
            set { _idNumber = value; }
        }

        public string IdType
        {
            get { return _idType; }
            set { _idType = value; }
        }
    }


    public class Telephones
    {
        private Telephone _telephone;

        public Telephone Telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }
    }


    public class Telephone
    {
        private string _telephoneNumber;
        private string _telephoneType;
        private string _telephoneCountryCode;

        public string TelephoneNumber
        {
            get { return _telephoneNumber; }
            set { _telephoneNumber = value; }
        }

        public string TelephoneType
        {
            get { return _telephoneType; }
            set { _telephoneType = value; }
        }

        public string TelephoneCountryCode
        {
            get { return _telephoneCountryCode; }
            set { _telephoneCountryCode = value; }
        }
    }


    public class Addresses
    {
         private Address _address;

        public Address Address
        {
            get { return _address; }
            set { _address = value; }
        }
    }


    public class Address
    {
        private string _addressType;
        private string _addressLine1;
        private string _addressLine2;
        private string _addressLine3;
        private string _addressLine4;
        private string _addressLine5;
        private string _city;
        private string _pinCode;
        private string _residenceType;
        private string _stateCode;

        public string AddressType
        {
            get { return _addressType; }
            set { _addressType = value; }
        }

        public string AddressLine1
        {
            get { return _addressLine1; }
            set { _addressLine1 = value; }
        }

        public string AddressLine2
        {
            get { return _addressLine2; }
            set { _addressLine2 = value; }
        }

        public string AddressLine3
        {
            get { return _addressLine3; }
            set { _addressLine3 = value; }
        }

        public string AddressLine4
        {
            get { return _addressLine4; }
            set { _addressLine4 = value; }
        }

        public string AddressLine5
        {
            get { return _addressLine5; }
            set { _addressLine5 = value; }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        public string PinCode
        {
            get { return _pinCode; }
            set { _pinCode = value; }
        }

        public string ResidenceType
        {
            get { return _residenceType; }
            set { _residenceType = value; }
        }

        public string StateCode
        {
            get { return _stateCode; }
            set { _stateCode = value; }
        }
    }


    public class Services
    {
        [JsonProperty("Service")] private List<Service> _serviceList;

        public List<Service> ServiceList
        {
            get { return _serviceList; }
            set { _serviceList = value; }
        }
    }


    public class Service
    {
        private string _id;
        private Operations _operations;
        private string _status;
        private string _name;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Operations Operations
        {
            get { return _operations; }
            set { _operations = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }


    public class Operations
    {
        [JsonProperty("Operation")] private List<Operation> _operationList;

        public List<Operation> OperationList
        {
            get { return _operationList; }
            set { _operationList = value; }
        }
    }


    public class Operation
    {
        private string _name;
        private Params _params;
        private string _id;
        private Data _data;
        private string _status;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Params Params
        {
            get { return _params; }
            set { _params = value; }
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Data Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }


    public class Params
    {
        [JsonProperty("Param")] private List<Param> _paramList;

        public List<Param> ParamList
        {
            get { return _paramList; }
            set { _paramList = value; }
        }
    }


    public class Param
    {
        private string _name;
        private string _value;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }


    public class Data
    {
        private Response _response;

        public Response Response
        {
            get { return _response; }
            set { _response = value; }
        }
    }


    public class Response
    {
         private RawResponse _rawResponse;

        public RawResponse RawResponse
        {
            get { return _rawResponse; }
            set { _rawResponse = value; }
        }
    }
    public class DsIDVision
    {
        private Document _document;
        public Document Document
        {
            get { return _document; }
            set { _document = value; }
        }
    }

    public class RawResponse
    {
        private string _bureauResponseXml;
        private string _secondaryReportXml;
        private Document _document;
        private DsIDVision _idvision;

        public string BureauResponseXml
        {
            get { return _bureauResponseXml; }
            set { _bureauResponseXml = value; }
        }

        public string SecondaryReportXml
        {
            get { return _secondaryReportXml; }
            set { _secondaryReportXml = value; }
        }

        public Document Document
        {
            get { return _document; }
            set { _document = value; }
        }
        public DsIDVision DsIDVision
        {
            get { return _idvision; }
            set { _idvision = value; }
        }
    }


    public class Document
    {
        private int _id;
        private string _name;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }


    public class ApplicationData
    {
        private string _gSTStateCode;
        private Services1 _services;

        public string GSTStateCode
        {
            get { return _gSTStateCode; }
            set { _gSTStateCode = value; }
        }

        public Services1 Services
        {
            get { return _services; }
            set { _services = value; }
        }
    }


    public class Services1
    {
        private Service1 _service;

        public Service1 Service
        {
            get { return _service; }
            set { _service = value; }
        }
    }


    public class Service1
    {
        private string _id;
        private string _skip;
        private string _consent;
        private string _enableSimulation;
        private string _name;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Skip
        {
            get { return _skip; }
            set { _skip = value; }
        }

        public string Consent
        {
            get { return _consent; }
            set { _consent = value; }
        }

        public string EnableSimulation
        {
            get { return _enableSimulation; }
            set { _enableSimulation = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }


}
