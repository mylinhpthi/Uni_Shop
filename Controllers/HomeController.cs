using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Uni_Shop.ModelDBs;
using Uni_Shop.Models;

namespace Uni_Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        TN230Context db = new TN230Context();

        [Route("login")]
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("remember") != null)
            {
                return RedirectPermanentPreserveMethod("/");
            }
            LoginModel model = new LoginModel();
            return View(model);
        }
        [Route("login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.TenDangNhap == null)
                {
                    ModelState.AddModelError("", "Vui lòng nhập tên đăng nhập! ");
                    return View();
                }
                if (model.MatKhau == null)
                {
                    ModelState.AddModelError("", "Vui lòng nhập mật khẩu! ");
                    return View();
                }
                var res = db.TaiKhoans.Where(s => s.TenDangNhap == model.TenDangNhap && s.MatKhau == model.MatKhau).SingleOrDefault();
                if (res != null)
                {
                    if (model.Remember)
                    {
                        HttpContext.Session.SetString("remember", model.TenDangNhap);

                    }
                    HttpContext.Session.SetString("username", model.TenDangNhap);
                    return RedirectPermanentPreserveMethod("/");
                }
                else
                {
                    ModelState.AddModelError("", "Thông tin đăng nhập không đúng! ");
                }
            }
            else
            {
                ModelState.AddModelError("", "Thông tin đăng nhập nhập rỗng! ");
            }
            return View();
        }
        [Route("register")]
        [HttpGet]
        public IActionResult Register()
        {
            TaiKhoan model = new TaiKhoan();
            return View(model);
        }

        [Route("register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("TenDangNhap, MatKhau,Email")] TaiKhoan tk)
        {
            var existAcc = db.TaiKhoans.FirstOrDefault(p => p.TenDangNhap == tk.TenDangNhap);
            if (existAcc != null)
            {
                ViewBag.error = "Tên đăng nhập đã tồn tại! ";
                return View();
            }
            tk.MaQuyen = 2;
            if (ModelState.IsValid)
            {
                db.Add(tk);
                await db.SaveChangesAsync();
                return RedirectPermanentPreserveMethod("/Login");
            }
            return View();
        }

        [Route("Logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectPermanentPreserveMethod("/Login");
        }


        public IActionResult Privacy()
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
