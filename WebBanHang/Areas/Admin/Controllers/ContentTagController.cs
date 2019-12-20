using Model.DAO;
using Model.EntityF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class ContentTagController : BaseController
    {
        // GET: Admin/ContentTag
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ContentTagDAO();
            var model = dao.ListContent(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ContentTag con)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContentTagDAO();
                long id = dao.Insert(con);
                if (id > 0)
                {
                    return RedirectToAction("Index", "ContentTag");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công");
                }
            }
            SetViewBag();
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ContentTag ct)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContentTagDAO();
                var result = dao.Update(ct);
                if (result == true)
                {
                    return RedirectToAction("Index", "ContentTag");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thất bại");
                }
            }
            SetViewBag(ct.TagID);
            return View("Index");
        }
        [ValidateInput(false)]
        public ActionResult Edit(int id)
        {
            var conten = new ContentTagDAO().ViewDetail(id);
            SetViewBag(conten.TagID);
            return View(conten);
        }
        [HttpDelete]///method
        public ActionResult Delete(int id)
        {
            new ContentTagDAO().Delete(id);
            return RedirectToAction("Index");
        }
        public void SetViewBag(string selectedID = null)
        {
            var dao = new TagDAO();
            ViewBag.TagID = new SelectList(dao.LissAll(), "ID", "Name", selectedID);
        }
    }
}