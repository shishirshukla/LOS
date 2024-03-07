using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileBackend.Models;
using System.Data;
using System.Threading.Tasks;

namespace MobileBackend.Controllers
{
    public class ControlPointController : Controller
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public readonly CustomIDataProtection _protector;
        public ControlPointController(IWebHostEnvironment appEnvironment, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ApplicationDbContext context, CustomIDataProtection protector)
        {
            _appEnvironment = appEnvironment;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
            _protector = protector;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            DataTable dt = new DataTable();
            using (var conn = new Npgsql.NpgsqlConnection(_configuration.GetConnectionString("ConnStr")))
            {
                conn.Open();
                using (var cmd = new Npgsql.NpgsqlCommand())
                {
                    cmd.Connection = conn;
                   
                    cmd.CommandText = $"SELECT * from loanflow.loan_acc_excluding_closed_acc('{user.BranchId}')";
                    var rd = cmd.ExecuteReader();
                    dt.Load(rd);
                }

            }
            
            return View(dt);
        }

        public async Task<IActionResult> Summary()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            DataTable dt = new DataTable();
            using (var conn = new Npgsql.NpgsqlConnection(_configuration.GetConnectionString("ConnStr")))
            {
                conn.Open();
                using (var cmd = new Npgsql.NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = $"SELECT * from loanflow.lms_cbs_diff_cnt('{user.BranchId}')";
                    var rd = cmd.ExecuteReader();
                    dt.Load(rd);
                }

            }

            return View(dt);
        }

        public async Task<IActionResult> IndexLimit()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            DataTable dt = new DataTable();
            using (var conn = new Npgsql.NpgsqlConnection(_configuration.GetConnectionString("ConnStr")))
            {
                conn.Open();
                using (var cmd = new Npgsql.NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    
                    cmd.CommandText = $"SELECT * from loanflow.lms_cbs_diff_odlimit('{user.BranchId}')";
                    var rd = cmd.ExecuteReader();
                    dt.Load(rd);
                }

            }

            return View(dt);
        }
        public async Task<IActionResult> IndexROI()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            DataTable dt = new DataTable();
            using (var conn = new Npgsql.NpgsqlConnection(_configuration.GetConnectionString("ConnStr")))
            {
                conn.Open();
                using (var cmd = new Npgsql.NpgsqlCommand())
                {
                    cmd.Connection = conn;
                     
                    cmd.CommandText = $"SELECT * from loanflow.lms_cbs_diff_roi('{user.BranchId}')";
                    var rd = cmd.ExecuteReader();
                    dt.Load(rd);
                }

            }

            return View(dt);
        }
    }
}
