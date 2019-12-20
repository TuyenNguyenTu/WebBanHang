using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var dao = new ProductDAO();
            ViewBag.ListAllProduct = dao.ListAllProduct();
            return View();
        }
        public ActionResult FeaturedProducts()
        {
            var dao = new ProductDAO();
            ViewBag.FeaturedProducts = dao.DanhSachSanPhamNoiBat();
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDAO().ListAll();

            return PartialView(model);
        }
        public ActionResult Category(long id, int page = 1, int pageSize = 3 )
        {
            var cate = new ProductCategoryDAO().ViewDetail(id);
            ViewBag.Category = cate;
            int totalRecord = 0;
            var model = new ProductDAO().ListProductByCategory(id, ref totalRecord,page,pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            int maxPage = 3;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
        public ActionResult Detail(long id)
        {
            var productDAO = new ProductDAO();
            var product = new ProductDAO().ViewDetail(id);
            ViewBag.Category = new ProductCategoryDAO().ViewDetail(product.CategoryID.Value);
            ViewBag.RelatedProduct = productDAO.ListRelatedProducts(id);
            return View(product);
        }
    }
}