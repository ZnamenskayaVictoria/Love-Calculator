
using System.Data.Entity;

namespace TdataBase.Models
{
    public class PartnersContext : DbContext
    {
        public DbSet<Partners> Partner { get; set; }
    }
}