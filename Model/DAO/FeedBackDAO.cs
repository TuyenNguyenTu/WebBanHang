using Model.EntityF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
  public  class FeedBackDAO
    {
        OnlineShopDbContext db = null;
        public FeedBackDAO()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(FeedBack fb)
        {
            fb.CreatedDate = DateTime.Now;
            db.FeedBacks.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }
        public IEnumerable<FeedBack> ListContent(string search, int page, int pageSize)
        {
            IQueryable<FeedBack> model = db.FeedBacks;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Name.Contains(search) || x.Email.Contains(search)||x.Content.Contains(search));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public FeedBack ViewDetail(int id)
        {
            return db.FeedBacks.Find(id);
        }
        public bool Update(FeedBack fb)
        {
            try
            {
                var feed = db.FeedBacks.Find(fb.ID);
                feed.Name = fb.Name;
                feed.Phone = fb.Phone;
                feed.Email = fb.Email;
                feed.Content = fb.Content;
                feed.Status = fb.Status;
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
                var ab = db.FeedBacks.Find(id);
                db.FeedBacks.Remove(ab);
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
