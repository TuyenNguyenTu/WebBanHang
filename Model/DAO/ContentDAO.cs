using Model.EntityF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ContentDAO
    {
        OnlineShopDbContext db = null;
        public ContentDAO()
        {
            db = new OnlineShopDbContext();
        }
        /// <summary>
        /// Thêm mới nội dung
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        public long Insert(Content con)
        {
            con.MetaTitle = XuLyChuoi.GetMetaTitle(con.Name);
            con.CreatedDate = DateTime.Now;
            db.Contents.Add(con);
            db.SaveChanges();
            return con.ID;
        }
        public List<Content> GetContent()
        {
            return db.Contents.Where(x => x.Status == true).ToList();
        }
        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Content> ListContent(string search, int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Name.Contains(search) || x.MetaTitle.Contains(search)||x.Tags.Contains(search));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        /// <summary>
        /// xem danh sách
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Content ViewDetail(int id)
        {
            return db.Contents.Find(id);
        }
        public bool Update(Content con)
        {
            try
            {
                var content = db.Contents.Find(con.ID);
                content.Image = con.Image;
                content.MetaDecriptions = con.MetaDecriptions;
                content.MetaKeyword = con.MetaKeyword;
                content.MetaTitle = con.MetaTitle;
                content.Name = con.Name;
                content.Status = con.Status;
                content.Tags = con.Tags;
                content.Warranty = con.Warranty;
                content.CategoryID = con.CategoryID;
                content.Decription = con.Decription;
                content.Detail = con.Detail;
                content.ModifiedDate = DateTime.Now;
                content.ModifiedBy = con.ModifiedBy;
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
                var user = db.Contents.Find(id);
                db.Contents.Remove(user);
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
