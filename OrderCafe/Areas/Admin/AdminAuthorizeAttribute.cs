using System;
using System.Linq;
using System.Web.Mvc;
using OrderCafe.Models;

namespace OrderCafe.Areas.Admin
{
    public class AdminAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            var role = session["role"] as string;
            var email = session["Email"] as string;

            bool isCustomer = string.Equals(role, "Customer", StringComparison.OrdinalIgnoreCase);
            bool isAdmin = false;

            // Xác minh email với bảng admin ở máy chủ; không dựa riêng vào role do Session lưu.
            if (!string.IsNullOrWhiteSpace(email) && !isCustomer)
            {
                using (var db = new CoffeeOrderDBEntities())
                {
                    isAdmin = db.AdminUsers.Any(admin => admin.Email == email);
                }
            }

            if (!isAdmin)
            {
                // Chặn mọi yêu cầu quản trị khi phiên đăng nhập không ánh xạ tới tài khoản admin.
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new { area = "", controller = "Account", action = "Index" }));
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
