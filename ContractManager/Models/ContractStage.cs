using ContractManager.Models.Base;
using System;
using System.Text.Json.Serialization;

namespace ContractManager.Models
{
    public class ContractStage: BaseEntity
    {
        public string Caption { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        [JsonIgnore]
        public Contract Contract { get; set; }
    }
}
