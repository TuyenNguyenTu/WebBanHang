using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList.Mvc;
using PagedList;
using Model.EntityF;
namespace Model.DAO
{
    public class AccountDAO
    {
        OnlineShopDbContext db = null;
        public AccountDAO()
        {
            db = new OnlineShopDbContext();
        }
        /// <summary>
        /// Thêm
        /// </summary>
        /// <param name="ac"></param>
        /// <returns></returns>
        public long Insert(Account ac)
        {
            ac.CreatedDate = DateTime.Now;
            db.Accounts.Add(ac);
            db.SaveChanges();
            return ac.ID;
        }
        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Account> ListAcc(string searchString, int page, int pageSize)
        {
            IQueryable<Account> model = db.Accounts;
            if(!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.DisplayName.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        /// <summary>
        /// Danh sách Account theo userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Account GetByUserName(string userName)
        {
            return db.Accounts.SingleOrDefault(x => x.UserName == userName);
        }
        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public int Login(string user, string pass)
        {
            var result = db.Accounts.SingleOrDefault(x => x.UserName == user);
            if (result == null)
                return 0;
            else
            {
                if (result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.PassWord == pass && result.Status==true)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
        }
        public Account ViewDetail(int id)
        {
            return db.Accounts.Find(id);
        }
        public bool Update(Account acc)
        {
            try
            {
                var account = db.Accounts.Find(acc.ID);
                account.DisplayName = acc.DisplayName;
                account.Email = acc.Email;
                if (!string.IsNullOrEmpty(acc.PassWord))
                {
                    account.PassWord = acc.PassWord;
                }
                account.Address = acc.Address;
                account.Phone = acc.Phone;
                account.ModifiedDate = DateTime.Now;
                account.ModifiedBy = acc.ModifiedBy;
                db.SaveChanges();
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.Accounts.Find(id);
                db.Accounts.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }

        }
        public bool ChangStatus(long id)
        {
            var acc = db.Accounts.Find(id);
            acc.Status = !acc.Status;
            db.SaveChanges();
            return acc.Status;
        }
        public bool CheckUserName(string userName)
        {
            return db.Accounts.Count(x => x.UserName == userName) > 0;
        }
        public bool CheckEmail(string email)
        {
            return db.Accounts.Count(x => x.Email == email) > 0;
        }
    }
}
