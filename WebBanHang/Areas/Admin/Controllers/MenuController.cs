using Model.DAO;
using Model.EntityF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Admin/Menu
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new MenuDAO();
            var model = dao.ListContent(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Menu con)
        {
            if (ModelState.IsValid)
            {
                var dao = new MenuDAO();
                long id = dao.Insert(con);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Menu");
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
        public ActionResult Edit(Menu ct)
        {
            if (ModelState.IsValid)
            {
                var dao = new MenuDAO();
                var result = dao.Update(ct);
                if (result == true)
                {
                    return RedirectToAction("Index", "Menu");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thất bại");
                }
            }
            SetViewBag(ct.TypeID);
            return View("Index");
        }

        public ActionResult Edit(int id)
        {
            var conten = new MenuDAO().ViewDetail(id);
            SetViewBag(conten.TypeID);
            return View(conten);
        }
        [HttpDelete]///method
        public ActionResult Delete(int id)
        {
            new MenuDAO().Delete(id);
            return RedirectToAction("Index");
        }
        public void SetViewBag(long? selectedID = null)
        {
            var dao = new MenuTypeDAO();
            ViewBag.TypeID = new SelectList(dao.LissAll(), "ID", "Name", selectedID);
        }
    }
}