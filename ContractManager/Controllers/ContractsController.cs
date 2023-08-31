using ContractManager.Data.Interfaces;
using ContractManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace ContractManager.Controllers
{
    [Route("/")]
    public class ContractsController : Controller
    {
        IRepository<Contract> _contractRep;

        public ContractsController(IRepository<Contract> contractRep)
        {
            _contractRep = contractRep;
        }

        public IActionResult Index()
        {   
            return View(_contractRep?.Items);
        }
        /// <summary>
        /// Импорт данных
        /// </summary>
        /// <returns></returns>
        [Route("Import")]
        public IActionResult Import()
        {
            return View();
        }
        /// <summary>
        /// Добавление
        /// </summary>
        /// <param name="newContract"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public IActionResult Add(Contract newContract)
        {
            _contractRep.AddAsync(newContract);
            return View();
        }
        /// <summary>
        /// Редактирование
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit(Contract contract)
        {
            Contract currentContract = _contractRep.Items.FirstOrDefault(C => C.Id == contract.Id);
            if (currentContract == null)
            {
                ModelState.AddModelError("Ошибка редактирования", "Договор в БД не существует");
                return View();
            }

            currentContract.Caption = contract.Caption;
            currentContract.Client = contract.Client;
            currentContract.StageCantracts = contract.StageCantracts;

            _contractRep.UpdateAsync(currentContract);
            return View();
        }
        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Remove")]
        public IActionResult Remove(int id)
        {
            Contract currentContract = _contractRep.Items.FirstOrDefault(C => C.Id == id);
            if (currentContract == null)
            {
                ModelState.AddModelError("Ошибка удаления", "Договор в БД не существует");
                return View();
            }

            _contractRep.RemoveAsync(id);
            return Ok();
        }
    }
}
