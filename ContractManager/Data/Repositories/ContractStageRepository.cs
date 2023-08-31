using ContractManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ContractManager.Data.Repositories
{
    public class ContractStageRepository : DbRepository<ContractStage>
    {
        public ContractStageRepository(ContractManagerDBContext db) : base(db) { }

        public override IQueryable<ContractStage> Items => base.Items;
    }
}
