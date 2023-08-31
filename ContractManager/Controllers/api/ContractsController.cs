using ContractManager.Data.Interfaces;
using ContractManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ContractManager.Controllers.api
{
    public class ContractsController : Controller
    {
        private IRepository<Contract> _contractRep;
        private IRepository<ContractStage> _contractStagesRep;
        public ContractsController(IRepository<Contract> contractRep, IRepository<ContractStage> contractStagesRep) 
        {
            _contractRep = contractRep;
            _contractStagesRep = contractStagesRep;
        }

        [HttpGet("/api/Contracts")]
        public IActionResult GetContracts() => Ok(_contractRep?.Items);
        [HttpGet("/api/Contracts/{id}")]
        public IActionResult GetContracts(int id) => Ok(_contractRep?.Get(id));

        [HttpGet("/api/ContractStages")]
        public IActionResult GetContractStages() => Ok(_contractStagesRep?.Items);
        [HttpGet("/api/ContractStages/{contractCode}")]
        public IActionResult GetContractStages(string contractCode) => Ok(_contractStagesRep?.Items.Where(X=> X.Contract != null && X.Contract.Code == contractCode));
    }
}
