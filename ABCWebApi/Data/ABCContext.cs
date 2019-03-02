using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ABCWebApi.Models
{
    public class ABCContext : DbContext
    {
        public ABCContext (DbContextOptions<ABCContext> options)
            : base(options)
        {
        }

        public DbSet<ABCWebApi.Models.Cliente> Cliente { get; set; }
    }
}
