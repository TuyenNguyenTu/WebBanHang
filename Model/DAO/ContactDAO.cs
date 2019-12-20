using Model.EntityF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
   public class ContactDAO
    {
        OnlineShopDbContext db = null;
        public ContactDAO()
        {
            db = new OnlineShopDbContext();
        }
        public Contact GetActiveContact()
        {
            return db.Contacts.Single(x => x.Status == true);
        }
        public long Insert(Contact ab)
        {
            db.Contacts.Add(ab);
            db.SaveChanges();
            return ab.ID;
        }
        public IEnumerable<Contact> ListContent(string search, int page, int pageSize)
        {
            IQueryable<Contact> model = db.Contacts;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Content.Contains(search));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
        public Contact ViewDetail(int id)
        {
            return db.Contacts.Find(id);
        }
        public bool Update(Contact contac)
        {
            try
            {
                var con = db.Contacts.Find(contac.ID);
                con.Content = contac.Content;
                con.Status = contac.Status;
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
                var ab = db.Contacts.Find(id);
                db.Contacts.Remove(ab);
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
