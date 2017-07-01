using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDo.DAL
{
    public class RequestBidsModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public UsersModel Bidder { get; set; }
        public int RequestID { get; set; }
        public RequestModel Header { get; set; }
        public decimal Price { get; set; }
        public Nullable<bool> IsAwarded { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
    }
}
