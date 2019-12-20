using Model.DAO;
using Model.EntityF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var model = new ContactDAO().GetActiveContact();
            return View(model);
        }
        [HttpPost]
        public JsonResult Send(string name, string moblie, string address, string email, string content)
        {
            var feedBack = new FeedBack();
            feedBack.Name = name;
            feedBack.Phone = moblie;
            feedBack.Address = address;
            feedBack.Email = email;
            feedBack.Content = content;
            feedBack.CreatedDate = DateTime.Now;

           var id =  new FeedBackDAO().Insert(feedBack);
            if (id > 0)
            {
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }

        }
    }
}