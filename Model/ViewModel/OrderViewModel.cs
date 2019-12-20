using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    /// <summary>
    /// Chart
    /// </summary>
    public class OrderViewModel
    {
        public DateTime? CreatedDate { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
