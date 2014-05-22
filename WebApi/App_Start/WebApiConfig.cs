using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using TripFiles.Mobile.HttpHandlers;
using Microsoft.Owin.Security.OAuth;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new ApiKeyMessageHandler());
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            //Force HTTPS on entire API
            config.Filters.Add(new RequireHttpsAttribute());
            // Web API routes
            config.MapHttpAttributeRoutes();
            
            //Add CORS support
            var corsAttr = new EnableCorsAttribute("*", "X-API", "*");
            config.EnableCors(corsAttr);
        }
    }
}
