using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MobileBackend.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MobileBackend.Controllers
{
    public class VisitController : Controller
    {
        private readonly ILogger<VisitController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _database;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly CustomIDataProtection _protector;
        private IConfiguration _config;
        public VisitController(CustomIDataProtection protector,ILogger<VisitController> logger, IWebHostEnvironment webHostEnvironment, ApplicationDbContext database, IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _database = database;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
            _userManager = userManager;
            _protector = protector;
        }

        async Task<ApplicationUser> getEmp(string token) {
            try
            {
              
                ApplicationUser employeeMaster = await _userManager.FindByNameAsync(User.Identity.Name);

                return employeeMaster;
            }
            catch (Exception)
            {

                return null;
            }

        }


        public async Task<IActionResult> Login(string userid, string password)
        {
            var user = await _userManager.FindByNameAsync(userid);
            var x = await _userManager.CheckPasswordAsync(user, password);
            if (x != false)
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var token = new JwtSecurityToken(
                    issuer: _config["JWT:ValidIssuer"],
                    audience: _config["JWT:ValidAudience"],
                    expires: DateTime.Now.AddYears(3),
                    claims: authClaims,
                     signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
                //HttpContext.Session.SetString("Token", new JwtSecurityTokenHandler().WriteToken(token));
                return Json(new JwtSecurityTokenHandler().WriteToken(token));
            }
            return Json("Ok");
        }
        public async Task<IActionResult> LoginByApp(string userid)
        {
            var user = await _userManager.FindByNameAsync(userid);
            
            if (user != null)
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var token = new JwtSecurityToken(
                    issuer: _config["JWT:ValidIssuer"],
                    audience: _config["JWT:ValidAudience"],
                    expires: DateTime.Now.AddYears(3),
                    claims: authClaims,
                     signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
                //HttpContext.Session.SetString("Token", new JwtSecurityTokenHandler().WriteToken(token));
                return Json(new JwtSecurityTokenHandler().WriteToken(token));
            }
            return Json("Failed");
        }
        [HttpPost]
        public async Task<IActionResult> LoginBC(string userid, string password)
        {

            var user = await _userManager.FindByNameAsync(userid);
            var x= await _userManager.CheckPasswordAsync(user, password);
            if (x != false)
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var token = new JwtSecurityToken(
                    issuer: _config["JWT:ValidIssuer"],
                    audience: _config["JWT:ValidAudience"],
                    expires: DateTime.Now.AddYears(3),
                    claims: authClaims,
                     signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
                //HttpContext.Session.SetString("Token", new JwtSecurityTokenHandler().WriteToken(token));
                return Json(new JwtSecurityTokenHandler().WriteToken(token));
            }
            return Json("Failed");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var x = await _userManager.CheckPasswordAsync(user, oldPassword);
            if (x != false)
            {
                await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
                ViewBag.Message = "Password Changed";
                return View();
            }
            ViewBag.Message = "Old Password Mismatch";
            return View();

        }

        public IActionResult GetBranches(string x)
        {
            if (x == "HO")
            {
                ViewBag.Branches = null;
                ViewBag.Data1 = "HO";
            }
            else if (x == "NW1")
            {
                ViewBag.Branches = null;
                ViewBag.Data1 = "NW1";
            }
            else if (x == "NW2")
            {
                ViewBag.Branches = null;
                ViewBag.Data1 = "NW2";
            }
            else
            {
                var branches = _database.Branches.Where(a => a.RegionalOffice == x).ToList();
                ViewBag.Branches = branches;
            }

            return View();
        }

        public IActionResult BranchPerformance()
        {
            List<string> DataPoints = new List<string>();
            DataPoints.Add("Mar-20");
            DataPoints.Add("Mar-21");
            DataPoints.Add("Sep-21");
            DataPoints.Add("Nov-21");
            DataPoints.Add("30/12/21");
            ViewBag.DataPoints = DataPoints;
            var x = _database.BranchPerformances.Find("HO");
            return View(x);
        }
        [HttpPost]
        public IActionResult BranchPerformance(string Region, string Branch)
        {
            List<string> DataPoints = new List<string>();
            DataPoints.Add("Mar-20");
            DataPoints.Add("Mar-21");
            DataPoints.Add("Sep-21");
            DataPoints.Add("Nov-21");
            DataPoints.Add("30/12/21");
            ViewBag.DataPoints = DataPoints;
            var x = _database.BranchPerformances.Find(Branch);
            return View(x);
        }
        public IActionResult BCVisitSummary()
        {
            ViewBag.PostBack = false;
            return View();
        }
        [HttpPost]
        public IActionResult BCVisitSummary(string region, DateTime fromDate, DateTime toDate)
        {
            ViewBag.PostBack = true;
            var x = _database.CSPVisits.Include(a => a.Csp).Include(a => a.EmployeeMaster).Include(a => a.Csp.BranchMaster).Where(a => a.Csp.BranchMaster.RegionalOffice == region && a.VisitDate >= fromDate && a.VisitDate <= toDate).ToList();
            ViewBag.Data = x;
            return View();
        }
        public IActionResult BCVisitCount()
        {
            ViewBag.PostBack = false;
            return View();
        }
        [HttpPost]
        public IActionResult BCVisitCount(string region, DateTime fromDate, DateTime toDate)
        {
            ViewBag.PostBack = true;
            var x = _database.CSPVisits.Include(a => a.Csp).Include(a => a.EmployeeMaster).Include(a => a.Csp.BranchMaster).Where(a => a.Csp.BranchMaster.RegionalOffice == region && a.VisitDate >= fromDate && a.VisitDate <= toDate).GroupBy(a => a.EmployeeMaster.EmployeeName).Select(g => new { Key = g.Key, Count = g.Count() }).ToDictionary(k => k.Key, i => i.Count);
            ViewBag.Data = x;
            return View();
        }
        public IActionResult PreInspectionList() {
            List<PreInspection> list = new List<PreInspection>();
            return View(list);
        }


        [HttpPost]

        public IActionResult PreInspectionList(int ApplicationId)
        {
            var c = _database.PreInspection.Where(a => a.ApplicationId == ApplicationId).ToList();
            return View(c);
        }

        public async Task<IActionResult> CSPList(string token, string lat, string loc_long)
        {
            ApplicationUser employeeMaster = await getEmp(token);
            ViewBag.lat = lat;
            ViewBag.loc_long = loc_long;
            ViewBag.token = token;
            return View(employeeMaster);
        }                                                                                           
       
        [Authorize]
        public async Task<IActionResult> EmployeeInfo(string token)
        {
            ApplicationUser employeeMaster = await getEmp(token);
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            int Year = 0;
            if (DateTime.Now.Month > 3)
            {
                Year = DateTime.Now.Year;
            }
            else
            {
                Year = DateTime.Now.Year - 1;
            }
            var firstDayOfFY = new DateTime(Year, 4, 1);
            int totalVisits = _database.CSPVisits.Where(a=> a.UserId == employeeMaster.Id && a.VisitDate >= firstDayOfFY).Count();
            
            int visitCurrentMonth = _database.CSPVisits.Where(a => a.UserId == employeeMaster.Id && a.VisitDate >= firstDayOfMonth).Count();
            Dashboard dashboard = new Dashboard();
            dashboard.CSPVisitsCM = visitCurrentMonth;
            dashboard.TotalCSPVisitsFY = totalVisits;
            dashboard.employee = employeeMaster;
            return Json(dashboard);
        }
        [Authorize]
        public async Task<IActionResult> BCInfo()
        {
            ApplicationUser employeeMaster = await _userManager.FindByNameAsync(User.Identity.Name);
            UserInfo userInfo = new UserInfo();
            userInfo.UserId = employeeMaster.UserName;
            userInfo.UserName = employeeMaster.EmployeeName;
            userInfo.ProfileImage = employeeMaster.ProfileImage;
            userInfo.BranchDetails = employeeMaster.BranchId;
            userInfo.Designation = employeeMaster.Designation;
            return Json(userInfo);
        }
        [Authorize]
        public async Task<IActionResult> UpdateProfile(string Img)
        {
            
            try
            {
                ApplicationUser employeeMaster = await _userManager.FindByNameAsync(User.Identity.Name);
                employeeMaster.ProfileImage = Img;
                _database.Entry(employeeMaster).State = EntityState.Modified;
                _database.SaveChanges();
                return Ok("Success");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }
        public async Task<IActionResult> ViewList(string token, string lat, string loc_long)
        {
            ApplicationUser employeeMaster = await getEmp(token);
            var CSPs = _database.Csps.Where(a=> a.BrCode == int.Parse(employeeMaster.BranchId).ToString()).ToList();
            ViewBag.CSPs = CSPs;
            ViewBag.lat = lat;
            ViewBag.loc_long = loc_long;
            ViewBag.token = token;
            return View(employeeMaster);
        }
        [Authorize()]
        public async Task<IActionResult> AddNewInspection(string token, string lat, string loc_long)
        {
            ApplicationUser employeeMaster = await _userManager.FindByNameAsync(User.Identity.Name);
            employeeMaster.EmployeeName = employeeMaster.EmployeeName;
            ViewBag.Location_lat = String.Format("{0},{1}", lat, loc_long);
            ViewBag.Token = token;
            List<string> list = new List<string>();
            var brInfo = _database.Branches.Find(employeeMaster.BranchId);
            if (brInfo.BrType == "Branch")
            {
                list.Add(brInfo.Id);
            }
            else if (brInfo.BrType == "AMH")
            {
                list.AddRange(_database.Branches.Where(a => a.AMHCode == employeeMaster.BranchId).Select(a => a.Id).ToList());
            }
            else if (brInfo.BrType == "HO")
            {
                list.AddRange(_database.Branches.Select(a => a.Id).ToList());
            }
            else
            {
                list.AddRange(_database.Branches.Where(a => a.RegionalOffice == employeeMaster.BranchId).Select(a => a.Id).ToList());
            }
            var applications = _database.Applications.Where(a => a.Status == "Pending" && list.Contains(a.BranchId)).ToList();
            ViewBag.Applications = applications;
            return View(employeeMaster);
        }
        [Authorize()]
        public async Task<IActionResult> AddNewInspectionApi()
        {
            ApplicationUser employeeMaster = await _userManager.FindByNameAsync(User.Identity.Name);
            employeeMaster.EmployeeName = employeeMaster.EmployeeName;
            
            List<string> list = new List<string>();
            var brInfo = _database.Branches.Find(employeeMaster.BranchId);
            if (brInfo.BrType == "Branch")
            {
                list.Add(brInfo.Id);
            }
            else if (brInfo.BrType == "AMH")
            {
                list.AddRange(_database.Branches.Where(a => a.AMHCode == employeeMaster.BranchId).Select(a => a.Id).ToList());
            }
            else if (brInfo.BrType == "HO")
            {
                list.AddRange(_database.Branches.Select(a => a.Id).ToList());
            }
            else
            {
                list.AddRange(_database.Branches.Where(a => a.RegionalOffice == employeeMaster.BranchId).Select(a => a.Id).ToList());
            }
            var applications = _database.Applications.Where(a => a.Status == "Pending" && list.Contains(a.BranchId)).ToList();
            //ViewBag.Applications = applications;
            return Json(applications);
        }
        [Authorize()]
        public async Task<IActionResult> GetAppListApi(string typ="KCC")
        {
            ApplicationUser employeeMaster = await _userManager.FindByNameAsync(User.Identity.Name);
            employeeMaster.EmployeeName = employeeMaster.EmployeeName;

            List<string> list = new List<string>();
            var brInfo = _database.Branches.Find(employeeMaster.BranchId);
            if (brInfo.BrType == "Branch")
            {
                list.Add(brInfo.Id);
            }
            else if (brInfo.BrType == "AMH")
            {
                list.AddRange(_database.Branches.Where(a => a.AMHCode == employeeMaster.BranchId).Select(a => a.Id).ToList());
            }
            else if (brInfo.BrType == "HO")
            {
                list.AddRange(_database.Branches.Select(a => a.Id).ToList());
            }
            else
            {
                list.AddRange(_database.Branches.Where(a => a.RegionalOffice == employeeMaster.BranchId).Select(a => a.Id).ToList());
            }
            var applications = _database.Applications.Where(a => a.Status == "Pending" && list.Contains(a.BranchId) && a.LoanScheme.Contains(typ)).ToList();
            //ViewBag.Applications = applications;
            return Json(applications);
        }


        [Authorize()]
        public async Task<IActionResult> CreditScore(string token)
        {
            ApplicationUser employeeMaster = await _userManager.FindByNameAsync(User.Identity.Name);
            employeeMaster.EmployeeName = employeeMaster.EmployeeName;
            //ViewBag.Location_lat = String.Format("{0},{1}",lat,loc_long);
            ViewBag.Token = token;
            List<string> list = new List<string>();
            var brInfo = _database.Branches.Find(employeeMaster.BranchId);
            if (brInfo.BrType == "Branch")
            {
                list.Add(brInfo.Id);
            }
            else if (brInfo.BrType == "AMH")
            {
                list.AddRange(_database.Branches.Where(a => a.AMHCode == employeeMaster.BranchId).Select(a => a.Id).ToList());
            }
            else
            {
                list.AddRange(_database.Branches.Where(a => a.RegionalOffice == employeeMaster.BranchId).Select(a => a.Id).ToList());
            }
            var applications = _database.Applications.Where(a => a.Status == "Pending" && list.Contains(a.BranchId)).ToList();
            ViewBag.Applications = applications;
            ViewBag.PostBack = false;
            return View(employeeMaster);
        }
        [HttpPost()]

        public async Task<IActionResult> CreditScore(string token,int ApplicationId)
        {
            ViewBag.Token = token;
            ViewBag.PostBack = true;
            ApplicationUser employeeMaster = await _userManager.FindByNameAsync(User.Identity.Name);
            employeeMaster.EmployeeName = employeeMaster.EmployeeName;
            var applications = _database.Applications.Include(a=> a.Applicants).Where(a => a.Id == ApplicationId).ToList();
            List<string> scores = new List<string>();
            List<int> scoreIds = new List<int>();
            foreach (var a in applications.FirstOrDefault().Applicants)
            {
                var checkId = a.Id;
                var cibilReqId = _database.CIBILRequests.Where(b => b.ApplicantId == checkId).ToList();
                if (cibilReqId.Count > 0)
                {
                    scoreIds.Add(cibilReqId.Last().Id);
                    scores.Add(cibilReqId.Last().ScoreName1);
                }
                else
                {
                    scoreIds.Add(0);
                    scores.Add("NA");
                }
            }
            ViewBag.Scores = scores;
            ViewBag.Scoresa = scoreIds;
            ViewBag.Applications = applications;
            return View(employeeMaster);
        }
        [HttpPost]
        public ActionResult AddNewInspection(PreInspection g, string token)
        {
            PreInspection visit = new PreInspection();
            g.CopyAllTo(visit);
            visit.SystemLogDate = DateTime.Now;
            _database.PreInspection.Add(visit);
            _database.SaveChanges();
            return RedirectToAction("InspectionStart", new { VisitId = visit.Id, token = token });
        }
        [HttpPost]
        public ActionResult AddNewPostInspectionApi (PostInspection g)
        {
            PostInspection visit = new PostInspection();
            g.CopyAllTo(visit);
            visit.UserId = User.Identity.Name;
            visit.SystemLogDate = DateTime.Now;
            _database.PostInspection.Add(visit);
            _database.SaveChanges();
            return Json(visit.Id);
        }

        [HttpPost]
        public ActionResult AddNewInspectionApi(PreInspection g)
        {
            PreInspection visit = new PreInspection();
            g.CopyAllTo(visit);
            visit.UserId = User.Identity.Name;
            visit.SystemLogDate = DateTime.Now;
            _database.PreInspection.Add(visit);
            _database.SaveChanges();
            return Json(visit.Id);
        }

        public async Task<IActionResult> AddNew(string token , string BCId, string lat, string loc_long)
        {
            ApplicationUser employeeMaster = await getEmp(token);
            CSPVisit CspVisit = new CSPVisit();
            CspVisit.UserId = employeeMaster.Id;
            CspVisit.CSPId = BCId;
            CspVisit.Status = "Pending";
            CspVisit.VisitDate = DateTime.Now;
            CspVisit.VisitRemarks = token;
            CspVisit.Location_lat = lat;
            CspVisit.Location_long = loc_long;
            return View(CspVisit);
        }
        [HttpPost]
        public ActionResult AddNew(CSPVisit g,string token, string lat, string loc_long)
        {
            CSPVisit visit = new CSPVisit();
            visit.UserId = g.UserId;
            visit.VisitDate = g.VisitDate;
            visit.SystemLogDate = DateTime.Now;
            visit.Status = "Pending";
            visit.CSPId = g.CSPId;
            visit.Location_lat = lat;
            visit.Location_long = loc_long;
            _database.CSPVisits.Add(visit);
            _database.SaveChanges();
            return RedirectToAction("VisitStart", new {VisitId = visit.Id , token = token});
        }
        public ActionResult VisitStart(int VisitId , string token)
        {
            var ValueStatements = _database.CSPValueStatements.ToList();
            ViewBag.Data = ValueStatements;
            ViewBag.VisitId = VisitId;
            ViewBag.Token = token;
            return View();
        }

        private List<DropDownValues> createDD()
        {
            var x = new List<DropDownValues>();
            x.Add(new DropDownValues { TypeOfValue= "Type of Organization", Value="Central Govt. Office / Site" });
            x.Add(new DropDownValues { TypeOfValue = "Type of Organization", Value = "State Govt. Office / Site" });
            x.Add(new DropDownValues { TypeOfValue = "Type of Organization", Value = "PSU Office / Site" });
            x.Add(new DropDownValues { TypeOfValue = "Type of Organization", Value = "Own Business place (Shop/Office)" });
            x.Add(new DropDownValues { TypeOfValue = "Type of Organization", Value = "Rented Business place (Shop/Office)" });
            x.Add(new DropDownValues { TypeOfValue = "Type of Ownership", Value = "Self Owned" });
            x.Add(new DropDownValues { TypeOfValue = "Type of Ownership", Value = "Rented" });
            x.Add(new DropDownValues { TypeOfValue = "Type of Ownership", Value = "Parental" });
            x.Add(new DropDownValues { TypeOfValue = "Type of Security", Value = "Flat" });
            x.Add(new DropDownValues { TypeOfValue = "Type of Security", Value = "Residential Building" });
            x.Add(new DropDownValues { TypeOfValue = "Type of Security", Value = "Residential Plot (Bounded)" });
            x.Add(new DropDownValues { TypeOfValue = "Type of Security", Value = "Commercial Building" });
            x.Add(new DropDownValues { TypeOfValue = "Type of Security", Value = "Commercial Plot (Bounded)" });
            x.Add(new DropDownValues { TypeOfValue = "Type of Security", Value = "Open Land" });

            return x;
        }
        public ActionResult InspectionStart(int VisitId, string token)
        {
            var visitType = _database.PreInspection.Find(VisitId).Status;
            var ValueStatements = _database.PreInspectionValueStatements.Where(a=> a.Status == visitType).ToList();
            ViewBag.Data = ValueStatements;
            ViewBag.VisitId = VisitId;
            ViewBag.Token = token;
            ViewBag.DropDowns = createDD();
            return View();
        }
        [Authorize]
        public ActionResult InspectionStartApi(int VisitId)
        {
            var visitType = _database.PreInspection.Find(VisitId).Status;
            var ValueStatements = _database.PreInspectionValueStatements.Where(a => a.Status == visitType).ToList();
             
            return Json(ValueStatements);
        }

        [Authorize]
        public ActionResult PostInspectionStartApi(int VisitId)
        {
            var visitType = _database.PostInspection.Find(VisitId).Status;
            var ValueStatements = _database.PreInspectionValueStatements.Where(a => a.Status == visitType).ToList();

            return Json(ValueStatements);
        }

        [Authorize]
        [HttpPost]
        public ActionResult PostInspectionStartApi(int VisitId, IFormCollection formCollection)
        {
            Console.WriteLine($"{VisitId} | {formCollection.Count}");
            var v = _database.PreInspection.Find(VisitId);
            try
            {
                foreach (var item in formCollection)
                {
                    int key = 0;
                    int.TryParse(item.Key, out key);
                    if (key > 0)
                    {
                        PostInspectionVisitRemark visit = new PostInspectionVisitRemark();
                        visit.VisitId = VisitId;
                        visit.RemarksVisitingOfficial = item.Value;
                        visit.ValueStatementId = int.Parse(item.Key);
                        visit.Status = "Entered";
                        visit.RemarksVerifyingOfficial = "";
                        _database.PostInspectionVisitRemark.Add(visit);


                    }
                }
                foreach (var item in formCollection.Files)
                {

                    var uniqueFileName = GetUniqueFileName(item.FileName);
                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploadedImages");
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    item.CopyTo(new FileStream(filePath, FileMode.Create));
                    MemoryStream memoryStream = new MemoryStream();
                    item.CopyTo(memoryStream);
                    byte[] data = memoryStream.ToArray();
                    PostInspectionVisitRemark visit = new PostInspectionVisitRemark();
                    visit.VisitId = VisitId;
                    visit.RemarksVisitingOfficial = Convert.ToBase64String(data);
                    visit.ValueStatementId = int.Parse(item.Name);
                    visit.Status = "File";
                    visit.RemarksVerifyingOfficial = uniqueFileName;
                    _database.PostInspectionVisitRemark.Add(visit);
                }
                _database.SaveChanges();
                ViewBag.Result = "OK";
                return Json("Ok");
            }
            catch (Exception ex)
            {
                return Json($"Fail {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult InspectionStartApi(int VisitId, IFormCollection formCollection)
        {
            Console.WriteLine($"{VisitId} | {formCollection.Count}");
            var v = _database.PreInspection.Find(VisitId);
            try
            {
                foreach (var item in formCollection)
                {
                    int key = 0;
                    int.TryParse(item.Key, out key);
                    if (key > 0)
                    {
                        PreVisitRemark visit = new PreVisitRemark();
                        visit.VisitId = VisitId;
                        visit.RemarksVisitingOfficial = item.Value;
                        visit.ValueStatementId = int.Parse(item.Key);
                        visit.Status = "Entered";
                        visit.RemarksVerifyingOfficial = "";
                        _database.PreIsnpectionRemarks.Add(visit);


                    }
                }
                foreach (var item in formCollection.Files)
                {

                    var uniqueFileName = GetUniqueFileName(item.FileName);
                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploadedImages");
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    item.CopyTo(new FileStream(filePath, FileMode.Create));
                    MemoryStream memoryStream = new MemoryStream();
                    item.CopyTo(memoryStream);
                    byte[] data = memoryStream.ToArray();
                    PreVisitRemark visit = new PreVisitRemark();
                    visit.VisitId = VisitId;
                    visit.RemarksVisitingOfficial = Convert.ToBase64String(data);
                    visit.ValueStatementId = int.Parse(item.Name);
                    visit.Status = "File";
                    visit.RemarksVerifyingOfficial = uniqueFileName;
                    _database.PreIsnpectionRemarks.Add(visit);
                }
                _database.SaveChanges();
                ViewBag.Result = "OK";
                return Json("Ok");
            }
            catch (Exception ex)
            {
                return Json($"Fail {ex.Message}");
            }
        }
        [HttpPost]
        public IActionResult InspectionStart(int VisitId, IFormCollection formCollection, string token)
        {
            var v = _database.PreInspection.Find(VisitId);
            try
            {

                


                foreach (var item in formCollection)
                {
                    int key = 0;
                    int.TryParse(item.Key, out key);
                    if (key > 0)
                    {
                        PreVisitRemark visit = new PreVisitRemark();
                        visit.VisitId = VisitId;
                        visit.RemarksVisitingOfficial = item.Value;
                        visit.ValueStatementId = int.Parse(item.Key);
                        visit.Status = "Entered";
                        visit.RemarksVerifyingOfficial = "";
                        _database.PreIsnpectionRemarks.Add(visit);


                    }


                }
                foreach (var item in formCollection.Files)
                {

                    var uniqueFileName = GetUniqueFileName(item.FileName);
                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploadedImages");
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    item.CopyTo(new FileStream(filePath, FileMode.Create));
                    MemoryStream memoryStream = new MemoryStream();
                    item.CopyTo(memoryStream);
                    byte[] data = memoryStream.ToArray();
                    PreVisitRemark visit = new PreVisitRemark();
                    visit.VisitId = VisitId;
                    visit.RemarksVisitingOfficial = Convert.ToBase64String(data);
                    visit.ValueStatementId = int.Parse(item.Name);
                    visit.Status = "File";
                    visit.RemarksVerifyingOfficial = uniqueFileName;
                    _database.PreIsnpectionRemarks.Add(visit);

                }
                _database.SaveChanges();
                ViewBag.Result = "OK";
                if (v.ClosureOfficialId.Contains("local"))
                {
                    return RedirectToAction("Application", "LoanFlow", new {Id =  _protector.Decode(v.ApplicationId.ToString()) });
                }
                return View("Result");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                ViewBag.Result = "BAD";
                if (v.ClosureOfficialId.Contains("local"))
                {
                    return RedirectToAction("Application", "LoanFlow", new { Id = _protector.Decode(v.ApplicationId.ToString()) });
                }
                return View("Result");
            }

        }
        public ActionResult VisitList()
        {
            var visits1 = _database.CSPVisits.Where(a=> a.Status == "Pending").OrderByDescending(a=> a.VisitDate).ToList();
            //List<CSPVisitView> visits = new List<CSPVisitView>();
            //foreach (var v in visits1)
            //{
            //    CSPVisitView x = new CSPVisitView();
            //    v.CopyAllTo(x);
            //    x.EmployeeDetails = _database.EmployeeMasters.Find(v.UserId);
            //    x.BCDetails = _database.Csps.Find(v.CSPId);
            //    visits.Add(x);
            //}
            return View(visits1);
        }
        public ActionResult ReportPreInspection(int Id)
        {
            var visit = _database.PreInspection.Find(Id);
            var visitReport = _database.PreIsnpectionRemarks.Where(a => a.VisitId == Id).ToList();
            var empl = _database.Users.Find(visit.UserId);
            
             
            return View(visit);
        }
        public ActionResult ReportCSP(int Id)
        {
            var visit = _database.CSPVisits.Find(Id);
            
            return View(visit);
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
        [HttpPost]
        public IActionResult VisitStart(int VisitId , IFormCollection formCollection , string token)
        {
            try {
                
                
            
                foreach (var item in formCollection)
                {
                    int key = 0;
                    int.TryParse(item.Key,out key);
                    if (key > 0)
                    {
                        VisitRemark visit = new VisitRemark();
                        visit.VisitId = VisitId;
                        visit.RemarksVisitingOfficial = item.Value;
                        visit.ValueStatementId = int.Parse(item.Key);
                        visit.Status = "Entered";
                        visit.RemarksVerifyingOfficial = "";
                        _database.VisitRemarks.Add(visit);
                       
                        
                    }
                   

                }
                foreach (var item in formCollection.Files)
                {

                    var uniqueFileName = GetUniqueFileName(item.FileName);
                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploadedImages");
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    item.CopyTo(new FileStream(filePath, FileMode.Create));
                    MemoryStream stream = new MemoryStream();
                    item.CopyTo(stream);
                    byte[] bytes = stream.ToArray();

                    VisitRemark visit = new VisitRemark();
                    visit.VisitId = VisitId;
                    visit.RemarksVisitingOfficial = Convert.ToBase64String(bytes);
                    visit.ValueStatementId = int.Parse(item.Name);
                    visit.Status = "File";
                    visit.RemarksVerifyingOfficial = uniqueFileName;
                    _database.VisitRemarks.Add(visit);
                    
                }
                _database.SaveChanges();
                ViewBag.Result = "OK";
                return View("Result");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;   
                ViewBag.Result = "BAD";
               return View("Result");
            }
            
        }
        public IActionResult GetBranch(string d)
        {
            var g = _database.Branches.Where(a => a.District == d).ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in g)
            {
                items.Add(new SelectListItem { Text = item.BranchName, Value = item.Id });
            }
            return Json(items);
        }
       
       
        

        
        public IActionResult Index()
        {
           
            return View();
        }
        [HttpPost()]
        public IActionResult BranchLocator(string query)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
