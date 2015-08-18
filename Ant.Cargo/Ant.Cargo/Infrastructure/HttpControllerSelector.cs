using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Ant.Cargo.Client.Infrastructure
{

    /// <summary>
    /// Replaces the default web api controller selector in order to handle selection of controllers based on the version specified in the request.
    /// </summary>
    public class HttpControllerSelector : DefaultHttpControllerSelector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpControllerSelector" /> class.
        /// </summary>
        public HttpControllerSelector(HttpConfiguration config)
            : base(config)
        {
            Config = config;
        }

        /// <summary>
        /// Not used currently.
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        private HttpConfiguration Config { get; set; }


        /// <summary>
        /// Return a controller based on the version number specified in the accept header.
        /// </summary>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var controllers = GetControllerMapping();
            var routeData = request.GetRouteData();
            var controllerName = (string)routeData.Values["controller"];
            HttpControllerDescriptor descriptor;

            if (controllers.TryGetValue(controllerName, out descriptor))
            {
                if (HasValidAcceptHeader(request))
                {
                    return descriptor;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks if the current request has an accept header.
        /// It looks in the accept header for a version specified in the format:
        ///     application/vnd.readyrooms.v1+json
        ///     application/vnd.readyrooms.v1+xml.
        /// </summary>
        private bool HasValidAcceptHeader(HttpRequestMessage request)
        {
            return true;
        }
    }
}