using Model.EntityF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class SystemConfigDAO
    {
        OnlineShopDbContext db = null;
        public SystemConfigDAO()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(SystemConfig ab)
        {
            
            db.SystemConfigs.Add(ab);
            db.SaveChanges();
            return 1;
        }
        public IEnumerable<SystemConfig> ListContent(string search, int page, int pageSize)
        {
            IQueryable<SystemConfig> model = db.SystemConfigs;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Name.Contains(search) || x.Style.Contains(search) || x.Value.Contains(search));
            }
            return model.OrderByDescending(x => x.Name).ToPagedList(page, pageSize);
        }
        public SystemConfig ViewDetail(string id)
        {
            return db.SystemConfigs.Find(id);
        }
        public bool Update(SystemConfig sc)
        {
            try
            {
                var systemConfig = db.SystemConfigs.Find(sc.ID);
                systemConfig.Name = sc.Name;
                systemConfig.Style = sc.Style;
                systemConfig.Value = sc.Value;
                systemConfig.Status = sc.Status;
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
                var ab = db.SystemConfigs.Find(id);
                db.SystemConfigs.Remove(ab);
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
