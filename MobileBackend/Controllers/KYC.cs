using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileBackend.Models;
using RestSharp;
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
        public async Task<IActionResult> PanResult(string acc = "", int appId = 0)
        {
            if (appId > 0)
            {
                var app = _context.Applicants.Find(appId);
                if (app != null)
                {
                    var s = await VerifyPAN(app.PAN_No);
                    if (s != "")
                    {
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
                        var js = Newtonsoft.Json.JsonConvert.SerializeObject(req);

                        var request1 = new RestRequest("https://api.sandbox.co.in/kyc/pan");
                        request1.AddHeader("Accept", "*/*");
                        request1.AddHeader("Accept-Encoding", "gzip, deflate, br");
                        request1.AddHeader("Connection", "keep-alive");
                        request1.AddHeader("x-api-key", "key_live_OFvIY6g1pK23IQQmDJz5MyucAdIzCCJ0");
                        request1.AddHeader("x-api-version", "1.0");
                        request1.AddHeader("Authorization", token_response.Content.ToString());
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
