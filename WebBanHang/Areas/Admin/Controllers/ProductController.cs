using Model.DAO;
using Model.EntityF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ProductDAO();
            var model = dao.ListContent(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product con)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDAO();
                long id = dao.Insert(con);
                if (id > 0)
                {
                   
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                   
                    ModelState.AddModelError("", "Thêm mới không thành công");
                }
            }
            SetViewBag();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product ct)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDAO();
                var result = dao.Update(ct);
                if (result == true)
                {
                    SetAlert("Sửa thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thất bại");
                }
            }
            SetViewBag(ct.CategoryID);
            return View("Index");
        }
        [ValidateInput(false)]
        public ActionResult Edit(int id)
        {
            var conten = new ProductDAO().ViewDetail(id);
            SetViewBag(conten.CategoryID);
            return View(conten);
        }
        [HttpDelete]///method
        public ActionResult Delete(int id)
        {
            new ProductDAO().Delete(id);
            return RedirectToAction("Index");
        }
        public void SetViewBag(long? selectedID = null)
        {
            var dao = new ProductCategoryDAO();
            ViewBag.CategoryID = new SelectList(dao.ListProductCategory(), "ID", "Name",selectedID);
        }
    }
}