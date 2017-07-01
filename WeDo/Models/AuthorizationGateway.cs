using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeDo.DAL;

namespace WeDo.Models
{
    public class AuthorizationGateway
    {
        public static UsersModel  GetAuthorizedInfo()
        {
            UsersModel em = new UsersModel();

            var agent = System.Web.HttpContext.Current;
            var session = HttpContext.Current.Session;

            bool debugMode = false;

#if DEBUG
            debugMode = true;
#endif




            if (debugMode)
            {

                return em.FindUser("abner@gmail.com", "P@ssw0rd");

                //  return em.FindAgent("juandelacruz@gmail.com", "P@ssw0rd");

            }
            else
            {



                if (HttpContext.Current.Session["UserData"] != null)
                {
                    return (UsersModel)HttpContext.Current.Session["UserData"];
                }

                return null;

                
            }
        }

      

    }
}