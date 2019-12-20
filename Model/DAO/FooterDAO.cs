using Model.EntityF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
  public  class FooterDAO
    {
        OnlineShopDbContext db = null;
        public FooterDAO()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(Footer ab)
        {
            
            db.Footers.Add(ab);
            db.SaveChanges();
            return 1;
        }
        public Footer GetFooter()
        {
            return db.Footers.SingleOrDefault(x => x.Status == true);
        }
        public IEnumerable<Footer> ListContent(string search, int page, int pageSize)
        {
            IQueryable<Footer> model = db.Footers;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Content.Contains(search));
            }
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }
        public Footer ViewDetail(string id)
        {
            return db.Footers.Find(id);
        }
        public bool Update(Footer ft)
        {
            try
            {
                var foot = db.Footers.Find(ft.ID);
                foot.Content = ft.Content;
                foot.Status = ft.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Delete(string id)
        {
            try
            {
                var ab = db.Footers.Find(id);
                db.Footers.Remove(ab);
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
