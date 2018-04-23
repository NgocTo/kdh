using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;
using Stripe;

namespace kdh
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //setup a secret key here
            StripeConfiguration.SetApiKey(ConfigurationManager.AppSettings["stripeSecretKey"]);
        }
    }
}
