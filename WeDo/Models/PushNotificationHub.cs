﻿using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDo.DAL;

namespace WeDo
{
   public class PushNotificationHub : Hub
    {
        public void SubscribeAsProvider(string customerId="Providers")
        {
            Groups.Add(Context.ConnectionId, customerId);
        }
        

        public void UnSubscribeAsProvider(string customerId = "Providers")
        {
            Groups.Remove(Context.ConnectionId, customerId);
        }

        public void SendRequestToProviders(RequestModel request)
        {
            var subscribed = Clients.Group("Providers");
            subscribed.sendRequestToProviders(request);
        }


        public void CancelMyRequest(int requestID)
        {
            var subscribed = Clients.Group("Providers");
            subscribed.cancelRequest(requestID);
        }

        public void BroadcastBid(RequestBidsModel bid)
        {
            Clients.All.broadcastBid(bid.RequestID,bid);
        }

        public void AcceptBid(int bidID)
        {
            Clients.All.acceptBid(bidID);
        }

    }
}
