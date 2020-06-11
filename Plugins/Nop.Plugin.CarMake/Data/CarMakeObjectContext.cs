using Nop.Core;
using Nop.Data;
using Nop.Plugin.CarMake.Data;
using Nop.Plugin.CarMake.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.BulkImages.Data
{
 public   class CarMakeObjectContext: DbContext,IDbContext
    {
        public bool ProxyCreationEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AutoDetectChangesEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public CarMakeObjectContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CarMakeBulkImagesMap());
            modelBuilder.Configurations.Add(new CarMakeImagesMap());
            modelBuilder.Configurations.Add(new ColorHexMap());

            base.OnModelCreating(modelBuilder);
        }

        // this create table
        public string CreateDatabaseInstallationScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();

        }


        public void Install()
        {
            Database.SetInitializer<CarMakeObjectContext>(null);
            Database.ExecuteSqlCommand(CreateDatabaseInstallationScript());

        }

        public void Uninstall()
        {
            this.DropPluginTable("CarMakeBulkImages");
            this.DropPluginTable("CarMakeImages");
            this.DropPluginTable("ColorHex");

        }

        IDbSet<TEntity> IDbContext.Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Detach(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
