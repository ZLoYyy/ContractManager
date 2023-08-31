using ContractManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ContractManager.Data.Repositories
{
    public class ContractRepository : DbRepository<Contract>
    {
        public ContractRepository(ContractManagerDBContext db) : base(db) { }

        public override IQueryable<Contract> Items => base.Items
            .Include(item => item.StageCantracts);
    }
}
