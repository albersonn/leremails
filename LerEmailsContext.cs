using LerEmail.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LerEmail
{
    class LerEmailsContext : DbContext
    {
        public DbSet<Email> Emails { get; set; }
    }
}
