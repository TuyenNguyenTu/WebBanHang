using Model.EntityF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
  public  class ProductDAO
    {
        OnlineShopDbContext db = null;
        public ProductDAO()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(Product ab)
        {
            ab.CreatedDate = DateTime.Now;
            ab.MetaTitle = XuLyChuoi.GetMetaTitle(ab.Name);
            db.Products.Add(ab);
            db.SaveChanges();
            return ab.ID;
        }
        public List<Product> ListAllProduct()
        {
            return db.Products.Where(x => x.Status == true).ToList();
        }
        public List<Product> ListProductByCategory(long id,  ref int totalRecord, int pageIndex = 1, int pageSize = 10)
        {
            totalRecord = db.Products.Where(x => x.CategoryID == id).OrderBy(x => x.CreatedDate).Count();
            var model =  db.Products.Where(x => x.CategoryID == id).OrderBy(x => x.CreatedDate).Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
            return model;
        }
        public List<Product> DanhSachSP(int top)
        {
            List<Product> items = new List<Product>();
            items = db.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
            return items;       
        }
        public List<Product> DanhSachSanPhamNoiBat()
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).Where(x => x.TopHot != null).ToList();
        }
        public List<Product> ListFeartureProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).Where(x => x.TopHot != null && x.TopHot>DateTime.Now).Take(top).ToList();
        }
        public List<Product> ListRelatedProducts(long productID)
        {
            var product = db.Products.Find(productID);
            return db.Products.OrderByDescending(x => x.CreatedDate).Where(x => x.ID != productID && x.CategoryID == product.CategoryID).ToList();
        }
        public IEnumerable<Product> ListContent(string search, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Name.Contains(search)||x.MetaTitle.Contains(search));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
        public bool Update(Product pro)
        {
            try
            {
                var product = db.Products.Find(pro.ID);
                product.Code = pro.Code;
                product.Name = pro.Name;
                product.Decription = pro.Decription;
                product.MetaTitle = XuLyChuoi.GetMetaTitle(pro.Name);
                product.Image = pro.Image;
                product.Price = pro.Price;
                product.PromotionPrice = pro.PromotionPrice;
                product.IncludeVAT = pro.IncludeVAT;
                product.Quantity = pro.Quantity;
                product.CategoryID = pro.CategoryID;
                product.Detail = pro.Detail;
                product.Warranty = pro.Warranty;
                product.ModifiedDate = DateTime.Now;
                product.Status = pro.Status;
                product.TopHot = pro.TopHot;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var ab = db.Products.Find(id);
                db.Products.Remove(ab);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
