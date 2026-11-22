using OrderCafe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OrderCafe.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        CoffeeOrderDBEntities db = new CoffeeOrderDBEntities();
        // GET: Admin/Category
        public ActionResult Index()
        {
            // Nạp toàn bộ danh mục để quản trị viên xem và chọn thao tác.
            var category = db.Categories.ToList();
            return View(category);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                // Thêm danh mục mới và lưu thay đổi vào database.
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("loi tao category");
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = db.Categories.Where(c => c.CategoryId == id).FirstOrDefault();
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(int id, Category category)
        {
            // Đánh dấu danh mục nhận từ form là bản ghi cần cập nhật.
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            }
            var category = db.Categories.Where(c => c.CategoryId == id).FirstOrDefault();
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // Xóa danh mục; database có thể từ chối nếu sản phẩm vẫn tham chiếu danh mục này.
                var category = db.Categories.Where(c => c.CategoryId == id).FirstOrDefault();
                db.Categories.Remove(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("khong xoa dc vi lien quan den bang khac");
            }
        }

    }
}
