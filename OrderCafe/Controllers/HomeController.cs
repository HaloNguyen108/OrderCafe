using OrderCafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderCafe.Controllers
{
    public class HomeController : Controller
    {

        
        public ActionResult GiaoDien()
        {
            // Chỉ định rõ view trang chủ dùng chung thay vì dựa vào tên action.
            return View("GiaoDien");
        }

        public ActionResult ThongTin()
        {
            // MVC tự tìm view Home/ThongTin.cshtml theo tên action.
            return View();
        }

        public ActionResult DangNhap()
        {
            // Action này cung cấp trang đăng nhập theo luồng giao diện hiện có.
            return View();
        }

        



    }
}
