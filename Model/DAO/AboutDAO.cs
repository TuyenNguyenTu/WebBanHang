using Model.EntityF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
  public class AboutDAO
    {
        OnlineShopDbContext db = null;
        public AboutDAO()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(About ab)
        {
            ab.CreatedDate = DateTime.Now;
            ab.MetaTitle = XuLyChuoi.GetMetaTitle(ab.Name);
            db.Abouts.Add(ab);
            db.SaveChanges();
            return ab.ID;
        }
        public About GetAbout()
        {
            return db.Abouts.SingleOrDefault(x => x.Status == true);
        }
        public IEnumerable<About> ListContent(string search, int page, int pageSize)
        {
            IQueryable<About> model = db.Abouts;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Name.Contains(search) || x.MetaTitle.Contains(search) || x.MetaKeyword.Contains(search));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public About ViewDetail(int id)
        {
            return db.Abouts.Find(id);
        }
        public bool Update(About ab)
        {
            try
            {
                var about = db.Abouts.Find(ab.ID);
                about.Image = ab.Image;
                about.Name = ab.Name;
                about.Status = ab.Status;
                about.MetaKeyword = ab.MetaKeyword;
                about.ModifiedDate = DateTime.Now;
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
                var ab = db.Abouts.Find(id);
                db.Abouts.Remove(ab);
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
