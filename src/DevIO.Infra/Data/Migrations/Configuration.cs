﻿using DevIO.Infra.Data.Context;
using System.Data.Entity.Migrations;

namespace DevIO.Infra.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    }
}