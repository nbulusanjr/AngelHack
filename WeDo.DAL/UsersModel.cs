using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDo.DAL
{
    public class UsersModel
    {
        public int ID { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsActivated { get; set; }
        public string ImageUrl { get; set; }
        public int UserTypeID { get; set; }



        private IMapper mapper;


        public UsersModel()
        {
            var newConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<user, UsersModel>();


            });

            mapper = newConfig.CreateMapper();
        }

        public void CreateOrUpdate(UsersModel user)
        {

            var db = new angelhackEntities();
            Boolean isNewRecord = true;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {




                    var newUser = db.users.Where(x => x.ID == user.ID).FirstOrDefault();


                    if (newUser == null)
                    {
                        newUser = new user();
                        isNewRecord = true;
                    }


                    newUser.Fullname = user.Fullname;
                    newUser.Email = user.Email;
                    newUser.ImageUrl = user.ImageUrl;
                    newUser.IsActivated = user.IsActivated;
                    newUser.IsActive = user.IsActive;
                    newUser.UserTypeID = user.UserTypeID;

                    if (isNewRecord)
                    {
                        db.users.Add(newUser);
                    }

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

        public Boolean CheckUserIfExist(angelhackEntities db,UsersModel user)
        {
            var chkUser = db.users.Where(x => x.ID == user.ID && x.IsActive == true).FirstOrDefault();

            if (chkUser == null) throw new Exception("User does not exist!");

            return true;
        }

        public Boolean IsUserServiceProvider(angelhackEntities db, UsersModel user)
        {
            var chkUser = db.users.Where(x => x.ID == user.ID && x.IsActive == true && x.UserTypeID == (int)UserType.PROVIDER).FirstOrDefault();

            if (chkUser == null) throw new Exception("Provider does not exist!");

            return true;
        }


        public void UpdatePassword(UsersAuth uAuth)
        {
            var db = new angelhackEntities();    

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    
                    var newUser = db.users.Where(x => x.ID == uAuth.ID).FirstOrDefault();
                    newUser.Password = uAuth.Password;               

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


    }
}
