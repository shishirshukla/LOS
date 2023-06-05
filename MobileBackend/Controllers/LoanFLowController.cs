using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MobileBackend.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MobileBackend.Controllers
{
    public class LoanFlowController : Controller
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public readonly CustomIDataProtection _protector;
        public LoanFlowController(IWebHostEnvironment appEnvironment, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ApplicationDbContext context, CustomIDataProtection protector)
        {
            _appEnvironment = appEnvironment;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
            _protector = protector;
        }
        public string DecryptString(string encyptedValue)
        {


           // byte[] iv = new "5183666c72eec9e4".ToBy;
            byte[] buffer = Convert.FromBase64String(encyptedValue);
            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Key = Encoding.UTF8.GetBytes("crgba5898a4e4133bbce2ea2315a1916");
                aes.IV = Encoding.UTF8.GetBytes("5183666c72eec9e4"); 
                //aes.IV = Encoding.UTF8.GetBytes("5183666c72eec9e4");
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sw = new StreamReader(cs))
                        {
                            return sw.ReadToEnd();
                        }
                    }
                }
            }
        }
        public string EncryptString(string stringValue)
        {


            //-------------AES256--------------
            //$iv -- '5183666c72eec9e4';
            //$key -- 'crgba5898a4e4133bbce2ea2315a1916';
            //$text -- '4729388';
            //Result -- 8XjiUYBjJwMtk3JmNqUNoA==
            //Myresult -- LFGijVei/dCorleTge9ymA==
            //LFGijVei/dCorleTge9ymA==
            //---------------------------------


            byte[] iv = new byte[16];
            byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Key = Encoding.UTF8.GetBytes("crgba5898a4e4133bbce2ea2315a1916");
                aes.IV = Encoding.UTF8.GetBytes("5183666c72eec9e4"); 
                //aes.IV = Encoding.UTF8.GetBytes("5183666c72eec9e4");
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(stringValue);
                        }
                        array = ms.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(array);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserApi(string username , string empname , string scale , string br )
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = username;
            user.EmployeeName = empname;
            user.Designation = scale;
            user.BranchId = br;
            user.Role = "Staff";
            var c = await _userManager.CreateAsync(user, "admin@123");
            if (c.Succeeded)
            {
                return Ok("success");
            }
            return Ok("fail");
        }



        [HttpPost]
        public async Task<IActionResult> ResetPasswordApi(string username)
        {
            var uid = DecryptString(username);
            
            ApiUser apiUser = new ApiUser();
            var u = await _userManager.FindByNameAsync(uid);
            if (u == null)
            {
                apiUser.Status = "False";
                apiUser.UserId = uid;
                return Ok(apiUser);
            }
            var t = await _userManager.GeneratePasswordResetTokenAsync(u);
            await _userManager.ResetPasswordAsync(u, t, "admin@123");
            apiUser.Status = "True";
            apiUser.UserId = uid;
           
            return Ok(apiUser);
        }




        [HttpPost]
        public async Task<IActionResult>  LoginByApi(string username , string pwd)
        {
            var uid = DecryptString(username);
            var p = DecryptString(pwd);
            ApiUser apiUser = new ApiUser();
            var u = await _userManager.FindByNameAsync(uid);
            if (u == null)
            {
                apiUser.Status = "False";
                apiUser.UserId = uid;
                return Ok(apiUser);
            }
            var s = await _userManager.CheckPasswordAsync(u,p);
            if (s == true)
            {
                apiUser.UserId = uid;
                apiUser.Status = "True";
                apiUser.EmpName = u.EmployeeName;
                apiUser.Branch = u.BranchId;
                apiUser.Designation = u.Designation;
            }
            else
            {
                apiUser.Status = "False";
                apiUser.UserId = uid;
            }
            return Ok(apiUser);
        }


        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        public IActionResult KCCRenewal()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> KCCRenewal(KCCRenewal r )
        {
            CustomerMaster c = new CustomerMaster();
            HttpClient httpClient = new HttpClient();
            var d = await httpClient.GetAsync($"http://10.80.1.135/Mobile/SearchCustomer?acc={r.AcNumber}");
            if (d.IsSuccessStatusCode)
            {
                try
                {
                    var k = await d.Content.ReadAsStringAsync();
                    c = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerMaster>(k);
                }
                catch (Exception)
                {

                    return View();
                }
                
            }
            // var c = _context.AccountData.FromSql("select")
            var ac = _context.AccountData.FromSqlRaw($"SELECT account_no,  acctopendate, currentbalance,  odlimit,  accounttype, interestcat, intrate,name,cust_name 	FROM public.temp_loan Where account_no = LPAD('{r.AcNumber}',17,'0')").FirstOrDefault();
            Application app =new Application();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var brInfo = _context.Branches.Find(user.BranchId);
            app.ApplicantShortName = ac.cust_name;
            app.LoanScheme = r.Product;
            app.AppliedAmt = r.AppliedAmt;
            app.BranchId = user.BranchId;
            app.ApplicationDate = app.ApplicationDate;
            app.Status = "Pending";
            app.Purpose = "KCC Renewal";
            app.Owner = brInfo.BrType;
            app.OwnerUser = user.UserName;
            app.MappedCCAccount = r.AcNumber;
            app.ApplicationDate = DateTime.Now;
            _context.Applications.Add(app);
            _context.SaveChanges();
            r.ApplicationId = app.Id;
            _context.KCCRenewal.Add(r);
            _context.SaveChanges();
            

            //CustomerMaster c = new CustomerMaster();

            Applicant applicant = new Applicant();
            applicant.ApplicationId = app.Id;
            applicant.Applicant_First_Name = c.FirstName;
            applicant.Applicant_Middle_Name = c.MiddleName;
            applicant.Applicant_Last_Name = c.LastName;
            applicant.MobileNumber = c.Mobile;
            applicant.PAN_No = c.PAN;
            applicant.Adhaar_No  =c.Adhaar;
            applicant.Date_Of_Birth = c.DOB.ToShortDateString();
            applicant.PINCode = c.Pincode;
            applicant.Address_Line1 = c.Address1;
            _context.Applicants.Add(applicant);
            _context.SaveChanges();
            AddLog(app.Id, user.UserName, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Add Application");
            return RedirectToAction("Application", new { Id = _protector.Decode(app.Id.ToString()) });
            return View();
        }


        [Authorize]
        public async Task<IActionResult> SendApplication(int id, string ViewType = "Normal")
        {
            
            var app = _context.Applications.Find(id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var brInfo = _context.Branches.Find(user.BranchId);
            List<Branch> list = new List<Branch>();
            ViewBag.ApplicationId = id ;
            if (brInfo.BrType == "Branch")
            {
                list.AddRange(_context.Branches.Where(a => a.RegionalOffice == brInfo.RegionalOffice && (a.BrType == "Branch" || a.BrType == "AMH")).ToList());
            }
            else if (brInfo.BrType == "AMH")
            {
                list.AddRange(_context.Branches.Where(a => a.RegionalOffice == brInfo.RegionalOffice && (a.BrType == "Branch" || a.BrType == "AMH" || a.BrType == "RO")).ToList());
            }
            else if (brInfo.BrType == "HO")
            {
                list.AddRange(_context.Branches.ToList());
            }
            else
            {
                list.AddRange(_context.Branches.Where(a => a.RegionalOffice == user.BranchDetails.RegionalOffice).ToList());
                list.AddRange(_context.Branches.Where(a => a.BrType == "HO").ToList());
            }
            ViewBag.BranchList = list;
            ViewBag.ViewType = ViewType;
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SendApplication(Remark rem , string BranchId , string UserName)
        {
           
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var app = _context.Applications.Find(rem.ApplicationId);
            if (UserName == user.UserName)
            {
                return RedirectToAction("Application", new { Id = _protector.Decode(app.Id.ToString()) });
            }
            rem.UserId = User.Identity.Name;
            rem.EmpName = user.EmployeeName;
            rem.Designation = user.Designation;
            rem.DataType = "Movement";
            rem.SenderUserId = user.UserName;
           
            if (rem.TypeOfRemark == "Approve Deviation")
            {
                
                rem.TypeOfRecomendation = $"Deviation Approved by {user.EmployeeName} ({user.UserName}) To {UserName}  at {DateTime.Now.ToLongDateString()} ";
            }
            else if (rem.TypeOfRemark == "Reject Deviation")
            {
                rem.TypeOfRecomendation = $"Deviation Rejected by {user.EmployeeName} ({user.UserName}) To {UserName}  at {DateTime.Now.ToLongDateString()}";
            }
            else if (rem.TypeOfRemark == "Return")
            {
                rem.TypeOfRecomendation = $"Application Returned by {user.EmployeeName} ({user.UserName}) To {UserName}  at {DateTime.Now.ToLongDateString()}";
            }
            else
            {
                rem.TypeOfRecomendation = $"Application Forwarded by {user.EmployeeName} ({user.UserName}) To {UserName} {rem.TypeOfRemark} at {DateTime.Now.ToLongDateString()}";
            }
            
            rem.Timestamp = DateTime.Now;
           
            _context.Remarks.Add(rem);
            
            var br = _context.Branches.Find(BranchId);
            if (br.BrType == "Branch")
            {
                app.BranchId = BranchId;
            }
            if (rem.TypeOfRemark == "For Deviation (Int Rate)" || rem.TypeOfRemark == "For Deviation (Scheme)")
            {
                app.PendingAction = "Deviation";
            }
            else
            {
                app.ForwardedDate = DateTime.Now;
                app.PendingAction = "Sanction";
            }
            app.Owner = br.BrType;
            app.OwnerUser = UserName;
            _context.Entry(app).State = EntityState.Modified;
            _context.SaveChanges();
            if (rem.TypeOfRemark == "Return")
            {
                if (app.Owner != "Branch")
                {
                    app.Owner = br.BrType;
                    app.Status = "Pending";
                    _context.Entry(app).State = EntityState.Modified;
                    _context.SaveChanges();

                    AddLog(app.Id, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", rem.TypeOfRemark);
                }
                
            }
            AddLog(rem.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Added / Transfered Application");
            return RedirectToAction("Application", new { Id = _protector.Decode(app.Id.ToString()) });
            //return View();

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

        private bool AddLog(int ApplicationId , string UserName , string Ip , string Mode , string Action)
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

        private IActionResult GetDetails(string account, Single amt)
        {
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection())
            {
                conn.ConnectionString = _configuration.GetConnectionString("ConnStr1");
                conn.Open();
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = $"SELECT \"uploadFile\" 	FROM public.\"BGLoanInfo\" Where \"Account_No\" ='{account}' and \"Amount\" = {amt}";
                    var z = cmd.ExecuteReader();
                    if (z.Read())
                    {
                        byte[] result = (byte[])z[0];
                        var filePath = Path.Join(_appEnvironment.WebRootPath, $"{account}.pdf");
                        System.IO.File.WriteAllBytes(filePath, result);
                        return Content(filePath);
                    }
                    else
                    {
                        return Content("");
                    }

                }
            }

        }


        [Authorize]
        public async Task<IActionResult> DeleteApplicant(int Id)
        {
            var applicant = _context.Applicants.Find(Id);
            if (await Check(applicant.ApplicationId) )
            {
                applicant.Driving_Lic = "Deleted";
                _context.Entry(applicant).State = EntityState.Modified;
                _context.SaveChanges(true);
                var cc = _context.CIBILRequests.Where(a=> a.ApplicantId == Id).ToList();
                foreach (var c in cc)
                {
                    c.CiibilControlNumber = "Deleted";
                    _context.Entry(c).State = EntityState.Modified;
                    _context.SaveChanges(true);

                }
                AddLog(applicant.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Delete Applicant : Id :" + applicant.Id.ToString());
               
            }
            return RedirectToAction("Application", new { Id = _protector.Decode(applicant.ApplicationId.ToString()) });
        }
        [Authorize]
        public async Task<IActionResult> DeleteCharge(int Id)
        {
            var applicant = _context.Charges.Find(Id);
            if (await Check(applicant.ApplicationId))
            {
                _context.Entry(applicant).State = EntityState.Deleted;
                _context.SaveChanges(true);
                AddLog(applicant.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Delete Charge ");

            }
            return RedirectToAction("Application", new { Id = _protector.Decode(applicant.ApplicationId.ToString()) });
        }
        [Authorize]
        public async Task<IActionResult> DeleteSecurity(int Id)
        {
            var applicant = _context.Securities.Find(Id);
            if (await Check(applicant.ApplicationId))
            {
                _context.Entry(applicant).State = EntityState.Deleted;
                _context.SaveChanges(true);
                AddLog(applicant.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Delete Security ");

            }
            return RedirectToAction("Application", new { Id = _protector.Decode(applicant.ApplicationId.ToString()) });
        }
        [Authorize]
        public async Task<IActionResult> DeleteDisb(int Id)
        {
            var applicant = _context.Disbursements.Find(Id);
            if (await Check(applicant.ApplicationId))
            {
                _context.Entry(applicant).State = EntityState.Deleted;
                _context.SaveChanges(true);
                AddLog(applicant.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Delete Disbursement");

            }
            return RedirectToAction("Application", new { Id = _protector.Decode(applicant.ApplicationId.ToString()) });
        }
        [Authorize]
        public async Task<IActionResult> DeleteApplication(int Id)
        {
            var applicant = _context.LoanApplications.Find(Id);
            if (await Check(applicant.ApplicationId))
            {
                _context.Entry(applicant).State = EntityState.Deleted;
                _context.SaveChanges(true);
                AddLog(applicant.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Delete Loan Applied Amount : Id :" + applicant.Id.ToString());

            }
            return RedirectToAction("Application", new { Id = _protector.Decode(applicant.ApplicationId.ToString()) });
        }
        [Authorize]
        public async Task<IActionResult> DeleteProjectCost(int Id)
        {
            var applicant = _context.ProjectCosts.Find(Id);
            if (await Check(applicant.ApplicationId))
            {
                _context.Entry(applicant).State = EntityState.Deleted;
                _context.SaveChanges(true);
                AddLog(applicant.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Delete Project Cost : Id :" + applicant.Id.ToString());

            }
            return RedirectToAction("Application", new { Id = _protector.Decode(applicant.ApplicationId.ToString()) });
        }
        [Authorize]
        public async Task<IActionResult> DeleteKCCLand(int Id)
        {
            var applicant = _context.KCCLandDetails.Find(Id);
            if (await Check(applicant.ApplicationId))
            {
                _context.Entry(applicant).State = EntityState.Deleted;
                _context.SaveChanges(true);
                AddLog(applicant.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Delete KCC Land : Khasra :" + applicant.KhasraNo.ToString());

            }
            return RedirectToAction("Application", new { Id = _protector.Decode(applicant.ApplicationId.ToString()) });
        }

        [Authorize]
        public async Task<IActionResult> DeleteExistingLand(int Id)
        {
            var applicant = _context.KCCExistingLand.Find(Id);
            
                _context.Entry(applicant).State = EntityState.Deleted;
                _context.SaveChanges(true);
                //AddLog(applicant.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Delete KCC Land : Khasra :" + applicant.KhasraNo.ToString());

             
            return RedirectToAction("ExistingKCC","LoanFlow");
        }

        [Authorize]
        public async Task<IActionResult> DeleteKCCCrop(int Id)
        {
            var applicant = _context.KCCCropDetails.Find(Id);
            if (await Check(applicant.ApplicationId))
            {
                _context.Entry(applicant).State = EntityState.Deleted;
                _context.SaveChanges(true);
                AddLog(applicant.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Delete KCC Crop : Id :" + applicant.Crop.ToString());

            }
            return RedirectToAction("Application", new { Id = _protector.Decode(applicant.ApplicationId.ToString()) });
        }
        [Authorize]
        public async Task<IActionResult> DeleteDocument(int Id)
        {
            var applicant = _context.Documents.Find(Id);
            if (await Check(applicant.ApplicationId))
            {
                _context.Entry(applicant).State = EntityState.Deleted;
                _context.SaveChanges(true);
                AddLog(applicant.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Delete Document : Id :" + applicant.Id.ToString());
            }
                
            return RedirectToAction("Application", new { Id = _protector.Decode(applicant.ApplicationId.ToString()) });
        }
        [Authorize]
        public async Task<IActionResult> DeleteRemark(int Id)
        {
            var applicant = _context.Remarks.Find(Id);
            if (await Check(applicant.ApplicationId))
            {
                _context.Entry(applicant).State = EntityState.Deleted;
                _context.SaveChanges(true);
                AddLog(applicant.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Delete Remark : Id :" + applicant.Id.ToString());
            }

            return RedirectToAction("Application", new { Id = _protector.Decode(applicant.ApplicationId.ToString()) });
        }
        [Authorize]
        public async Task<IActionResult> DeleteInspection(int Id)
        {
            var applicant = _context.PreInspection.Find(Id);
            if (await Check(applicant.ApplicationId))
            {
                _context.Entry(applicant).State = EntityState.Deleted;
                _context.SaveChanges(true);
                AddLog(applicant.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Delete Inspection : Id :" + applicant.Id.ToString());
            }

            return RedirectToAction("Application", new { Id = _protector.Decode(applicant.ApplicationId.ToString()) });
        }
        [Authorize]
        public IActionResult ManualInspection()
        {
            var token = HttpContext.Session.GetString("Token");
            var lat = "local";
            var longi = "local";
            return RedirectToAction("AddNewInspection","Visit", new { token = token , lat = lat , loc_long = longi });
        }
        [Authorize]
        public IActionResult CopyApplicant(int Id,int AppId)
        {
            var applicant = _context.Applicants.Find(Id);
            applicant.Id = 0;
            applicant.ApplicationId = AppId;
            applicant.Driving_Lic = "";
            _context.Applicants.Add(applicant);
            _context.SaveChanges(true);
          

            AddLog(applicant.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Copy Applicant : Id :" + applicant.Id.ToString());
            return RedirectToAction("Application", new { Id = _protector.Decode(AppId.ToString()) });
        }
        [Authorize]
        public IActionResult SearchApplicant(int Id)
        {
            ViewBag.ApplicationId = Id;
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult SearchApplicant(int ApplicationId, string idType , string IdNumber)
        {
            List<Applicant> applicants = new List<Applicant>();
            if (idType == "PAN")
            {
                applicants = _context.Applicants.Where(a=> a.PAN_No == IdNumber).ToList();
            }
            else if (idType == "Adhar")
            {
                applicants = _context.Applicants.Where(a => a.Adhaar_No == IdNumber).ToList();
            }
            else if (idType == "Voter")
            {
                applicants = _context.Applicants.Where(a => a.Voter_Id == IdNumber).ToList();
            }
            else if (idType == "Mobile")
            {
                applicants = _context.Applicants.Where(a => a.MobileNumber == IdNumber).ToList();
            }
            else if (idType == "Name")
            {
                applicants = _context.Applicants.Where(a => a.Applicant_First_Name.Contains(IdNumber)) .ToList();
            }
            ViewBag.ApplicantId = ApplicationId;
            return View("SearchResult",applicants);
        }

        [Authorize]
        public IActionResult AddApplicant(int Id)
        {
            ViewBag.ApplicationId = Id;  
            return View();
        }

        [Authorize]
        public IActionResult TPL(int Id)
        {
            ViewBag.ApplicationId = Id;
            return View();
        }

        [Authorize]
        public IActionResult AddKCCLand(int Id)
        {
            ViewBag.ApplicationId = Id;
            var dist1 = _context.KeyValues.FromSqlRaw($"SELECT Distinct dist_value as code, dist_name as value  FROM loanflow.villagemaster;").ToList();
            List<Tuple<string, string>> dists = new List<Tuple<string, string>>();
            foreach (var dist in dist1) {
                dists.Add(new Tuple<string, string>(dist.code,dist.value));
            }
            ViewBag.District = dists;
            return View();
        }
        [Authorize]
        public IActionResult UpdateKCCLand(int Id)
        {
            ViewBag.ApplicationId = Id;
            var kl = _context.KCCLandDetails.Where(a => a.ApplicationId == Id).ToList();
            return View(kl);
        }
        [Authorize]
        [HttpPost]
        public IActionResult UpdateKCCLand(KCCLandDetail kcc)
        {
            var l = _context.KCCLandDetails.Find(kcc.Id);
            l.IrrigatedArea = kcc.IrrigatedArea;
            l.UnIrrigatedArea = kcc.UnIrrigatedArea;
            l.NonAgriArea = kcc.NonAgriArea;
            l.SourceofIrrigation = kcc.SourceofIrrigation;
            _context.Entry(l).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }
        [Authorize]
        public IActionResult ExistingKCC(string msg = "")
        {
            ViewBag.Message = msg;
            return View();
        }
        [Authorize]
        public IActionResult GetFBTehsil(string dd)
        {

            var report_amh = _context.KeyValues.FromSqlRaw($"SELECT Distinct lvl4code  as code, lvl4name as value FROM loanflow.district_master_lvl where dist_code = '{dd}' ").ToList();

            return View("KeyValue",report_amh);
        }
        [Authorize]
        public IActionResult GetFBVillage(string dd , string teh)
        {

            var report_amh = _context.KeyValues.FromSqlRaw($"SELECT  Distinct lvl7code  as code, lvl7name as value FROM loanflow.district_master_lvl where dist_code = '{dd}' and lvl4code = '{teh}'").ToList();

            return View("KeyValue", report_amh);
        }
        [Authorize]
        public IActionResult UpdateKCCLandAccount(string Id)
        {
            ViewBag.ApplicationId = Id;
            var kl = _context.KCCExistingLand.Where(a => a.AccountNo == Id).ToList();
            return View(kl );
        }

        [Authorize]
        public async Task<IActionResult> MapLand()
        {
            var u = await _userManager.FindByNameAsync(User.Identity.Name);
            var s = u.BranchId;
           
            var report_amh = _context.VillageMaster.FromSqlRaw($"SELECT * from loanflow.kccvillage('{s}')").ToList();
             
            return View(report_amh);
        }

        [Authorize]
       // [Obsolete]
        public IActionResult AddVillageMap(VillageMasterBhuiyan vill)
        {
            var c = _context.Database.ExecuteSqlRaw($"INSERT INTO loanflow.bhuiyanfasalbima(bhuiyandist, bhuiyandistcode, fasalbimadistcode, bhuiyantehsil, bhuiyantehsilcode, fasalbimatehsilcode, bhuiyanvillage, bhuiyanvillagecode, fasalbimavillagecode) VALUES ('{vill.District}', '{vill.DistrictCode}', '{vill.Tehsil}', '{vill.TehsilCode}', '{vill.Village}', '{vill.VillageCode}', '{vill.DistrictCodeFB}', '{vill.TehsilCodeFB}', '{vill.VillageCodeFB}');");
            return Ok("Saved");
        }

        [Authorize]
        [HttpPost]
        public IActionResult UpdateKCCLandAccount(KCCExistingLand kcc)
        {
            var l = _context.KCCExistingLand.Find(kcc.Id);
            l.IrrigatedArea = kcc.IrrigatedArea;
            l.UnIrrigatedArea = kcc.UnIrrigatedArea;
            l.NonAgriArea = kcc.NonAgriArea;
            l.SourceofIrrigation = kcc.SourceofIrrigation;
            _context.Entry(l).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        [Authorize]
        public IActionResult AddCrop(int Id , string season)
        {
            ViewBag.ApplicationId = Id;
            ViewBag.Season = season;
            var lands = _context.KCCLandDetails.Where(a=> a.ApplicationId == Id).ToList();
            ViewBag.lands = lands;
            var dists = lands.Select(a=> a.District).Distinct().ToList();
            List<KeyValue> dist1 = new List<KeyValue>();
            foreach (var item in dists)
            {
                dist1.AddRange(_context.KeyValues.FromSqlRaw($"SELECT distinct crop as code, crop as value FROM loanflow.scale where district = '{item}' order by crop;").ToList());
            }
            ViewBag.crops = dist1;
            return View();
        }
        [Authorize]
        public IActionResult AddCropExisting(string Id, string season)
        {
            ViewBag.ApplicationId = Id;
            ViewBag.Season = season;
            var lands = _context.KCCExistingLand.Where(a => a.AccountNo == Id).ToList();
            ViewBag.lands = lands;
            var dists = lands.Select(a => a.District).Distinct().ToList();
            List<KeyValue> dist1 = new List<KeyValue>();
            foreach (var item in dists)
            {
                dist1.AddRange(_context.KeyValues.FromSqlRaw($"SELECT distinct \"cropCode\" as code, \"cropName\" as value FROM loanflow.crop_master  order by \"cropName\";").ToList());
            }
            ViewBag.crops = dist1;
            return View();
        }

        [Authorize]
        public IActionResult ScaleOfFinance(string dist, string crop)
        {
            var x = _context.KeyValues.FromSqlRaw($"SELECT  scaleoffinance::text as code, concat(scaleoffinance_un::text,',',cropinsurance)  as value 	FROM loanflow.scale where district = '{dist}' and crop = '{crop}' ").FirstOrDefault();
            //var s = $"Irri - {x.code} , Unirri - {x.value}";
            return Ok(x);
        }



        [Authorize]
        public IActionResult AddLoanApplication(int Id)
        {
            ViewBag.ApplicationId = Id;
            List<ProjectCost> pj = _context.ProjectCosts.Where(a => a.ApplicationId == Id).ToList();
            ViewBag.Cost = pj.Sum(a=> a.Cost);
            ViewBag.Applied = pj.Sum(a => a.AppliedAmount);
            return View();
        }
        [Authorize]
        public IActionResult AddProjectCost(int Id)
        {
            ViewBag.ApplicationId = Id;
            var applied = _context.ProjectCosts.Where(a => a.ApplicationId == Id).Sum(a => a.AppliedAmount);
            var cost = _context.ProjectCosts.Where(a => a.ApplicationId == Id && a.EligibleForLoan == "Yes").Sum(a => a.Cost);
            var loanApplied = _context.LoanApplications.Where(a => a.ApplicationId == Id && a.TypeOfFacility == "TL").Sum(a => a.AppliedAmount);
            ViewBag.Applied = applied;
            ViewBag.Cost = cost;
            ViewBag.Loan = loanApplied;
            ViewBag.Scheme = _context.Applications.Find(Id).LoanScheme;
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult AddProjectCost(ProjectCost loanApplication)
        {
            _context.ProjectCosts.Add(loanApplication);
            _context.SaveChanges();
            AddLog(loanApplication.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Add Project Cost Details ");
            return RedirectToAction("Application", new { Id = _protector.Decode(loanApplication.ApplicationId.ToString()) });

        }

        [Authorize]
        public IActionResult SplitLand(int Id)
        {
            ViewBag.ApplicationId = Id;
            var applied = _context.KCCLandDetails.Where(a => a.ApplicationId == Id).ToList();
            
            ViewBag.Applied = applied;
            
            return View();
        }

        [Authorize]
        public IActionResult SplitLandExisting(string Id)
        {
            ViewBag.ApplicationId = Id;
            var applied = _context.KCCExistingLand.Where(a => a.AccountNo == Id).ToList();

            ViewBag.Applied = applied;

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult SplitLandExisting(int landId, decimal irrigatedArea, decimal unirrigatedArea, decimal padti)
        {

            var l = _context.KCCExistingLand.Find(landId);
             
            if (l.TotalArea < padti)
            {
                return RedirectToAction("ExistingKCC", "LoanFlow");
            }
            
            l.TotalArea = l.TotalArea - padti;
            //
            _context.Entry(l).State = EntityState.Modified;
            _context.SaveChanges();

            var crop = _context.KCCCropDetailExisting.Where(a => a.KCCExistingLandId == l.Id).ToList();
            foreach (var item in crop)
            {
                _context.Entry(item).State = EntityState.Deleted;
                _context.SaveChanges();
            }


            KCCExistingLand landDetail = new KCCExistingLand();
            landDetail.District = l.District;
            landDetail.AccountNo = l.AccountNo;
            landDetail.RI = l.RI;
            landDetail.Charge = l.Charge;
            landDetail.Tehsil = l.Tehsil;
            landDetail.DharanAdhikar = l.DharanAdhikar;
            landDetail.IrrigatedArea = irrigatedArea;
            landDetail.UnIrrigatedArea = unirrigatedArea;
            landDetail.TotalArea = (irrigatedArea + unirrigatedArea + padti);
            landDetail.NonAgriArea = padti;
            landDetail.OwnerName = l.OwnerName;
            landDetail.KhasraNo = $"{l.KhasraNo.Trim()} (Split)";
            landDetail.Village = l.Village;
            landDetail.SourceofIrrigation = l.SourceofIrrigation;

            _context.KCCExistingLand.Add(landDetail);
            _context.SaveChanges();
            //AddLog(landDetail.AccountNo, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Split land details ");
            return RedirectToAction("ExistingKCC","LoanFlow" , new {msg = "Saved"});

        }


        [HttpPost]
        [Authorize]
        public IActionResult SplitLand(int landId , decimal irrigatedArea , decimal unirrigatedArea , decimal padti)
        {

           

            var l = _context.KCCLandDetails.Find(landId);

            if (l.UnIrrigatedArea < unirrigatedArea)
            {
                return RedirectToAction("Application", new { Id = _protector.Decode(l.ApplicationId.ToString()) });
            }
            if (l.IrrigatedArea < irrigatedArea)
            {
                return RedirectToAction("Application", new { Id = _protector.Decode(l.ApplicationId.ToString()) });
            }
            if (l.NonAgriArea < padti)
            {
                return RedirectToAction("Application", new { Id = _protector.Decode(l.ApplicationId.ToString()) });
            }
            l.IrrigatedArea = l.IrrigatedArea - irrigatedArea;
            l.UnIrrigatedArea = l.UnIrrigatedArea - unirrigatedArea;
            l.NonAgriArea = l.NonAgriArea - padti;
            l.TotalArea = l.NonAgriArea + l.IrrigatedArea + l.UnIrrigatedArea;
            _context.Entry(l).State= EntityState.Modified;
            _context.SaveChanges();

            var crop = _context.KCCCropDetails.Where(a => a.KCCLandDetailId == l.Id).ToList();
            foreach (var item in crop)
            {
                _context.Entry(item).State = EntityState.Deleted;
                _context.SaveChanges();
            }


            KCCLandDetail landDetail = new KCCLandDetail();
            landDetail.District = l.District;
            landDetail.ApplicationId = l.ApplicationId;
            landDetail.RI = l.RI;
            landDetail.Charge = l.Charge;
            landDetail.Tehsil = l.Tehsil;
            landDetail.DharanAdhikar = l.DharanAdhikar;
            landDetail.IrrigatedArea = irrigatedArea;
            landDetail.UnIrrigatedArea = unirrigatedArea;
            landDetail.TotalArea = (irrigatedArea + unirrigatedArea + padti);
            landDetail.NonAgriArea = padti;
            landDetail.OwnerName = l.OwnerName;
            landDetail.KhasraNo = $"{l.KhasraNo.Trim()} (Split)";
            landDetail.Village = l.Village;
            landDetail.SourceofIrrigation = l.SourceofIrrigation;

            _context.KCCLandDetails.Add(landDetail);
            _context.SaveChanges();
            AddLog(landDetail.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Split land details ");
            return RedirectToAction("Application", new { Id = _protector.Decode(landDetail.ApplicationId.ToString()) });

        }


        public IActionResult GetEMI(int Id)
        {
            var scheme = _context.Applications.Find(Id).LoanScheme;
            var la = _context.LoanApplications.Where(a => a.TypeOfFacility == "TL" && a.ApplicationId == Id).FirstOrDefault();
            var max_date = DateTime.Parse(_context.Applicants.Where(a => a.ApplicationId == Id && a.NMI > 0 && a.Driving_Lic != "Deleted" && a.TypeofApplicant != "Guarantor").Min(b => b.Driving_Lic));
            if (la != null)
            {
                double months = ((max_date.Year - DateTime.Now.Year) * 12 + (max_date.Month - DateTime.Now.Month));
                if (scheme == "PL" || scheme == "PL-Ag" )
                {
                    months = Math.Min(72, months);
                }
                if (scheme == "PL-Express" || scheme == "PL-SECL")
                {
                    months = Math.Min(60, months);
                }
                if (scheme == "HL"  )
                {
                    months = Math.Min(360, months);
                }
                if (scheme.ToLower() == "car")
                {
                    months = Math.Min(84, months);
                }

                la.Repayment = (decimal)Microsoft.VisualBasic.Financial.Pmt((double)(la.RateOfInterest / 1200), (double)months, (double)la.AppliedAmount);
                
            }
            return Ok(la.Repayment * -1);
        }



        [HttpPost]
        [Authorize]
        public IActionResult AddLoanApplication(LoanApplication  loanApplication)
        {
            

            
            _context.LoanApplications.Add(loanApplication);
            _context.SaveChanges();
            AddLog(loanApplication.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Add Loan Details ");

            return RedirectToAction("Application", new { Id = _protector.Decode(loanApplication.ApplicationId.ToString()) });

        }
        [Authorize]
        public IActionResult AddDocument(int Id)
        {
            ViewBag.ApplicationId = Id;
            return View();
        }

       
        public async Task<IActionResult> AddDocumentApp(int Id, string userId , string tk)
        {
            var x = await _userManager.FindByNameAsync(userId);
            if (x != null)
            {
                ViewBag.ApplicationId = Id;
                ViewBag.UserId = userId;
                ViewBag.token = tk;
            }
            

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDocumentApp(Document document, IFormFile KYCDocument)
        {
            var fileName = Guid.NewGuid().ToString().Replace("-", "").Substring(1, 6) + KYCDocument.FileName;
            if (KYCDocument.Length > 0)
            {

                var filePath = Path.Join(_appEnvironment.WebRootPath, fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await KYCDocument.CopyToAsync(stream);
                }
                document.FilePath = fileName;
                document.UserId = User.Identity.Name;
                document.UploadDate = DateTime.Now;
                _context.Documents.Add(document);
                _context.SaveChanges();
                ViewBag.Result = "OK";
                AddLog(document.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Mobile App", "Add Document ");
                return View("Result");
            }
            else
            {
                ViewBag.Result = "Upload Failed";
                document.FilePath = "Not Uploaded";
            }

            return View();
        }

        public IActionResult GetUsers(string br,string CheckType = "table")
        {
            var c = _context.Users.Where(a => a.BranchId == br && a.Designation != "BC").ToList();
            ViewBag.CheckType = CheckType;
            return View(c);
        }

        public IActionResult GetAgencyList(string br, int AppId,string CheckType = "table")
        {
            var app = _context.Applications.Find(AppId);
            
            var brInfo = _context.Branches.Find(app.BranchId);
            List<Branch> list = new List<Branch>();

            if (brInfo.BrType == "Branch")
            {
                list.AddRange(_context.Branches.Where(a => a.RegionalOffice == brInfo.RegionalOffice && (a.BrType == "Branch" || a.BrType == "AMH")).ToList());
            }
            else if (brInfo.BrType == "AMH")
            {
                list.AddRange(_context.Branches.Where(a => a.RegionalOffice == brInfo.RegionalOffice && (a.BrType == "Branch" || a.BrType == "AMH" || a.BrType == "RO")).ToList());
            }
            else if (brInfo.BrType == "HO")
            {
                list.AddRange(_context.Branches.ToList());
            }
            else
            {
                list.AddRange(_context.Branches.Where(a => a.RegionalOffice == brInfo.RegionalOffice).ToList());
                list.AddRange(_context.Branches.Where(a => a.BrType == "HO").ToList());
            }
            var bcList = _context.Users.Where(a => list.Select(b => b.Id).ToList().Contains(a.BranchId) && a.Designation == br).ToList();
             
            return View(bcList);
        }

        public async Task<IActionResult> ResetPassword(string Id)
        {
            var u = await _userManager.FindByNameAsync(Id);
            var token = await _userManager.GeneratePasswordResetTokenAsync(u);
            await _userManager.ResetPasswordAsync(u, token, "admin@123");
            //AddLog(Id, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Password Reset");
            return Ok("New Password is admin@123");
        }

        public async Task<IActionResult> GetCIFDetails(string Id)
        {
            Id = Id.PadLeft(17,'0');
            var cif = _context.CIFDatas.FromSqlRaw($"SELECT * from public.customer_details('{Id}')").ToList().FirstOrDefault();
            cif.cname = cif.cname.Trim();
            cif.cname = Regex.Replace(cif.cname, @"\s+", " "); ;
            cif.father_name = cif.father_name.Trim();
            cif.add1 = cif.add1.Trim();
            cif.add2 = cif.add2.Trim();
            cif.add3 = cif.add3.Trim();
            return Ok(cif);
        }

        public async Task<IActionResult> GetKCCInfo(string Id)
        {
            Id = Id.PadLeft(17, '0');
            var cif = _context.KCCInfo.FromSqlRaw($"SELECT \"beneficiaryName\", \"aadhaarNumber\", \"beneficiaryPassbookName\", mobile, dob, COALESCE(\"socialCategory\",0) \"socialCategory\", COALESCE(\"farmerCategory\",0) \"farmerCategory\", COALESCE(\"farmerType\",0) \"farmerType\", residential_vill FROM loanflow.\"Basic_Details\" Where \"ApplicationId\" = '{Id}'").ToList().FirstOrDefault();
            var vill_name = _context.KeyValues.FromSqlRaw($"SELECT lvl7code as code, lvl7name as value FROM loanflow.district_master_lvl Where lvl7code = '{cif.residential_vill}'").FirstOrDefault();
            if (vill_name != null)
            {
                cif.residential_vill = vill_name.value;
            }
            
            return Ok(cif);
        }
        public async Task<IActionResult> DeleteCrops(string a)
        {
            var x = _context.KCCCropDetailExisting.Where(b => b.AccountNo == a).ToList();
            foreach (var item in x)
            {
                _context.Entry(item).State = EntityState.Deleted;
            }
            _context.SaveChanges();
            return Ok("Deleted");
        }

        public async Task<IActionResult> GetExistingCrop(string Id)
        {
            //Id = Id.PadLeft(17, '0');
            var cif = _context.KCCCropDetailExisting.FromSqlRaw($"SELECT a.\"Id\", \"KCCExistingLandId\", a.\"AccountNo\", \"Season\",b.\"cropName\" as \"Crop\", a.\"IrrigatedArea\", a.\"UnIrrigatedArea\", \"SOFIrrigatedArea\", \"SOFUnIrrigatedArea\", c.\"KhasraNo\" as \"CropInsurance\" FROM loanflow.\"KCCCropDetailExisting\" a inner join loanflow.crop_master b on (a.\"Crop\" = b.\"cropCode\") inner join loanflow.\"KCCExistingLand\" c on (a.\"KCCExistingLandId\" = c.\"Id\")  where a.\"AccountNo\" = '{Id}' Order By \"Season\"").Include(a=> a.KCCExistingLand).ToList();
            
            return View(cif);
        }

        public async Task<IActionResult> UpdateDesignation(string Id , string desig)
        {

            var u = await _userManager.FindByNameAsync(Id);
            var user = _context.Users.Find(u.Id);
            user.Designation = desig;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok("Designation Changed");
        }
        public async Task<IActionResult> UpdateBranch(string Id, string desig)
        {

            var u = await _userManager.FindByNameAsync(Id);
            var user = _context.Users.Find(u.Id);
            user.BranchId = desig;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok("Branch Changed");
        }
        public  IActionResult UpdateBranchLoan(int Id, string desig)
        {

            
            var user = _context.Applications.Find(Id);
            user.BranchId = desig;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok("Branch Changed for Application");
        }
        public async Task<IActionResult> UpdateSchemeLoan(int Id, string desig)
        {

            var u = await _userManager.FindByNameAsync(User.Identity.Name);
            
            var user = _context.Applications.Find(Id);

            if (user.Status == "Pending" && user.BranchId == u.BranchId)
            {
                user.LoanScheme = desig;
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok("Scheme Changed for Application");
            }
            else
            {
                return Ok("Scheme cannot be changed for this Application by this user");
            }
              
           
            
        }
        public async Task<IActionResult> UpdatePassword(string Id)
        {

            var u = await _userManager.FindByNameAsync(Id);
            var user = _context.Users.Find(u.Id);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _userManager.ResetPasswordAsync(user, token, "admin@123");
            return Ok("Branch Changed");
        }

        public IActionResult UpdateAMH(string BrId, string AMHId)
        {

            var b = _context.Branches.Find(BrId);
            b.AMHCode = AMHId;
            _context.Entry(b).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok("AMH Changed");
        }



        public async Task<IActionResult> UserManagment()
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            List<string> list = new List<string>();
            var brInfo = _context.Branches.Find(user.BranchId);
            var amhList = _context.Branches.Where(a=> a.BrType == "AMH" && a.RegionalOffice == brInfo.RegionalOffice).ToList();
            ViewBag.UserInfo = $"{user.EmployeeName} ({user.UserName}) , Branch : {user.BranchId} ({brInfo.BrType})";
            if (brInfo.BrType == "Branch")
            {
                list.Add(brInfo.Id);
            }
            else if (brInfo.BrType == "AMH")
            {
                list.AddRange(_context.Branches.Where(a => a.AMHCode == user.BranchId && a.BrType == "Branch").Select(a => a.Id).ToList());
            }
            else if (brInfo.BrType == "HO")
            {
                list.AddRange(_context.Branches.Select(a => a.Id ).ToList());
            }
            else
            {
                list.AddRange(_context.Branches.Where(a => a.RegionalOffice == user.BranchDetails.RegionalOffice ).Select(a => a.Id).ToList());
            }
            ViewBag.BranchList = list;
            ViewBag.AmhList = amhList;
            ViewBag.AllUser = brInfo.BrType;
            return View();
        }

        [HttpPost]
       
        public async Task<IActionResult> UploadFile(IFormFile KYCDocument)
        {
            var fileName = Guid.NewGuid().ToString().Replace("-", "").Substring(1, 6) + KYCDocument.FileName;
            if (KYCDocument.Length > 0)
            {

                var filePath = Path.Join(_appEnvironment.WebRootPath, fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await KYCDocument.CopyToAsync(stream);
                }
               
                
                return Ok("OK");
            }
            else
            {
                return Ok("False");
            }

           // return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddDocument(Document document, IFormFile KYCDocument)
        {
            var fileName = Guid.NewGuid().ToString().Replace("-", "").Substring(1, 6) + KYCDocument.FileName;
            if (KYCDocument.Length > 0)
            {

                var filePath = Path.Join(_appEnvironment.WebRootPath, fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await KYCDocument.CopyToAsync(stream);
                }
                document.FilePath = fileName;
                document.UserId = User.Identity.Name;
                document.UploadDate = DateTime.Now; 
                _context.Documents.Add(document);
                _context.SaveChanges();
                AddLog(document.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Add Document ");
                return RedirectToAction("Application", new { Id = _protector.Decode(document.ApplicationId.ToString()) });
            }
            else
            {
                document.FilePath = "Not Uploaded";
            }

            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddApplicant(Applicant applicant,IFormFile KYCDocument)
        {
            
            if (KYCDocument!=  null)
            {
                if (KYCDocument.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString().Replace("-", "").Substring(1, 6) + KYCDocument.FileName;
                    var filePath = Path.Join(_appEnvironment.WebRootPath, fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await KYCDocument.CopyToAsync(stream);
                    }
                    applicant.KYC_Upload = fileName;
                }
               
            }
            else
            {
                applicant.KYC_Upload = "Not Uploaded";
            }
            _context.Applicants.Add(applicant);
            _context.SaveChanges();
            AddLog(applicant.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Add Applicant");
            return RedirectToAction("Application",new {Id =  _protector.Decode(applicant.ApplicationId.ToString())  });
        }
        [Authorize]
        public IActionResult EditApplicant(int Id)
        {
            var app = _context.Applicants.Find(Id);
            return View(app);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditApplicant(Applicant applicant, IFormFile KYCDocument)
        {
            
            if (KYCDocument != null)
            {
                var fileName = Guid.NewGuid().ToString().Replace("-", "").Substring(1, 6) + KYCDocument.FileName;
                var filePath = Path.Join(_appEnvironment.WebRootPath, fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await KYCDocument.CopyToAsync(stream);
                }
                applicant.KYC_Upload = fileName;
            }
           
            _context.Entry(applicant).State = EntityState.Modified;
            _context.SaveChanges();
            AddLog(applicant.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Edit Applicant :" + applicant.Id);
            return RedirectToAction("Application", new { Id = _protector.Decode(applicant.ApplicationId.ToString()) });
        }

        [Authorize]
        public IActionResult AddApplication()
        {
            return View();
        }


        [Authorize]
        public IActionResult SaveResidence(string ac, string vill)
        {
            _context.Database.ExecuteSqlRaw($"UPDATE loanflow.\"Basic_Details\" SET  residential_vill='{vill}' WHERE \"ApplicationId\" = '{ac}' ");

           return Ok(0);
        }


        [Authorize]
        [HttpPost]
        
        public async Task<IActionResult> AddApplication(Application application)
        {
            if (application.AppliedAmt > 1)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var brInfo = _context.Branches.Find(user.BranchId);
                application.BranchId = user.BranchId;
                application.ApplicationDate = application.ApplicationDate;
                application.Status = "Pending";
                application.Owner = brInfo.BrType;
                application.OwnerUser = user.UserName;
                _context.Applications.Add(application);
                _context.SaveChanges();
                AddLog(application.Id, user.UserName, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Add Application");
                return RedirectToAction("Application", new { Id = _protector.Decode(application.Id.ToString()) });
            }
            ViewBag.Message = "Applied Amount less than 0";
            return View();
        }

        
        [Authorize]
        public IActionResult MapAccount(int Id , string Tl ,string  Cc)
        {
            var app = _context.Applications.Find(Id);
            if (app == null) {
                return RedirectToAction("MapAccounts", new { msg = "Something went wrong" });
            }
            
            var c = _context.Applications.Where(a=> a.MappedCCAccount == Cc || a.MappedTLAccount == Cc ).Count();
            if (Cc == "0") {
                c = 0;
            }
            var d = _context.Applications.Where(a => a.MappedCCAccount == Tl || a.MappedTLAccount == Tl).Count();
            if (Tl == "0")
            {
                d = 0;
            }
            if (c + d > 0)
            {

                return RedirectToAction("MapAccounts",new { msg="Already Mapped Accounts"});
            }

            var tl_acc = _context.KeyValues.FromSqlRaw($"SELECT * from public.check_acc('{Tl}');").ToList();
            var cc_acc = _context.KeyValues.FromSqlRaw($"SELECT * from public.check_acc('{Cc}');").ToList();

            if (tl_acc.Count == 0 && Tl != "0")
            {
                return RedirectToAction("MapAccounts", new { msg = "Invalid TL Account" });
            }
            else if (tl_acc.Count > 0)
            {
                if (tl_acc[0].value == "C")
                {
                    return RedirectToAction("MapAccounts", new { msg = "Invalid TL Account" });
                }
            }
            if (cc_acc.Count == 0 && Cc != "0")
            {
                return RedirectToAction("MapAccounts", new { msg = "Invalid CC Account" });
            }
            else if (cc_acc.Count > 0  )
            {
                if (cc_acc[0].value == "L")
                {
                    return RedirectToAction("MapAccounts", new { msg = "Invalid CC Account" });
                }
            }

            app.MappedTLAccount = Tl;
            app.MappedCCAccount = Cc;
            _context.Entry(app).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("MapAccounts", new { msg = "Mapped Successfully" }); ;
        }

        [Authorize]
        public async Task<IActionResult> ThirdParty(int Id)
        {
            ViewBag.ApplicationId = Id;
            
            return View();
        }


        [Authorize]
        public IActionResult AddCharge(int Id)
        {
            ViewBag.ApplicationId = Id;
            return View();
        }
        [Authorize]
        public IActionResult AddStatement(int Id)
        {
            ViewBag.ApplicationId = Id;
            ViewBag.Apps = _context.Applicants.Where(a => a.ApplicationId == Id).ToList();
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddStatement(int AppId , int ApplicantId , string mobile)
        {
            
            var o1 = new RestClientOptions();
            o1.Proxy = new WebProxy("10.43.5.6:3128");
            var client = new RestClient(o1);
            var request1 = new RestRequest("https://crgbrrb.moneyone.in/v2/requestconsent");

            var reqId = Guid.NewGuid().ToString().Replace("-", "");
            request1.AddHeader("client_id", _configuration["aa_id"]);
            request1.AddHeader("client_secret", _configuration["aa_secret"]);
            request1.AddHeader("organisationId", _configuration["aa_secret"]);
            request1.AddHeader("appIdentifier", _configuration["aa_ident"]);
            //var content = new StringContent("{\n  \"partyIdentifierType\": \"MOBILE\",\n  \"partyIdentifierValue\": \"" + mobile + "\",\n  \"productID\": \"PL2\",\n  \"accountID\": \"" + reqId + "\",\n  \"vua\": \"" + mobile + "@onemoney\"\n}", null, "application/json");
            AARequest aa = new AARequest();
            aa.partyIdentifierValue = mobile;
            aa.partyIdentifierType = "MOBILE";
            aa.vua = $"{mobile}@onemoney";
            aa.accountID = reqId;
            aa.productID = "PL2";
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(aa);
            request1.AddBody( body, "application/json");
            var response = await client.ExecuteAsync(request1, Method.Post);

            var k = response.Content.ToString();
            var d = Newtonsoft.Json.JsonConvert.DeserializeObject<AAResponse>(k);
            if (d.status == "success")
            {
                AccountStatments a = new AccountStatments();
                a.Consent_Handle = reqId;
                a.ApplicationId = AppId;
                a.ApplicantId = ApplicantId;
                a.status_request = d.data.consent_handle;
                a.requestDate = DateTime.Now;
                a.UserId = User.Identity.Name;
                a.MobileNumber = mobile;
                a.request_status = "Pending";
                _context.AccountStatments.Add(a);
                _context.SaveChanges();
                return RedirectToAction("Application", new { Id = _protector.Decode(AppId.ToString()), Message = "OK" });
            }
            return RedirectToAction("Application", new { Id = _protector.Decode(AppId.ToString()), Message = "Fail" });
        }

        [Authorize]
        public async Task<IActionResult> AACheck(int Id)
        {
            var aa = _context.AccountStatments.Find(Id);
            if (aa.request_status == "Data Fetched")
            {
                var dataf = Newtonsoft.Json.JsonConvert.DeserializeObject<AAStatement>(aa.statement);
                return View(dataf);
            }
            AACRequest aac = new AACRequest();
            aac.partyIdentifierValue = aa.MobileNumber;
            aac.accountID = aa.Consent_Handle;
            aac.partyIdentifierType = "MOBILE";
            aac.productID = "PL2";
            var o1 = new RestClientOptions();
            o1.Proxy = new WebProxy("10.43.5.6:3128");
            var client = new RestClient(o1);
            var request1 = new RestRequest("https://crgbrrb.moneyone.in/v2/getconsentslist");

            //var reqId = Guid.NewGuid().ToString().Replace("-", "");
            request1.AddHeader("client_id", "b310593699f3fc5200a0643fc47a45887f9021fc");
            request1.AddHeader("client_secret", "b179de9bb16c99a1c094cd4103158e4ddbedcc7c");
            request1.AddHeader("organisationId", "crgbrrb-fiu");
            request1.AddHeader("appIdentifier", "cgbank.in");
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(aac);
            request1.AddBody(body, "application/json");
            var response = await client.ExecuteAsync(request1, Method.Post);

            var k = response.Content.ToString();
            var d = Newtonsoft.Json.JsonConvert.DeserializeObject<AACResponse>(k);
            if (d.status == "success")
            {
                var ss = d.data.Where(a => a.consentHandle == aa.status_request).FirstOrDefault();
                if (ss != null)
                {
                    if (ss.status == "ACTIVE")
                    {
                        ViewBag.Status = "A";
                        ViewBag.Accounts = ss.accounts;

                        var client1 = new RestClient(o1);
                        var request11 = new RestRequest("https://crgbrrb.moneyone.in/getallfidata");
                        DataReq dq = new DataReq();
                        dq.consentID = ss.consentID;
                        //var reqId = Guid.NewGuid().ToString().Replace("-", "");
                        request11.AddHeader("client_id", "b310593699f3fc5200a0643fc47a45887f9021fc");
                        request11.AddHeader("client_secret", "b179de9bb16c99a1c094cd4103158e4ddbedcc7c");
                        request11.AddHeader("organisationId", "crgbrrb-fiu");
                        request11.AddHeader("appIdentifier", "cgbank.in");
                        var body1 = Newtonsoft.Json.JsonConvert.SerializeObject(dq);
                        request11.AddBody(body1, "application/json");
                        var response11 = await client1.ExecuteAsync(request11, Method.Post);
                        var d1 = Newtonsoft.Json.JsonConvert.DeserializeObject<AAStatement>(response11.Content);
                        if (d1.status == "success" )
                        {
                            if (d1.data.FirstOrDefault().Profile.Holders != null)
                            {
                                aa.statement = response11.Content;
                                aa.request_status = "Data Fetched";
                                _context.Entry(aa).State = EntityState.Modified;
                                _context.SaveChanges();
                                return View(d1);
                            }
                           
                        }
                        
                    }
                    else
                    {
                        //aa.statement = response11.Content;
                        aa.request_status = ss.status;
                        _context.Entry(aa).State = EntityState.Modified;
                        _context.SaveChanges();
                    }
                }
            }

            return View();
        }


        [Authorize]
        public async Task<IActionResult> GoToApplication(int appid)
        {
            var app = _context.Applications.Find(appid);
            if (app == null) return RedirectToAction("Home","Dashboard");
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            List<string> list = new List<string>();
            var brInfo = _context.Branches.Find(user.BranchId);
            List<string> ls = new List<string>();
            ls.Add("PL");
            ls.Add("Car");
            ls.Add("HL");
            ls.Add("PL-Ag");
            ls.Add("Express");
            ls.Add("PL-Express");
            ls.Add("PL-SECL");
            ls.Add("KCC");
            ls.Add("KCC-20");
            ls.Add("MUDRA-Simplified");
            ls.Add("MUDRA-Transport");
            ls.Add("KCC-Renewal");
            ls.Add("KCC-20-Renewal");
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
            if (list.Contains(app.BranchId) && ls.Contains(app.LoanScheme))
            {
                return RedirectToAction("Application", "LoanFlow" ,new {Id= _protector.Decode(appid.ToString()) });
            }
            return RedirectToAction("Home", "Dashboard");
        }

        [Authorize]
        public async Task<IActionResult> MandateExisting()
        {
            var u = await _userManager.FindByNameAsync(User.Identity.Name);
            var mandates = _context.ExistingAcMandates.Where(a => a.branch == u.BranchId).ToList();


            return View(mandates);
        }


        [Authorize]
        public IActionResult AddMandateExisting()
        {
            return View();
        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddMandateExisting(string LoanAc, string acNumber, string acType, int EMI, string bankName, string startDate, string emailid,string mob,string cname)
        {

            ExistingAcMandate man = new ExistingAcMandate();
            man.LoanAc = LoanAc;
            man.mobile_number = mob;
            man.phone_number = mob;
            man.debtor_name = cname;
            man.consumer_reference_number = LoanAc;
            man.scheme_reference_number = "LOAN";
            man.debtor_account_id = acNumber;
            man.debtor_account_type = acType;
            man.amount = EMI;
            man.instructed_agent_code = bankName;
            man.first_collection_date = startDate;
            man.email_address = emailid;
            man.created_by = User.Identity.Name;
            var u = await _userManager.FindByNameAsync(User.Identity.Name);
            man.branch = u.BranchId;
            var js = Newtonsoft.Json.JsonConvert.SerializeObject(man);
            var o1 = new RestClientOptions();
            o1.Proxy = new WebProxy("10.43.5.6:3128");
            var client = new RestClient(o1);
            var request1 = new RestRequest("https://api.signdesk.in/api/live/emandateRequest");
            request1.AddHeader("Accept", "*/*");
            request1.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request1.AddHeader("Connection", "keep-alive");

            request1.AddHeader("x-parse-rest-api-key", "49eba9893d6ca293d80ac67d2d645bd5");
            request1.AddHeader("x-parse-application-id", "chhattisgarh-rajya-gramin-bank_enach_production");
            request1.AddHeader("Content-Type", "application/json");
            request1.AddBody(js, "application/json");

            var token_response = await client.ExecuteAsync(request1, Method.Post);
            if (token_response.StatusCode == HttpStatusCode.OK)
            {
                var st = token_response.Content.ToString();
                var d = Newtonsoft.Json.JsonConvert.DeserializeObject<MandateResponse>(st);
                man.createDate = DateTime.Now;
                man.emandate_id = d.emandate_id;
                man.api_response_id = d.status;
                man.mandate_status = "Created";
                man.is_cancelled = "False";
                _context.ExistingAcMandates.Add(man);
                _context.SaveChanges();

            }
            return RedirectToAction("Dashboard", "Home");
        }


        [Authorize]
        public IActionResult AddMandate(int Id)
        {
            ViewBag.ApplicationId = Id;
            ViewBag.Apps = _context.Applicants.Where(a => a.ApplicationId == Id).ToList();
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddMandate(int appId , string acNumber , string acType , int EMI , string bankName , string startDate , string emailid)
        {
            var app = _context.Applicants.Find(appId);
            Mandate man = new Mandate();
            man.ApplicantId = appId;
            man.ApplicationId = app.ApplicationId;
            man.mobile_number =  app.MobileNumber;
            man.phone_number =   app.MobileNumber;
            man.debtor_name = $"{app.Applicant_First_Name} {app.Applicant_Middle_Name} {app.Applicant_Last_Name}";
            man.consumer_reference_number = $"CRGB{app.ApplicationId}";
            man.scheme_reference_number = "LoanAC";
            man.debtor_account_id = acNumber;
            man.debtor_account_type = acType;
            man.amount = EMI;
            man.instructed_agent_code = bankName;
            man.first_collection_date = startDate;
            man.email_address = emailid;

            var js = Newtonsoft.Json.JsonConvert.SerializeObject(man);
            var o1 = new RestClientOptions();
            o1.Proxy = new WebProxy("10.43.5.6:3128");
            var client = new RestClient(o1);
            var request1 = new RestRequest("https://api.signdesk.in/api/live/emandateRequest");
            request1.AddHeader("Accept", "*/*");
            request1.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request1.AddHeader("Connection", "keep-alive");

            request1.AddHeader("x-parse-rest-api-key", "49eba9893d6ca293d80ac67d2d645bd5");
            request1.AddHeader("x-parse-application-id", "chhattisgarh-rajya-gramin-bank_enach_production");
            request1.AddHeader("Content-Type", "application/json");
            request1.AddBody( js,"application/json");
            
            var token_response = await client.ExecuteAsync(request1, Method.Post);
            if (token_response.StatusCode == HttpStatusCode.OK)
            {
                var st = token_response.Content.ToString();
                var d = Newtonsoft.Json.JsonConvert.DeserializeObject<MandateResponse>(st);
                man.createDate = DateTime.Now;
                man.emandate_id = d.emandate_id;
                man.api_response_id = d.status;
                man.mandate_status = "Created";
                man.is_cancelled = "False";
                _context.Mandates.Add(man);
                _context.SaveChanges();

            }
            return RedirectToAction("Application", new { Id = _protector.Decode(app.ApplicationId.ToString()) });
        }

        [Authorize]
        [HttpPost]

        public async Task<IActionResult> AddCharge(Charge application)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            _context.Charges.Add(application);
            _context.SaveChanges();
            AddLog(application.ApplicationId, user.UserName, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Charge Added");
            return RedirectToAction("Sanction", new { Id = _protector.Decode(application.ApplicationId.ToString()) });
        }
        [Authorize]
        public IActionResult AddRemark(int Id , string formName , string view="")
        {
            ViewBag.ApplicationId = Id;
            if (formName == "application")
            {
                ViewBag.IsSanction = false;
                ViewBag.IsControl = false;
            }
            if (formName == "sanction")
            {
                ViewBag.IsSanction = true;
                ViewBag.IsControl = false;
            }
            if (formName == "control")
            {
                ViewBag.IsSanction = false;
                ViewBag.IsControl = true;
            }
            ViewBag.FormName = formName;
            if (view == "other")
            {
                return View("AddOtherDetails");
            }
            if (view == "KCC")
            {
                return View("AddKCCDetails");
            }
            return View();
        }


        [Authorize]
        public IActionResult AddRemarkList(int Id, string formName, string view = "")
        {
            ViewBag.ApplicationId = Id;
            if (formName == "application")
            {
                ViewBag.IsSanction = false;
                ViewBag.IsControl = false;
            }
            if (formName == "sanction")
            {
                ViewBag.IsSanction = true;
                ViewBag.IsControl = false;
            }
            if (formName == "control")
            {
                ViewBag.IsSanction = false;
                ViewBag.IsControl = true;
            }
            ViewBag.FormName = formName;
            if (view == "other")
            {
                return View("AddOtherDetailsHL");
            }
            if (view == "KCC")
            {
                return View("AddKCCDetailsList");
            }
            if (view == "TPL")
            {
                return View("AddOtherDetailsTPL");
            }
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult AddRemarkList(int ApplicationId, string formName, IFormCollection formCollection,string SpecialRemark = "Yes")
        {
            foreach (var item in formCollection)
            {

                if (!(item.Key == "ApplicationId" || item.Key == "formName" || item.Key == "SpecialRemark") && item.Value != "")
                {
                    var exst = _context.Remarks.Where(a => a.ApplicationId == ApplicationId && a.TypeOfRemark == item.Key).ToList();

                    foreach (var i in exst)
                    {
                        _context.Entry(i).State = EntityState.Deleted;
                        _context.SaveChanges();
                    }


                    Remark visit = new Remark();
                    visit.ApplicationId = ApplicationId;
                    visit.Statment = item.Value;
                    visit.TypeOfRemark = item.Key;
                    visit.UserId = User.Identity.Name;
                    visit.SpecialRemark = SpecialRemark;
                    _context.Remarks.Add(visit);
                    _context.SaveChanges();
                    AddLog(ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", $"Remark Added {item.Key}");



                }


            }
            if (formName == "application")
            {
                return RedirectToAction("Application", new { Id = _protector.Decode(ApplicationId.ToString()) });
            }
            if (formName == "sanction")
            {
                return RedirectToAction("Sanction", new { Id = _protector.Decode(ApplicationId.ToString()) });
            }
            if (formName == "control")
            {
                return RedirectToAction("Application", new { Id = _protector.Decode(ApplicationId.ToString()) });
            }


            return View();
        }


        [Authorize]
        [HttpPost]

        public async Task<IActionResult> AddScore(Remark application)
        {

            var app = _context.Applications.Find(application.ApplicationId);
            if (app.Status != "Pending")
            {
                return RedirectToAction("Application", new { Id = _protector.Decode(application.ApplicationId.ToString()) });
            }
            var exst = _context.Remarks.Where(a => a.ApplicationId == application.ApplicationId && a.TypeOfRemark == application.TypeOfRemark).ToList();
            foreach (var item in exst)
            {
                _context.Entry(item).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            application.UserId = user.UserName;
            _context.Remarks.Add(application);
            _context.SaveChanges();
            AddLog(application.ApplicationId, user.UserName, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Score Added by Score card");
            return RedirectToAction("Application", new { Id = _protector.Decode(application.ApplicationId.ToString()) });
           
        }


        [Authorize]
        [HttpPost]

        public async Task<IActionResult> AddRemark(Remark application,string formName)
        {
            var exst = _context.Remarks.Where(a => a.ApplicationId == application.ApplicationId && a.TypeOfRemark == application.TypeOfRemark).ToList();
            foreach (var item in exst)
            {
                _context.Entry(item).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            application.UserId = user.UserName;
            _context.Remarks.Add(application);
            _context.SaveChanges();
            AddLog(application.ApplicationId, user.UserName, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Remarks Added");
            if (formName == "application")
            {
                return RedirectToAction("Application", new { Id = _protector.Decode(application.ApplicationId.ToString()) });
            }
            if (formName == "sanction")
            {
                return RedirectToAction("Sanction", new { Id = _protector.Decode(application.ApplicationId.ToString()) });
            }
            if (formName == "control")
            {
                return RedirectToAction("Control", new { Id = _protector.Decode(application.ApplicationId.ToString()) });
            }
            return RedirectToAction("Application", new { Id = _protector.Decode(application.ApplicationId.ToString()) });

        }
        [Authorize]
        public IActionResult AddSecurity(int Id)
        {
            ViewBag.ApplicationId = Id;
            return View();
        }

        [Authorize]
        [HttpPost]

        public async Task<IActionResult> AddSecurity(Security application)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            _context.Securities.Add(application);
            _context.SaveChanges();
            AddLog(application.ApplicationId, user.UserName, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Security Added");
            return RedirectToAction("Application", new { Id = _protector.Decode(application.ApplicationId.ToString()) });
        }
        [Authorize]
        [HttpPost]

        public async Task<IActionResult> AddTPL(TPLDetail application)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var tpl = _context.TPLDetails.Where(a=> a.ApplicationId == application.ApplicationId).FirstOrDefault();
            if (tpl != null)
            {
                _context.Entry(tpl).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            _context.TPLDetails.Add(application);
            _context.SaveChanges();
            AddLog(application.ApplicationId, user.UserName, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "TPL Added");
            return RedirectToAction("Application", new { Id = _protector.Decode(application.ApplicationId.ToString()) });
        }

        [Authorize]
        public IActionResult MudraDetails(int Id)
        {
            ViewBag.ApplicationId = Id;
            return View();
        }

        [Authorize]
        public IActionResult AddDisb(int Id)
        {
            ViewBag.ApplicationId = Id;
            return View();
        }

        [Authorize]
        [HttpPost]

        public async Task<IActionResult> AddDisb(Disbursement application)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            _context.Disbursements.Add(application);
            _context.SaveChanges();
            AddLog(application.ApplicationId, user.UserName, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Disbursement Added");
            return RedirectToAction("Sanction", new { Id = _protector.Decode(application.ApplicationId.ToString()) });
        }


        [Authorize]
        public IActionResult AddCibil(int Id)
        {
            var cibilInfo = _context.CIBILRequests.Include(a=> a.ApplicantDetail).Where(b=> b.Id == Id).FirstOrDefault();
            ViewBag.ApplicationId = cibilInfo.ApplicantDetail.ApplicationId;
            ViewBag.Id = Id;
            return View();
        }

        [Authorize]
        [HttpPost]

        public async Task<IActionResult> AddCibil(CIBILLoanInfo application , int ApplicationId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            _context.CIBILLoanInfo.Add(application);
            _context.SaveChanges();
            AddLog(ApplicationId, user.UserName, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Additional Loan Added");
             
            return RedirectToAction("Application", new { Id = _protector.Decode(ApplicationId.ToString()) , Message = "OK" });
        }

        [Authorize]
        public async Task<IActionResult> ListApplication(string search = "")
        {
            List<string> ls = new List<string>();
            ls.Add("PL");
            ls.Add("Car");
            ls.Add("HL");
            ls.Add("PL-Ag");
            ls.Add("Express");
            ls.Add("PL-Express");
            ls.Add("PL-SECL");
            ls.Add("KCC");
            ls.Add("KCC-20");
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            List<string> list = new List<string>();
            var brInfo = _context.Branches.Find(user.BranchId);
            if (brInfo.BrType == "Branch")
            {
                list.Add(brInfo.Id);
            }
            else if (brInfo.BrType == "AMH")
            {
                list.AddRange(_context.Branches.Where(a=> a.AMHCode == user.BranchId).Select(a=> a.Id).ToList());
            }
            else if (brInfo.BrType == "HO")
            {
                list.AddRange(_context.Branches.Select(a => a.Id).ToList());
            }
            else 
            {
                list.AddRange(_context.Branches.Where(a => a.RegionalOffice == user.BranchDetails.RegionalOffice && a.BrType == "Branch").Select(a => a.Id).ToList());
            }
            if (search == "")
            {
                var applications = _context.Applications.Include(a => a.Branch).Where(a => a.Status == "Pending" && list.Contains(a.BranchId)).ToList();
                return View(applications);
            }
            else if (search == "LMS")
            {
                var applications = _context.Applications.Include(a => a.Branch).Where(a => a.Status == "Pending" && ls.Contains(a.LoanScheme) && list.Contains(a.BranchId)).ToList();
                return View(applications);
            }
            else if (search == "PreSanction")
            {
                var applications = _context.Applications.Include(a => a.Branch).Include(a => a.Inspections).Where(a => a.Status == "Pending" && list.Contains(a.BranchId) && a.Inspections.Count > 0).ToList();
                return View(applications);
            }
            else if (search == "Forwarded")
            {
                var applications = _context.Applications.Include(a => a.Branch).Include(a => a.Inspections).Where(a => a.Status == "Pending" && list.Contains(a.BranchId) && a.Inspections.Count > 0 && a.Owner != "Branch").ToList();
                return View(applications);
            }
            
            else
            {
                var applications = _context.Applications.Include(a => a.Branch).Where(a => a.Status == "Pending" && list.Contains(a.BranchId) ).ToList();
                return View(applications);
            }

        }



        [Authorize]
        public async Task<IActionResult> Inbox(string search = "")
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
           
            var applications = _context.Applications.Include(a => a.Branch).Where(a => a.Status == "Pending" && a.OwnerUser == user.UserName).ToList();
            return View("ListApplication",applications);
            

        }

        [Authorize]
        public async Task<IActionResult> BeyondTAT()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            List<string> list = new List<string>();
            var brInfo = _context.Branches.Find(user.BranchId);
            var region_name = "HO";
            if (brInfo.BrType == "Branch")
            {
                region_name = brInfo.RegionalOffice;
                list.Add(brInfo.Id);
            }
            else if (brInfo.BrType == "AMH")
            {
                region_name = brInfo.RegionalOffice;
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
            var applications = _context.Applications.Include(a => a.Branch).Where(a => a.Status == "Pending" && list.Contains(a.BranchId)).ToList();
            
           // ViewBag.Report = report;
            return View(applications);
        }

        [Authorize]
        public async Task<IActionResult> ListReturnApplication()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            List<string> list = new List<string>();
            var brInfo = _context.Branches.Find(user.BranchId);
            if (brInfo.BrType == "Branch")
            {
                list.Add(brInfo.Id);
            }
            else if (brInfo.BrType == "AMH")
            {
                list.AddRange(_context.Branches.Where(a => a.AMHCode == user.BranchId).Select(a => a.Id).ToList());
            }
            else
            {
                list.AddRange(_context.Branches.Where(a => a.RegionalOffice == user.BranchDetails.RegionalOffice && a.BrType == "Branch").Select(a => a.Id).ToList());
            }
            var applications = _context.Applications.Include(a => a.Branch).Where(a => a.Status == "Returned" && a.Owner == brInfo.BrType && list.Contains(a.BranchId)).ToList();
            return View("ListApplication",applications);
        }
        [Authorize]
        public async Task<IActionResult> ControlReturn(string msg = "")
        {
            ViewBag.Message = msg;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var branch = _context.Branches.Find(user.BranchId);
            List<string> list = new List<string>();
            var brInfo = _context.Branches.Find(user.BranchId);
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
            ViewBag.UserLevel = branch.BrType;
            var applications = _context.Applications.Where(a => a.Status == "Sanctioned"  && (a.ControlStatus=="Pending" || a.ControlStatus == "Control Return") && list.Contains(a.BranchId) ).Include(a=> a.Branch).ToList();
            return View(applications);
        }
        [Authorize]
        public async Task<IActionResult> PendingControl(string msg = "")
        {
            ViewBag.Message = msg;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var branch = _context.Branches.Find(user.BranchId);
            List<string> list = new List<string>();
            var brInfo = _context.Branches.Find(user.BranchId);
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
            ViewBag.UserLevel = branch.BrType;

            var applications = new List<Application>();
            if (user.Designation == "Regional Manager")
            {
                applications = _context.Applications.Include(a=> a.Branch).Where(a => a.Status == "Sanctioned" && a.ControlStatus == "SendToControl" && list.Contains(a.BranchId) && (a.SanctioningLevel == "AMH Head" || a.SanctioningLevel == "Manager Business" || a.SanctioningLevel == "Manager Advance")).ToList();
            }
            else if (user.Designation == "Manager Business" || user.Designation == "AMH Head")
            {
                applications = _context.Applications.Include(a => a.Branch).Where(a => a.Status == "Sanctioned" && a.ControlStatus == "SendToControl" && list.Contains(a.BranchId) && (a.SanctioningLevel == "AMH 2nd Officer")).ToList();
            }
            return View(applications);
        }
        [Authorize]
        public async Task<IActionResult> ReturnedControl(string msg = "")
        {
            ViewBag.Message = msg;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var branch = _context.Branches.Find(user.BranchId);
            List<string> list = new List<string>();
            var brInfo = _context.Branches.Find(user.BranchId);
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
            ViewBag.UserLevel = branch.BrType;

            var applications = new List<Application>();
            if (user.Designation == "Regional Manager")
            {
                applications = _context.Applications.Include(a => a.Branch).Where(a => a.Status == "Sanctioned" && a.ControlStatus == "Control Returned" && list.Contains(a.BranchId) && (a.SanctioningLevel == "AMH Head" || a.SanctioningLevel == "Manager Business" || a.SanctioningLevel == "Manager Advance")).ToList();
            }
            else if (user.Designation == "Manager Business")
            {
                applications = _context.Applications.Include(a => a.Branch).Where(a => a.Status == "Sanctioned" && a.ControlStatus == "Control Returned" && list.Contains(a.BranchId) && !(a.SanctioningLevel == "AMH Head" || a.SanctioningLevel == "Manager Business" || a.SanctioningLevel == "Manager Advance")).ToList();
            }
            return View("PendingControl",applications);
        }
        [Authorize]
        public async Task<IActionResult> Controlled(string msg = "")
        {
            ViewBag.Message = msg;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var branch = _context.Branches.Find(user.BranchId);
            List<string> list = new List<string>();
            var brInfo = _context.Branches.Find(user.BranchId);
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
                list.AddRange(_context.Branches.Where(a => a.RegionalOffice == user.BranchDetails.RegionalOffice && a.BrType == "Branch" ).Select(a => a.Id).ToList());
            }
            ViewBag.UserLevel = branch.BrType;
            var applications = _context.Applications.Where(a => a.Status == "Sanctioned" && a.ControlStatus == "Controlled" && list.Contains(a.BranchId)).Include(a=> a.Branch).ToList();
            return View(applications);
        }
        [Authorize]
        public async Task<IActionResult> MapAccounts(string msg = "")
        {
            ViewBag.Message = msg;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var branch = _context.Branches.Find(user.BranchId);
            List<string> list = new List<string>();
            var brInfo = _context.Branches.Find(user.BranchId);
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
            ViewBag.UserLevel = branch.BrType;
            var applications = _context.Applications.Where(a => a.Status == "Sanctioned" && list.Contains(a.BranchId) && (a.MappedTLAccount == null && a.MappedCCAccount == null)).ToList();
            return View(applications);
        }

        //[HttpPost]
        //public JsonResult GetEmployeeList()
        //{
        //    int totalRecord = 0;
        //    int filterRecord = 0;
        //    var draw = Request.Form["draw"].FirstOrDefault();
        //    var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        //    var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        //    var searchValue = Request.Form["search[value]"].FirstOrDefault();
        //    int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
        //    int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
        //    var data = _context.Set<Application>().Where(a=>a.Status == "Sanctioned").Include(a=> a.Branch).AsQueryable();
        //    //get total count of data in table
        //    totalRecord = data.Count();
        //    // search data when search value found
        //    if (!string.IsNullOrEmpty(searchValue))
        //    {
        //        data = data.Where(x => x.ApplicantShortName.ToLower().Contains(searchValue.ToLower()) || x.Branch.BranchName.ToLower().Contains(searchValue.ToLower()) || x.Id.ToString().ToLower().Contains(searchValue.ToLower()) );
        //    }
        //    // get total count of records after search
        //    filterRecord = data.Count();
        //    //sort data
        //    if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection)) data = data.OrderBy(sortColumn + " " + sortColumnDirection);
        //    //pagination
        //    var empList = data.Skip(skip).Take(pageSize).ToList();
        //    var returnObj = new
        //    {
        //        draw = draw,
        //        recordsTotal = totalRecord,
        //        recordsFiltered = filterRecord,
        //        data = empList
        //    };
        //}
    

    [Authorize]
        public async Task<IActionResult> AppSanctionedAjax()
        {
        int totalRecord = 0;
        int filterRecord = 0;
        var draw = Request.Form["draw"].FirstOrDefault();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
        int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
        return Ok("");
        }

        [Authorize]
        public async Task<IActionResult> ListApplicationSanction()
        {
            //var user = await _userManager.FindByNameAsync(User.Identity.Name);
            //List<string> list = new List<string>();
            //var brInfo = _context.Branches.Find(user.BranchId);
            //if (brInfo.BrType == "Branch")
            //{
            //    list.Add(brInfo.Id);
            //}
            //else if (brInfo.BrType == "AMH")
            //{
            //    list.AddRange(_context.Branches.Where(a => a.AMHCode == user.BranchId).Select(a => a.Id).ToList());
            //}
            //else if (brInfo.BrType == "HO")
            //{
            //    list.AddRange(_context.Branches.Select(a => a.Id).ToList());
            //}
            //else
            //{
            //    list.AddRange(_context.Branches.Where(a => a.RegionalOffice == user.BranchDetails.RegionalOffice && a.BrType == "Branch").Select(a => a.Id).ToList());
            //}
            //var applications = _context.Applications.Include(a => a.Branch).Where(a =>  a.Status == "Sanctioned" && list.Contains(a.BranchId)).ToList();
            return View();
        }

        [Authorize]
        public async Task<IActionResult> SanctionList(string st , string en , string sch)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            List<string> list = new List<string>();
            var brInfo = _context.Branches.Find(user.BranchId);
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
            DateTime startD = DateTime.Parse(st);
            DateTime endD = DateTime.Parse(en);
            endD = endD.AddDays(1);
            List<string> ls = new List<string>();





            if (sch == "PL")
            {
                ls.Add("PL");

                ls.Add("PL-Ag");
                ls.Add("Express");
                ls.Add("PL-Express");
                ls.Add("PL-SECL");


            }
            else if (sch == "Car")
            {
                ls.Add("Car");
            }
            else if (sch == "KCC")
            {
                ls.Add("KCC");
                ls.Add("KCC-20");
            }
            else if (sch == "HL")
            {
                ls.Add("HL");
            }
            else if (sch == "MUDRA-Transport")
            {
                ls.Add("MUDRA-Transport");
            }
            else if (sch == "Other")
            {
                ls.Add("LAP");
                ls.Add("Topup");
                ls.Add("Sme");
                ls.Add("PMEGP");
                ls.Add("Gold");
                ls.Add("Gold-Agri");
                ls.Add("KCC-AH");
                ls.Add("KCC-Fish");
                ls.Add("NRLM");
                ls.Add("NULM");
                ls.Add("TPL");
                ls.Add("Svanidhi");
                ls.Add("Edu");
                ls.Add("ATL");
                ls.Add("SME-GSS");
                ls.Add("MMYSRY");
                ls.Add("DL");
                ls.Add("OD-Dep");
                ls.Add("2Wheeler");
                ls.Add("2Wheeler-Agri");
                ls.Add("2Wheeler-Mudra");
                ls.Add("KisanCar");
                ls.Add("MUDRA-Simplified");
                ls.Add("CRGB-Realty");
            }
            else
            {
                ls.Add("PL");
                ls.Add("KCC");
                ls.Add("KCC-20");
                ls.Add("PL-Ag");
                ls.Add("Express");
                ls.Add("PL-Express");
                ls.Add("PL-SECL");
                ls.Add("Car");
                ls.Add("HL");
                ls.Add("MUDRA-Transport");
            }
            var applications = _context.Applications.Include(a => a.Branch).Include(a => a.LoanApplications).Where(a => a.Status == "Sanctioned" && list.Contains(a.BranchId) && ls.Contains(a.LoanScheme) && a.SanctionedDate >= startD && a.SanctionedDate <= endD).ToList();
            return View(applications);
        }
       [Authorize]
       public async Task<IActionResult> createRequestAsync(string mobile,string id)
        {
            int AppId = int.Parse(_protector.Encode(id));
            var o1 = new RestClientOptions();
            o1.Proxy = new WebProxy("10.43.5.6:3128");
            var client = new RestClient(o1);
            var request1 = new RestRequest("https://crgbrrb.moneyone.in/v2/requestconsent");
           
           
            request1.AddHeader("client_id", "b310593699f3fc5200a0643fc47a45887f9021fc");
            request1.AddHeader("client_secret", "b179de9bb16c99a1c094cd4103158e4ddbedcc7c");
            request1.AddHeader("organisationId", "crgbrrb-fiu");
            request1.AddHeader("appIdentifier", "cgbank.in");
            var content = new StringContent("{\n  \"partyIdentifierType\": \"MOBILE\",\n  \"partyIdentifierValue\": \""+mobile+ "\",\n  \"productID\": \"PL2\",\n  \"accountID\": \"LMS" + AppId.ToString() + "\",\n  \"vua\": \"" + mobile + "@onemoney\"\n}", null, "application/json");
            request1.AddParameter("text/plain", content , ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request1,Method.Post);
            
            var k = response.Content.ToString();
            var d = Newtonsoft.Json.JsonConvert.DeserializeObject<AAResponse>(k);
            
            return Ok(d);

        }

        public async Task<bool> Check(int Id)
        {

            var application = _context.Applications.Find(Id);
            
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var brInfo = _context.Branches.Find(user.BranchId);
           
            if (application.Owner == brInfo.BrType)
            {
                return true;
            }
            
            return false;
        }

        [Authorize]
        public async Task<IActionResult> Application(string Id,string message = "", string dmessage = "")
        {
            
           ViewData["Message"] = message;
            ViewData["DMessage"] = dmessage;

            List<string> ls = new List<string>();
            ls.Add("PL");
            ls.Add("Car");
            ls.Add("HL");
            ls.Add("PL-Ag");
            ls.Add("Express");
            ls.Add("PL-Express");
            ls.Add("PL-SECL");
            ls.Add("KCC");
            ls.Add("KCC-20");
            ls.Add("MUDRA-Simplified");
            ls.Add("MUDRA-Transport");
            ls.Add("KCC-Renewal");
            ls.Add("KCC-20-Renewal");

            int id = int.Parse(_protector.Encode(Id));

            var app = _context.Applications.Find(id);
            if (!ls.Contains(app.LoanScheme) )
            {
                return RedirectToAction("AddControlDetails",new {Id = Id });
            }
            _context.Entry(app).Collection(a=> a.Applicants).Load();
            List<int> applicantIds = app.Applicants.Where(a=> a.ApplicationId == id && a.Driving_Lic != "Deleted").Select(b=> b.Id).ToList();
            List<CIBILRequest> cs = _context.CIBILRequests.Include(a=> a.CibilLoans).Include(a=> a.ApplicantDetail).Where(a=>  applicantIds.Contains(a.ApplicantId) && a.CiibilControlNumber != "Deleted" && a.Score2 != "Failed").ToList();
            List<ProjectCost> pj = _context.ProjectCosts.Where(a => a.ApplicationId == id).ToList();
            if (app.LoanScheme.Contains("KCC"))
            {
                _context.Entry(app).Collection(a => a.KCCCropDetails).Load();
                _context.Entry(app).Collection(a => a.KCCLandDetails).Load();
                var c = _context.KCCCropDetails.Include(a => a.KCCLandDetails).Where(a => a.ApplicationId == id).ToList();
                ViewBag.KCCCropDetails = c;
            }
            if (app.LoanScheme.Contains("Transport"))
            {
                _context.Entry(app).Reference(a => a.TPLDetails).Load();
            }
            if (app.LoanScheme.Contains("Renewal"))
            {
                _context.Entry(app).Reference(a => a.Renewal).Load();
            }
            _context.Entry(app).Collection(a => a.Documents).Load();
            _context.Entry(app).Collection(a => a.Remarks).Load();
            _context.Entry(app).Collection(a => a.LoanApplications).Load();
            _context.Entry(app).Collection(a => a.Inspections).Load();
            _context.Entry(app).Collection(a => a.ProjectCost).Load();
            _context.Entry(app).Collection(a => a.Securities).Load();
            _context.Entry(app).Collection(a => a.AccountStatments).Load();
            //var application = _context.Applications.Include(a => a.Applicants).Include(a => a.LoanApplications).Include(a => a.Documents).Include(a => a.Inspections).Include(a => a.Charges).Include(a => a.Disbursements).Include(a => a.Remarks).Include(a => a.Securities).Include(a => a.KCCLandDetails).Include(a => a.KCCCropDetails).FirstOrDefault(a => a.Id == id);

            ViewBag.CreditScore = cs;
            ViewBag.ProjectCost = pj;
            ViewBag.Editable = await Check(id);
            
            foreach (var item in app.Inspections)
            {
                var cnt = _context.PreIsnpectionRemarks.Where(a => a.VisitId == item.Id).Count();
                if (cnt == 0)
                {
                    item.ClosureOfficialId = "No";
                }
            }
            
            if (app.Status == "Sanctioned")
            {
                _context.Entry(app).Collection(a => a.Charges).Load();
                _context.Entry(app).Collection(a => a.Disbursements).Load();
                _context.Entry(app).Collection(a => a.Mandates).Load();
                ViewBag.Editable = false;
            }
            //if (User.Identity.Name != app.OwnerUser)
            //{
            //    ViewBag.Editable = false;
            //}
            if (User.Identity.Name == "4729388")
            {
                ViewBag.Editable = true;
            }
            return View(app);
        }
        [Authorize]
        public async Task<IActionResult> AddControlDetails(string Id)
        {
            int id = int.Parse(_protector.Encode(Id));
            var application = _context.Applications.Include(a => a.Applicants).Include(a => a.LoanApplications).Include(a => a.Documents).Include(a => a.ControlInformations).Include(a => a.Charges).Include(a => a.Disbursements).Include(a => a.Remarks).Include(a => a.Securities).Include(a => a.KCCLandDetails).Include(a => a.KCCCropDetails).Where(a => a.Id == id).FirstOrDefault();
            if (application.ControlInformations is null)
            {
                application.ControlInformations = new ControlInformation();
            }
            ViewBag.Editable = await Check(id);
            List<int> applicantIds = application.Applicants.Where(a => a.ApplicationId == id && a.Driving_Lic != "Deleted").Select(b => b.Id).ToList();
            List<CIBILRequest> cs = _context.CIBILRequests.Include(a => a.CibilLoans).Include(a => a.ApplicantDetail).Where(a => applicantIds.Contains(a.ApplicantId) && a.CiibilControlNumber != "Deleted").ToList();
            ViewBag.CreditSCore = cs;
            if (application.Status == "Sanctioned")
            {
                ViewBag.Editable = false;
            }
            else
            {
                ViewBag.Editable = true;
            }

            return View(application);
        }
        [Authorize]
        [HttpPost]
        public  IActionResult AddControlDetails(ControlInformation c , int ApplicationId , string SanctionedDate)
        {
            if (_context.ControlInformation.Where(a=> a.ApplicationId == ApplicationId).Count() > 0)
            {
                var d = _context.ControlInformation.Where(a => a.ApplicationId == ApplicationId).FirstOrDefault();
                _context.Entry(d).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            c.ApplicationId = ApplicationId;
            _context.ControlInformation.Add(c);
            _context.SaveChanges();
            var app = _context.Applications.Find(ApplicationId);
            
            app.SanctionedLevel = app.Owner;
            app.Owner = "Branch";
            app.Status = "Sanctioned";
            app.ControlStatus = "Pending";

            app.SanctionedDate = DateTime.Parse(SanctionedDate);
            _context.Entry(app).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("ListApplication");
        }
        [Authorize]
        public async Task<IActionResult> Control(string Id)
        {
          
            int id = int.Parse(_protector.Encode(Id));
            var app = _context.Applications.Find(id);
           
            _context.Entry(app).Collection(a => a.Applicants).Load();
            List<int> applicantIds = app.Applicants.Where(a => a.ApplicationId == id && a.Driving_Lic != "Deleted").Select(b => b.Id).ToList();
            List<CIBILRequest> cs = _context.CIBILRequests.Include(a => a.CibilLoans).Include(a => a.ApplicantDetail).Where(a => applicantIds.Contains(a.ApplicantId) && a.CiibilControlNumber != "Deleted" && a.Score2 != "Failed").ToList();
            var acc_no = "00";
            if (app.MappedTLAccount != null)
            {
                if (app.MappedTLAccount != "0")
                {
                    acc_no = app.MappedTLAccount;
                }
                
            }
            if (app.MappedCCAccount != null)
            {
                if (app.MappedCCAccount != "0")
                {
                    acc_no = app.MappedCCAccount;
                }
                   
            }

            var ac = _context.AccountData.FromSqlRaw($"SELECT account_no,  acctopendate, currentbalance,  odlimit,  accounttype, interestcat, intrate,name,cust_name 	FROM public.temp_loan Where account_no = LPAD('{acc_no}',17,'0')").FirstOrDefault();
            ViewBag.AccountDetails = ac;
            var application = _context.Applications.Include(a => a.Applicants).Include(a => a.LoanApplications).Include(a => a.Documents).Include(a => a.Inspections).Include(a => a.Charges).Include(a => a.Disbursements).Include(a=> a.Remarks).Where(a => a.Id == id).FirstOrDefault();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var branch = _context.Branches.Find(user.BranchId);
            ViewBag.IsAbleToControl = false;
            if ((user.Designation == "Manager Business" || user.Designation == "Regional Manager") && branch.BrType == "RO")
            {
                ViewBag.IsAbleToControl = true;
            }
            ViewBag.Editable = false;
            ViewBag.CreditScore = cs;

            return View(application);
        }
        [Authorize]
        public IActionResult ControlDetails(string Id)
        {
            int id = int.Parse(_protector.Encode(Id));
            var application = _context.Applications.Include(a => a.Applicants).Include(a=> a.ControlInformations).Where(a => a.Id == id).FirstOrDefault();
            if (application.ControlInformations is null)
            {
                application.ControlInformations = new ControlInformation();
            }
            ViewBag.Editable = false;


            return View(application);
        }


        [Authorize]
        public async Task<IActionResult> ApplicationExisting(string Id, string message = "", string dmessage = "")
        {

            ViewData["Message"] = message;
            ViewData["DMessage"] = dmessage;

            List<string> ls = new List<string>();
            ls.Add("PL");
            ls.Add("Car");
            ls.Add("HL");
            ls.Add("PL-Ag");
            ls.Add("Express");
            ls.Add("PL-Express");
            ls.Add("PL-SECL");
            ls.Add("KCC");
            ls.Add("KCC-20");
            ls.Add("MUDRA-Simplified");
            ls.Add("MUDRA-Transport");
            ls.Add("KCC-Renewal");
            ls.Add("KCC-20-Renewal");

//            int id = int.Parse(_protector.Encode(Id));
            

            var app = _context.AccountInfo.Find(Id.PadLeft(17,'0'));

            app.Applicants = _context.ExistingApplicant.Where(a => a.AccountInfoId == Id.PadLeft(17, '0')).ToList();
           
            if (app.ProductDesc.Contains("KCC"))
            {
                ViewBag.LandDetails = _context.KCCExistingLand.Include(a=> a.KCCCrops).Where(a => a.AccountNo == Id).ToList();
                ViewBag.CropDetails = _context.KCCCropDetailExisting.Include(a=> a.KCCExistingLand).Where(a => a.AccountNo == Id).ToList();
                // ViewBag.CropDetails = _context.KCCCropDetails.Where(a => a.AccountNo == Id);
            }
           
           
           
            //var application = _context.Applications.Include(a => a.Applicants).Include(a => a.LoanApplications).Include(a => a.Documents).Include(a => a.Inspections).Include(a => a.Charges).Include(a => a.Disbursements).Include(a => a.Remarks).Include(a => a.Securities).Include(a => a.KCCLandDetails).Include(a => a.KCCCropDetails).FirstOrDefault(a => a.Id == id);
            //if (User.Identity.Name != app.OwnerUser)
            //{
            //    ViewBag.Editable = false;
            //}
            if (User.Identity.Name == "4729388")
            {
                ViewBag.Editable = true;
            }
            return View(app);
        }
       
        
        [Authorize]
        [HttpPost]
        public IActionResult UpdateEligiblity(int AppId,decimal Elig)
        {
            var app = _context.Applications.Find(AppId);
            app.Eligibility = Elig;
            _context.Entry(app).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok("Eligibility Saved");
        }

        [Authorize]
        public async Task<IActionResult> Sanction(String Id)
        {
            int id = int.Parse(_protector.Encode(Id));
            var application = _context.Applications.Include(a => a.Applicants).Include(a => a.LoanApplications).Include(a => a.Documents).Include(a => a.Inspections).Include(a => a.Disbursements).Include(a => a.Charges).Include(a=> a.Remarks).Include(a=> a.Securities).Where(a => a.Id == id).FirstOrDefault();
            ViewBag.Editable = await Check(id);
            Dictionary<int, List<CIBILLoanInfo>> result = new Dictionary<int, List<CIBILLoanInfo>>();
            foreach (var a in application.Applicants.Where(a=> a.Driving_Lic != "Deleted"))
            {
                var checkId = a.Id;
                var cibilReqId = _context.CIBILRequests.Where(b => b.ApplicantId == checkId && b.CiibilControlNumber != "Deleted").ToList().Max(z=> z.Id);
                //int cibilReqId = 0;
                result.Add(a.Id, _context.CIBILLoanInfo.Where(b => b.CIBILRequestId == cibilReqId).ToList());
            }
            ViewBag.LoanInfo = result;
            return View(application);
        }

        private List<Payment> GetPayments(double loanAmount , double interestRate , int numPayments)
        {
             

            // Calculate the monthly payment using the loan amount, interest rate, and number of payments
            double monthlyPayment = loanAmount * interestRate / (1 - 1 / Math.Pow(1 + interestRate, numPayments));

            // Create a list to store the results of each payment
            List<Payment> payments = new List<Payment>();

            // Loop through the number of payments, calculating the interest and principal for each payment
            for (int i = 0; i < numPayments; i++)
            {
                // Calculate the interest for this payment by multiplying the loan amount by the interest rate
                double interest = loanAmount * interestRate;

                // Calculate the principal for this payment by subtracting the interest from the monthly payment
                double principal = monthlyPayment - interest;

                // Update the loan amount by subtracting the principal from the loan amount
                loanAmount -= principal;

                // Create a new Payment object to store the results for this payment
                Payment payment = new Payment
                {
                    PaymentNumber = i + 1,
                    Interest = interest,
                    Principal = principal,
                    RemainingBalance = loanAmount
                };

                // Add the Payment object to the list
                payments.Add(payment);
            }
            return payments;
        }
        
        public async Task<IActionResult> Eligibility(int Id , string vt = "No")
        {
            var application = _context.Applications.Include(a => a.Applicants).Include(a => a.LoanApplications).Include(a => a.Documents).Include(a => a.Inspections).Include(a => a.Disbursements).Include(a => a.Charges).Include(a => a.Remarks).Include(a => a.Securities).Where(a => a.Id == Id).FirstOrDefault();
            //ViewBag.Editable = await Check(Id);
            Dictionary<int, List<CIBILLoanInfo>> result = new Dictionary<int, List<CIBILLoanInfo>>();
            foreach (var a in application.Applicants.Where(a => a.Driving_Lic != "Deleted" && a.TypeofApplicant != "Guarantor"))
            {
                var checkId = a.Id;
                var cibilReqId = _context.CIBILRequests.Where(b => b.ApplicantId == checkId && b.CiibilControlNumber != "Deleted"&& b.Score2 != "Failed").ToList().Max(z => z.Id);
                //int cibilReqId = 0;
                result.Add(a.Id, _context.CIBILLoanInfo.Where(b => b.CIBILRequestId == cibilReqId).ToList());
            }
            List<ProjectCost> pj = _context.ProjectCosts.Where(a => a.ApplicationId == Id).ToList();
            ViewBag.ProjectCost = pj;
            ViewBag.LoanInfo = result;
            ViewBag.VT = vt;
            if (application.LoanScheme == "Car")
            {

                return View("EligibilityCar",application);
            }
            else if (application.LoanScheme == "HL")
            {
               // var otherDetails = _context.Remarks.Where(b => b.ApplicationId == Id).ToList();
                return View("EligibilityHL", application);
            }
            else if (application.LoanScheme == "MUDRA-Transport")
            {
               
                _context.Entry(application).Reference(a => a.TPLDetails).Load();
                int repayTerms = 60;
                if (application.TPLDetails.TypeOfVehichle == "3 Wheeler")
                {
                    repayTerms = 48;
                }

                ViewBag.RepaySchedule = GetPayments((double)application.LoanApplications.Sum(a => a.AppliedAmount), (double)(application.LoanApplications.Where(a => a.TypeOfFacility == "TL").FirstOrDefault().RateOfInterest / 1200), repayTerms);

                // var otherDetails = _context.Remarks.Where(b => b.ApplicationId == Id).ToList();
                return View("EligibilityTPL", application);
            }
            else if (application.LoanScheme == "KCC" || application.LoanScheme == "KCC-20")
            {
                ViewBag.KCCAddl = _context.ProjectCosts.Where(a => a.ApplicationId == Id && a.Description == "KCC-Addl").Select(b => b.AppliedAmount).FirstOrDefault();
                ViewBag.KCCAppl = _context.ProjectCosts.Where(a => a.ApplicationId == Id && a.Description == "KCC").Select(b => b.AppliedAmount).FirstOrDefault();
                ViewBag.LandInfo = _context.KCCLandDetails.Where(a => a.ApplicationId == Id).ToList();
                ViewBag.CropInfo = _context.CropDetails.FromSqlRaw($"SELECT * from  loanflow.cropdetail({Id})").ToList(); 
                ViewBag.Kharif = _context.KCCEligillity.FromSqlRaw($"SELECT * from loanflow.cropcalculation('Kharif', {Id})").ToList();
                ViewBag.Rabi = _context.KCCEligillity.FromSqlRaw($"SELECT * from loanflow.cropcalculation('Rabi', {Id})").ToList();
                ViewBag.Summer = _context.KCCEligillity.FromSqlRaw($"SELECT * from loanflow.cropcalculation('Summer', {Id})").ToList();
                return View("EligibilityKCC", application);
            }
            return View(application);
        }
        public IActionResult CheckList(int Id)
        {
            var application = _context.Applications.Include(a => a.Applicants).Include(a => a.LoanApplications).Include(a => a.Remarks).Include(a => a.Securities).Where(a => a.Id == Id).FirstOrDefault();
            //ViewBag.Editable = await Check(Id);
            return View(application);
        }
        [Authorize]
        [HttpPost]
        public IActionResult SaveLoan(LoanApplication applicant)
        {

            var app = _context.LoanApplications.Find(applicant.Id);
            app.SanctionedAmount = applicant.SanctionedAmount;  
            app.RepayTerm = applicant.RepayTerm;    
            app.Repayment = applicant.Repayment;
            app.RateOfInterest = applicant.RateOfInterest;
            app.Moratorium = applicant.Moratorium;
            app.RepayStartDate = applicant.RepayStartDate;

            _context.Entry(app).State = EntityState.Modified;
            _context.SaveChanges();
            AddLog(app.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Approved Limit Added");
            return Ok("Saved");
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddB1(int AppnID , string docid , string khasra)
        {

            Document document = new Document();
            document.ApplicationId = AppnID;
            document.Details = $"B1 for Khasra {khasra}" ;
            document.FilePath = docid;
            document.UserId = User.Identity.Name;
            document.UploadDate = DateTime.Now;
            _context.Documents.Add(document);
            _context.SaveChanges();

            AddLog(AppnID, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", $"KCC B1 Added {khasra}");
            return Ok("Saved");
        }
        [Authorize]
        [HttpPost]
        public IActionResult SaveKCCLand(KCCLandDetail applicant)
        {

            var c = _context.KCCLandDetails.Where(a=> a.ApplicationId == applicant.ApplicationId && a.KhasraNo == applicant.KhasraNo && a.Village == applicant.Village ).FirstOrDefault();
            if (c != null)
            {
                c.TotalArea = applicant.TotalArea;
                c.IrrigatedArea = applicant.IrrigatedArea;
                c.UnIrrigatedArea = applicant.UnIrrigatedArea;
                c.NonAgriArea = applicant.NonAgriArea;
                c.SourceofIrrigation = applicant.SourceofIrrigation;
                _context.Entry(c).State = EntityState.Modified;
                _context.SaveChanges();
            }
            else
            {
                _context.KCCLandDetails.Add(applicant);
                _context.SaveChanges();
            }

            
            AddLog(applicant.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", $"KCC Land Added {applicant.KhasraNo}");
            return Ok("Saved");
        }
        
        [Authorize]
        [HttpPost]
        public IActionResult SaveKCCCrop(KCCCropDetail applicant)
        {

            var c = _context.KCCCropDetails.Where(a => a.KCCLandDetailId == applicant.KCCLandDetailId && a.Season == applicant.Season).FirstOrDefault();
            if (c != null)
            {
                c.SOFIrrigatedArea = applicant.SOFIrrigatedArea;
                c.SOFUnIrrigatedArea = applicant.SOFUnIrrigatedArea;
                c.IrrigatedArea = applicant.IrrigatedArea;
                c.UnIrrigatedArea = applicant.UnIrrigatedArea;
                c.Crop = applicant.Crop;
                c.CropInsurance = applicant.CropInsurance;
                _context.Entry(c).State = EntityState.Modified;
                _context.SaveChanges();
            }
            else
            {
                _context.KCCCropDetails.Add(applicant);
                _context.SaveChanges();
            }


            AddLog(applicant.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", $"KCC Crop Added");
            return Ok("Saved");
        }
        [Authorize]
        [HttpPost]
        public IActionResult SaveKCCCropExisting(KCCCropDetailExisting applicant)
        {

            var c = _context.KCCCropDetailExisting.Where(a => a.KCCExistingLandId == applicant.KCCExistingLandId && a.Season == applicant.Season).FirstOrDefault();
            if (c != null)
            {
                c.SOFIrrigatedArea = applicant.SOFIrrigatedArea;
                c.SOFUnIrrigatedArea = applicant.SOFUnIrrigatedArea;
                c.IrrigatedArea = applicant.IrrigatedArea;
                c.UnIrrigatedArea = applicant.UnIrrigatedArea;
                c.Crop = applicant.Crop;
                c.CropInsurance = applicant.CropInsurance;
                _context.Entry(c).State = EntityState.Modified;
                _context.SaveChanges();
            }
            else
            {
                _context.KCCCropDetailExisting.Add(applicant);
                _context.SaveChanges();
            }


            //AddLog(applicant.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", $"KCC Crop Added");
            return Ok("Saved");
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddMUDRA(MudraDetail applicant)
        {

            var c = _context.MudraDetails.Where(a => a.ApplicationId == applicant.ApplicationId && a.Parameter == applicant.Parameter).FirstOrDefault();
            if (c != null)
            {
                c.PrevYear = applicant.PrevYear;
                c.CurrentYear = applicant.CurrentYear;
                c.Estimate1 = applicant.Estimate1;
                c.Estimate2 = applicant.Estimate2;
                
                _context.Entry(c).State = EntityState.Modified;
                _context.SaveChanges();
            }
            else
            {
                _context.MudraDetails.Add(applicant);
                _context.SaveChanges();
            }


            AddLog(applicant.ApplicationId, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", $"Mudra Details Added");
            return Ok("Saved");
        }
        [Authorize]
        public async Task<IActionResult> ReadyForSanction(string Id)
        {
            int id = int.Parse(_protector.Encode(Id));
            var application = _context.Applications.Find(id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var loanLimitAdded = _context.LoanApplications.Where(a=> a.ApplicationId == id && a.SanctionedAmount > 0).Count();
            var charges = _context.Charges.Where(a => a.ApplicationId == id).Count();
            var applicants = _context.Applicants.Where(a=> a.ApplicationId == id && a.Driving_Lic != "Deleted").Count();
            if (!(user.Designation == "Branch Manager" || user.Designation == "AMH Head" || user.Designation == "AMH 2nd Officer" || user.Designation == "Manager Business"))
            {
                return Ok("User does not have delegation powers.");
            }
            if (loanLimitAdded > 0 && charges >= 0 && applicants > 0)
            {
                return Ok("Yes");
            }

            return Ok("Loan Limit not approved");
        }

        [Authorize]

        public ActionResult MarkControl(string Id)
        {
            int id = int.Parse(_protector.Encode(Id));
            var application = _context.Applications.Find(id);
            application.ControlledDate = DateTime.Now;
            application.ControlStatus = "Controlled";
            _context.Entry(application).State = EntityState.Modified;
            _context.SaveChanges();
            AddLog(id, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", "Control");
            return Ok("Done");

        }


        [Authorize]
        public async Task<IActionResult> UpdateStatus(string Id,string action1)
        {
            int id = int.Parse(_protector.Encode(Id));
            var application = _context.Applications.Find(id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userBranch = _context.Branches.Find(user.BranchId);
            if (action1 == "Return")
            {
                if (application.Owner != "Branch")
                {
                    application.Owner = "Branch";
                    application.Status = "Pending";
                    _context.Entry(application).State = EntityState.Modified;
                    _context.SaveChanges();

                    AddLog(id, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", action1);
                }
                else
                {
                    application.Owner = "Customer";
                    application.Status = "Returned";
                    _context.Entry(application).State = EntityState.Modified;
                    _context.SaveChanges();

                    AddLog(id, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", action1);
                }
            }
            else if (action1 == "Reject")
            {
                application.Owner = "Customer";
                application.Status = "Rejected";
                _context.Entry(application).State = EntityState.Modified;
                _context.SaveChanges();

                AddLog(id, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", action1);
            }
            else if (action1 == "Sanction")
            {
                if (user.Designation == "Branch Manager" || user.Designation == "AMH Head" || user.Designation == "AMH 2nd Officer" || user.Designation == "Manager Advance" || user.Designation == "Manager Business")
                {

                    application.SanctionedLevel = application.Owner;
                    application.Owner = "Branch";
                    application.Status = "Sanctioned";
                    application.ControlStatus = "Pending";
                    application.SanctioningLevel = user.Designation;
                    application.SanctioningUserId = user.UserName;   
                    application.SanctionedDate = DateTime.Now;
                    _context.Entry(application).State = EntityState.Modified;
                    _context.SaveChanges();

                    AddLog(id, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", action1);
                }
                
            }
            else if (action1 == "SendToControl")
            {
                    application.SendToControlDate = DateTime.Now;
                    application.ControlStatus = "SendToControl";
                    _context.Entry(application).State = EntityState.Modified;
                    _context.SaveChanges();
                    AddLog(id, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", action1);
                    return RedirectToAction("ControlReturn", new { msg = "Sent to Control" });

            }
            else if (action1 == "ControlReturn")
            {
                
                application.ControlStatus = "Control Returned";
                _context.Entry(application).State = EntityState.Modified;
                _context.SaveChanges();
                AddLog(id, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", action1);

            }
            else if (action1 == "Control")
            {
                bool checkControl = false;
                if (application.SanctionedLevel == "Branch" || application.SanctionedLevel == "AMH")
                {
                    if (userBranch.BrType == "RO" && (user.Designation == "Manager Business" || user.Designation == "Regional Manager"))
                    {
                        checkControl = true;
                    }
                }
                if (application.SanctionedLevel == "RO")
                {
                    if (userBranch.BrType == "HO" && (user.Designation == "Manager Business" || user.Designation == "Regional Manager"))
                    {
                        checkControl = true;
                    }
                }
                if (checkControl)
                {
                    application.ControlledDate = DateTime.Now;
                    application.ControlStatus = "Controlled";
                    _context.Entry(application).State = EntityState.Modified;
                    _context.SaveChanges();
                    //AddLog(id, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", action1);
                    return RedirectToAction("PendingControl", new { msg = "Application Controlled" });
                }
                return RedirectToAction("PendingControl", new { msg = "Control Not Allowed" });
            }
            else if (action1 == "Forward")
            {
                if (application.Owner == "AMH")
                {
                    application.Owner = "RO";
                    application.Status = "Pending";
                    application.ForwardedDate = DateTime.Now;   
                    _context.Entry(application).State = EntityState.Modified;
                    _context.SaveChanges();

                    AddLog(id, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", action1);
                }
                else if (application.Owner == "RO")
                {
                    application.Owner = "HO";
                    application.ForwardedDate = DateTime.Now;
                    application.Status = "Pending";
                    _context.Entry(application).State = EntityState.Modified;
                    _context.SaveChanges();

                    AddLog(id, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", action1);
                }
                else {
                    application.Owner = "AMH";
                    application.Status = "Pending";
                    application.ForwardedDate = DateTime.Now;
                    _context.Entry(application).State = EntityState.Modified;
                    _context.SaveChanges();

                    AddLog(id, User.Identity.Name, Request.HttpContext.Connection.RemoteIpAddress.ToString(), "Portal", action1);
                }
               
            }
            return RedirectToAction("Application", new { Id = Id });
        }
    }
}
