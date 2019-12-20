using Model.EntityF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class SlidesDAO
    {
        OnlineShopDbContext db = null;
        public SlidesDAO()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(Slide ab)
        {
            ab.CreatedDate = DateTime.Now;
            db.Slides.Add(ab);
            db.SaveChanges();
            return ab.ID;
        }

        public List<Slide> ListAll()
        {
            return db.Slides.Where(x => x.Status == true).ToList();
        }

        public IEnumerable<Slide> ListContent(string search, int page, int pageSize)
        {
            IQueryable<Slide> model = db.Slides;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Decription.Contains(search));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public Slide ViewDetail(int id)
        {
            return db.Slides.Find(id);
        }
        public bool Update(Slide sl)
        {
            try
            {
                var slide = db.Slides.Find(sl.ID);
                slide.Image = sl.Image;
                slide.DisplayOrder = sl.DisplayOrder;
                slide.Link = sl.Link;
                slide.Decription = sl.Decription;
                slide.ModifiedDate = DateTime.Now;
                slide.Status = sl.Status;
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
                var ab = db.Slides.Find(id);
                db.Slides.Remove(ab);
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
