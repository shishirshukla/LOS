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
//using MobileBackend.Models.CIBILNew;
using MobileBackend.Models.CIBILV1;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization; 
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MobileBackend.Models.PAN;
using System.Net;
using MobileBackend.Models.CIBILRespone;
using MobileBackend.Models.Cibil3;

namespace MobileBackend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        public HomeController(IWebHostEnvironment appEnvironment,ILogger<HomeController> logger , UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration,ApplicationDbContext  context)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
            _appEnvironment = appEnvironment;
        }
       
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }
        private  async Task<string> SaveImageAsBase64(string imageUrl )
        {
          
            var proxy = new WebProxy
            {
                Address = new Uri($"http://10.43.5.6:3128"),
                BypassProxyOnLocal = false,
                UseDefaultCredentials = false,
            };



            // Create a client handler that uses the proxy
            var httpClientHandler = new HttpClientHandler
            {
                Proxy = proxy,
            };
            try
            {
                Console.WriteLine($"Google API | Client initiated");

                using (HttpClient client = new HttpClient(httpClientHandler))
                {
                    // Fetch the image from the URL
                    HttpResponseMessage response = await client.GetAsync(imageUrl);
                    Console.WriteLine($"Google API | Inside Using");
                    if (response.IsSuccessStatusCode)
                    {
                        byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

                        // Convert the image to base64
                        string base64Image = Convert.ToBase64String(imageBytes);

                        // Save the base64 data to a file
                       return base64Image;
                    }
                    else
                    {
                        Console.WriteLine("Google API | Failed to retrieve image. HTTP status code: " + response.StatusCode);
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Google API | An error occurred: " + ex.Message);
                return "";
            }
        }
        public async Task<IActionResult> GetMap()
        {
            var s = await SaveImageAsBase64("https://maps.googleapis.com/maps/api/staticmap?markers=color:red|21.1636016,81.7702507&zoom=16&size=600x600&key=AIzaSyC_jI2iRe__2DPzkCXT-wEQmUWuoRjAFog");
            ViewBag.Img = s;
            return View();
        }


        [Authorize]
        [HttpPost]
        public IActionResult GetPostSanction(string acc)
        {
            var ll = _context.PostSanctionVisits.Where(a => a.AccountNo == acc.PadLeft(17,'0')).ToList();

            return View(ll);
        }

        [Authorize]
        public async Task<IActionResult> ViewPostReport(int Id)
        {
            var ll = _context.PostSanctionVisits.Find(Id);
            var s = await SaveImageAsBase64($"https://maps.googleapis.com/maps/api/staticmap?markers=color:red|{ll.latlong.Replace('_', ',')}&zoom=16&size=600x600&key=AIzaSyC_jI2iRe__2DPzkCXT-wEQmUWuoRjAFog");
            ViewBag.Img = s;
            var ac = _context.AccountData.FromSqlRaw($"SELECT account_no,  acctopendate, currentbalance,  odlimit,  accounttype, interestcat, intrate,name,cust_name 	FROM public.temp_loan Where upload_date = (select max(upload_date)  FROM public.temp_loan) and account_no = LPAD('{ll.AccountNo}',17,'0')").FirstOrDefault();
            ViewBag.AccountDetails = ac;
            return View(ll);
        }


        [Authorize]
        public IActionResult ValidatePAN()
        {
            ViewBag.Response = "";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ValidatePAN(string PAN)
        {
            PANRpac man = new PANRpac();
            man.entity = PAN;
            var js = Newtonsoft.Json.JsonConvert.SerializeObject(man);
            var o1 = new RestClientOptions();
            o1.Proxy = new WebProxy("10.43.5.6:3128");
            var client = new RestClient(o1);
            //var client = new RestClient();
            var request1 = new RestRequest("https://api.rpacpc.com/services/get-adv-lite");
            request1.AddHeader("Accept", "*/*");
            request1.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request1.AddHeader("Connection", "keep-alive");

            request1.AddHeader("secretkey", _configuration["rpac_secret"]);
            request1.AddHeader("token", _configuration["rpac_token"]);
            request1.AddHeader("Content-Type", "application/json");
            request1.AddBody(js, "application/json");
            var token_response = await client.ExecuteAsync(request1, Method.Post);

            PANRpac206 man1 = new PANRpac206();
            man1.pancard = PAN;
            var js1 = Newtonsoft.Json.JsonConvert.SerializeObject(man1);

            var request2 = new RestRequest("https://api.rpacpc.com/services/get-ab-details");
            request2.AddHeader("Accept", "*/*");
            request2.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request2.AddHeader("Connection", "keep-alive");

            request2.AddHeader("secretkey", _configuration["rpac_secret"]);
            request2.AddHeader("token", _configuration["rpac_token"]);
            request2.AddHeader("Content-Type", "application/json");
            request2.AddBody(js1, "application/json");
            var token_response1 = await client.ExecuteAsync(request2, Method.Post);

            PANValidation pan = new PANValidation();
            pan.UserId = User.Identity.Name;
            pan.PANNo = PAN;
            pan.response1 = token_response.Content;
            pan.response2 = token_response1.Content;
            pan.req_date = DateTime.Now;
            _context.PANValidation.Add(pan);
            _context.SaveChanges();


            ViewBag.Response = Newtonsoft.Json.JsonConvert.DeserializeObject<C206AB>(token_response1.Content); 

            return View(Newtonsoft.Json.JsonConvert.DeserializeObject<RPACRoot>(token_response.Content));
        }

        public async Task<IActionResult> UpdateMandateStatus()
        {
            
            var mans = _context.Mandates.Where(a => a.api_response_id == "success" && (a.mandate_status == "Created" || a.mandate_status == "NA")).ToList();
            foreach (var item in mans)
            {
                CheckMandate man = new CheckMandate();
                man.emandate_id = item.emandate_id;
                var js = Newtonsoft.Json.JsonConvert.SerializeObject(man);
                var o1 = new RestClientOptions();
                o1.Proxy = new WebProxy("10.43.5.6:3128");
                var client = new RestClient(o1);
                var request1 = new RestRequest("https://api.signdesk.in/api/live/getemandatestatus");
                request1.AddHeader("Accept", "*/*");
                request1.AddHeader("Accept-Encoding", "gzip, deflate, br");
                request1.AddHeader("Connection", "keep-alive");

                request1.AddHeader("x-parse-rest-api-key", _configuration["EmandateKey"]);
                request1.AddHeader("x-parse-application-id", _configuration["EmandateApp"]);
                request1.AddHeader("Content-Type", "application/json");
                request1.AddBody(js, "application/json");

                var token_response = await client.ExecuteAsync(request1, Method.Post);
                if (token_response.StatusCode == HttpStatusCode.OK)
                {
                    var st = token_response.Content.ToString();
                    var d = Newtonsoft.Json.JsonConvert.DeserializeObject<MandateCheckResponse>(st);
                    item.mandate_status = d.mandate_status;
                    item.umrn_mandate = d.umrn;
                    _context.Entry(item).State = EntityState.Modified;
                    _context.SaveChanges();
                }

            }

            var mans1 = _context.ExistingAcMandates.Where(a => a.api_response_id == "success" && (a.mandate_status == "Created" || a.mandate_status == "NA")).ToList();
            foreach (var item in mans1)
            {
                CheckMandate man = new CheckMandate();
                man.emandate_id = item.emandate_id;
                var js = Newtonsoft.Json.JsonConvert.SerializeObject(man);
                var o1 = new RestClientOptions();
                o1.Proxy = new WebProxy("10.43.5.6:3128");
                var client = new RestClient(o1);
                var request1 = new RestRequest("https://api.signdesk.in/api/live/getemandatestatus");
                request1.AddHeader("Accept", "*/*");
                request1.AddHeader("Accept-Encoding", "gzip, deflate, br");
                request1.AddHeader("Connection", "keep-alive");

                request1.AddHeader("x-parse-rest-api-key", _configuration["EmandateKey"]);
                request1.AddHeader("x-parse-application-id", _configuration["EmandateApp"]);
                request1.AddHeader("Content-Type", "application/json");
                request1.AddBody(js, "application/json");

                var token_response = await client.ExecuteAsync(request1, Method.Post);
                if (token_response.StatusCode == HttpStatusCode.OK)
                {
                    var st = token_response.Content.ToString();
                    var d = Newtonsoft.Json.JsonConvert.DeserializeObject<MandateCheckResponse>(st);
                    item.mandate_status = d.mandate_status;
                    item.umrn_mandate = d.umrn;
                    _context.Entry(item).State = EntityState.Modified;
                    _context.SaveChanges();
                }

            }

            return Ok();
        }

        public async Task<IActionResult> UpdateUMRN()
        {

            var mans = _context.Mandates.Where(a => a.api_response_id == "success" && a.mandate_status == "Registered" && a.umrn_mandate == null).ToList();
            foreach (var item in mans)
            {
                CheckMandate man = new CheckMandate();
                man.emandate_id = item.emandate_id;
                var js = Newtonsoft.Json.JsonConvert.SerializeObject(man);
                var o1 = new RestClientOptions();
                o1.Proxy = new WebProxy("10.43.5.6:3128");
                var client = new RestClient(o1);
                var request1 = new RestRequest("https://api.signdesk.in/api/live/getemandatestatus");
                request1.AddHeader("Accept", "*/*");
                request1.AddHeader("Accept-Encoding", "gzip, deflate, br");
                request1.AddHeader("Connection", "keep-alive");

                request1.AddHeader("x-parse-rest-api-key", _configuration["EmandateKey"]);
                request1.AddHeader("x-parse-application-id", _configuration["EmandateApp"]);
                request1.AddHeader("Content-Type", "application/json");
                request1.AddBody(js, "application/json");

                var token_response = await client.ExecuteAsync(request1, Method.Post);
                if (token_response.StatusCode == HttpStatusCode.OK)
                {
                    var st = token_response.Content.ToString();
                    var d = Newtonsoft.Json.JsonConvert.DeserializeObject<MandateCheckResponse>(st);
                    item.mandate_status = d.mandate_status;
                    item.umrn_mandate = d.umrn;
                    _context.Entry(item).State = EntityState.Modified;
                    _context.SaveChanges();
                }

            }

            var mans1 = _context.ExistingAcMandates.Where(a => a.api_response_id == "success" && a.mandate_status == "Registered" && a.umrn_mandate == null).ToList();
            foreach (var item in mans1)
            {
                CheckMandate man = new CheckMandate();
                man.emandate_id = item.emandate_id;
                var js = Newtonsoft.Json.JsonConvert.SerializeObject(man);
                var o1 = new RestClientOptions();
                o1.Proxy = new WebProxy("10.43.5.6:3128");
                var client = new RestClient(o1);
                var request1 = new RestRequest("https://api.signdesk.in/api/live/getemandatestatus");
                request1.AddHeader("Accept", "*/*");
                request1.AddHeader("Accept-Encoding", "gzip, deflate, br");
                request1.AddHeader("Connection", "keep-alive");

                request1.AddHeader("x-parse-rest-api-key", _configuration["EmandateKey"]);
                request1.AddHeader("x-parse-application-id", _configuration["EmandateApp"]);
                request1.AddHeader("Content-Type", "application/json");
                request1.AddBody(js, "application/json");

                var token_response = await client.ExecuteAsync(request1, Method.Post);
                if (token_response.StatusCode == HttpStatusCode.OK)
                {
                    var st = token_response.Content.ToString();
                    var d = Newtonsoft.Json.JsonConvert.DeserializeObject<MandateCheckResponse>(st);
                    item.mandate_status = d.mandate_status;
                    item.umrn_mandate = d.umrn;
                    _context.Entry(item).State = EntityState.Modified;
                    _context.SaveChanges();
                }

            }

            return Ok();
        }

        public async Task<IActionResult> Document(string docType , int AppId , string loanType = "", string docId = "")
        {
            HttpClient httpClient = new HttpClient();
            ViewBag.IsScore = false;
            ViewBag.ShowAccept = false;
            var app = _context.Applications.Find(AppId);
            if (docType == "scoreCard"  )
            {
                var docURL1 = $"http://127.0.0.1:2000/scoreCard/getScore?appID={AppId}";
                var c = await httpClient.GetAsync(docURL1);
                ViewBag.Doc = "NILL";
                ViewBag.IsScore = true;
                ViewBag.AppId = AppId;
                ViewBag.DocumentType = await c.Content.ReadAsStringAsync();
                if (app.Status == "Pending")
                {
                    ViewBag.ShowAccept = true;
                }
               
                return View();
            }

           
            //if (loanType == "KCC")
            //{
            //    if (docType == "preSanction")
            //    {
            //        docId = "5";
            //    }
            //    var docURLKCC = $"http://127.0.0.1:2000/kccDoc/getDoc?appID={AppId}&LoanType={loanType}&docId={docId}";
            //    var s = await httpClient.GetAsync(docURLKCC);
            //    var tt = Guid.NewGuid().ToString() + ".pdf";
            //    var filePath = Path.Join(_appEnvironment.WebRootPath, tt);
            //    if (System.IO.File.Exists(filePath))
            //    {
            //        System.IO.File.Delete(filePath);
            //    }
            //    using (var fs = new FileStream(filePath, FileMode.CreateNew))
            //    {
            //        await s.Content.CopyToAsync(fs);
            //    }
            //    ViewBag.Doc = tt;
            //    return View();
            //}
            var docURL = $"http://127.0.0.1:2000/docGenerate/{docType}?appID={AppId}&LoanType={loanType}&docId={docId}";
            
            if (docType == "preSanction" || docType == "loanForm" || docType == "processNote")
            {
                var c = await httpClient.GetAsync(docURL);
                ViewBag.Doc = "NILL";
                ViewBag.DocumentType = await c.Content.ReadAsStringAsync();
                return View();
            }
            else
            {
                if (loanType.Contains("KCC"))
                {
                    var docURL1 = $"http://127.0.0.1:2000/kccDoc/{docType}?appID={AppId}&LoanType={loanType}&docId={docId}";
                    var s = await httpClient.GetAsync(docURL1);
                    var tt = Guid.NewGuid().ToString() + ".pdf";
                    var filePath = Path.Join(_appEnvironment.WebRootPath, tt);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    using (var fs = new FileStream(filePath, FileMode.CreateNew))
                    {
                        await s.Content.CopyToAsync(fs);
                    }
                    ViewBag.Doc = tt;
                    return View();
                }
               
                else
                {
                    var s = await httpClient.GetAsync(docURL);
                    var tt = Guid.NewGuid().ToString() + ".pdf";
                    var filePath = Path.Join(_appEnvironment.WebRootPath, tt);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    using (var fs = new FileStream(filePath, FileMode.CreateNew))
                    {
                        await s.Content.CopyToAsync(fs);
                    }
                    ViewBag.Doc = tt;
                    return View();
                }
                
            }
            
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            Console.WriteLine("LMS | In Dashboard");
            List<string> list = new List<string>();
            var brInfo = _context.Branches.Find(user.BranchId);
            ViewBag.UserInfo = $"{user.EmployeeName} ({user.UserName}) , Branch : {user.BranchId} ({brInfo.BrType})";
            ViewBag.UserDetail = user;
            ViewBag.BrInfo = brInfo;
            ViewBag.Report111 = new List<ChartReport>();
            ViewBag.Report1111 = new List<ChartReport>();
            if (brInfo.BrType == "Branch")
            {
                list.Add(brInfo.Id);
            }
            else if (brInfo.BrType == "AMH")
            {
                list.AddRange(_context.Branches.Where(a => a.AMHCode == user.BranchId).Select(a => a.Id).ToList());
            }
            else if (brInfo.BrType == "HO" )
            {
                list.AddRange(_context.Branches.Select(a => a.Id).ToList());
                var report_amh = _context.Report.FromSqlRaw($"SELECT * from  loanflow.amhstatus('HO');").ToList();
                ViewBag.Report = report_amh;
                var report_br = _context.Report.FromSqlRaw($"SELECT * from  loanflow.brstatus('HO');").ToList();
                ViewBag.ReportBr = report_br;
                var report_report = _context.Report.FromSqlRaw($"SELECT * from  loanflow.rostatus();").ToList();
                ViewBag.Report1 = report_report;
                var scheme_report = _context.SchemeReport.FromSqlRaw($"SELECT * from loanflow.schemewise_with_pre_sanction('HO')").ToList();
                ViewBag.Report11 = scheme_report;
                var chart_report = _context.ChartReports.FromSqlRaw($"SELECT * from  loanflow.application_chart('HO')").ToList();
                ViewBag.Report111 = chart_report;
                var chart_report2 = _context.ChartReports.FromSqlRaw($"SELECT * from  loanflow.sanction_chart('HO')").ToList();
                ViewBag.Report1111 = chart_report2;
            }
            else
            {
                var report_amh = _context.Report.FromSqlRaw($"SELECT * from  loanflow.amhstatus('{brInfo.RegionalOffice}');").ToList();
                ViewBag.Report = report_amh;
                var report_br = _context.Report.FromSqlRaw($"SELECT * from  loanflow.brstatus('{brInfo.RegionalOffice}');").ToList();
                ViewBag.ReportBr = report_br;
                var report_report = _context.Report.FromSqlRaw($"SELECT * from  loanflow.rostatus();").ToList();
                ViewBag.Report1 = report_report;
                var scheme_report = _context.SchemeReport.FromSqlRaw($"SELECT * from loanflow.schemewise_with_pre_sanction('{brInfo.RegionalOffice}')").ToList();
                ViewBag.Report11 = scheme_report;
                var chart_report = _context.ChartReports.FromSqlRaw($"SELECT * from  loanflow.application_chart('{brInfo.RegionalOffice}')").ToList();
                ViewBag.Report111 = chart_report;
                var chart_report2 = _context.ChartReports.FromSqlRaw($"SELECT * from  loanflow.sanction_chart('{brInfo.RegionalOffice}')").ToList();
                ViewBag.Report1111 = chart_report2;
                list.AddRange(_context.Branches.Where(a => a.RegionalOffice == user.BranchDetails.RegionalOffice && a.BrType == "Branch").Select(a => a.Id).ToList());
            }
            Console.WriteLine("LMS | In Dashboard Reports Done");
            var applications = _context.Applications.Include(a=> a.Inspections).Include(a=> a.Branch).Where(a => list.Contains(a.BranchId)).ToList();
            var appCnt = _context.Applications.Where(a => a.OwnerUser == user.UserName && a.Status == "Pending").Count();
            ViewBag.Applications = applications.Where(a=> a.Status == "Pending").Count();
            ViewBag.ApplicationsNI = applications.Where(a => a.Status == "Pending" && a.Inspections.Count > 0).Count();
            ViewBag.ApplicationsF = applications.Where(a => a.Status == "Pending" && a.Owner != "Branch").Count();
            ViewBag.ApplicationsS = applications.Where(a => a.Status == "Sanctioned").Count();
            ViewBag.PendingApps = applications.Where(a => a.Status == "Pending").OrderBy(u => u.ApplicationDate).Take(10).ToList();
            ViewBag.RecentActivity = _context.Logs.Where(a=> a.UserId== user.UserName).OrderByDescending(u => u.Timestamp).Take(10).ToList();
            ViewBag.BranchType = brInfo.BrType;
            ViewBag.PendingControl = applications.Where(a => a.Status == "Sanctioned" && a.ControlStatus == "Pending").Count();
            ViewBag.SendToControl = applications.Where(a => a.Status == "Sanctioned" && a.ControlStatus == "SendToControl").Count();
            ViewBag.Controlled = applications.Where(a => a.Status == "Sanctioned" &&  a.ControlStatus == "Controlled").Count();
            ViewBag.ControlReturn = applications.Where(a => a.Status == "Sanctioned" &&  a.ControlStatus == "Control Returned").Count();
            ViewBag.AppCount = appCnt;
            var tplleads = _context.TPLLeads.Include(a => a.Branch).Where(a => list.Contains(a.BranchId) && a.LeadStatus == "Sourced").ToList();
            var kccleads = _context.KCCLeads.Include(a => a.Branch).Where(a => list.Contains(a.BranchId) && a.LeadStatus == "Sourced").ToList();
            var goldleads = _context.GoldLeads.Include(a => a.Branch).Where(a => list.Contains(a.BranchId) && a.LeadStatus == "Sourced").ToList();
            var genLeads = _context.CommonLeads.Include(a => a.Branch).Where(a => list.Contains(a.BranchId)).ToList();
            int Leads = tplleads.Count() + kccleads.Count() + goldleads.Count() + genLeads.Count();
            ViewBag.LeadCount = Leads;
            Console.WriteLine("LMS | Going to View");
            return View();
        }

        string parseCibilDate(string c)
        {
            return c;
        }

        public IActionResult CreditScore(int Id,string c = "NO")
        {
            string json;
            var t = _context.CIBILRequests.Find(Id);
            var app = _context.Applicants.Find(t.ApplicantId);
            json = t.ResponseFileName;
            ViewBag.ApplicationId = app.ApplicationId;
            var d = new DateTime(2023, 01,3, 17, 00, 0);

            if (DateTime.Parse(t.RequestDate) > d.Date)
            {
                var resp = JsonConvert.DeserializeObject<Models.Cibil3.Root>(json);
                ViewBag.Score = t.ScoreName1;
                ViewBag.Id = Id;
                if (c != "NO")
                {
                    ViewBag.Request = t.Score1;
                    ViewBag.Response = json;
                }
                else
                {
                    ViewBag.Request = "";
                    ViewBag.Response = "";
                }

                return View("CreditScoreNew", resp);
            }
            else
            {
                var resp = JsonConvert.DeserializeObject<CIBILV2.CibilResponseV2>(json);
                ViewBag.Score = t.ScoreName1;
                ViewBag.Id = Id;
                if (c != "NO")
                {
                    ViewBag.Request = t.Score1;
                    ViewBag.Response = json;
                }
                else
                {
                    ViewBag.Request = "";
                    ViewBag.Response = "";
                }

                return View(resp);
            }
            
        }
        public IActionResult RawCibil(int Id)
        {
            string json;
            var t = _context.CIBILRequests.Find(Id);
            json = t.Score2;
            ViewBag.Score = json;
            
            return View();
        }

        public IActionResult IDVReport(int Id)
        {
            string json;
            var t = _context.CIBILRequests.Find(Id);
            json = t.Score3;
            ViewBag.Score = json;
            return View("RawCibil");
        }

        public IActionResult UpdateEMI(int Id)
        {
            ViewBag.ReqId = Id;
            var resp = _context.CIBILLoanInfo.Where(a=> a.CIBILRequestId == Id).ToList();
            return View(resp);
        }
        [HttpPost]
        public IActionResult UpdateEMI(IFormCollection c , int RequestId)
        {
            foreach (var item in c)
            {
                if (item.Key.Contains("EMI-"))
                {
                    var id = int.Parse(item.Key.Split("-")[1]);
                    var req = _context.CIBILLoanInfo.Find(id);
                    if (req != null)
                    {
                        req.EMIConsidered = int.Parse(item.Value);
                        _context.Entry(req).State = EntityState.Modified;
                        _context.SaveChanges();
                    }
                }
                else if (item.Key.Contains("Remark-"))
                {
                    var id = int.Parse(item.Key.Split("-")[1]);
                    var req = _context.CIBILLoanInfo.Find(id);
                    if (req != null)
                    {
                        req.EmployeeResponse = item.Value;
                        _context.Entry(req).State = EntityState.Modified;
                        _context.SaveChanges();
                    }
                }
            }
            return RedirectToAction("CreditScore", new { Id = RequestId });
        }
        public async Task<IActionResult> OnlineEligibility(int Id)
        {
            using (Npgsql.NpgsqlConnection con = new Npgsql.NpgsqlConnection("Host=10.80.1.161;Username=shishir;Password=dev$123;Database=loanflow; Command Timeout = 30000; KeepAlive = 30000;Timeout=0"))
            {
                string loanDetails;
                Single emi = 0;
                Single nmi = 0;
                using (var cmd = new Npgsql.NpgsqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = $"SELECT account_info,employment,net_monthly_income,net_income_itr,emi,response	FROM public.\"Online_Loan_Capture\" Where app_id={Id}";
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        loanDetails = reader.GetString(0);

                    }
                    reader.Close();
                    cmd.Dispose();
                }

                return Ok("OK");
            }


                
        }

            public async Task<IActionResult> FetchCibil(int Id)
        {
            using (Npgsql.NpgsqlConnection con = new Npgsql.NpgsqlConnection("Host=10.80.1.161;Username=shishir;Password=dev$123;Database=loanflow; Command Timeout = 30000; KeepAlive = 30000;Timeout=0"))
            {
                con.Open();
                string DemoRequest = "";
            var filePath = Path.Join(_appEnvironment.WebRootPath, "request.txt");
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
            {
                DemoRequest = sr.ReadToEnd();
            }

            var requestNew = JsonConvert.DeserializeObject<Models.CIBILNew.CibilRequestNew>(DemoRequest);

            
                using (var cmd = new Npgsql.NpgsqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = $"SELECT name,pan_no, dob , gender,  address1, address2, city, pincode, mobile_no , amount	FROM public.\"Online_Loan_Capture\" Where app_id={Id}";
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        requestNew.Fields.Applicants.Applicant.Identifiers.Identifier.Add(new Models.CIBILNew.Identifier() { IdType = "01", IdNumber = reader.GetString(1) });
                        requestNew.Fields.Applicants.Applicant.ApplicantFirstName = reader.GetString(0);
                        requestNew.Fields.Applicants.Applicant.ApplicantMiddleName = "";
                        requestNew.Fields.Applicants.Applicant.ApplicantLastName = "";
                        requestNew.Fields.Applicants.Applicant.DateOfBirth = DateTime.Parse(reader.GetDate(2).ToString()).ToString("ddMMyyyy");
                        requestNew.Fields.Applicants.Applicant.Gender = reader.GetValue(3).ToString();
                        requestNew.Fields.Applicants.Applicant.Addresses.Address.AddressLine1 = reader.GetString(4);
                        requestNew.Fields.Applicants.Applicant.Addresses.Address.AddressLine2 = reader.GetString(5);
                        requestNew.Fields.Applicants.Applicant.Addresses.Address.City = reader.GetString(6);
                        requestNew.Fields.Applicants.Applicant.Addresses.Address.StateCode = "28";
                        requestNew.Fields.Applicants.Applicant.Addresses.Address.PinCode = reader.GetValue(7).ToString();
                        requestNew.Fields.Applicants.Applicant.Telephones.Telephone.TelephoneType = "01";
                        requestNew.Fields.Applicants.Applicant.Telephones.Telephone.TelephoneNumber = reader.GetString(8);

                    }
                    reader.Close();
                    cmd.Dispose();
                }
            

     

            
            


            string requestJson = JsonConvert.SerializeObject(requestNew);
            var client = new RestClient();
            var request1 = new RestRequest("https://www.test.transuniondecisioncentre.co.in/DC/TUCL/TU.DE.Pont/Token");
            request1.AddHeader("MediaType", "application/json");
            request1.AddHeader("Content-Type", "");
            request1.AddHeader("Cookie", "NSC_VBU_UVTTQM_ED_UVDM_EDQpsubm=14b5a3d9d9b5b9058d0b5e99152e05d07f58eecc38d1438e01f044a4535271aa94f4c055");
            var body = @"grant_type=password&username=BR0460GO01_UAT001&password=xu#2)_]G@2[#M";
            request1.AddParameter("text/plain", body, ParameterType.RequestBody);
            var token_response = await client.ExecuteAsync(request1, Method.Post);
            string token = token_response.Content.Split(':')[1].Split(',')[0].Replace('"', ' ').Trim();


            // Debug.WriteLine(token);
            var o = new RestClientOptions("https://www.test.transuniondecisioncentre.co.in/DC/TUCL/TU.DE.Pont/Applications");

            var cl = new RestClient(o);
            var request = new RestRequest("https://www.test.transuniondecisioncentre.co.in/DC/TUCL/TU.DE.Pont/Applications", Method.Post);

            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Content-Type", "application/json");

            request.AddBody(requestJson, "application/json");

            var a = await cl.ExecuteAsync(request, Method.Post);
            var resp = JsonConvert.DeserializeObject<CIBILV2.CibilResponseV2>(a.Content);
            Debug.WriteLine(a.Content);
            CIBILRequest cIBILRequest = new CIBILRequest();
            Models.CIBILResponseNew.CreditReport credit = new Models.CIBILResponseNew.CreditReport();
            try
            {
                string IDVDocId = "";
                foreach (var item in resp.Fields.Applicants.ApplicantList[0].Services.ServiceList[0].Operations.OperationList)
                {
                    if (item.Name.Trim() == "IDV")
                    {
                        if (item.Data.Response.RawResponse.DsIDVision.Document != null)
                        {
                            IDVDocId = item.Data.Response.RawResponse.DsIDVision.Document.Id.ToString();
                        }

                    }
                }
                string creditScoreDocId = resp.Fields.Applicants.ApplicantList[0].Services.ServiceList[0].Operations.OperationList.Where(a => a.Name == "ConsumerCIR").FirstOrDefault().Data.Response.RawResponse.Document.Id.ToString();
                //string IDVDocId = resp.Fields.Applicants.ApplicantList[0].Services.ServiceList[0].Operations.OperationList.Where(a => a.Name == "IDV").FirstOrDefault().Data.Response.RawResponse.Document.Id.ToString();
                var request2 = new RestRequest($"https://www.test.transuniondecisioncentre.co.in/DC/TUCL/TU.DE.Pont/Documents/{creditScoreDocId}");
                request2.AddHeader("Authorization", $"Bearer {token}");
                var creditScoreReport = await client.ExecuteAsync(request2, Method.Get);

                request2 = new RestRequest($"https://www.test.transuniondecisioncentre.co.in/DC/TUCL/TU.DE.Pont/Documents/{IDVDocId}");
                request2.AddHeader("Authorization", $"Bearer {token}");
                var idvDocument = await client.ExecuteAsync(request2, Method.Get);

                if (resp.Fields.Applicants.ApplicantList.Count() > 0)
                {

                       



                        using (var cmd = new Npgsql.NpgsqlCommand($"Update public.\"Online_Loan_Capture\" Set  request = (@p1) , response =  (@p2) , score = (@p3), account_info = (@p4) Where app_id = {Id}", con) )
                        {
                            cmd.Parameters.AddWithValue("p1",requestJson);
                            cmd.Parameters.AddWithValue("p2", creditScoreReport.Content);
                            cmd.Parameters.AddWithValue("p3", idvDocument.Content);
                            cmd.Parameters.AddWithValue("p4", resp.Fields.Applicants.ApplicantList[0].Services.ServiceList[0].Operations.OperationList.Where(a => a.Name == "ConsumerCIR").FirstOrDefault().Data.Response.RawResponse.BureauResponseXml);

                            cmd.ExecuteNonQuery();
                            
                        }
                    
                 
                 

                }
            }
            catch (Exception)
            {

                return Ok("ErrorCibil");
            }


          
                return Ok("OK");
            }
            
        }

        public  IActionResult GetPAN()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetPAN(string fullname , string panno , string dob , string gender,string mobile , string email)
        {
            var uid = Guid.NewGuid().ToString();
            var str = $"{uid}|{Request.HttpContext.Connection.RemoteIpAddress}|{panno}|{DateTime.Now.ToString()}";
            string DemoRequest = "";
            var filePath = Path.Join(_appEnvironment.WebRootPath, "panrequest.txt");
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
            {
                DemoRequest = sr.ReadToEnd();
            }
            ViewBag.IsPostback = "Yes";
            var requestNew = JsonConvert.DeserializeObject<Models.PANVerification>(DemoRequest);
            requestNew.txnId = Guid.NewGuid().ToString();
            requestNew.certificateParameters.PANFullName = fullname;
            requestNew.certificateParameters.FullName = fullname;
            requestNew.certificateParameters.panno = panno;
            requestNew.certificateParameters.GENDER  = gender;
            requestNew.certificateParameters.DOB = DateTime.Parse(dob).ToString("dd-MM-yyyy");
            requestNew.consentArtifact.consent.user.mobile = mobile;
            requestNew.consentArtifact.consent.user.email = email;
            string requestJson = JsonConvert.SerializeObject(requestNew);
            Console.WriteLine(requestJson);
            var client = new RestClient();
            var request1 = new RestRequest("https://apisetu.gov.in/certificate/v3/pan/pancr");
            request1.AddHeader("Content-Type", "application/json");
            request1.AddHeader("X-APISETU-CLIENTID", "in.cgbank");
            request1.AddHeader("X-APISETU-APIKEY", "KUcREKZ6bWPa86bKvXnWLAMENfrKOzjO");
            request1.AddBody(requestJson, "application/json");
            var a = await client.ExecuteAsync(request1, Method.Post);
            try
            {
                XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(Certificate));
                Certificate certificate = new Certificate();
                using (TextReader reader = new StringReader(a.Content))
                {

                    certificate = (Certificate)ser.Deserialize(reader);
                    ViewBag.Error = "OK";
                    certificate.IssuedTo.Person.Email = uid;
                    ViewBag.Certificate = certificate;
                    using (StreamWriter sw = new StreamWriter(Path.Join(_appEnvironment.WebRootPath, $"{uid}.txt")))
                    {
                        sw.Write(a.Content);
                        sw.Close();
                    }

                }
            }
            catch (Exception exc)
            {

                ViewBag.Error = a.Content;
            }

            using (StreamWriter sw = new StreamWriter(Path.Join(_appEnvironment.WebRootPath, "panlog.txt"),true))
            {
                sw.WriteLine(str);
                sw.Close();
            }


            return View("GetPANResp",null);
        }

        public async Task<IActionResult> GetCibil(int AppId)
        {
            var qry = $"SELECT * FROM loanflow.\"CIBILRequests\" Where COALESCE(\"CiibilControlNumber\",'OK') != 'Deleted' and \"Score2\" != 'Failed' and \"ApplicantId\" = {AppId} and \"RequestDate\"::date >= '{DateTime.Now.AddDays(-5).ToShortDateString()}'";
            var cnt = _context.CIBILRequests.FromSqlRaw(qry).Count();
            if (cnt > 0)
            {
                ViewBag.Error = "Cibil Already fetched within 5 Days";
                return View("ErrorCibil");
            }
            qry = $"SELECT * FROM loanflow.\"CIBILRequests\" Where COALESCE(\"CiibilControlNumber\",'OK') != 'Deleted' and \"Score2\" = 'Failed' and \"ApplicantId\" = {AppId} and \"RequestDate\"::date >= '{DateTime.Now.AddDays(-5).ToShortDateString()}'";
            cnt = _context.CIBILRequests.FromSqlRaw(qry).Count();
            if (cnt > 0)
            {
                qry = $"SELECT * FROM loanflow.\"CIBILRequests\" Where COALESCE(\"CiibilControlNumber\",'OK') != 'Deleted' and \"Score2\" = 'Failed' and \"ApplicantId\" = {AppId} Order BY \"Id\"";
                var err = _context.CIBILRequests.FromSqlRaw(qry).FirstOrDefault();
                var error1 = JsonConvert.DeserializeObject<Models.Cibil3.Root>(err.ResponseFileName);
                ViewBag.Error = "Cibil Error present , Please contact HO IT for resolution" + "<br/>" + error1.Fields.Applicants.Applicant.Services.Service[0].Operations.Operation.Where(a => a.Name == "ConsumerCIR").FirstOrDefault().Data.Response.RawResponse.BureauResponseXml;
                //ViewBag.Error = "Cibil Error present , Please contact HO IT for resolution";
                return View("ErrorCibil");
            }
            string DemoRequest = "";
            var filePath = Path.Join(_appEnvironment.WebRootPath, "request.txt");
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
            {
                DemoRequest = sr.ReadToEnd();
            }

            var requestNew = JsonConvert.DeserializeObject<Models.CIBILNew.CibilRequestNew>(DemoRequest);
            
            var applicant = _context.Applicants.Find(AppId);
            if (applicant.Applicant_Last_Name == null)
            {
                applicant.Applicant_Last_Name = "";
            }
            if (applicant.Applicant_Middle_Name == null)
            {
                applicant.Applicant_Middle_Name = "";
            }

            var appliedAmt = _context.LoanApplications.Where(a=> a.ApplicationId == applicant.ApplicationId).Sum(a=> a.AppliedAmount);
            var application = _context.Applications.Find(applicant.ApplicationId);
            var purpose = "10";
            if (application.LoanScheme.Contains("PL"))
            {
                purpose = "05";
            }
            else if (application.LoanScheme.Contains("HL"))
            {
                purpose = "02";
            }
            else if (application.LoanScheme.Contains("Car"))
            {
                purpose = "01";
            }
            else if (application.LoanScheme.Contains("KCC"))
            {
                purpose = "36";
            }
            requestNew.Fields.Applicants.Applicant.Services.Service.Operations.Operation[0].Params.Param[2].Value = purpose;
            requestNew.Fields.Applicants.Applicant.Services.Service.Operations.Operation[0].Params.Param[1].Value = application.AppliedAmt.ToString();
            if (applicant.PAN_No.Length == 10)
            {
                requestNew.Fields.Applicants.Applicant.Identifiers.Identifier.Add(new Models.CIBILNew.Identifier() { IdType = "01", IdNumber = applicant.PAN_No });
                //cibilRequestV1.consumerInputSubject.ids.Add(new Models.Id { index = "I01", idNumber = applicant.PAN_No, idType = "01" });

            }

            //if (applicant.ElectionId.Length > 9)
            //{
            //    requestNew.Fields.Applicants.Applicant.Identifiers.Identifier.Add(new Models.CIBILNew.Identifier() { IdType = "01", IdNumber = applicant.ElectionId });
            //    //cibilRequestV1.consumerInputSubject.ids.Add(new Models.Id { index = "I01", idNumber = applicant.PAN_No, idType = "01" });

            //}
            if (applicant.Adhaar_No.Length == 12)
            {
                requestNew.Fields.Applicants.Applicant.Identifiers.Identifier.Add(new Models.CIBILNew.Identifier() { IdType = "06", IdNumber = applicant.Adhaar_No });
            }
            
            requestNew.Fields.Applicants.Applicant.ApplicantFirstName = applicant.Applicant_First_Name;
            requestNew.Fields.Applicants.Applicant.ApplicantMiddleName = applicant.Applicant_Middle_Name;
            requestNew.Fields.Applicants.Applicant.ApplicantLastName = applicant.Applicant_Last_Name;
            requestNew.Fields.Applicants.Applicant.DateOfBirth = DateTime.Parse(applicant.Date_Of_Birth).ToString("ddMMyyyy");
            requestNew.Fields.Applicants.Applicant.Gender = applicant.Gender;
            requestNew.Fields.Applicants.Applicant.Addresses.Address.AddressLine1 = applicant.Address_Line1;
            requestNew.Fields.Applicants.Applicant.Addresses.Address.AddressLine2 = applicant.Address_Line2;
            requestNew.Fields.Applicants.Applicant.Addresses.Address.City = applicant.City_Village;
            requestNew.Fields.Applicants.Applicant.Addresses.Address.StateCode = applicant.State;
            requestNew.Fields.Applicants.Applicant.Addresses.Address.PinCode = applicant.PINCode;
            requestNew.Fields.Applicants.Applicant.Telephones.Telephone.TelephoneType = "01";
            requestNew.Fields.Applicants.Applicant.Telephones.Telephone.TelephoneNumber = applicant.MobileNumber;



            string requestJson = JsonConvert.SerializeObject(requestNew);
            Console.WriteLine("Fetching Token");
            var o1 = new RestClientOptions();
            o1.Proxy = new WebProxy("10.43.5.6:3128");
            var client = new RestClient(o1);
            var request1 = new RestRequest("https://dc.cibil.com/DE/TU.DE.Pont/Token");
            request1.AddHeader("MediaType", "application/json");
            request1.AddHeader("Content-Type", "");
            request1.AddHeader("Cookie", "NSC_VBU_UVTTQM_ED_UVDM_EDQpsubm=14b5a3d9d9b5b9058d0b5e99152e05d07f58eecc38d1438e01f044a4535271aa94f4c055");
            var body = _configuration["Cibil"];
            request1.AddParameter("text/plain", body, ParameterType.RequestBody);
            var token_response = await client.ExecuteAsync(request1,Method.Post);
            string token = token_response.Content.Split(':')[1].Split(',')[0].Replace('"', ' ').Trim();
            Console.WriteLine("TokenGenerated");

            // Debug.WriteLine(token);
            var o = new RestClientOptions("https://dc.cibil.com/DE/TU.DE.Pont/Applications");
            o.Proxy = new WebProxy("10.43.5.6:3128");
            var cl = new RestClient(o);
            var request = new RestRequest("https://dc.cibil.com/DE/TU.DE.Pont/Applications", Method.Post);

            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Content-Type", "application/json");
            
            request.AddBody(requestJson, "application/json");

            var a = await cl.ExecuteAsync(request, Method.Post);
            var resp = JsonConvert.DeserializeObject<Models.Cibil3.Root>(a.Content);
            Debug.WriteLine(a.Content);
            CIBILRequest cIBILRequest = new CIBILRequest();
            Models.CIBILResponseNew.CreditReport credit = new Models.CIBILResponseNew.CreditReport();
            try
            {
                string IDVDocId = "";
                foreach (var item in resp.Fields.Applicants.Applicant.Services.Service[0].Operations.Operation)
                {
                    if (item.Name.Trim() == "IDV")
                    {
                        if (item.Data.Response.RawResponse.DsIDVision.Document != null)
                        {
                            IDVDocId = item.Data.Response.RawResponse.DsIDVision.Document.Id.ToString();
                        }
                        
                    }
                }
                string creditScoreDocId = resp.Fields.Applicants.Applicant.Services.Service[0].Operations.Operation.Where(a => a.Name == "ConsumerCIR").FirstOrDefault().Data.Response.RawResponse.Document.Id.ToString();
                //string IDVDocId = resp.Fields.Applicants.ApplicantList[0].Services.ServiceList[0].Operations.OperationList.Where(a => a.Name == "IDV").FirstOrDefault().Data.Response.RawResponse.Document.Id.ToString();
                var request2 = new RestRequest($"https://dc.cibil.com/DE/TU.DE.Pont/Documents/{creditScoreDocId}");
                request2.AddHeader("Authorization", $"Bearer {token}");
                var creditScoreReport = await client.ExecuteAsync(request2, Method.Get);

                request2 = new RestRequest($"https://dc.cibil.com/DE/TU.DE.Pont/Documents/{IDVDocId}");
                request2.AddHeader("Authorization", $"Bearer {token}");
                var idvDocument = await client.ExecuteAsync(request2, Method.Get);

                if (resp.Fields.Applicants.Applicant != null)
                {

                    cIBILRequest.ApplicantId = AppId;
                    cIBILRequest.RequestDate = DateTime.Now.ToShortDateString();
                    cIBILRequest.ResponseFileName = a.Content;
                    cIBILRequest.Score1 = requestJson;
                    cIBILRequest.Score2 = creditScoreReport.Content;
                    cIBILRequest.Score3 = idvDocument.Content;
                    Models.CIBILResponseNew.CreditReport report = new Models.CIBILResponseNew.CreditReport();
                    XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(Models.CIBILResponseNew.CreditReport));

                    using (TextReader reader = new StringReader(resp.Fields.Applicants.Applicant.Services.Service[0].Operations.Operation.Where(a => a.Name == "ConsumerCIR").FirstOrDefault().Data.Response.RawResponse.BureauResponseXml))
                    {
                        credit = (Models.CIBILResponseNew.CreditReport)ser.Deserialize(reader);
                    }


                    cIBILRequest.ScoreName1 = credit.ScoreSegment.Score.ToString();
                    _context.CIBILRequests.Add(cIBILRequest);
                    _context.SaveChanges();
                    ViewBag.Message = a.Content.ToString();

                }
            }
            catch (Exception ex)
            {
                cIBILRequest.ApplicantId = AppId;
                cIBILRequest.RequestDate = DateTime.Now.ToShortDateString();
                cIBILRequest.ResponseFileName = a.Content;
                cIBILRequest.Score1 = requestJson;
                cIBILRequest.Score2 = "Failed";
                cIBILRequest.Score3 = "Failed";
                _context.CIBILRequests.Add(cIBILRequest);
                _context.SaveChanges();
                ViewBag.Error = ex.Message;
                return View("ErrorCibil");
            }
            

            // Debug.WriteLine(a.Content);
            CibilHelper cibilHelper = new CibilHelper();
            
            if (resp.Fields.Applicants.Applicant != null)
            {
                foreach (var item in credit.Account)
                {
                    CIBILLoanInfo loanInfo = new CIBILLoanInfo();
                    loanInfo.CIBILRequestId = cIBILRequest.Id;
                    loanInfo.ownershipIndicator = CibilHelper.GetOwnership(item.AccountNonSummarySegmentFields.OwenershipIndicator.ToString());
                    loanInfo.amountOverdue = item.AccountNonSummarySegmentFields.AmountOverdue ?? "0";
                    loanInfo.accountType = cibilHelper.GetAccountType(item.AccountNonSummarySegmentFields.AccountType);
                    loanInfo.currentBalance = item.AccountNonSummarySegmentFields.CurrentBalance;
                    loanInfo.dateOpened = DateTime.ParseExact(item.AccountNonSummarySegmentFields.DateOpenedOrDisbursed.ToString(),"ddMMyyyy", CultureInfo.CurrentCulture).ToShortDateString();
                    try
                    {
                        loanInfo.dateClosed = DateTime.ParseExact(item.AccountNonSummarySegmentFields.DateClosed, "ddMMyyyy", CultureInfo.CurrentCulture).ToShortDateString();
                    }
                    catch (Exception)
                    {

                        loanInfo.dateClosed = null;
                    }
                    
                    loanInfo.dateReported = DateTime.ParseExact(item.AccountNonSummarySegmentFields.DateReportedAndCertified.ToString(), "ddMMyyyy", CultureInfo.CurrentCulture).ToShortDateString();
                    loanInfo.memberShortName = item.AccountNonSummarySegmentFields.ReportingMemberShortName;
                    loanInfo.paymentHistory = item.AccountNonSummarySegmentFields.PaymentHistory1.ToString();
                   // loanInfo.accountType = item.accountType;
                    _context.CIBILLoanInfo.Add(loanInfo);
                    _context.SaveChanges();
                }
            }
            
            return RedirectToAction("CreditScore",new { Id = cIBILRequest.Id});
        }
        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string userid , string password)
        {
            var user = await _userManager.FindByNameAsync(userid);
            if (user.Designation == "BC")
            {
                return View();
            }
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
                HttpContext.Session.SetString("Token", new JwtSecurityTokenHandler().WriteToken(token));
                Console.WriteLine("LMS | Redirecting to Dashboard");
                return RedirectToAction("Dashboard","Home",null);
            }
            return View();
        }
        

        [HttpPost]
        public async Task<IActionResult> LoginByApp(string userid)
        {
            
            var user = await _userManager.FindByNameAsync(userid);
           
            if (user != null)
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
                HttpContext.Session.SetString("Token", new JwtSecurityTokenHandler().WriteToken(token));
                return RedirectToAction("Dashboard", "Home", null);
            }
            return View();
        }

       
        public async Task<IActionResult> GlobalLogin(string userid, string password)
        {
            var user = await _userManager.FindByNameAsync(userid);
            var x = await _userManager.CheckPasswordAsync(user, password);
            if (x != false)
            {
                return Ok("True");
            }
            return Ok("False");
        }

        public async Task<IActionResult> AddUserApi(string empCode , string empName , string scale , string branchId )
        {
                 ApplicationUser user = new ApplicationUser();
                    user.UserName = empCode;
                    user.EmployeeName = empName;
                    user.Scale = scale;
                    user.BranchId = branchId;
                    user.Role = "Staff";
                    var c = await _userManager.CreateAsync(user, "admin@123");
           


            return Ok("Done");
        }

        public async Task<IActionResult> AddBCApi(string empCode, string empName,  string branchId)
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = empCode;
            user.EmployeeName = empName;
            user.Scale = "BC";
            user.Designation = "BC";
            user.BranchId = branchId;
            user.Role = "BC";
            var c = await _userManager.CreateAsync(user, "crgb@123");

            return Ok("Done");
        }


        public async Task<IActionResult> ChangePasswordApi(string empCode, string oldPassword, string newPassword)
        {
            try
            {
                var u = await _userManager.FindByNameAsync(empCode);
                var b = await _userManager.ChangePasswordAsync(u, oldPassword, newPassword);
                return Ok("Done");
            }
            catch (Exception)
            {
                return BadRequest("Mismatch in Details"); 
            }

            
        }


        public async Task<IActionResult> AddUser1()
        { 
            return View();
        }


     
        public async Task<IActionResult> AddBC()
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader("D:\\emp.csv"))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    var x = sr.ReadLine();
                    ApplicationUser user = new ApplicationUser();
                    user.UserName = x.Split(',')[0];
                    user.EmployeeName = x.Split(',')[2];
                    user.Scale = "BC";
                    user.BranchId = x.Split(',')[1].PadLeft(5, '0');
                    user.Role = "BC";
                    user.Designation = "BC";
                    var c = await _userManager.CreateAsync(user, "admin@123");
                }
            }


            return Json("Done");
        }


     
        public async Task<IActionResult> ReMandate(int Id)
        {

            Mandate man = _context.Mandates.Find(Id);
            man.instructed_agent_id_type = "IFSC";
            StreamReader sr = new StreamReader(Path.Combine(_appEnvironment.WebRootPath, "livebanks.csv"));
            Dictionary<string, string> bb = new Dictionary<string, string>();
            while (!sr.EndOfStream)
            {
                var s = sr.ReadLine();
                bb.Add(s.Split(',')[0], s.Split(',')[1]);
            }

            man.instructed_agent_id = bb[man.instructed_agent_code];
            man.reference_id = Guid.NewGuid().ToString().Replace("-", "");
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
                _context.Entry(man).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(d.status);
            }
            return  Ok("Please check");
        }


        public async Task<IActionResult> AddUser()
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader("D:\\emp.csv"))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    var x = sr.ReadLine();
                    ApplicationUser user = new ApplicationUser();
                    user.UserName = x.Split(',')[0];
                    user.EmployeeName = x.Split(',')[2];
                    user.Scale = x.Split(',')[3];
                    user.BranchId = x.Split(',')[1].PadLeft(5,'0');
                    user.Role = "Staff";
                    var c = await _userManager.CreateAsync(user, "admin@123");
                }
            }
            
            
            return Json("Done");
        }
        [Authorize]
        public IActionResult AuthoTest()
        {
            return Json(User.Identity.Name);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
