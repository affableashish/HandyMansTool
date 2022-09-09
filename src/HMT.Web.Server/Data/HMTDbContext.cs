using Microsoft.EntityFrameworkCore;
using HMT.Web.Server.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMT.Web.Server.Data
{
    public class HMTDbContext : DbContext
    {
        public HMTDbContext(DbContextOptions<HMTDbContext> options) : base(options) { }

        public DbSet<RepairOrder> RepairOrders { get; set; }
    }
}
