using OrderCafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace OrderCafe.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        CoffeeOrderDBEntities SPdb = new CoffeeOrderDBEntities();
        public ActionResult Index(int? maTL)
        {
            // Nạp Category để view hiển thị tên danh mục mà không truy vấn riêng cho từng sản phẩm.
            var ds = SPdb.Products.Include("Category").AsQueryable();
            // Bộ lọc chỉ áp dụng khi người dùng đã chọn một mã thể loại.
            if (maTL.HasValue) ds = ds.Where(s => s.CategoryId == maTL.Value);
            ViewBag.Title = "Danh sách loại";
            return View(ds.ToList());
        }

        public ActionResult MenuTheLoai()
        {
            // Trả về một phần giao diện danh mục để có thể nhúng vào nhiều trang.
            return PartialView("CategoryPd", SPdb.Categories.ToList());
        }

        public ActionResult Details(int id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var product = SPdb.Products.Find(id); // or repository.Get(id)
            if (product == null)
                return HttpNotFound(); // or: return RedirectToAction("Index");

            return View(product);
        }
    }
}
