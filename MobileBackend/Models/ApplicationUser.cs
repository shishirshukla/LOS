using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MobileBackend.Models
{
    public class UserInfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string BranchDetails { get; set; }
        public string ProfileImage { get; set; }
        public string Designation { get; set; }
    }
    public class ApplicationUser :IdentityUser
    {
        public string EmployeeName { get; set; }
        public string BranchId { get; set; }
        public string Designation { get; set; }
        public string Role { get; set; }
       public string Scale { get; set; }
        public string OtherRole { get; set; }
        public string ProfileImage { get; set; }
        public string Location { get; set; }

        public string FCMId { get; set; }
        public Branch BranchDetails { get; set; }
    }
    public class Branch {
        [Key]

        public string Id { get; set; }
        public string BranchName { get; set; }
        public string RegionalOffice { get; set; }
        public string District { get; set; }
        public string BrType { get; set; }
        public string AMHCode { get; set; }
    }
    public class Dashboard
    {
        public ApplicationUser employee { get; set; }
        public int TotalCSPVisitsFY { get; set; }
        public int CSPVisitsCM { get; set; }

    }
}
