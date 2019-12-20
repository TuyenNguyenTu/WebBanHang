using Model.EntityF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model.DAO
{
   public class TagDAO
    {
        OnlineShopDbContext db = null;
        public TagDAO()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(Tag ab)
        {
            
            db.Tags.Add(ab);
            db.SaveChanges();
            return 1;
        }
        public List<Tag> LissAll()
        {
            return db.Tags.ToList();
        }
        public IEnumerable<Tag> ListContent(string search, int page, int pageSize)
        {
            IQueryable<Tag> model = db.Tags;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Name.Contains(search));
            }
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }
        public Tag ViewDetail(string id)
        {
            return db.Tags.Find(id);
        }
        public bool Update(Tag sl)
        {
            try
            {
                var tag = db.Tags.Find(sl.ID);
                tag.Name = sl.Name;
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
                var ab = db.Tags.Find(id);
                db.Tags.Remove(ab);
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
