using Model.DAO;
using Model.EntityF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new CategoryDAO();
            var model = dao.ListContent(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Category con)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDAO();
                long id = dao.Insert(con);
                if (id > 0)
                {
                    
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công");
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Edit(Category ct)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDAO();
                var result = dao.Update(ct);
                if (result == true)
                {
                   
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thất bại");
                }
            }
            return View("Index");
        }

        public ActionResult Edit(int id)
        {
            var conten = new CategoryDAO().ViewDetail(id);

            return View(conten);
        }
        [HttpDelete]///method
        public ActionResult Delete(int id)
        {
            new CategoryDAO().Delete(id);
            return RedirectToAction("Index");
        }
    }
}