using Model.EntityF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
   public class MenuDAO
    {
        OnlineShopDbContext db = null;
        public MenuDAO()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(Menu ab)
        {
           
            db.Menus.Add(ab);
            db.SaveChanges();
            return ab.ID;
        }
        public IEnumerable<Menu> ListContent(string search, int page, int pageSize)
        {
            IQueryable<Menu> model = db.Menus;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Text.Contains(search) || x.Target.Contains(search)||x.Link.Contains(search));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
        public Menu ViewDetail(int id)
        {
            return db.Menus.Find(id);
        }
        public List<Menu> ListByGroupID(long groupID)
        {
            return db.Menus.Where(x => x.TypeID == groupID && x.Status==true).OrderBy(x=>x.DisplayOrder).ToList();
        }
        public bool Update(Menu mn)
        {
            try
            {
                var menu = db.Menus.Find(mn.ID);
                menu.Text = mn.Text;
                menu.Link = mn.Link;
                menu.DisplayOrder = mn.DisplayOrder;
                menu.Target = mn.Target;
                menu.Status = mn.Status;
                menu.TypeID = mn.TypeID;
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
                var ab = db.Menus.Find(id);
                db.Menus.Remove(ab);
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
