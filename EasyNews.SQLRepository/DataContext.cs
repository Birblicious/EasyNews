using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNews.Core.Models;
using System.Data.Entity;
using EasyNews.SQLRepository.Migrations;

namespace EasyNews.SQLRepository
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("Defaultconnection")
        {

        }

        DbSet<GuardianFields> GuardianFields { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Configuration>());
            base.OnModelCreating(modelBuilder);
        }
    }
}
