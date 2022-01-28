namespace StackOverflowClone.DomainModels.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StackOverflowClone.DomainModels.ContextClass>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "StackOverflowClone.DomainModels.ContextClass";
        }

        protected override void Seed(StackOverflowClone.DomainModels.ContextClass context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
