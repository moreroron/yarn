
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace yarn_rider.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<yarn_rider.Models.SiteDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}