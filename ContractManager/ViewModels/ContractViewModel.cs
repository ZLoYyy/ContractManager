using ContractManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractManager.ViewModels
{
    public class ContractViewModel
    {
        public IEnumerable<Contract> Contracts { get; set; }
    }
}
