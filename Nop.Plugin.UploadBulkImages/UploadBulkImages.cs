using Microsoft.AspNetCore.Routing;
using Nop.Core.Plugins;
using Nop.Services.Common;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nop.Plugin.UploadBulkImages
{
    public class UploadBulkImages : BasePlugin//, IAdminMenuPlugin
    {
        /// <summary>
        /// Gets a route for provider configuration
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "HomeController";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.UploadBulkImages.Controllers" }, { "area", null } };
        }

        //public void ManageSiteMap(SiteMapNode rootNode)
        //{
        //    var menuItem = new SiteMapNode()
        //    {
        //        SystemName = "UploadBulkImages",
        //        Title = "UploadBulkImages Title",
        //        ControllerName = "HomeController",
        //        ActionName = "Configure",
        //        Visible = true,

        //        //RouteValues = new RouteValueDictionary() { { "area", null } },
        //    };
        //    var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
        //    if (pluginNode != null)
        //        pluginNode.ChildNodes.Add(menuItem);
        //    else
        //        rootNode.ChildNodes.Add(menuItem);
        //}
    }
}

