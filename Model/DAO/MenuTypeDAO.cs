using Model.EntityF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
  public  class MenuTypeDAO
    {
        OnlineShopDbContext db = null;
        public MenuTypeDAO()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(MenuType mt)
        { 
            db.MenuTypes.Add(mt);
            db.SaveChanges();
            return mt.ID;
        }
        public List<MenuType> LissAll()
        {
            return db.MenuTypes.ToList();
        }
        public IEnumerable<MenuType> ListContent(string search, int page, int pageSize)
        {
            IQueryable<MenuType> model = db.MenuTypes;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Name.Contains(search));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
        public MenuType ViewDetail(int id)
        {
            return db.MenuTypes.Find(id);
        }
        public bool Update(MenuType mn)
        {
            try
            {
                var menu = db.MenuTypes.Find(mn.ID);
                menu.Name = mn.Name;
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
                var ab = db.MenuTypes.Find(id);
                db.MenuTypes.Remove(ab);
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
