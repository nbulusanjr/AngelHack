using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDo.DAL
{
    public class RequestNotificationsModel
    {
        public int ID { get; set; }
        public int RequestID { get; set; }
        public RequestModel Request {get;set;}
      
        public Boolean IsAccepted { get; set; }
        public Boolean IsDeclined { get; set; }
        public System.DateTime DateReceived { get; set; }
        public System.DateTime DateLastUpdated { get; set; }
        public int RequestorID { get; set; }
        public int UserID { get; set; }

        public UsersModel Recipient { get; set; }


    }
}
