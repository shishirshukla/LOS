using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileBackend.Models;
using RestSharp;
using System.Linq;
using System.Threading.Tasks;

namespace MobileBackend.Controllers
{
    public class KYC : Controller
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public readonly CustomIDataProtection _protector;
        public KYC(IWebHostEnvironment appEnvironment, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ApplicationDbContext context, CustomIDataProtection protector)
        {
            _appEnvironment = appEnvironment;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
            _protector = protector;
        }
        [HttpPost]
        public async Task<IActionResult> AdhaarVerify(string ref_id , string otp , int kycId , string calltype)
        {
            return View();
        }

        public async Task<IActionResult> AdhaarVerify(string acc = "", int appId = 0)
        {
            if (appId > 0)
            {
                var app = _context.Applicants.Find(appId);
                if (app != null)
                {
                    if (_context.KycInfo.Where(a => a.ApplicantId == app.Id && a.IdType == "Adhaar" && a.VerificationStatus == "Yes").FirstOrDefault() == null)
                    {
                        var s = await AdhaarSendOTP(app.Adhaar_No);
                        if (s != "")
                        {
                            KYCInfo k = new KYCInfo();
                            k.ApplicantId = app.Id;
                            k.IdType = "Adhaar";
                            k.IdNumber = app.Adhaar_No;
                            k.VerificationStatus = "No";
                            _context.KycInfo.Add(k);
                            _context.SaveChanges();
                            var z = Newtonsoft.Json.JsonConvert.DeserializeObject<AdhaarRespOTP>(s);
                            ViewBag.callType = "Applicant";
                            ViewBag.KycId = k.Id;
                            return View(z);
                        }

                    }
                    else
                    {
                        var s = _context.KycInfo.Where(a => a.ApplicantId == app.Id && a.IdType == "Adhaar" && a.VerificationStatus == "Yes").FirstOrDefault();
                        return RedirectToAction("AdhaarResult",new {dbId = s.Id });
                    }


                }
            }
            if (acc != "")
            {
                var app = _context.ExistingApplicant.Where(a => a.AccountInfoId == acc.PadLeft(17, '0')).FirstOrDefault();
                if (app != null)
                {
                    if (_context.KycInfoExisting.Where(a => a.ExistingApplicantId == app.AccountInfoId && a.IdType == "Adhaar" && a.VerificationStatus == "Yes").FirstOrDefault() == null)
                    {
                        var s = await AdhaarSendOTP(app.Adhaar_No);
                        if (s != "")
                        {
                            var z = Newtonsoft.Json.JsonConvert.DeserializeObject<AdhaarRespOTP>(s);
                            KYCInfoExisting k = new KYCInfoExisting();
                            k.ExistingApplicantId = app.AccountInfoId;
                            k.IdType = "Adhaar";
                            k.IdNumber = app.Adhaar_No;
                           
                            k.VerificationStatus = "No";
                            _context.KycInfoExisting.Add(k);
                            _context.SaveChanges();
                            ViewBag.callType = "Existing";
                            ViewBag.KycId = k.Id;
                            return View(z);
                        }
                    }
                    else
                    {
                        var s = _context.KycInfoExisting.Where(a => a.ExistingApplicantId == app.AccountInfoId && a.IdType == "Adhaar" && a.VerificationStatus == "Yes").FirstOrDefault();
                        return RedirectToAction("AdhaarResultExisting", new { dbId = s.Id });
                    }

                }
            }
            return View();
        }

