using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Ant.Cargo.Client
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.MessageHandlers.Add(new NullJsonHandler());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = "get", id = RouteParameter.Optional }
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            //config.EnableSystemDiagnosticsTracing();
        }

        public class NullJsonHandler : DelegatingHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var response = await base.SendAsync(request, cancellationToken);
                if (response.Content == null)
                {
                    response.Content = new StringContent(String.Empty);
                }
                else if (response.Content is ObjectContent)
                {
                    var objectContent = (ObjectContent)response.Content;
                    if (objectContent.Value == null)
                    {
                        response.Content = new StringContent(String.Empty);
                    }

                }
                return response;
            }
        }
    }
}
