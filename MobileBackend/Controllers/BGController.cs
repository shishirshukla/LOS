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
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MobileBackend.Controllers
{
    public class BGController : Controller
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public BGController(IWebHostEnvironment appEnvironment, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ApplicationDbContext context)
        {
            _appEnvironment = appEnvironment;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }

        public IActionResult GetDetails(string account , Single  amt )
        {
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection())
            {
                conn.ConnectionString = _configuration.GetConnectionString("ConnStr1");
                conn.Open();
                using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = $"SELECT \"uploadFileStr\" 	FROM public.\"BGLoanInfo\" Where \"Account_No\" ='{account}' and \"Amount\" = {amt}";
                    var z = cmd.ExecuteReader();
                    if (z.Read())
                    {
                       string k = z[0].ToString();
                            
                        var result = Convert.FromBase64String(k);
                        var filePath = Path.Join(_appEnvironment.WebRootPath, $"{account}.pdf");
                        System.IO.File.WriteAllBytes(filePath, result);
                        return Content("Success");
                    }
                    else
                    {
                        return Content("");
                    }
                    
                }
            }
            
        }
        public IActionResult AddBGDetails() {
            return View();
        }
      

    }
}
