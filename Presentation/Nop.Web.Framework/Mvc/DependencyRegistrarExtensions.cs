using System;
using Autofac;
using Autofac.Core;
using Nop.Core.Data;
using Nop.Core.Domain.CareMake;
using Nop.Core.Domain.Photo360;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Services.Photos360;

namespace Nop.Web.Framework.Mvc
{
    /// <summary>
    /// Extensions for DependencyRegistrar
    /// </summary>
    public static class DependencyRegistrarExtensions
    {
        /// <summary>
        /// Register custom DataContext for a plugin
        /// </summary>
        /// <typeparam name="T">Class implementing IDbContext</typeparam>
        /// <param name="dependencyRegistrar">Dependency registrar</param>
        /// <param name="builder">Builder</param>
        /// <param name="contextName">Context name</param>
        public static void RegisterPluginDataContext<T>(this IDependencyRegistrar dependencyRegistrar,
            ContainerBuilder builder, string contextName)
             where T: IDbContext
        {
            // Override required repository with your custom context
            builder
                .RegisterType<EfRepository<Image360>>()
                .As<IRepository<Image360>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("CONTEXT_NAME"))
                .InstancePerLifetimeScope();
            // Override required repository with your custom context
            builder
                .RegisterType<EfRepository<CarMakeBulkImages>>()
                .As<IRepository<CarMakeBulkImages>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("CONTEXT_NAME"))
                .InstancePerLifetimeScope();
            //data layer
            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();

            if (dataProviderSettings != null && dataProviderSettings.IsValid())
            {
                //register named context
                builder.Register(c => (IDbContext)Activator.CreateInstance(typeof(T), new object[] { dataProviderSettings.DataConnectionString }))
                    .Named<IDbContext>(contextName)
                    .InstancePerLifetimeScope();

                builder.Register(c => (T)Activator.CreateInstance(typeof(T), new object[] { dataProviderSettings.DataConnectionString }))
                    .InstancePerLifetimeScope();
            }
            else
            {
                //register named context
                builder.Register(c => (T)Activator.CreateInstance(typeof(T), new object[] { c.Resolve<DataSettings>().DataConnectionString }))
                    .Named<IDbContext>(contextName)
                    .InstancePerLifetimeScope();

                builder.Register(c => (T)Activator.CreateInstance(typeof(T), new object[] { c.Resolve<DataSettings>().DataConnectionString }))
                    .InstancePerLifetimeScope();
            }
        }
    }
}
