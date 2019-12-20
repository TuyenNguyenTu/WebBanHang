using Model.EntityF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
  public  class ContentTagDAO
    {
        OnlineShopDbContext db = null;
        public ContentTagDAO()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(ContentTag ct)
        {
            
            db.ContentTags.Add(ct);
            db.SaveChanges();
            return ct.ContentID;
        }
        public IEnumerable<ContentTag> ListContent(string search, int page, int pageSize)
        {
            IQueryable<ContentTag> model = db.ContentTags;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.TagID.Contains(search));
            }
            return model.OrderByDescending(x => x.ContentID).ToPagedList(page, pageSize);
        }
        public ContentTag ViewDetail(int id)
        {
            return db.ContentTags.SingleOrDefault(x=>x.ContentID==id);
        }
        public bool Update(ContentTag contac)
        {
            try
            {
                var con = db.ContentTags.Find(contac.ContentID);
                con.TagID = contac.TagID;
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
                var ab = db.ContentTags.Find(id);
                db.ContentTags.Remove(ab);
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
