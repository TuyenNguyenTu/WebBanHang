using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Tuyen = new SlidesDAO().ListAll();
            var productDAO = new ProductDAO();
            ViewBag.NewProduct = productDAO.DanhSachSP(4);
            ViewBag.ListFeatureProduct = productDAO.ListFeartureProduct(4);
            return View();
        }
        [ChildActionOnly]
        public ActionResult Slides()
        {
            var model = new SlidesDAO().ListAll();

            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            var model = new MenuDAO().ListByGroupID(2);

            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[WebBanHang.Common.CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return PartialView(list);
        }
        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = new FooterDAO().GetFooter();

            return PartialView(model);
        }

    }
}