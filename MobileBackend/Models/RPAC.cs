using System;
using System.Collections.Generic;

namespace MobileBackend.Models
{


    public class PANValidation
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string PANNo { get; set; }
        public string response1 { get; set; }
        public string response2 { get; set; }
        public DateTime req_date { get; set; }
    }

    public class RPACAddress
    {
        public string line_1 { get; set; }
        public string line_2 { get; set; }
        public string line_3 { get; set; }
        public string street_name { get; set; }
        public string zip { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string full { get; set; }
    }

    public class RPACData
    {
        public string firstName { get; set; }
        public string midName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string userpan { get; set; }
        public string panStatus { get; set; }
        public string gender { get; set; }
        public string idenityType { get; set; }
        public string aadharLinked { get; set; }
    }

    public class RPACRoot
    {
        public string status { get; set; }
        public object error { get; set; }
        public RPACData data { get; set; }
        public string message { get; set; }
        public string requestid { get; set; }
        public string sequenceId { get; set; }
    }
}
