using Model.EntityF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
  public  class ProductCategoryDAO
    {
        OnlineShopDbContext db = null;
        public ProductCategoryDAO()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(ProductCategory ab)
        {
            ab.MetaTitle = XuLyChuoi.GetMetaTitle(ab.Name);
            ab.CreatedDate = DateTime.Now;
            db.ProductCategories.Add(ab);
            db.SaveChanges();
            return ab.ID;
        }
        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories.Where(x => x.Status == true).OrderBy(x=>x.DisplayOrder).ToList();
        }
        public IEnumerable<ProductCategory> ListContent(string search, int page, int pageSize)
        {
            IQueryable<ProductCategory> model = db.ProductCategories;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Name.Contains(search) || x.MetaTitle.Contains(search));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
        public bool Update(ProductCategory pc)
        {
            try
            {
                var productcate = db.ProductCategories.Find(pc.ID);
                productcate.Name = pc.Name;
                productcate.MetaTitle = XuLyChuoi.GetMetaTitle(pc.Name);
                productcate.ParentID = pc.ParentID;
                productcate.DisplayOrder = pc.DisplayOrder;
                productcate.SeoTitle = pc.SeoTitle;
                productcate.ModifiedDate = DateTime.Now;
                productcate.Status = pc.Status;
                productcate.ShowOnHome = pc.ShowOnHome;
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
                var ab = db.ProductCategories.Find(id);
                db.ProductCategories.Remove(ab);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public List<ProductCategory> ListProductCategory()
        {
            return db.ProductCategories.Where(x=>x.Status==true).ToList();
        }
    }
}
