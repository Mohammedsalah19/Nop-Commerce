using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Mvc;
using Nop.Data;
using Nop.Plugin.BulkImages.Domain;
using Nop.Core.Data;
using Autofac.Core;

namespace Nop.Plugin.BulkImages.Data
{
    public class BulkImageDependencyRegister : IDependencyRegistrar
    {
        private const string CONTEXT_NAME = "nop_object_context_image_slider";
        public int Order { get {return 1; } }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            this.RegisterPluginDataContext<ImageBulkObjectContext>(builder, CONTEXT_NAME);

            builder.RegisterType<EfRepository<Images360>>()
                .As<IRepository<Images360>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerLifetimeScope(); 
               
        
        }
    }
}
