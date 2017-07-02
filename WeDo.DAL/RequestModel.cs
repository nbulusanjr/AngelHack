using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDo.DAL
{

    public enum RequestStatus
    {
        AWAITING = 1,
        CONFIRMED = 2,
        SERVED = 3,
        CANCELLED = 4,
        CLOSED = 5
    }

    public enum UserType
    {
        USER = 1,
        PROVIDER = 2
    }


    public class RequestModel
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string CurrentLocation { get; set; }
        public RequestStatus StatusID { get; set; }
        public System.DateTime DateRequested { get; set; }
        public System.DateTime DateLastUpdated { get; set; }
        public System.DateTime DeliveryDate { get; set; }
        public int UserID { get; set; }
        public UsersModel User { get; set; }

        private IMapper mapper;
        public RequestModel()
        {
            var newConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<user, UsersModel>();

                cfg.CreateMap<request, RequestModel>()
                            .ForMember(dest => dest.User, conf => conf.MapFrom(o => o.user));

                cfg.CreateMap<requestnotification, RequestNotificationsModel>()
                 .ForMember(dest => dest.Recipient, conf => conf.MapFrom(o => o.user1))
                 .ForMember(dest => dest.Request, conf => conf.MapFrom(o => o.request));

                cfg.CreateMap<requestbid, RequestBidsModel>()
              .ForMember(dest => dest.Bidder, conf => conf.MapFrom(o => o.user))
              .ForMember(dest => dest.Header, conf => conf.MapFrom(o => o.request));

            });

            mapper = newConfig.CreateMapper();
        }


        #region USER
        public RequestModel Create(RequestModel request, UsersModel requestor)
        {
            var db = new angelhackEntities();
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {

                    var chkUser = db.users.Where(x => x.ID == requestor.ID).FirstOrDefault();


                    if (chkUser == null) throw new Exception("User does not exist!");

                    var newRequest = new request();
                    newRequest.CurrentLocation = request.CurrentLocation;
                    newRequest.DateLastUpdated = DateTime.Now;
                    newRequest.DateRequested = DateTime.Now;
                    newRequest.DeliveryDate = request.DeliveryDate;
                    newRequest.Description = request.Description;
                    newRequest.StatusID = (int)request.StatusID;
                    newRequest.UserID = requestor.ID;

                    db.requests.Add(newRequest);
                    db.SaveChanges();

                    transaction.Commit();

                    request.ID = newRequest.ID;
                    request.UserID = requestor.ID;
                    request.User = mapper.Map<user,UsersModel>(chkUser);
                    return request;

                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException dbU)
                {
                    transaction.Rollback();
                    Exception ex = dbU.GetBaseException();
                    throw new Exception(ex.Message);
                }
                catch (DbEntityValidationException dbEx)
                {


                    transaction.Rollback();
                    string errorMessages = null;

                    foreach (DbEntityValidationResult validationResult in dbEx.EntityValidationErrors)
                    {
                        string entityName = validationResult.Entry.Entity.GetType().Name;
                        foreach (DbValidationError error in validationResult.ValidationErrors)
                        {
                            errorMessages += (entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
                        }
                    }

                    throw new Exception(errorMessages);

                }

                finally
                {
                    db.Database.Connection.Close();

                }


            }

        }
        public void CancelRequest(RequestModel request, UsersModel requestor)
        {
            var db = new angelhackEntities();
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {

                    var chkUser = db.users.Where(x => x.ID == requestor.ID).FirstOrDefault();


                    if (chkUser == null) throw new Exception("User does not exist!");

                    var newRequest = db.requests.Where(x => x.ID == request.ID
                    &&
                    (
                    x.StatusID != (int)RequestStatus.CANCELLED &&
                      x.StatusID != (int)RequestStatus.SERVED &&
                        x.StatusID != (int)RequestStatus.CLOSED
                    )

                    && x.UserID == requestor.ID).FirstOrDefault();

                    if (newRequest == null) throw new Exception("Request does not exist!");

                    newRequest.StatusID = (int)RequestStatus.CANCELLED;

                    db.SaveChanges();

                    transaction.Commit();

                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException dbU)
                {
                    transaction.Rollback();
                    Exception ex = dbU.GetBaseException();
                    throw new Exception(ex.Message);
                }
                catch (DbEntityValidationException dbEx)
                {


                    transaction.Rollback();
                    string errorMessages = null;

                    foreach (DbEntityValidationResult validationResult in dbEx.EntityValidationErrors)
                    {
                        string entityName = validationResult.Entry.Entity.GetType().Name;
                        foreach (DbValidationError error in validationResult.ValidationErrors)
                        {
                            errorMessages += (entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
                        }
                    }

                    throw new Exception(errorMessages);

                }

                finally
                {
                    db.Database.Connection.Close();

                }


            }

        }
        
        public List<RequestModel> GetAllMyRequest(UsersModel user)
        {
            UsersModel userRepo = new UsersModel();
            List<RequestModel> myRequests = new List<RequestModel>();
            using(var db = new angelhackEntities())
            {
                userRepo.CheckUserIfExist(db,user);

                var result = db.requests.Where(x => x.UserID == user.ID).ToList();

                myRequests = mapper.Map<List<request>,List<RequestModel>>(result);
            }

            return myRequests;
        }

        public void AcceptBid(RequestBidsModel bid, UsersModel user)
        {
            var db = new angelhackEntities();
            UsersModel userRepo = new UsersModel();
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    userRepo.CheckUserIfExist(db,user);

                    var chkBid = db.requestbids.Where(x => x.ID == bid.ID && x.request.UserID == user.ID).FirstOrDefault();

                    if (chkBid == null) throw new Exception("Bid does not exist!");

                    chkBid.request.StatusID = (int)RequestStatus.CONFIRMED;
                    chkBid.IsAwarded = true;


                    db.SaveChanges();
                    transaction.Commit();
                   

                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException dbU)
                {
                    transaction.Rollback();
                    Exception ex = dbU.GetBaseException();
                    throw new Exception(ex.Message);
                }
                catch (DbEntityValidationException dbEx)
                {


                    transaction.Rollback();
                    string errorMessages = null;

                    foreach (DbEntityValidationResult validationResult in dbEx.EntityValidationErrors)
                    {
                        string entityName = validationResult.Entry.Entity.GetType().Name;
                        foreach (DbValidationError error in validationResult.ValidationErrors)
                        {
                            errorMessages += (entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
                        }
                    }

                    throw new Exception(errorMessages);

                }

                finally
                {
                    db.Database.Connection.Close();

                }


            }


        }
        
        public List<RequestBidsModel> GetBidsForMyRequest(int requestID, UsersModel user)
        {
            UsersModel userRepo = new UsersModel();
            List<RequestBidsModel> notifications = new List<RequestBidsModel>();

            using (var db = new angelhackEntities())
            {

                userRepo.CheckUserIfExist(db, user);

                var result = db.requestbids.Where(x => x.IsAwarded == false
                && x.request.UserID == user.ID
                && x.RequestID == requestID
                &&
               (
                    x.request.StatusID != (int)RequestStatus.CANCELLED &&
                      x.request.StatusID != (int)RequestStatus.SERVED &&
                        x.request.StatusID != (int)RequestStatus.CLOSED
                    )

               ).ToList();

                notifications = mapper.Map<List<requestbid>, List<RequestBidsModel>>(result);


            }


            return notifications;
        }

        

        #endregion

        #region PROVIDERS
        public void SendToNotifications(RequestModel notification, UsersModel user)
        {
            var db = new angelhackEntities();
            UsersModel userRepo = new UsersModel();
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    userRepo.CheckUserIfExist(db, user);
                    userRepo.IsUserServiceProvider(db, user);


                    var newNotification = new requestnotification();
                    newNotification.DateLastUpdated = DateTime.Now;
                    newNotification.DateReceived = DateTime.Now;
                    newNotification.IsAccepted = false;
                    newNotification.RequestID = notification.ID;
                    newNotification.RequestorID = notification.UserID;
                    newNotification.UserID = user.ID;
                    newNotification.IsAccepted = false;
                    newNotification.IsDeclined = false;

                    db.requestnotifications.Add(newNotification);


                    db.SaveChanges();
                    transaction.Commit();

                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException dbU)
                {
                    transaction.Rollback();
                    Exception ex = dbU.GetBaseException();
                    throw new Exception(ex.Message);
                }
                catch (DbEntityValidationException dbEx)
                {


                    transaction.Rollback();
                    string errorMessages = null;

                    foreach (DbEntityValidationResult validationResult in dbEx.EntityValidationErrors)
                    {
                        string entityName = validationResult.Entry.Entity.GetType().Name;
                        foreach (DbValidationError error in validationResult.ValidationErrors)
                        {
                            errorMessages += (entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
                        }
                    }

                    throw new Exception(errorMessages);

                }

                finally
                {
                    db.Database.Connection.Close();

                }


            }

        }

        public void AcceptRequest(RequestModel notification, UsersModel user)
        {
            var db = new angelhackEntities();
            UsersModel userRepo = new UsersModel();
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    userRepo.CheckUserIfExist(db, user);
                    userRepo.IsUserServiceProvider(db, user);


                    var newNotification = db.requestnotifications.Where(x => x.UserID == user.ID
                    && x.IsDeclined == false
                    && x.IsAccepted ==false
                    && (
                    x.request.StatusID != (int)RequestStatus.CANCELLED &&
                      x.request.StatusID != (int)RequestStatus.SERVED &&
                        x.request.StatusID != (int)RequestStatus.CLOSED
                    )

                    && x.RequestID == notification.ID).FirstOrDefault();

                    if (newNotification == null) throw new Exception("Request no longer exist!");

                    newNotification.DateLastUpdated = DateTime.Now;
                    newNotification.IsAccepted = true;
                    db.SaveChanges();
                    transaction.Commit();

                  

                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException dbU)
                {
                    transaction.Rollback();
                    Exception ex = dbU.GetBaseException();
                    throw new Exception(ex.Message);
                }
                catch (DbEntityValidationException dbEx)
                {


                    transaction.Rollback();
                    string errorMessages = null;

                    foreach (DbEntityValidationResult validationResult in dbEx.EntityValidationErrors)
                    {
                        string entityName = validationResult.Entry.Entity.GetType().Name;
                        foreach (DbValidationError error in validationResult.ValidationErrors)
                        {
                            errorMessages += (entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
                        }
                    }

                    throw new Exception(errorMessages);

                }

                finally
                {
                    db.Database.Connection.Close();

                }


            }

        }
        

        public List<RequestModel> PendingForMyBid(UsersModel user)
        {
            UsersModel userRepo = new UsersModel();
            List<RequestModel> myRequests = new List<RequestModel>();
            using (var db = new angelhackEntities())
            {
                userRepo.CheckUserIfExist(db, user);
                userRepo.IsUserServiceProvider(db, user);
                //var result = db.requestnotifications.Where(x=>x.IsAccepted == true &&
                //(
                // x.request.StatusID != (int)RequestStatus.CANCELLED &&
                //      x.request.StatusID != (int)RequestStatus.SERVED &&
                //        x.request.StatusID != (int)RequestStatus.CLOSED
                //    )

                //).Select(x=>x.request).ToList();

                var result = (from x in db.requestnotifications
                              join a in db.requestbids on new { RequestID = x.RequestID, UserID = x.UserID } equals new { RequestID = a.RequestID, UserID = a.UserID } into aa
                              from aax in aa.DefaultIfEmpty()
                              where aax == null &&
                              x.IsAccepted == true &&
                (
                 x.request.StatusID != (int)RequestStatus.CANCELLED &&
                      x.request.StatusID != (int)RequestStatus.SERVED &&
                        x.request.StatusID != (int)RequestStatus.CLOSED
                    )
                              select x.request).ToList();


                     myRequests = mapper.Map<List<request>, List<RequestModel>>(result);
            }

            return myRequests;
        }

        public void DeclineRequest(RequestModel notification, UsersModel user)
        {
            var db = new angelhackEntities();
            UsersModel userRepo = new UsersModel();
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    userRepo.CheckUserIfExist(db, user);
                    userRepo.IsUserServiceProvider(db, user);


                    var newNotification = db.requestnotifications.Where(x => x.UserID == user.ID
                   
                    && x.IsDeclined == false
                    &&
                    (
                    x.request.StatusID != (int)RequestStatus.CANCELLED &&
                      x.request.StatusID != (int)RequestStatus.SERVED &&
                        x.request.StatusID != (int)RequestStatus.CLOSED
                    )
                    &&
                    x.RequestID == notification.ID).FirstOrDefault();

                    if (newNotification == null) throw new Exception("Request no longer exist!");

                    newNotification.DateLastUpdated = DateTime.Now;
                    newNotification.IsDeclined = true;
                    db.SaveChanges();
                    transaction.Commit();

                 

                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException dbU)
                {
                    transaction.Rollback();
                    Exception ex = dbU.GetBaseException();
                    throw new Exception(ex.Message);
                }
                catch (DbEntityValidationException dbEx)
                {


                    transaction.Rollback();
                    string errorMessages = null;

                    foreach (DbEntityValidationResult validationResult in dbEx.EntityValidationErrors)
                    {
                        string entityName = validationResult.Entry.Entity.GetType().Name;
                        foreach (DbValidationError error in validationResult.ValidationErrors)
                        {
                            errorMessages += (entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
                        }
                    }

                    throw new Exception(errorMessages);

                }

                finally
                {
                    db.Database.Connection.Close();

                }


            }

        }

        public RequestBidsModel PlaceBid(RequestBidsModel bid, UsersModel user)
        {
            var db = new angelhackEntities();
            UsersModel userRepo = new UsersModel();
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {

                    userRepo.CheckUserIfExist(db, user);
                    userRepo.IsUserServiceProvider(db, user);

                    var newBid = new requestbid();
                    newBid.IsAwarded = false;
                    newBid.Price = bid.Price;
                    newBid.RequestID = bid.RequestID;
                    newBid.ShopAddress = bid.ShopAddress;
                    newBid.ShopName = bid.ShopName;
                    newBid.UserID = user.ID;
                    newBid.BidDate = DateTime.Now;
                    db.requestbids.Add(newBid);

                    db.SaveChanges();
                    transaction.Commit();


                    var getBid = db.requestbids.Where(x => x.ID == newBid.ID).FirstOrDefault();

                    return mapper.Map<requestbid,RequestBidsModel>(getBid);

                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException dbU)
                {
                    transaction.Rollback();
                    Exception ex = dbU.GetBaseException();
                    throw new Exception(ex.Message);
                }
                catch (DbEntityValidationException dbEx)
                {
                    transaction.Rollback();
                    string errorMessages = null;
                    foreach (DbEntityValidationResult validationResult in dbEx.EntityValidationErrors)
                    {
                        string entityName = validationResult.Entry.Entity.GetType().Name;
                        foreach (DbValidationError error in validationResult.ValidationErrors)
                        {
                            errorMessages += (entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
                        }
                    }
                    throw new Exception(errorMessages);
                }
                finally
                {
                    db.Database.Connection.Close();

                }


            }


        }

        public List<RequestNotificationsModel> GetNotifications(UsersModel user)
        {

            UsersModel userRepo = new UsersModel();
            List<RequestNotificationsModel> notifications = new List<RequestNotificationsModel>();

            using (var db = new angelhackEntities())
            {

                userRepo.CheckUserIfExist(db, user);
                userRepo.IsUserServiceProvider(db, user);


                var result = db.requestnotifications.Where(x => x.IsAccepted == false
                && x.UserID == user.ID
                &&
               (
                    x.request.StatusID != (int)RequestStatus.CANCELLED &&
                      x.request.StatusID != (int)RequestStatus.SERVED &&
                        x.request.StatusID != (int)RequestStatus.CLOSED
                    )

                && x.IsDeclined == false).ToList();

                notifications = mapper.Map<List<requestnotification>, List<RequestNotificationsModel>>(result);


            }


            return notifications;

        }

        public List<RequestBidsModel> GetBidsByMe(UsersModel user)
        {
            UsersModel userRepo = new UsersModel();
            List<RequestBidsModel> notifications = new List<RequestBidsModel>();

            using (var db = new angelhackEntities())
            {

                userRepo.CheckUserIfExist(db, user);
                userRepo.IsUserServiceProvider(db, user);


                var result = db.requestbids.Where(x => x.IsAwarded == false
                && x.UserID == user.ID
                &&
               (
                    x.request.StatusID != (int)RequestStatus.CANCELLED &&
                      x.request.StatusID != (int)RequestStatus.SERVED &&
                        x.request.StatusID != (int)RequestStatus.CLOSED
                    )

               ).ToList();

                notifications = mapper.Map<List<requestbid>, List<RequestBidsModel>>(result);


            }


            return notifications;
        }



        #endregion


        

        public RequestBidsModel ViewBid(int requestID, UsersModel user)
        {

            UsersModel userRepo = new UsersModel();
            RequestBidsModel bid = new RequestBidsModel();

            using (var db = new angelhackEntities())
            {

                userRepo.CheckUserIfExist(db, user);

                var result = db.requestbids.Where(x => x.IsAwarded == false
                &&( x.request.UserID == user.ID ||  x.UserID ==user.ID)
                && x.RequestID == requestID
                &&
               (
                    x.request.StatusID != (int)RequestStatus.CANCELLED &&
                      x.request.StatusID != (int)RequestStatus.SERVED &&
                        x.request.StatusID != (int)RequestStatus.CLOSED
                    )

               ).FirstOrDefault();

                bid = mapper.Map<requestbid, RequestBidsModel>(result);


            }


            return bid;

        }


    }




}
