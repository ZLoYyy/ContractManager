using ContractManager.Models.Base;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ContractManager.Models
{
    public class Contract: BaseEntity
    {
        public string Code { get; set; }
        
        public string Caption { get; set; }

        public string Client { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<ContractStage> StageCantracts { get; set; } = new List<ContractStage>();
    }
}
