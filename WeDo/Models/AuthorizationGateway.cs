using Newtonsoft.Json;
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
            //var session = HttpContext.Current.Session;
            var session = HttpContext.Current.Request.Cookies["UserData"];


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

                if (session!=null)
                {            

                    return JsonConvert.DeserializeObject<UsersModel>(session.Value);
                }

                return null;

                
            }
        }

      

    }
}