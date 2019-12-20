using Model.DAO;
using Model.EntityF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ProductCategoryDAO();
            var model = dao.ListContent(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult Create(ProductCategory con)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryDAO();
                long id = dao.Insert(con);
                if (id > 0)
                {

                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {

                    ModelState.AddModelError("", "Thêm mới không thành công");
                }
            }
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProductCategory ct)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryDAO();
                var result = dao.Update(ct);
                if (result == true)
                {
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thất bại");
                }
            }

            return View("Index");
        }
        [ValidateInput(false)]
        public ActionResult Edit(int id)
        {
            var conten = new ProductCategoryDAO().ViewDetail(id);
            return View(conten);
        }
        [HttpDelete]///method
        public ActionResult Delete(int id)
        {
            new ProductDAO().Delete(id);
            return RedirectToAction("Index");
        }
    }
}