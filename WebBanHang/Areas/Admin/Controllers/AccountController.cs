using Model.DAO;
using Model.EntityF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Common;
using PagedList;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Admin/Account
        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize">Số lượng dòng 1 trang</param>
        /// <returns></returns>
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new AccountDAO();
            var model = dao.ListAcc(searchString,page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet] //khi tải trang
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost] //khi form đc post lên
        public ActionResult Create(Account acc)
        {
            if (ModelState.IsValid)
            {
                var dao = new AccountDAO();
                var encryptedMd5Pass = Encryptor.MD5Hash(acc.PassWord);
                acc.PassWord = encryptedMd5Pass;
                long id = dao.Insert(acc);
                if (id > 0)
                {
                    SetAlert("Thêm thành công", "success");
                    return RedirectToAction("Index", "Account");

                }
                else
                {
                    SetAlert("Thêm không thành công", "error");
                    ModelState.AddModelError("", "Thêm thất bại");
                }

            }
            return View("Index");
        }
           [HttpPost] //khi form đc post lên
        public ActionResult Edit(Account acc)
        {
            if (ModelState.IsValid)
            {
                var dao = new AccountDAO();
                //người dùng k nhập pass => không update
                if (!string.IsNullOrEmpty(acc.PassWord))
                {
                    var encryptedMd5Pass = Encryptor.MD5Hash(acc.PassWord);
                    acc.PassWord = encryptedMd5Pass;
                }
               
                var result = dao.Update(acc);
                if (result == true)
                {
                    SetAlert("Sửa thành công", "success");
                    return RedirectToAction("Index", "Account");

                }
                else
                {
                    SetAlert("Sửa không thành công", "error");
                    ModelState.AddModelError("", "Cập nhật thất bại");
                }

            }
            return View("Index");
        }

        public ActionResult Edit(int id)
        {
            var acc = new AccountDAO().ViewDetail(id);
            return View(acc);
        }

        [HttpDelete]///method
        public ActionResult Delete(int id)
        {
            new AccountDAO().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangStatus(long id)
        {
            var dao = new AccountDAO().ChangStatus(id);
            
            return Json(new
            {
                status = dao
            });

        }
    }
}