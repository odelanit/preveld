using System.Data.Entity;
using System.Threading;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;

namespace Preveld
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static SimpleMemberShipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer<ApplicationDBContext>(new DropCreateDatabaseIfModelChanges<ApplicationDBContext>());

            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        public class SimpleMemberShipInitializer
        {
            public SimpleMemberShipInitializer()
            {
        //        // using (var db = new ApplicationDBContext())
        //        //     db.UserProfiles.Find(1);

                if (!WebSecurity.Initialized)
                {
                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "user_profile_db", "ID", "User_ID", autoCreateTables: false, SimpleMembershipProviderCasingBehavior.RelyOnDatabaseCollation);
                }
            }
        }
    }
}
