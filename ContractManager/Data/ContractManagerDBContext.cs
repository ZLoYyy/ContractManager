using ContractManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractManager.Data
{
    public class ContractManagerDBContext: DbContext
    {
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractStage> StageCantracts { get; set; }
        public ContractManagerDBContext(DbContextOptions<ContractManagerDBContext> options) : base(options)
        {

        }
    }
}
