using Model.DAO;
using Model.EntityF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class FeedBackController : BaseController
    {
        // GET: Admin/FeedBack
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new FeedBackDAO();
            var model = dao.ListContent(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FeedBack con)
        {
            if (ModelState.IsValid)
            {
                var dao = new FeedBackDAO();
                long id = dao.Insert(con);
                if (id > 0)
                {
                    
                    return RedirectToAction("Index", "FeedBack");
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
        public ActionResult Edit(FeedBack ct)
        {
            if (ModelState.IsValid)
            {
                var dao = new FeedBackDAO();
                var result = dao.Update(ct);
                if (result == true)
                {
                    return RedirectToAction("Index", "FeedBack");
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
            var conten = new FeedBackDAO().ViewDetail(id);

            return View(conten);
        }
        [HttpDelete]///method
        public ActionResult Delete(int id)
        {
            new FeedBackDAO().Delete(id);
            return RedirectToAction("Index");
        }
    }
}