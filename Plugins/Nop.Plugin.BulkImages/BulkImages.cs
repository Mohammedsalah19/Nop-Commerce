using Nop.Core.Plugins;
using Nop.Plugin.BulkImages.Data;
using Nop.Services.Events;
using Nop.Web.Framework.Events;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
namespace Nop.Plugin.BulkImages
{
    public class BulkImages : BasePlugin, IAdminMenuPlugin/*, IConsumer<AdminTabStripCreated>*/
    {

        private ImageBulkObjectContext _context;

        public BulkImages(ImageBulkObjectContext context)
        {
            _context = context;
        }

        public override void Install()
        {
            _context.Install();
            base.Install();
        }
        public override void Uninstall()
        {
            _context.Uninstall();

            base.Uninstall();
        }
        /// <summary>
        /// Gets a route for provider configuration
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "ImageBulkManagementController";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.UploadBulkImages.Controllers" }, { "area", null } };
        }

     

        public void ManageSiteMap(Web.Framework.Menu.SiteMapNode rootNode)
        {
            var menuItem = new Web.Framework.Menu.SiteMapNode()
            {
                SystemName = "BulkImages",
                Title = "Upload Bulk Images Title",
                ControllerName = "ImageBulkManagementController",
                ActionName = "Create",
                Visible = true,
                RouteValues = new RouteValueDictionary() { { "area", null } },
            };
            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
            if (pluginNode != null)
                pluginNode.ChildNodes.Add(menuItem);
            else
                rootNode.ChildNodes.Add(menuItem);
        }


        //public void HandleEvent(AdminTabStripCreated tabEventInfo)
        //{
        //    if (tabEventInfo != null && !string.IsNullOrEmpty(tabEventInfo.TabStripName))
        //    {
        //        if (tabEventInfo.TabStripName == "product-edit")
        //        {
        //            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

        //            object objectId = HttpContext.Current.Request.RequestContext.RouteData.Values["id"];

        //            if (!string.IsNullOrEmpty(objectId.ToString()))
        //            {
        //                string text = "Picture 360";
        //                string content = urlHelper.Action("ImageBulkManagement", "Configure", new { id = objectId });

        //                tabEventInfo.BlocksToRender.Add(
        //                  new MvcHtmlString(
        //                    "<script>" +
        //                      "$(document).ready(function() {" +
        //                        "$('#product-edit').data('kendoTabStrip').append(" +
        //                          "[{" +
        //                            "text: '" + text + "'," +
        //                            "contentUrl: '" + content + "'" +
        //                          "}]" +
        //                        ");" +
        //                      "});" +
        //                    "</script>"
        //                  )
        //                );
        //            }
        //        }
        //    }
        //}
    }
}

