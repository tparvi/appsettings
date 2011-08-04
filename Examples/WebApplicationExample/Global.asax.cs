using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace WebApplicationExample
{
    using System.Web.Configuration;

    using ApplicationSettings;

    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)        
        {
            // ** IMPORTANT **
            // Best practices when reading Web.config is out of the scope of this example.
            var path = HttpContext.Current.Request.ApplicationPath;
            var mapPath = HttpContext.Current.Server.MapPath(path);        
            var absolutePathToWebConfig = System.IO.Path.Combine(mapPath, "Web.config");

            var settings = new AppSettings(absolutePathToWebConfig);
            var value = settings.GetValue("StringValue");
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
