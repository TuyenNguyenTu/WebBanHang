using Model.EntityF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class Chart
    {
        OnlineShopDbContext db = null;
        public Chart()
        {
            db = new OnlineShopDbContext();
        }
        public List<OrderViewModel> ChartAll()
        {
            var model = from a in db.Orders
                        join b in db.OrderDetails
                        on a.ID equals b.OrderID
                        select new OrderViewModel()
                        {
                            CreatedDate = a.CreatedDate,
                            TotalPrice = (b.Price*b.Quantity)
                        
                        };
            model.OrderByDescending(x => x.CreatedDate).ToList();
            return model.ToList();
        }
    }
}
