using HtmlAgilityPack;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MobileBackend.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MobileBackend.Controllers
{
    public class KCCController : Controller
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public readonly CustomIDataProtection _protector;
        public static String BASE_URL2 = "https://revenue.cg.nic.in/bhuiyanuser/User/selection_report_for_user.aspx?flg=Y";
        public static String BASE_URL = "https://revenue.cg.nic.in/bhuiyanuser/User/Selection_Report_For_KhasraDetail.aspx";
        public static String BASE_URL_Fruit = "https://revenue.cg.nic.in/bhuiyanuser/User/Selection_Report_For_KhasraDetail.aspx/GetFruits";
        public string STEP1_URL = "https://revenue.cg.nic.in/bhuiyanuser/User/Selection_Report_For_KhasraDetail.aspx";
        public List<Tuple<String, String>> pairs = new List<Tuple<String, String>>();
        private string  khasra_no, dist, tehsil, village, ri = "";
        private CookieContainer cookies_s1 = new CookieContainer();
        public KCCController(IWebHostEnvironment appEnvironment, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ApplicationDbContext context, CustomIDataProtection protector)
        {
            _appEnvironment = appEnvironment;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
            _protector = protector;
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
        private  List<Tuple<string, string>> GetAllInput(string htmldoc)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmldoc);
            List<Tuple<string, string>> pairs = new List<Tuple<string, string>>();
            foreach (HtmlNode input in doc.DocumentNode.SelectNodes("//input"))
            {
                Tuple<string, string> tuple = new Tuple<string, string>(input.GetAttributeValue("name", ""), input.GetAttributeValue("value", ""));
                if (input.GetAttributeValue("name", "").Contains("ImageButton") == false)
                {
                    pairs.Add(tuple);
                }


            }


            return pairs;
        }
        private async Task Step2()
        {
            var form_data_s1 = new Dictionary<string, string>();
            for (int i = 0, loopTo = pairs.Count - 1; i <= loopTo; i++)
            {
                form_data_s1.Add(pairs[i].Item1, pairs[i].Item2);
            }

            form_data_s1.Add("ScriptManager1", "UpdatePanel1|ddlDist");
            form_data_s1.Add("ddlDist", $"{dist}");
            form_data_s1.Add("__EVENTARGUMENT", "");
            form_data_s1.Add("__ASYNCPOST", "true");
            form_data_s1.Add("__EVENTTARGET", $"ddlDist");
            var handler_s1 = new HttpClientHandler();
            handler_s1.UseProxy = true;
            handler_s1.Proxy = new WebProxy("10.43.5.6:3128");
           // handler_s1.CookieContainer = cookies_s1;
            var client1 = new HttpClient(handler_s1);
            var req = new HttpRequestMessage(HttpMethod.Post, BASE_URL);
            var encodedItems = form_data_s1.Select(i => WebUtility.UrlEncode(i.Key) + "=" + WebUtility.UrlEncode(i.Value));
            var encodedContent = new StringContent(string.Join("&", encodedItems), null, "application/x-www-form-urlencoded");
            req.Content = encodedContent;
            IEnumerable<Cookie> responseCookies = cookies_s1.GetCookies(new Uri(BASE_URL)).Cast<Cookie>();
            req.Headers.Add("cookie", $"{responseCookies.FirstOrDefault()}");
            req.Headers.Add("authority", "revenue.cg.nic.in");
            req.Headers.Add("x-microsoftajax", "Delta=true");
            req.Headers.Add("method", "post");
            req.Headers.Add("path", "/User/Selection_Report_For_KhasraDetail.aspx");
            req.Headers.Add("scheme", "https");
            req.Headers.Add("X-Requested-With", "XMLHttpRequest");
            req.Headers.Add("origin", "https://revenue.cg.nic.in");
            req.Headers.Add("referer", BASE_URL);
            req.Headers.Add("sec-fetch-dest", "empty");
            req.Headers.Add("sec-fetch-mode", "cors");
            req.Headers.Add("sec-fetch-site", "same-origin");
            req.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36");
            req.Content.Headers.ContentType = (new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("revenue.cg.nic.in");
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

            var Res = await client1.SendAsync(req);
            var html_s2 = await Res.Content.ReadAsStringAsync();
            var array = html_s2.Split("|");
            Console.WriteLine(array.Length);
            pairs = new List<Tuple<String, String>>();
            int index_viewstate = Array.IndexOf(array, "__VIEWSTATE") + 1;
            int index_previouspage = Array.IndexOf(array, "__PREVIOUSPAGE") + 1;
            int index_viewstategenerator = Array.IndexOf(array, "__VIEWSTATEGENERATOR") + 1;
            int index_eventvalidation = Array.IndexOf(array, "__EVENTVALIDATION") + 1;
            pairs.Add(new Tuple<String, String>("__VIEWSTATE", array[index_viewstate]));
            pairs.Add(new Tuple<String, String>("__PREVIOUSPAGE", array[index_previouspage]));
            pairs.Add(new Tuple<String, String>("__VIEWSTATEGENERATOR", array[index_viewstategenerator]));
            pairs.Add(new Tuple<String, String>("__EVENTVALIDATION", array[index_eventvalidation]));
            Console.WriteLine("Step 2 completed");
        }
        private async Task Step3()
        {
            var form_data_s1 = new Dictionary<string, string>();
            for (int i = 0, loopTo = pairs.Count - 1; i <= loopTo; i++)
            {
                form_data_s1.Add(pairs[i].Item1, pairs[i].Item2);
            }

            form_data_s1.Add("ScriptManager1", "UpdatePanel1|ddlTehsil");
            form_data_s1.Add("ddlDist", $"{dist}");
            form_data_s1.Add("ddlTehsil", $"{tehsil}");
            form_data_s1.Add("ddlGram", "");
            form_data_s1.Add("__ASYNCPOST", "true");
            form_data_s1.Add("__EVENTTARGET", $"ddlTehsil");
            var handler_s1 = new HttpClientHandler();
            handler_s1.UseProxy = true;
            handler_s1.Proxy = new WebProxy("10.43.5.6:3128");
            //handler_s1.CookieContainer = cookies_s1;
            var client1 = new HttpClient(handler_s1);
            var req = new HttpRequestMessage(HttpMethod.Post, BASE_URL);
            var encodedItems = form_data_s1.Select(i => WebUtility.UrlEncode(i.Key) + "=" + WebUtility.UrlEncode(i.Value));
            var encodedContent = new StringContent(string.Join("&", encodedItems), null, "application/x-www-form-urlencoded");
            req.Content = encodedContent;
            IEnumerable<Cookie> responseCookies = cookies_s1.GetCookies(new Uri(BASE_URL)).Cast<Cookie>();
            req.Headers.Add("cookie", $"{responseCookies.FirstOrDefault()}");
            req.Headers.Add("authority", "revenue.cg.nic.in");
            req.Headers.Add("x-microsoftajax", "Delta=true");
            req.Headers.Add("method", "post");
            req.Headers.Add("path", "/User/Selection_Report_For_KhasraDetail.aspx");
            req.Headers.Add("scheme", "https");
            req.Headers.Add("X-Requested-With", "XMLHttpRequest");
            req.Headers.Add("origin", "https://revenue.cg.nic.in");
            req.Headers.Add("referer", BASE_URL);
            req.Headers.Add("sec-fetch-dest", "empty");
            req.Headers.Add("sec-fetch-mode", "cors");
            req.Headers.Add("sec-fetch-site", "same-origin");
            req.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36");
            req.Content.Headers.ContentType = (new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("revenue.cg.nic.in");
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

            var Res = await client1.SendAsync(req);
            var html_s2 = await Res.Content.ReadAsStringAsync();
            //    Console.WriteLine(html_s2);
            var array = html_s2.Split("|");
            Console.WriteLine(array.Length);
            pairs = new List<Tuple<String, String>>();
            int index_viewstate = Array.IndexOf(array, "__VIEWSTATE") + 1;
            int index_previouspage = Array.IndexOf(array, "__PREVIOUSPAGE") + 1;
            int index_viewstategenerator = Array.IndexOf(array, "__VIEWSTATEGENERATOR") + 1;
            int index_eventvalidation = Array.IndexOf(array, "__EVENTVALIDATION") + 1;
            pairs.Add(new Tuple<String, String>("__VIEWSTATE", array[index_viewstate]));
            pairs.Add(new Tuple<String, String>("__PREVIOUSPAGE", array[index_previouspage]));
            pairs.Add(new Tuple<String, String>("__VIEWSTATEGENERATOR", array[index_viewstategenerator]));
            pairs.Add(new Tuple<String, String>("__EVENTVALIDATION", array[index_eventvalidation]));
            Console.WriteLine(pairs.Count);
            Console.WriteLine("Step 3 Completed");
        }
        private async Task Step4()
        {
            var form_data_s1 = new Dictionary<string, string>();
            for (int i = 0, loopTo = pairs.Count - 1; i <= loopTo; i++)
            {
                form_data_s1.Add(pairs[i].Item1, pairs[i].Item2);
            }


            form_data_s1.Add("ScriptManager1", "UpdatePanel1|ddlRI");
            form_data_s1.Add("ddlDist", $"{dist}");
            form_data_s1.Add("ddlTehsil", $"{tehsil}");
            form_data_s1.Add("ddlRI", $"{ri}");
            form_data_s1.Add("ddlGram", "");
            form_data_s1.Add("__ASYNCPOST", "true");
            form_data_s1.Add("__EVENTTARGET", $"ddlRI");


            var handler_s1 = new HttpClientHandler();
            handler_s1.UseProxy = true;
            handler_s1.Proxy = new WebProxy("10.43.5.6:3128");
            // handler_s1.CookieContainer = cookies_s1;
            var client1 = new HttpClient(handler_s1);
            var req = new HttpRequestMessage(HttpMethod.Post, BASE_URL);
            var encodedItems = form_data_s1.Select(i => WebUtility.UrlEncode(i.Key) + "=" + WebUtility.UrlEncode(i.Value));
            var encodedContent = new StringContent(string.Join("&", encodedItems), null, "application/x-www-form-urlencoded");
            req.Content = encodedContent;
            IEnumerable<Cookie> responseCookies = cookies_s1.GetCookies(new Uri(BASE_URL)).Cast<Cookie>();
            req.Headers.Add("cookie", $"{responseCookies.FirstOrDefault()}");
            req.Headers.Add("authority", "revenue.cg.nic.in");
            req.Headers.Add("x-microsoftajax", "Delta=true");
            req.Headers.Add("method", "post");
            req.Headers.Add("path", "/User/Selection_Report_For_KhasraDetail.aspx");
            req.Headers.Add("scheme", "https");
            req.Headers.Add("X-Requested-With", "XMLHttpRequest");
            req.Headers.Add("origin", "https://revenue.cg.nic.in");
            req.Headers.Add("referer", BASE_URL);
            req.Headers.Add("sec-fetch-dest", "empty");
            req.Headers.Add("sec-fetch-mode", "cors");
            req.Headers.Add("sec-fetch-site", "same-origin");
            req.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36");
            req.Content.Headers.ContentType = (new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("revenue.cg.nic.in");
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

            var Res = await client1.SendAsync(req);
            var html_s2 = await Res.Content.ReadAsStringAsync();
            //  Console.WriteLine(html_s2);
            var array = html_s2.Split("|");
            Console.WriteLine(array.Length);
            pairs = new List<Tuple<String, String>>();
            int index_viewstate = Array.IndexOf(array, "__VIEWSTATE") + 1;
            int index_previouspage = Array.IndexOf(array, "__PREVIOUSPAGE") + 1;
            int index_viewstategenerator = Array.IndexOf(array, "__VIEWSTATEGENERATOR") + 1;
            int index_eventvalidation = Array.IndexOf(array, "__EVENTVALIDATION") + 1;
            pairs.Add(new Tuple<String, String>("__VIEWSTATE", array[index_viewstate]));
            pairs.Add(new Tuple<String, String>("__PREVIOUSPAGE", array[index_previouspage]));
            pairs.Add(new Tuple<String, String>("__VIEWSTATEGENERATOR", array[index_viewstategenerator]));
            pairs.Add(new Tuple<String, String>("__EVENTVALIDATION", array[index_eventvalidation]));
            Console.WriteLine(pairs.Count);
            Console.WriteLine("Step 4 Completed");
        }

        private async Task Step5()
        {
            var form_data_s1 = new Dictionary<string, string>();
            for (int i = 0, loopTo = pairs.Count - 1; i <= loopTo; i++)
            {
                form_data_s1.Add(pairs[i].Item1, pairs[i].Item2);
            }


            form_data_s1.Add("ScriptManager1", "ctl09|ddlGram");
            form_data_s1.Add("ddlDist", $"{dist}");
            form_data_s1.Add("ddlTehsil", $"{tehsil}");
            form_data_s1.Add("ddlGram", $"{village}");
            form_data_s1.Add("__ASYNCPOST", "true");
            form_data_s1.Add("__EVENTTARGET", $"ddlGram");


            var handler_s1 = new HttpClientHandler();
            handler_s1.UseProxy = true;
            handler_s1.Proxy = new WebProxy("10.43.5.6:3128");
            // handler_s1.CookieContainer = cookies_s1;
            var client1 = new HttpClient(handler_s1);
            var req = new HttpRequestMessage(HttpMethod.Post, BASE_URL);
            var encodedItems = form_data_s1.Select(i => WebUtility.UrlEncode(i.Key) + "=" + WebUtility.UrlEncode(i.Value));
            var encodedContent = new StringContent(string.Join("&", encodedItems), null, "application/x-www-form-urlencoded");
            req.Content = encodedContent;
            IEnumerable<Cookie> responseCookies = cookies_s1.GetCookies(new Uri(BASE_URL)).Cast<Cookie>();
            req.Headers.Add("cookie", $"{responseCookies.FirstOrDefault()}");
            req.Headers.Add("authority", "revenue.cg.nic.in");
            req.Headers.Add("x-microsoftajax", "Delta=true");
            req.Headers.Add("method", "post");
            req.Headers.Add("path", "/User/Selection_Report_For_KhasraDetail.aspx");
            req.Headers.Add("scheme", "https");
            req.Headers.Add("X-Requested-With", "XMLHttpRequest");
            req.Headers.Add("origin", "https://revenue.cg.nic.in");
            req.Headers.Add("referer", BASE_URL);
            req.Headers.Add("sec-fetch-dest", "empty");
            req.Headers.Add("sec-fetch-mode", "cors");
            req.Headers.Add("sec-fetch-site", "same-origin");
            req.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36");
            req.Content.Headers.ContentType = (new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("revenue.cg.nic.in");
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

            var Res = await client1.SendAsync(req);
            var html_s2 = await Res.Content.ReadAsStringAsync();
            // Console.WriteLine(html_s2);
            var array = html_s2.Split("|");
            Console.WriteLine(array.Length);
            pairs = new List<Tuple<String, String>>();
            int index_viewstate = Array.IndexOf(array, "__VIEWSTATE") + 1;
            int index_previouspage = Array.IndexOf(array, "__PREVIOUSPAGE") + 1;
            int index_viewstategenerator = Array.IndexOf(array, "__VIEWSTATEGENERATOR") + 1;
            int index_eventvalidation = Array.IndexOf(array, "__EVENTVALIDATION") + 1;
            pairs.Add(new Tuple<String, String>("__VIEWSTATE", array[index_viewstate]));
            pairs.Add(new Tuple<String, String>("__PREVIOUSPAGE", array[index_previouspage]));
            pairs.Add(new Tuple<String, String>("__VIEWSTATEGENERATOR", array[index_viewstategenerator]));
            pairs.Add(new Tuple<String, String>("__EVENTVALIDATION", array[index_eventvalidation]));
            Console.WriteLine(pairs.Count);
            Console.WriteLine("Step 5 Completed");
            // New Addition

            var sw = new StreamWriter("shishir.txt");
            for (int i = 0, loopTo = pairs.Count - 1; i <= loopTo; i++)
            {
                sw.WriteLine(pairs[i].Item1, pairs[i].Item2);
            }
            IEnumerable<Cookie> responseCookies1 = cookies_s1.GetCookies(new Uri(BASE_URL)).Cast<Cookie>();
            StoreCookieCollection(responseCookies1, "shishir_coookies.json");
        }
        private  void StoreCookieCollection(IEnumerable<Cookie> cookies, string cookieFile)
        {
            cookieFile = Path.GetTempPath() + cookieFile;
            if (System.IO.File.Exists(cookieFile)) System.IO.File.Delete(cookieFile);
            var formatter = new BinaryFormatter();
            var fs = new FileStream(cookieFile, FileMode.Create);
            formatter.Serialize(fs, cookies);
            fs.Close();
        }

        private static CookieCollection LoadCookieCollection(string cookieFile)
        {
            cookieFile = Path.GetTempPath() + cookieFile;
            if (!System.IO.File.Exists(cookieFile)) return null;
            var age = System.IO.File.GetLastWriteTime(cookieFile);
            if (DateTime.Now - age > TimeSpan.FromDays(1)) return null; // a day old
            var formatter = new BinaryFormatter();
            CookieCollection cc;
            using (Stream reader = new FileStream(cookieFile, FileMode.Open))
            {
                cc = (CookieCollection)formatter.Deserialize(reader);
            }
            return cc;
        }

        private async Task<DataTable> Step6()
        {
            var form_data_s1 = new Dictionary<string, string>();
            for (int i = 0, loopTo = pairs.Count - 1; i <= loopTo; i++)
            {
                form_data_s1.Add(pairs[i].Item1, pairs[i].Item2);
            }


            form_data_s1.Add("ScriptManager1", "ctl04|btnSearch");
            form_data_s1.Add("ddlDist", $"{dist}");
            form_data_s1.Add("ddlTehsil", $"{tehsil}");
            form_data_s1.Add("ddlGram", $"{village}");

            form_data_s1.Add("RblReportType", $"0");
            form_data_s1.Add("txtSearch", $"{khasra_no}");
            form_data_s1.Add("btnSearch", $"देखें");
            form_data_s1.Add("hdnBasrNo", $"{khasra_no}");
            form_data_s1.Add("hdnkhasraLst", $"{khasra_no}");
            form_data_s1.Add("hdgsrNo", $"{village}");
            form_data_s1.Add("__ASYNCPOST", "true");
            form_data_s1.Add("hdConstr", "Data Source=10.132.0.162;Initial Catalog=Central_Bhuiyan;User ID=sa;Password=Krutneeti84^86;Max Pool Size=500;Pooling=true;Connection Timeout=180");

            form_data_s1.Add("__EVENTTARGET", $"ddlKhasra");


            var handler_s1 = new HttpClientHandler();
            handler_s1.UseProxy = true;
            handler_s1.Proxy = new WebProxy("10.43.5.6:3128");
            // handler_s1.CookieContainer = cookies_s1;
            var client1 = new HttpClient(handler_s1);
            var req = new HttpRequestMessage(HttpMethod.Post, BASE_URL);
            var encodedItems = form_data_s1.Select(i => WebUtility.UrlEncode(i.Key) + "=" + WebUtility.UrlEncode(i.Value));
            var encodedContent = new StringContent(string.Join("&", encodedItems), null, "application/x-www-form-urlencoded");
            req.Content = encodedContent;
            IEnumerable<Cookie> responseCookies = cookies_s1.GetCookies(new Uri(BASE_URL)).Cast<Cookie>();
            
            req.Headers.Add("cookie", $"{responseCookies.FirstOrDefault()}");
            req.Headers.Add("authority", "revenue.cg.nic.in");
            req.Headers.Add("x-microsoftajax", "Delta=true");
            req.Headers.Add("method", "post");
            req.Headers.Add("path", "/User/Selection_Report_For_KhasraDetail.aspx");
            req.Headers.Add("scheme", "https");
            req.Headers.Add("X-Requested-With", "XMLHttpRequest");
            req.Headers.Add("origin", "https://revenue.cg.nic.in");
            req.Headers.Add("referer", BASE_URL);
            req.Headers.Add("sec-fetch-dest", "empty");
            req.Headers.Add("sec-fetch-mode", "cors");
            req.Headers.Add("sec-fetch-site", "same-origin");
            req.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36");
            req.Content.Headers.ContentType = (new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("revenue.cg.nic.in");
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

            var Res = await client1.SendAsync(req);
            var html_s2 = await Res.Content.ReadAsStringAsync();
            // Console.WriteLine(html_s2);
            var array = html_s2.Split("|");
            Console.WriteLine(array.Length);
            pairs = new List<Tuple<String, String>>();
            int index_viewstate = Array.IndexOf(array, "__VIEWSTATE") + 1;
            int index_previouspage = Array.IndexOf(array, "__PREVIOUSPAGE") + 1;
            int index_viewstategenerator = Array.IndexOf(array, "__VIEWSTATEGENERATOR") + 1;
            int index_eventvalidation = Array.IndexOf(array, "__EVENTVALIDATION") + 1;
            pairs.Add(new Tuple<String, String>("__VIEWSTATE", array[index_viewstate]));
            pairs.Add(new Tuple<String, String>("__PREVIOUSPAGE", array[index_previouspage]));
            pairs.Add(new Tuple<String, String>("__VIEWSTATEGENERATOR", array[index_viewstategenerator]));
            pairs.Add(new Tuple<String, String>("__EVENTVALIDATION", array[index_eventvalidation]));
            Console.WriteLine("Step 6 Complete");
            try
            {
                return GetData(html_s2, khasra_no);
            }
            catch (Exception)
            {

               return null;
            }
           
            
            
        }

        private  DataTable GetData(string html, string khasra)
        {
            var tableResult = new DataTable();
            tableResult.Columns.Add("Name", typeof(string));
            tableResult.Columns.Add("Khasra_Number", typeof(string));
            tableResult.Columns.Add("Total_Land", typeof(decimal));
            tableResult.Columns.Add("Land_Type", typeof(string));
            tableResult.Columns.Add("Charged_By_Bank", typeof(string));
            tableResult.Columns.Add("Status_Of_Charge", typeof(string));
            tableResult.Columns.Add("Status_Of_Account", typeof(string));
            tableResult.Columns.Add("Fasal_Sinchit", typeof(string));
            tableResult.Columns.Add("Fasal_ASinchit", typeof(string));
            tableResult.Columns.Add("B1Document", typeof(string));
            tableResult.Columns.Add("B1Name", typeof(string));
            tableResult.Columns.Add("rajaswa", typeof(string));
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var total_land_value = default(decimal);
            bool isExsitslblanyakhasara = doc.DocumentNode.SelectSingleNode("//span[@id='lblanyakhasara']") == null;
            string lblanyakhasara = "";
            if (isExsitslblanyakhasara == false)
            {
                lblanyakhasara = doc.DocumentNode.SelectSingleNode("//span[@id='lblanyakhasara']").InnerText;
                total_land_value = Regex.Matches(lblanyakhasara, @"\((.*?)(?:हे॰\))").Cast<Match>().Select(m => m.Groups[1].Value).ToList().Sum(m => Convert.ToDecimal(m));


            }

            var One_Khasra_value = default(decimal);
            string s;
            string s1;
            bool isExsitslblkhsarano = doc.DocumentNode.SelectSingleNode("//span[@id='lblkhsarano']") == null;
            string lblkhsarano = "";
            if (isExsitslblkhsarano == false)
            {
                lblkhsarano = doc.DocumentNode.SelectSingleNode("//span[@id='lblkhsarano']").InnerText;
                s = lblkhsarano.Substring(lblkhsarano.IndexOf("( ") + 1);
                s1 = s.Replace(" हे॰ )", "");
                One_Khasra_value = Decimal.Parse(s1);
            }

            string final_Land_Value = (total_land_value + One_Khasra_value).ToString();
            bool isExsitslblcaste = doc.DocumentNode.SelectSingleNode("//span[@id='lblcaste']") == null;
            string lblcaste = "";
            if (isExsitslblcaste == false)
            {
                lblcaste = doc.DocumentNode.SelectSingleNode("//span[@id='lblcaste']").InnerText;
            }
            bool isExsitslblbhuswami = doc.DocumentNode.SelectSingleNode("//span[@id='lblbhuswami']") == null;
            string lblbhuswami = "";
            if (isExsitslblbhuswami == false)
            {
                lblbhuswami = doc.DocumentNode.SelectSingleNode("//span[@id='lblbhuswami']").InnerText;

            }
            var signedb1 = doc.DocumentNode.SelectSingleNode("//*[@id='hyPdf']");
            var b1doc = "NA";
            var b1name = "";
            if (signedb1 != null)
            {
                b1doc = signedb1.GetAttributeValue("href","NA");
                b1name = signedb1.InnerText;
            }
            var element = doc.DocumentNode.SelectSingleNode(" // img[@id='GridView1_img_bnklca_y_0']");
            string BankName = "";
            string bnklca_y = "";
            string bnklc_n = "";
            if (element != null)
            {
                string Bank_Name = doc.DocumentNode.SelectSingleNode("//span[@id='GridView1_lblbnkname_0']").InnerText;
                string Image_Name = doc.DocumentNode.SelectSingleNode("//img[@id='GridView1_img_bnklca_y_0']").OuterHtml;
                if (Image_Name.Contains("yes.png"))
                {
                    BankName = Bank_Name;
                    bnklca_y = "बैंक में बंधक";
                    bnklc_n = "ऋण समाप्त नहीं";
                }
                else
                {
                    bnklca_y = "बैंक में बंधक नहीं";
                }
            }
            else
            {
                BankName = "बैंक में बंधक नहीं";
                bnklca_y = "बैंक में बंधक नहीं";
                bnklc_n = "बैंक में बंधक नहीं";
            }
            bool isExsitslblfasal1 = doc.DocumentNode.SelectSingleNode("//span[@id='lblfasal1']") == null;
            string lblfasal1 = "";
            if (isExsitslblfasal1 == false)
            {
                lblfasal1 = doc.DocumentNode.SelectSingleNode("//span[@id='lblfasal1']").InnerText.Trim();
            }
            bool isExsitslblfasal2 = doc.DocumentNode.SelectSingleNode("//span[@id='lblfasal2']") == null;
            string lblfasal2 = "";
            if (isExsitslblfasal2 == false)
            {
                lblfasal2 = doc.DocumentNode.SelectSingleNode("//span[@id='lblfasal2']").InnerText;
            }

            bool isExistsRajaswa = doc.DocumentNode.SelectSingleNode("//*[@id='trrevcase']") == null;
            string rajaswaNyay = "";
            if (isExistsRajaswa == false)
            {
                rajaswaNyay = doc.DocumentNode.SelectSingleNode("//*[@id='trrevcase']").InnerText;
            }

            string Address = "";
            tableResult.Rows.Add(lblbhuswami, lblkhsarano + "," + lblanyakhasara, final_Land_Value, lblcaste, BankName, bnklca_y, bnklc_n, lblfasal1, lblfasal2.Trim(),b1doc,b1name,rajaswaNyay);
            // Dim Insert_Extracted_Data As String = String.Format("INSERT INTO Extracted_Data (Name, Total_Khasra, Total_Land_In_Hectare, Land_Type, Charged_By_Bank, Status_Of_Charge, Status_Of_Account, Fasal_Sinchit, Fasal_ASinchit, Address) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", lblbhuswami.Replace("""", " "), lblkhsarano.Replace("""", " ") & " " & lblanyakhasara.Replace("""", " "), final_Land_Value, lblcaste.Replace("""", " "), BankName.Replace("""", " "), bnklca_y, bnklc_n, lblfasal1.Replace("""", " "), lblfasal2.Replace("""", " "), Address.Replace("""", " "))
            // Dim cmd3 As SQLiteCommand = New SQLiteCommand(Insert_Extracted_Data, _con)
            // cmd3.ExecuteNonQuery()
            return tableResult;
            // Using bulk = New BulkOperation(_con)
            // bulk.DestinationTableName = "Extracted_Data"
            // bulk.BulkInsert(tableResult)
            // End Using
        }

  

    public async Task<IActionResult> FetchBhuiyan(string dDistrict = "19", string dTehsil = "12", string dRI = "03", string dVillage = "6603193", string dKhasra = "1/3")
        {
            dist = dDistrict;
            tehsil = dTehsil;
            ri = dRI;
            village = dVillage;
            khasra_no = dKhasra;

            var handler_s1 = new HttpClientHandler();
            handler_s1.UseProxy = true;
            handler_s1.Proxy = new WebProxy("10.43.5.6:3128");
            handler_s1.CookieContainer = cookies_s1;
            var client1 = new HttpClient(handler_s1);
            var html_s1 = await client1.GetStringAsync(STEP1_URL);
            pairs = GetAllInput(html_s1);
            Console.WriteLine("Step1 Done");
            await Step2();
            await Step3();
            await Step4();
            await Step5();
            var x = await Step6();
            Guid guid = Guid.NewGuid();
            Bhuiyan bhuiyan = new Bhuiyan();
            bhuiyan.Bhuswami = x.Rows[0][0].ToString();
            bhuiyan.LandDetails = x.Rows[0][1].ToString();
            bhuiyan.Charge = x.Rows[0][4].ToString();
            bhuiyan.DharanAdhikar = x.Rows[0][3].ToString();
            bhuiyan.B1Signed = x.Rows[0][9].ToString();
            bhuiyan.rajaswa = x.Rows[0][11].ToString();
            bhuiyan.B1Number = "NA";
            if (bhuiyan.B1Signed != "NA")
            {
                bhuiyan.B1Number = guid.ToString() + x.Rows[0][10].ToString() + ".pdf";
                var handler_s2 = new HttpClientHandler();
                handler_s2.UseProxy = true;
                handler_s2.Proxy = new WebProxy("10.43.5.6:3128");
                // handler_s1.CookieContainer = cookies_s1;
                 
                var cl = new HttpClient(handler_s2);
                var s = await cl.GetAsync(bhuiyan.B1Signed);

                var filePath = Path.Join(_appEnvironment.WebRootPath, bhuiyan.B1Number);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                using (var fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    await s.Content.CopyToAsync(fs);
                }
            }
            
            return Ok(bhuiyan);
        }

        private Tuple<string, decimal> GetLandDetails(string aa)
        {
            var arr = aa.Split('(');
            var land = decimal.Parse(arr[1].Replace("हे॰ )", "").Replace("हे॰)", "").Trim());
            Tuple<string, decimal> t = new Tuple<string, decimal>(arr[0].Trim(), land );
            return t;
        }

        public async Task<IActionResult> InsertBhuiyan(int AppId = 441,string dDistrict = "सूरजपुर", string dTehsil = "", string dRI = "", string dVillage = "", string ownKhasra = "", string otherKhasra = "393/1 (0.2300 हे॰), 394/1 (0.6200 हे॰)", string dharanAdhikar = "भूमिस्वामी - कृषि भूमि", string bhuswami = "(1) रतन कुमार जोत का प्रकार - अकेला", string b1 = "https://bhuiyan.cg.nic.in/administrator/CheckSignedFileExistance.aspx?DscUrl=B1/6407218/20220928640700027.pdf", string charge = "", string dCode = "", string dTeh = "", string dVill = "")
        {
            string state = "URL Hit";
            try
            {
                string b1copy = "";
                List<Tuple<string, decimal>> dists = new List<Tuple<string, decimal>>();
                dists.Add(GetLandDetails(ownKhasra));
                if (otherKhasra != null)
                {
                    foreach (var item in otherKhasra.Split(','))
                    {
                        dists.Add(GetLandDetails(item));
                    }
                }
                


                state = "Khasra Added";

                foreach (var item in dists)
                {
                    var c = _context.KCCLandDetails.Where(a => a.ApplicationId == AppId && a.KhasraNo == item.Item1 && a.Village == dVillage && a.District == dDistrict).FirstOrDefault();
                    if (c != null)
                    {
                        state = "Db  Start 1";
                        c.TotalArea = item.Item2;
                        _context.Entry(c).State = EntityState.Modified;
                        _context.SaveChanges();
                    }
                    else
                    {
                        state = "Db  Start 2";
                        KCCLandDetail detail = new KCCLandDetail();
                        detail.ApplicationId = AppId;
                        detail.District = dDistrict;
                        detail.Tehsil = dTehsil;
                        detail.RI = dRI;
                        detail.Village = dVillage;
                        detail.OwnerName = bhuswami;
                        detail.DharanAdhikar = dharanAdhikar;
                        detail.Charge = charge;
                        detail.KhasraNo = item.Item1;
                        detail.TotalArea = item.Item2;
                        detail.DistrictCode = dCode;
                        detail.TehsilCode = dTeh;
                        detail.VillageCode = dVill;
                        _context.KCCLandDetails.Add(detail);
                        _context.SaveChanges();
                        state = "Db  Added";
                    }

                }
                if (b1.Length > 10)
                {
                    
                    
                    b1copy = Guid.NewGuid().ToString().Replace("-", "").Substring(1, 8) + "-" + AppId.ToString() + ".pdf";
                    var handler_s2 = new HttpClientHandler();
                    handler_s2.UseProxy = true;
                    handler_s2.Proxy = new WebProxy("10.43.5.6:3128");
                    // handler_s1.CookieContainer = cookies_s1;

                    var cl = new HttpClient(handler_s2);
                    var s = await cl.GetAsync(b1);

                    var filePath = Path.Join(_appEnvironment.WebRootPath, b1copy);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    using (var fs = new FileStream(filePath, FileMode.CreateNew))
                    {
                        await s.Content.CopyToAsync(fs);
                    }
                    var exists = _context.Documents.Where(a => a.ApplicationId == AppId && a.Details == ownKhasra + " B1 Signed").FirstOrDefault();
                    if (exists == null)
                    {
                        Document document = new Document();
                        document.ApplicationId = AppId;
                        document.UploadDate = DateTime.Now;
                        document.Details = ownKhasra + " B1 Signed";
                        document.FilePath = b1copy;
                        document.UserId = "Mobile App";
                        _context.Documents.Add(document);
                        _context.SaveChanges();
                    }
                   
                    state = "B1 Saved";
                }
                

                return Ok($"Success {AppId} | {dVillage} | {state} | {b1copy}");
            }
            catch (Exception exp)
            {

                return Ok($"Error {state} | {exp.Message} | {ownKhasra} | {dVillage} | {otherKhasra} | {AppId}");
            }
           
        }
        public async Task<IActionResult> InsertBhuiyanAccount(string AppId = "441", string dDistrict = "सूरजपुर", string dTehsil = "", string dRI = "", string dVillage = "", string ownKhasra = "", string otherKhasra = "393/1 (0.2300 हे॰), 394/1 (0.6200 हे॰)", string dharanAdhikar = "भूमिस्वामी - कृषि भूमि", string bhuswami = "(1) रतन कुमार जोत का प्रकार - अकेला", string b1 = "https://bhuiyan.cg.nic.in/administrator/CheckSignedFileExistance.aspx?DscUrl=B1/6407218/20220928640700027.pdf", string charge = "",string dCode = "",string dTeh = "",string dVill = "")
        {
            string state = "URL Hit";
            //try
            //{
                string b1copy = "";
                List<Tuple<string, decimal>> dists = new List<Tuple<string, decimal>>();
                dists.Add(GetLandDetails(ownKhasra));
                if (otherKhasra != null)
                {
                    foreach (var item in otherKhasra.Split(','))
                    {
                        dists.Add(GetLandDetails(item));
                    }
                }



                state = "Khasra Added";

                foreach (var item in dists)
                {
                    var c = _context.KCCExistingLand.Where(a => a.AccountNo == AppId && a.KhasraNo == item.Item1 && a.Village == dVillage && a.District == dDistrict).FirstOrDefault();
                    if (c != null)
                    {
                        state = "Db  Start 1";
                        c.TotalArea = item.Item2;
                        _context.Entry(c).State = EntityState.Modified;
                        _context.SaveChanges();
                    }
                    else
                    {
                        state = "Db  Start 2";
                        KCCExistingLand detail = new KCCExistingLand();
                        detail.AccountNo = AppId;
                        detail.District = dDistrict;
                        detail.Tehsil = dTehsil;
                        detail.RI = dRI;
                        detail.Village = dVillage;
                        detail.OwnerName = bhuswami;
                        detail.DharanAdhikar = dharanAdhikar;
                        detail.Charge = charge;
                        detail.KhasraNo = item.Item1;
                        detail.TotalArea = item.Item2;
                        detail.DistrictCode = dCode;
                        detail.TehsilCode = dTeh;
                        detail.VillageCode = dVill;
                        _context.KCCExistingLand.Add(detail);
                        _context.SaveChanges();
                        state = "Db  Added";
                    }

                }
            if (b1 != null)
            {
                if (b1.Length > 10)
                {


                    b1copy = Guid.NewGuid().ToString().Replace("-", "").Substring(1, 8) + "-" + AppId.ToString() + ".pdf";
                    var handler_s2 = new HttpClientHandler();
                    handler_s2.UseProxy = true;
                    handler_s2.Proxy = new WebProxy("10.43.5.6:3128");
                    // handler_s1.CookieContainer = cookies_s1;

                    var cl = new HttpClient(handler_s2);
                    var s = await cl.GetAsync(b1);

                    var filePath = Path.Join(_appEnvironment.WebRootPath, b1copy);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    using (var fs = new FileStream(filePath, FileMode.CreateNew))
                    {
                        await s.Content.CopyToAsync(fs);
                    }
                    var exists = _context.DocumentLoan.Where(a => a.AccountNo == AppId && a.Details == ownKhasra + " B1 Signed").FirstOrDefault();
                    if (exists == null)
                    {
                        DocumentLoan document = new DocumentLoan();
                        document.AccountNo = AppId;
                        document.UploadDate = DateTime.Now;
                        document.Details = ownKhasra + " B1 Signed";
                        document.FilePath = b1copy;
                        document.UserId = "Mobile App";
                        _context.DocumentLoan.Add(document);
                        _context.SaveChanges();
                    }

                    state = "B1 Saved";
                }
            }
            


                return Ok($"Success {AppId} | {dVillage} | {state} | {b1copy}");
            //}
            //catch (Exception exp)
            //{

            //    return Ok($"Error {state} | {exp.Message} | {ownKhasra} | {dVillage} | {otherKhasra} | {AppId}");
            //}

        }
        public IActionResult GetDistrict()
        {
            //ViewBag.ApplicationId = Id;
            var dist1 = _context.KeyValues.FromSqlRaw($"SELECT Distinct dist_value as code, dist_name as value  FROM loanflow.villagemaster ;").ToList();
            List<Tuple<string, string>> dists = new List<Tuple<string, string>>();
            foreach (var dist in dist1)
            {
                dists.Add(new Tuple<string, string>(dist.code, dist.value));
            }
            return Ok(dists);
        }

        public IActionResult GetTehsil(string Id)
        {
            ViewBag.ApplicationId = Id;
            var dist1 = _context.KeyValues.FromSqlRaw($"SELECT Distinct tehsil_val as code, tehsil_name as value  FROM loanflow.villagemaster where dist_value = '{Id}';").ToList();
            List<Tuple<string, string>> dists = new List<Tuple<string, string>>();
            foreach (var dist in dist1)
            {
                dists.Add(new Tuple<string, string>(dist.code, dist.value));
            }
             return Ok(dists);
        }
        public IActionResult GetRI(string Id,string Dist)
        {
            ViewBag.ApplicationId = Id;
            var dist1 = _context.KeyValues.FromSqlRaw($"SELECT Distinct ri_val as code, ri_text as value  FROM loanflow.villagemaster where dist_value = '{Dist}' and tehsil_val = '{Id}';").ToList();
            List<Tuple<string, string>> dists = new List<Tuple<string, string>>();
            foreach (var dist in dist1)
            {
                dists.Add(new Tuple<string, string>(dist.code, dist.value));
            }
            return Ok(dists);
        }
        public IActionResult GetVillage(string Id, string Dist,string tehsil)
        {
            ViewBag.ApplicationId = Id;
            var dist1 = _context.KeyValues.FromSqlRaw($"SELECT Distinct vil_val as code, vil_name as value  FROM loanflow.villagemaster where dist_value = '{Dist}' and tehsil_val = '{tehsil}' and ri_val='{Id}';").ToList();
            List<Tuple<string, string>> dists = new List<Tuple<string, string>>();
            foreach (var dist in dist1)
            {
                dists.Add(new Tuple<string, string>(dist.code, dist.value));
            }
            return Ok(dists);
        }
    }
}
