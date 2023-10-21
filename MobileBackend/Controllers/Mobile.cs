using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using MobileBackend.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
//using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MobileBackend.Controllers
{
    public class Mobile : Controller
    {
        private readonly ILogger<Mobile> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public readonly CustomIDataProtection _protector;
        public Mobile(ILogger<Mobile> logger, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ApplicationDbContext context, CustomIDataProtection protector)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
            _protector = protector;
        }
        [HttpPost]
        public async Task<IActionResult> Login(string userid, string password)
        {
            var user = await _userManager.FindByNameAsync(userid);
            var x = await _userManager.CheckPasswordAsync(user, password);
            if (x != false)
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddYears(3),
                    claims: authClaims,
                     signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new JwtSecurityTokenHandler().WriteToken(token) );
            }
            return BadRequest();
        }




        [HttpPost]
        public async Task<IActionResult> custinfo(string uidd)
        {
            Console.WriteLine("Customer360: Start");
            var user = "";
            var key = "";
            if (Request.Headers.ContainsKey("x-user"))
            {
                user = Request.Headers["x-user"];
            }
            else
            {
                Console.WriteLine("Customer360:Header Not Found");
            }
            if (Request.Headers.ContainsKey("x-key"))
            {
                key = Request.Headers["x-key"];
            }
            else
            {
                Console.WriteLine("Customer360:Key Not Found");
            }
            if (user == "IRIX" && key == "2bPRixRaXKJprAltl3jnohMJM7bAbstQ")
            {
                var aa = _context.Cust360New.FromSqlRaw($"SELECT irac_old,  pmjjby, pmsby, apy,sbi_pai FROM public.check_360_new where uidd = '{uidd}'").FirstOrDefault();
                if (aa != null)
                {
                    Console.WriteLine("Customer360:Success");
                    return Ok(aa);
                }
                Console.WriteLine($"Customer360:Invalid Adhaar {uidd}");
                return BadRequest(); 
            }
            else
            {
                Console.WriteLine("Customer360:Header & Key Mismatch");
                return BadRequest();
            }

            return BadRequest();

        }





        [Authorize]
        public async Task<IActionResult> UpdateDeviceId(string fcmId)
        {
            try
            {
                var u = await _userManager.FindByNameAsync(User.Identity.Name);
                var user = _context.Users.Find(u.Id);
                user.FCMId = fcmId; 
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();    
                return Ok("Saved");
            }
            catch (Exception c)
            {

                return BadRequest(c.Message);
            }

        }

        //[Authorize]
        //public async Task<IActionResult> SendNotification(int AppId , string UserName) 
        [HttpPost]
        public async Task<IActionResult> SendNotification(int AppId , string bcCode) 
        {
            GCM requestNew = new GCM();
            requestNew.notification = new Notification();
            requestNew.data = new DataG();
           var u = await  _userManager.FindByNameAsync(bcCode);
            var appls = _context.Applicants.Where(a => a.ApplicationId == AppId && a.Driving_Lic != "Deleted" && a.TypeofApplicant != "Guarantor").ToList();
            List<ApplicantBCDetail> details = new List<ApplicantBCDetail>();
            foreach (var item in appls)
            {
                ApplicantBCDetail applicant = new ApplicantBCDetail();
                applicant.ApplicantName = $"{item.Applicant_First_Name} {item.Applicant_Middle_Name} {item.Applicant_Last_Name}";
                applicant.Designation = item.Voter_Id;
                applicant.Occupation = item.Occupation;
                applicant.WorkPlaceName = item.WorkPlace;
                applicant.WorkPlaceAddress = item.WorkPlaceAddress;
                applicant.PresentAddress = $"{item.Address_Line1} , {item.Address_Line2} , {item.City_Village}";
                details.Add(applicant);
            }
            string payLoad = JsonConvert.SerializeObject(details);
            requestNew.notification.response = payLoad;
            requestNew.to = u.FCMId;
            requestNew.notification.title = "छत्तीसगढ़ राज्य ग्रामीण बैंक";
            requestNew.notification.click_action = "OPEN_ACTIVITY_2";
            requestNew.notification.body = "For All USER";
            requestNew.notification.priority = "high";
            requestNew.notification.applicant_id = AppId.ToString();
            requestNew.notification.image = "https://cgbank.in/MIS/BC1.png";
            requestNew.notification.title = "single";
            requestNew.notification.topic = "single";
            requestNew.data.title = "\u091b\u0924\u094d\u0924\u0940\u0938\u0917\u0922\u093c \u0930\u093e\u091c\u094d\u092f \u0917\u094d\u0930\u093e\u092e\u0940\u0923 \u092c\u0948\u0902\u0915 crgb";
            requestNew.data.body = "For All USER";
            requestNew.data.applicant_id = AppId.ToString(); 
            requestNew.data.response = payLoad;
            requestNew.data.title = "single";
            requestNew.data.topic = "single";
            requestNew.channel_id = "myfirebasechanelabhineet";
            string requestJson = JsonConvert.SerializeObject(requestNew);
            Console.WriteLine("Fetching Token");
            var o1 = new RestClientOptions();
             o1.Proxy = new WebProxy("10.43.5.6:3128");
            var client = new RestClient(o1);
            //var client = new RestClient();
            var request1 = new RestRequest("https://fcm.googleapis.com/fcm/send");
            request1.AddHeader("MediaType", "application/json");
            request1.AddHeader("Content-Type", "application/json");
            request1.AddHeader("Authorization", "key=AAAAU_gjCiM:APA91bHagdZLJIqsx8TxUUF_8khgBr1aIIENjCab6zA25EXzla8JOmrsWEqbHGLlIicTd0LOmNFqzi4iamxPjZT5StwBs2eE_JZemrvzXak2NSZUXarD_0LEjN2u1SYY1otwcOhsksPs");
            //var body = @"grant_type=password&username=BR0460GO01_PROD001&password=Admin@1234567";
            request1.AddParameter("text/plain", requestJson, ParameterType.RequestBody);
            var token_response = await client.ExecuteAsync(request1, Method.Post);

            if (token_response.StatusCode == HttpStatusCode.OK)
            {
                var app = _context.Applications.Find(AppId);
                app.InspectionAllotedTo = bcCode;
                app.InspectionAllotmentDate = DateTime.Now;
                app.InspectionDone = "False";
                _context.Entry(app).State = EntityState.Modified;
                _context.SaveChanges();
                AddLog(AppId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "External Verification Added");
                return RedirectToAction( "Application","LoanFlow", new { Id = _protector.Decode(app.Id.ToString()), Message = "OK" });
            }
            return RedirectToAction("Application", "LoanFlow", new { Id = _protector.Decode(AppId.ToString()), Message = "BAD" , dmessage = "Notification cannot be send" });
        }

        private bool AddLog(int ApplicationId, string UserName, string Ip, string Mode, string Action)
        {
            Log log = new Log();
            log.ApplicationId = ApplicationId;
            log.Action = Action;
            log.Timestamp = DateTime.Now;
            log.UserId = UserName;
            log.Mode = Mode;
            log.IpAddress = Ip;
            _context.Logs.Add(log);
            _context.SaveChanges();
            return true;
        }
        [Authorize]
        public async Task<IActionResult> ApplicantDetails(int Id)
        {
            try
            {
                var u = await _userManager.FindByNameAsync(User.Identity.Name);
                var appls = _context.Applicants.Where(a=> a.ApplicationId == Id).ToList();
                List<ApplicantBCDetail> details = new List<ApplicantBCDetail>();
                foreach (var item in appls)
                {
                    ApplicantBCDetail applicant = new ApplicantBCDetail();
                    applicant.ApplicantName = $"{item.Applicant_First_Name} {item.Applicant_Middle_Name} {item.Applicant_Last_Name}";
                    applicant.Designation = item.Voter_Id;
                    applicant.Occupation = item.Occupation;
                    applicant.WorkPlaceName = item.WorkPlace;
                    applicant.WorkPlaceAddress = item.WorkPlaceAddress;
                    applicant.PresentAddress = $"{item.Address_Line1} , {item.Address_Line2} , {item.City_Village}";
                    details.Add(applicant);
                }
                return Ok(details);
            }
            catch (Exception c)
            {

                return BadRequest(c.Message);
            }

        }

        [Authorize]
        public async Task<IActionResult> SaveKCCLead(KCCLeads leads)
        {
            try
            {
                var u = await _userManager.FindByNameAsync(User.Identity.Name);
                if (u.Designation == "BC" || u.Designation == "Counsellor")
                {
                    leads.SourcingAgency = u.Designation;
                    leads.SourcedBy = User.Identity.Name;
                }
                else
                {
                    leads.SourcedBy = "Direct";
                    leads.SourcingAgency = "Direct";
                }
                
                leads.LeadDate = DateTime.Now;
                leads.BranchId = u.BranchId;
                leads.LeadStatus = "Sourced";
                _context.KCCLeads.Add(leads);
                await _context.SaveChangesAsync();
                return Ok("Saved");
            }
            catch (Exception c)
            {

                return BadRequest(c.Message);
            }
            
        }
        [Authorize]
        public IActionResult Get360(string acc)
        {
            return Ok("");

        }
        [Authorize]
        public IActionResult CheckAccount(string acc)
        {
            try
            {
                var u = _context.AccountData.FromSqlRaw($"SELECT account_no,  acctopendate, currentbalance,  odlimit,  accounttype, interestcat, intrate,name,cust_name 	FROM public.temp_loan Where account_no = LPAD('{acc}',17,'0')").FirstOrDefault();

                return Ok(u);
            }
            catch (Exception c)
            {

                return BadRequest(c.Message);
            }
            return Ok("");

        }
        [Authorize]
        public async Task<IActionResult> SaveTPLLead(TPLLeads leads)
        {
            try
            {
                var u = await _userManager.FindByNameAsync(User.Identity.Name);
                if (u.Designation == "BC" || u.Designation == "Counsellor")
                {
                    leads.SourcingAgency = u.Designation;
                    leads.SourcedBy = User.Identity.Name;
                }
                else
                {
                    leads.SourcedBy = "Direct";
                    leads.SourcingAgency = "Direct";
                }
               
                leads.BranchId = u.BranchId;
                leads.LeadStatus = "Sourced";
                leads.LeadDate = DateTime.Now;
                _context.TPLLeads.Add(leads);
                await _context.SaveChangesAsync();
                return Ok("Saved");
            }
            catch (Exception c)
            {

                return BadRequest(c.Message);
            }

        }
        [Authorize]
        public async Task<IActionResult> SaveGoldLead(GoldLoanLeads leads)
        {
            try
            {
                var u = await _userManager.FindByNameAsync(User.Identity.Name);
                if (u.Designation == "BC" || u.Designation == "Counsellor")
                {
                    leads.SourcingAgency = u.Designation;
                    leads.SourcedBy = User.Identity.Name;
                }
                else
                {
                    leads.SourcedBy = "Direct";
                    leads.SourcingAgency = "Direct";
                }
                leads.BranchId = u.BranchId;
                leads.LeadStatus = "Sourced";
                
                leads.LeadDate = DateTime.Now;

                _context.GoldLeads.Add(leads);
                await _context.SaveChangesAsync();
                return Ok("Saved");
            }
            catch (Exception c)
            {

                return BadRequest(c.Message);
            }

        }

        [Authorize]
        public async Task<IActionResult> ListLeads(string ll = "Sourced")
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            List<string> list = new List<string>();
            var brInfo = _context.Branches.Find(user.BranchId);
            ViewBag.UserInfo = $"{user.EmployeeName} ({user.UserName}) , Branch : {user.BranchId} ({brInfo.BrType})";
            if (brInfo.BrType == "Branch")
            {
                list.Add(brInfo.Id);
            }
            else if (brInfo.BrType == "AMH")
            {
                list.AddRange(_context.Branches.Where(a => a.AMHCode == user.BranchId).Select(a => a.Id).ToList());
            }
            else if (brInfo.BrType == "HO")
            {
                list.AddRange(_context.Branches.Select(a => a.Id).ToList());
            }
            else
            {
                list.AddRange(_context.Branches.Where(a => a.RegionalOffice == user.BranchId).Select(a => a.Id).ToList());
            }
            var tplleads = _context.TPLLeads.Include(a => a.Branch).Where(a => list.Contains(a.BranchId) && a.LeadStatus == ll).ToList();
            var kccleads = _context.KCCLeads.Include(a => a.Branch).Where(a => list.Contains(a.BranchId) && a.LeadStatus == ll).ToList();
            var goldleads = _context.GoldLeads.Include(a => a.Branch).Where(a => list.Contains(a.BranchId) && a.LeadStatus == ll).ToList();
            var genLeads = _context.CommonLeads.Include(a => a.Branch).Where(a => list.Contains(a.BranchId) && a.LeadStatus == ll).ToList();
            LeadCollection leadCollection = new LeadCollection();
            leadCollection.KCCLead = kccleads;
            leadCollection.GoldLoanLeads = goldleads;
            leadCollection.TPLLeads = tplleads;
            leadCollection.GeneralLeads = genLeads;




            return View(leadCollection);
        }

       [Authorize]
        public async Task<IActionResult> LeadDashboard()
        {
            Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue()
            {
                Public = true,
                MaxAge = TimeSpan.FromSeconds(1),
                MustRevalidate = true,
                NoStore = true,
                NoCache = true,
                MaxStale = true
            };
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            List<string> list = new List<string>();
            var brInfo = _context.Branches.Find(user.BranchId);
            ViewBag.UserInfo = $"{user.EmployeeName} ({user.UserName}) , Branch : {user.BranchId} ({brInfo.BrType})";
            if (brInfo.BrType == "Branch")
            {
                list.Add(brInfo.Id);
            }
            else if (brInfo.BrType == "AMH")
            {
                list.AddRange(_context.Branches.Where(a => a.AMHCode == user.BranchId).Select(a => a.Id).ToList());
            }
            else if (brInfo.BrType == "HO")
            {
                list.AddRange(_context.Branches.Select(a => a.Id).ToList());
            }
            else
            {
                list.AddRange(_context.Branches.Where(a => a.RegionalOffice == user.BranchDetails.RegionalOffice && a.BrType == "Branch").Select(a => a.Id).ToList());
            }
            List<string> rejects = new List<string>();
            rejects.Add("Not Interested");
            rejects.Add("Not Eligible");
            rejects.Add("Migrated to Other Bank");
            rejects.Add("Fake Lead");
            var tplleads = _context.TPLLeads.Include(a => a.Branch).Where(a => list.Contains(a.BranchId)).ToList();
            var kccleads = _context.KCCLeads.Include(a => a.Branch).Where(a => list.Contains(a.BranchId)).ToList();
            var goldleads = _context.GoldLeads.Include(a => a.Branch).Where(a => list.Contains(a.BranchId)).ToList();
            var genLeads = _context.CommonLeads.Include(a => a.Branch).Where(a => list.Contains(a.BranchId)).ToList();
            LeadCollection leadCollection = new LeadCollection();
            leadCollection.KCCLead = kccleads.Where(a=> !rejects.Contains(a.LeadStatus)).ToList();
            leadCollection.GoldLoanLeads = goldleads.Where(a => !rejects.Contains(a.LeadStatus)).ToList();
            leadCollection.TPLLeads = tplleads.Where(a => !rejects.Contains(a.LeadStatus)).ToList();
            leadCollection.GeneralLeads = genLeads.Where(a => !rejects.Contains(a.LeadStatus)).ToList();
            ViewBag.FakeLead = tplleads.Where(a => a.LeadStatus == "Fake Lead").Count() + kccleads.Where(a => a.LeadStatus == "Fake Lead").Count() + goldleads.Where(a => a.LeadStatus == "Fake Lead").Count() + genLeads.Where(a => a.LeadStatus == "Fake Lead").Count();
            ViewBag.NotEligible = tplleads.Where(a => a.LeadStatus == "Not Eligible").Count() + kccleads.Where(a => a.LeadStatus == "Not Eligible").Count() + goldleads.Where(a => a.LeadStatus == "Not Eligible").Count() + genLeads.Where(a => a.LeadStatus == "Not Eligible").Count();
            ViewBag.Migrated = tplleads.Where(a => a.LeadStatus == "Migrated to Other Bank").Count() + kccleads.Where(a => a.LeadStatus == "Migrated to Other Bank").Count() + goldleads.Where(a => a.LeadStatus == "Migrated to Other Bank").Count() + genLeads.Where(a => a.LeadStatus == "Migrated to Other Bank").Count();
            ViewBag.NotInterested = tplleads.Where(a => a.LeadStatus == "Not Interested").Count() + kccleads.Where(a => a.LeadStatus == "Not Interested").Count() + goldleads.Where(a => a.LeadStatus == "Not Interested").Count() + genLeads.Where(a => a.LeadStatus == "Not Interested").Count();
            return View(leadCollection);
        }

        [Authorize]
        public async Task<IActionResult> ViewLead(int Id,string LeadType)
        {
            ViewBag.LeadType = LeadType;    
            if (LeadType == "KCC")
            {
                var lead = _context.KCCLeads.Find(Id);
                _context.Entry(lead).Reference(a => a.Branch).Load();
                lead.Comments = _context.LeadComments.Include(a=> a.LeadDetails).Include(a=> a.LeadDetails.Branch).Where(a => a.LeadId == Id).ToList();
                ViewBag.Lead = lead;
                return View(lead);
            }
            if (LeadType == "TPL")
            {
                var lead = _context.TPLLeads.Find(Id);
                _context.Entry(lead).Reference(a => a.Branch).Load();
                lead.Comments = _context.LeadComments.Include(a => a.LeadDetails).Include(a => a.LeadDetails.Branch).Where(a => a.LeadId == Id).ToList();
                ViewBag.Lead = lead;
                return View(lead);
            }
            if (LeadType == "Gold")
            {

                var lead = _context.GoldLeads.Find(Id);
                _context.Entry(lead).Reference(a => a.Branch).Load();
                lead.Comments = _context.LeadComments.Include(a => a.LeadDetails).Include(a => a.LeadDetails.Branch).Where(a => a.LeadId == Id).ToList();
                ViewBag.Lead = lead;
                return View(lead);
            }
            if (LeadType == "Gen")
            {

                var lead = _context.CommonLeads.Find(Id);
                _context.Entry(lead).Reference(a => a.Branch).Load();
                lead.Comments = _context.LeadCommentsGen.Include(a => a.LeadDetails).Include(a => a.LeadDetails.Branch).Where(a => a.LeadId == Id).ToList();
                ViewBag.Lead = lead;
                ViewBag.Cibil = _context.CibilAccounts.FromSqlRaw($"SELECT member_ref, member_name, acc_number, acc_type, date_open, date_close, high_credit, current_bal, amt_overdue, tenure, emi, hist1, hist1_date FROM cibil_soft.accounts where member_ref = '{lead.RefNoCRGB.PadLeft(17, '0').Substring(0, 16)}'").ToList();

                return View("ViewLeadGen",lead);
            }

            return View();
        }
        [Authorize]
        public async Task<IActionResult> TakeAction(int Id , string LeadType)
        {
            ViewBag.LeadId = Id;
            ViewBag.LeadType = LeadType;
            List<string> availActions = new List<string>();
            Leads leads = new Leads();
            availActions.Add("Not Interested");
            availActions.Add("Not Eligible");
            availActions.Add("Migrated to Other Bank");
            availActions.Add("Fake Lead");
            if (LeadType == "KCC")
            {
                leads = _context.KCCLeads.Find(Id);
            }
            if (LeadType == "Gold")
            {
                leads = _context.GoldLeads.Find(Id);
            }
            if (LeadType == "TPL")
            {
                leads = _context.TPLLeads.Find(Id);
            }
            if (LeadType == "Gen")
            {
                var gleads = _context.CommonLeads.Find(Id);
                if (gleads.LeadStatus != null)
                {
                    if (gleads.LeadStatus == "Sourced")
                    {
                        availActions.Add("Contact With Customer");
                    }
                    if (gleads.LeadStatus == "Contact With Customer")
                    {
                        availActions.Add("Contact With Customer");
                        availActions.Add("Documents Obtained");
                    }
                    if (gleads.LeadStatus == "Documents Obtained")
                    {
                        availActions.Add("Converted To Application");
                    }
                    ViewBag.AvailableActions = availActions;
                    return View();

                }
            }
            if (leads.LeadStatus != null)
            {
                if (leads.LeadStatus == "Sourced")
                {
                    availActions.Add("Contact With Customer");
                }
                if (leads.LeadStatus == "Contact With Customer")
                {
                    availActions.Add("Contact With Customer");
                    availActions.Add("Documents Obtained");
                }
                if (leads.LeadStatus == "Documents Obtained")
                {
                    availActions.Add("Converted To Application");
                }
                

            }
            ViewBag.AvailableActions = availActions;
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> TakeAction(int LeadId, string Statment,string UpdateAction,string LeadType)
        {

            var action = new LeadRoadMap();
            action.LeadId = LeadId;
            action.Remark = Statment;
            action.EntryType = UpdateAction;
            action.ActionDate = DateTime.Now;
            _context.LeadComments.Add(action);
            _context.SaveChanges();
            var leads = new Leads();
            if (LeadType == "KCC")
            {
                leads = _context.KCCLeads.Find(LeadId);
            }
            if (LeadType == "Gold")
            {
                leads = _context.GoldLeads.Find(LeadId);
            }
            if (LeadType == "TPL")
            {
                leads = _context.TPLLeads.Find(LeadId);
            }
            if (LeadType == "Gen")
            {
                var gleads = _context.CommonLeads.Find(LeadId);
                gleads.LeadStatus = UpdateAction;
                _context.Entry(gleads).State = EntityState.Modified;
                _context.SaveChanges(true);
            }
            else {
                leads.LeadStatus = UpdateAction;
                _context.Entry(leads).State = EntityState.Modified;
                _context.SaveChanges(true);
            }

           
            if (UpdateAction == "Converted To Application" && LeadType != "Gen")
            {
                var app = new Application();
                app.LoanScheme = LeadType;
                app.Purpose = LeadType;
                app.ApplicationDate = DateTime.Now;
                app.AppliedAmt = leads.AppliedAmt;
                app.Status = "Pending";
                app.Owner = "Branch";
                app.BranchId = leads.BranchId;
                _context.Applications.Add(app);
                _context.SaveChanges();
                
                
            }
            return RedirectToAction("ViewLead",new {Id=LeadId, LeadType = LeadType });
        }
    }
}
