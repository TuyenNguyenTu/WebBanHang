using Model.DAO;
using Model.EntityF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class SlideDAOController : BaseController
    {
        // GET: Admin/SlideDAO
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new SlidesDAO();
            var model = dao.ListContent(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Slide con)
        {
            if (ModelState.IsValid)
            {
                var dao = new SlidesDAO();
                long id = dao.Insert(con);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Slide");
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
        public ActionResult Edit(Slide ct)
        {
            if (ModelState.IsValid)
            {
                var dao = new SlidesDAO();
                var result = dao.Update(ct);
                if (result == true)
                {
                    return RedirectToAction("Index", "Slide");
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
            var conten = new SlidesDAO().ViewDetail(id);

            return View(conten);
        }
        [HttpDelete]///method
        public ActionResult Delete(int id)
        {
            new SlidesDAO().Delete(id);
            return RedirectToAction("Index");
        }
    }
}