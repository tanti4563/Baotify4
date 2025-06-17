using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.api.Data
{
    using System.Data.Entity;

    public class AppDbContext : ShipManageEntities
    {
        public AppDbContext() : base()
        {
        }
    }
}
