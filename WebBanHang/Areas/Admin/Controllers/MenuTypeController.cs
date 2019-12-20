using Model.DAO;
using Model.EntityF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class MenuTypeController : BaseController
    {
        // GET: Admin/MenuType
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new MenuTypeDAO();
            var model = dao.ListContent(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(MenuType con)
        {
            if (ModelState.IsValid)
            {
                var dao = new MenuTypeDAO();
                long id = dao.Insert(con);
                if (id > 0)
                {
                    return RedirectToAction("Index", "MenuType");
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
        public ActionResult Edit(MenuType ct)
        {
            if (ModelState.IsValid)
            {
                var dao = new MenuTypeDAO();
                var result = dao.Update(ct);
                if (result ==true)
                {
                    return RedirectToAction("Index", "MenuType");
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
            var conten = new MenuTypeDAO().ViewDetail(id);

            return View(conten);
        }
        [HttpDelete]///method
        public ActionResult Delete(int id)
        {
            new MenuTypeDAO().Delete(id);
            return RedirectToAction("Index");
        }
    }
}