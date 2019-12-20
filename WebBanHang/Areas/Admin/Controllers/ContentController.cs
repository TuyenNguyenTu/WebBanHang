using Model.DAO;
using Model.EntityF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace WebBanHang.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Admin/Content


        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ContentDAO();
            var model = dao.ListContent(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Content con)
        {
            if (ModelState.IsValid)
            {

                var dao = new ContentDAO();
                long id = dao.Insert(con);
                if (id > 0)
                {
    
                    return RedirectToAction("Index", "Content");
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
        public ActionResult Edit(Content ct)
        {
            if (ModelState.IsValid)
            {
                SetViewBag();
                var dao = new ContentDAO();
                var result = dao.Update(ct);
                if (result == true)
                {

                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thất bại");
                }
            }
            SetViewBag(ct.CategoryID);
            return View("Index");
        }

        public ActionResult Edit(int id)
        {

            var conten = new ContentDAO().ViewDetail(id);
            SetViewBag(conten.CategoryID);
            return View(conten);
        }
        [HttpDelete]///method
        public ActionResult Delete(int id)
        {
            new ContentDAO().Delete(id);
            return RedirectToAction("Index");
        }
        public void SetViewBag(long? selectedID = null)
        {
            var dao = new CategoryDAO();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedID);
        }
    }
}