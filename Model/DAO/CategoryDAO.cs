using Model.EntityF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model.DAO
{
   public class CategoryDAO
    {
        OnlineShopDbContext db = null;
        public CategoryDAO()
        {
            db = new OnlineShopDbContext();
        }
        public List<Category> ListAll()
        {
            return db.Categories.Where(x=>x.Status == true).ToList();
        }
        public long Insert(Category category)
        {
            category.CreatedDate = DateTime.Now;
            db.Categories.Add(category);
            db.SaveChanges();
            return category.ID;
        }
        public IEnumerable<Category> ListContent(string search, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Name.Contains(search) || x.SeoTitle.Contains(search)||x.MetaKeyword.Contains(search));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public Category ViewDetail(int id)
        {
            return db.Categories.Find(id);
        }
        public bool Update(Category category)
        {
            try
            {
                var cate = db.Categories.Find(category.ID);
                cate.Name = category.Name;
                cate.ParentID = category.ParentID;
                cate.ShowOnHome = category.ShowOnHome;
                cate.Status = category.Status;
                cate.MetaDecriptions = category.MetaDecriptions;
                cate.MetaTitle = cate.MetaTitle;
                cate.ModifiedDate = DateTime.Now;
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
                var ab = db.Categories.Find(id);
                db.Categories.Remove(ab);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
