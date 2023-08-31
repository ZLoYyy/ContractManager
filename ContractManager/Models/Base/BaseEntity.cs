using ContractManager.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractManager.Models.Base
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
