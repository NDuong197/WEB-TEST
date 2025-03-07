using Microsoft.EntityFrameworkCore;
using GoodsManagementAPI.Models;

namespace GoodsManagementAPI.Data
{
    public class GoodsDbContext : DbContext
    {
        public GoodsDbContext(DbContextOptions<GoodsDbContext> options) : base(options) { }

        public DbSet<Goods> Goods { get; set; }
    }
}
