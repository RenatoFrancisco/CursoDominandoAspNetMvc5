using System.Data.Entity.Migrations;

namespace DevIO.AppMvc.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<DevIO.AppMvc.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Models.ApplicationDbContext context)
        {
        }
    }
}
