using Model.DAO;
using Model.EntityF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class TagController : BaseController
    {
        // GET: Admin/Tag
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new TagDAO();
            var model = dao.ListContent(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Tag con)
        {
            if (ModelState.IsValid)
            {
                var dao = new TagDAO();
                long id = dao.Insert(con);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Tag");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công");
                }
            }

            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Tag ct)
        {
            if (ModelState.IsValid)
            {
                var dao = new TagDAO();
                var result = dao.Update(ct);
                if (result == true)
                {
                    return RedirectToAction("Index", "Tag");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thất bại");
                }
            }
            return View("Index");
        }
        [ValidateInput(false)]
        public ActionResult Edit(string id)
        {
            var conten = new TagDAO().ViewDetail(id);

            return View(conten);
        }
        [HttpDelete]///method
        public ActionResult Delete(string id)
        {
            new TagDAO().Delete(id);
            return RedirectToAction("Index");
        }
    }
}