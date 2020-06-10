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

using Nop.Core.Data;
using Autofac.Core;
using Nop.Plugin.CarMake.Models;

namespace Nop.Plugin.BulkImages.Data
{
    public class CarMakeDependencyRegister : IDependencyRegistrar
    {
        private const string CONTEXT_NAME = "nop_object_context_Car_Make";
        public int Order { get {return 1; } }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            this.RegisterPluginDataContext <CarMakeObjectContext>(builder, CONTEXT_NAME);

            builder.RegisterType<EfRepository<CarMakeBulkImages>>()
                .As<IRepository<CarMakeBulkImages>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<CarMakeImages>>()
                .As<IRepository<CarMakeImages>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerLifetimeScope();



        }
    }
}
