using Nop.Core.Plugins;
using Nop.Plugin.BulkImages.Data;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Nop.Plugin.CarMake
{
    public class CarMake : BasePlugin, IAdminMenuPlugin /*, IConsumer<AdminTabStripCreated>*/
    {

        private CarMakeObjectContext _context;

        public CarMake(CarMakeObjectContext context)
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
            actionName = "index";
            controllerName = "Configuration";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.CarMake.Controllers" }, { "area", null } };
        }


        public void ManageSiteMap(Web.Framework.Menu.SiteMapNode rootNode)
        {
            var CarMakeBulk = new Web.Framework.Menu.SiteMapNode()
            {
                Title = "Car Make Bulk",
                Visible = true,
                Url = "/CarMake/CreateBulk/"
            };
            var CarMakeBulkList= new Web.Framework.Menu.SiteMapNode()
            {
                Title = "Bulk List",
                Visible = true,
                Url = "/CarMake/BulkList/"
            };

            var ExtraCarMakeImages = new Web.Framework.Menu.SiteMapNode()
            {
                Title = "Extra Car Make Images",
                Visible = true,
                Url = "/CarMake/CreateCarMakeImages/"
            };

            var ExtraCarMakeImagesList = new Web.Framework.Menu.SiteMapNode()
            {
                Title = "Extra Car Make List",
                Visible = true,
                Url = "/CarMake/ImagesList/"
            };

            var ColorHex = new Web.Framework.Menu.SiteMapNode()
            {
                Title = "ColorHex",
                Visible = true,
                Url = "/CarMake/ColorHex/"
            };

            var ColorHexList = new Web.Framework.Menu.SiteMapNode()
            {
                Title = "ColoHex List",
                Visible = true,
                Url = "/CarMake/ColorHexList/"
            };



            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
            if (pluginNode != null)
            {
                pluginNode.ChildNodes.Add(CarMakeBulk);
                pluginNode.ChildNodes.Add(CarMakeBulkList);
                pluginNode.ChildNodes.Add(ExtraCarMakeImages);
                pluginNode.ChildNodes.Add(ExtraCarMakeImagesList);
                pluginNode.ChildNodes.Add(ColorHex);
                pluginNode.ChildNodes.Add(ColorHexList);
            }
            else
            {
                pluginNode.ChildNodes.Add(CarMakeBulk);
                pluginNode.ChildNodes.Add(CarMakeBulkList);
                pluginNode.ChildNodes.Add(ExtraCarMakeImages);
                pluginNode.ChildNodes.Add(ExtraCarMakeImagesList);
                pluginNode.ChildNodes.Add(ColorHex);
                pluginNode.ChildNodes.Add(ColorHexList);
            }
        }

    }
}