        public async Task<IActionResult> PanResult(string acc = "", int appId = 0)
        {
            if (appId > 0)
            {
                var app = _context.Applicants.Find(appId);
                if (app != null)
                {
                    if (_context.KycInfo.Where(a=> a.ApplicantId == app.Id && a.IdType == "PAN" && a.VerificationStatus == "Yes").FirstOrDefault() == null)
                    {
                        var s = await VerifyPAN(app.PAN_No);
                        if (s != "")
                        {
                            var z = Newtonsoft.Json.JsonConvert.DeserializeObject<PANResponse>(s);
                            KYCInfo k = new KYCInfo();
                            k.ApplicantId = app.Id;
                            k.IdType = "PAN";
                            k.IdNumber = app.PAN_No;
                            k.FirstName = z.data.first_name;
                            k.MiddleName = z.data.middle_name;
                            k.LastName = z.data.last_name;
                            k.response = s;
                            k.VerificationStatus = "Yes";
                            _context.KycInfo.Add(k);
                            _context.SaveChanges();
                            return View(z);
                        }
                       
                    }
                    else
                    {
                        var s = _context.KycInfo.Where(a => a.ApplicantId == app.Id && a.IdType == "PAN" && a.VerificationStatus == "Yes").FirstOrDefault().response;
                        var z = Newtonsoft.Json.JsonConvert.DeserializeObject<PANResponse>(s);
                        return View(z);
                    }


                }
            }
            if (acc != "")
            {
                var app = _context.ExistingApplicant.Where(a=> a.AccountInfoId == acc.PadLeft(17,'0')).FirstOrDefault();
                if (app != null)
                {
                    if (_context.KycInfoExisting.Where(a => a.ExistingApplicantId == app.AccountInfoId && a.IdType == "PAN" && a.VerificationStatus == "Yes").FirstOrDefault() == null)
                    {
                        var s = await VerifyPAN(app.PAN_No);
                        if (s != "")
                        {
                            var z = Newtonsoft.Json.JsonConvert.DeserializeObject<PANResponse>(s);
                            KYCInfoExisting k = new KYCInfoExisting();
                            k.ExistingApplicantId = app.AccountInfoId;
                            k.IdType = "PAN";
                            k.IdNumber = app.PAN_No;
                            k.FirstName = z.data.first_name;
                            k.MiddleName = z.data.middle_name;
                            k.LastName = z.data.last_name;
                            k.response = s;
                            k.VerificationStatus = "Yes";
                            _context.KycInfoExisting.Add(k);
                            _context.SaveChanges();
                            return View(z);
                        }
                    }
                    else
                    {
                        var s = _context.KycInfoExisting.Where(a => a.ExistingApplicantId == app.AccountInfoId && a.IdType == "PAN" && a.VerificationStatus == "Yes").FirstOrDefault().response;
                        var z = Newtonsoft.Json.JsonConvert.DeserializeObject<PANResponse>(s);
                        return View(z);
                    }
                    
                }
            }
            return View();
        }
        public async Task<string> VerifyPAN(string s)
        {
            PANReq req = new PANReq();
            req.pan = s;
            if (req.pan != null)
            {
                if (req.pan.Length == 10)
                {
                    var o1 = new RestClientOptions();
                    //o1.Proxy = new WebProxy("10.43.5.6:3128");
                    var client = new RestClient();
                    var request = new RestRequest("https://api.sandbox.co.in/authenticate");
                    request.AddHeader("Accept", "*/*");
                    request.AddHeader("Accept-Encoding", "gzip, deflate, br");
                    request.AddHeader("Connection", "keep-alive");

                    request.AddHeader("x-api-key", "key_live_OFvIY6g1pK23IQQmDJz5MyucAdIzCCJ0");
                    request.AddHeader("x-api-secret", "secret_live_OBR4AqcaHxF6ddc96imWpVJFWNponbTT");
                    request.AddHeader("x-api-version", "1.0");
                    request.AddHeader("Content-Type", "application/json");
                    var token_response = await client.ExecuteAsync(request, Method.Post);
                    if (token_response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var x = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(token_response.Content);
                        var js = Newtonsoft.Json.JsonConvert.SerializeObject(req);

                        var request1 = new RestRequest("https://api.sandbox.co.in/kyc/pan");
                        request1.AddHeader("Accept", "*/*");
                        request1.AddHeader("Accept-Encoding", "gzip, deflate, br");
                        request1.AddHeader("Connection", "keep-alive");
                        request1.AddHeader("x-api-key", "key_live_OFvIY6g1pK23IQQmDJz5MyucAdIzCCJ0");
                        request1.AddHeader("x-api-version", "1.0");
                        request1.AddHeader("Authorization", x.access_token);
                        request1.AddHeader("Content-Type", "application/json");
                        request1.AddBody(js, "application/json");
                        var resp = await client.ExecuteAsync(request1, Method.Post);
                        if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            return resp.Content.ToString();
                        }
                    }


                }
            }
            return "";
        }
        public async Task<string> AdhaarSendOTP(string s)
        {
            AdhaarReqOTP req = new AdhaarReqOTP();
            req.aadhaar_number = s;
            if (req.aadhaar_number != null)
            {
                if (req.aadhaar_number.Length == 12)
                {
                    var o1 = new RestClientOptions();
                    //o1.Proxy = new WebProxy("10.43.5.6:3128");
                    var client = new RestClient();
                    var request = new RestRequest("https://api.sandbox.co.in/authenticate");
                    request.AddHeader("Accept", "*/*");
                    request.AddHeader("Accept-Encoding", "gzip, deflate, br");
                    request.AddHeader("Connection", "keep-alive");

                    request.AddHeader("x-api-key", "key_live_OFvIY6g1pK23IQQmDJz5MyucAdIzCCJ0");
                    request.AddHeader("x-api-secret", "secret_live_OBR4AqcaHxF6ddc96imWpVJFWNponbTT");
                    request.AddHeader("x-api-version", "1.0");
                    request.AddHeader("Content-Type", "application/json");
                    var token_response = await client.ExecuteAsync(request, Method.Post);
                    if (token_response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var x = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(token_response.Content);
                        var js = Newtonsoft.Json.JsonConvert.SerializeObject(req);

                        var request1 = new RestRequest("https://api.sandbox.co.in/kyc/aadhaar/okyc/otp");
                        request1.AddHeader("Accept", "*/*");
                        request1.AddHeader("Accept-Encoding", "gzip, deflate, br");
                        request1.AddHeader("Connection", "keep-alive");
                        request1.AddHeader("x-api-key", "key_live_OFvIY6g1pK23IQQmDJz5MyucAdIzCCJ0");
                        request1.AddHeader("x-api-version", "1.0");
                        request1.AddHeader("Authorization", x.access_token);
                        request1.AddHeader("Content-Type", "application/json");
                        request1.AddBody(js, "application/json");
                        var resp = await client.ExecuteAsync(request1, Method.Post);
                        if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            return resp.Content.ToString();
                        }
                    }


                }
            }
            return "";
        }
    }
}
