using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LerEmail
{
    internal sealed class Configuration : DbMigrationsConfiguration<LerEmailsContext>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = false;
        }
    }
}
