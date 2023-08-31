using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using System.IO;
using System.Text;
using ContractManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContractManager.Data.Interfaces;
using System;
using System.Linq;

namespace ContractManager.Controllers.api
{
    [Route("/api/[controller]")]
    public class ImportController : Controller
    {
        private IRepository<Contract> _contractRep;
        private IRepository<ContractStage> _contractStageRep;

        public ImportController(IRepository<Contract> contractRep, IRepository<ContractStage> contractStageRep)
        {
            _contractRep = contractRep;
            _contractStageRep = contractStageRep;
        }
        /// <summary>
        /// Upload excel file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            if (file == null)
                return null;

            DataSet dataSet = ReadFile(file);
            if (dataSet == null) 
            {
                ModelState.AddModelError("Ошибка", "Формат не поддерживается");
                return View();
            }

            AddData(dataSet);
            return Ok();
        }
        
        /// <summary>
        /// Read excel file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private DataSet ReadFile(IFormFile file)
        {            
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Stream stream = file.OpenReadStream();
            IExcelDataReader reader = null;

            if (file.FileName.EndsWith(".xls"))
                reader = ExcelReaderFactory.CreateBinaryReader(stream);
            else if (file.FileName.EndsWith(".xlsx"))
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            else if (file.FileName.EndsWith(".csv"))
                reader = ExcelReaderFactory.CreateCsvReader(stream);
            else
                return null;

            DataSet result = new DataSet();
            try
            {
                foreach (DataTable dt_ in reader.AsDataSet().Tables)
                {
                    RemoveEmptyData(dt_);

                    DataRow row;
                    DataTable dt = new DataTable();
                    if (dt_.Columns.Count == 0)
                        continue;
                    for (int i = 0; i < dt_.Columns.Count; i++)
                    {
                        dt.Columns.Add(dt_.Rows[0][i].ToString());
                    }

                    int rowcounter = 0;
                    for (int row_ = 1; row_ < dt_.Rows.Count; row_++)
                    {
                        row = dt.NewRow();

                        for (int col = 0; col < dt_.Columns.Count; col++)
                        {
                            row[col] = dt_.Rows[row_][col].ToString();
                            rowcounter++;
                        }
                        dt.Rows.Add(row);
                    }
                    dt.TableName = dt_.TableName;
                    result.Tables.Add(dt);
                }
            }
            catch { return null; }

            reader.Close();
            reader.Dispose();

            return result;
        }
        /// <summary>
        /// Remove empty columns/rows
        /// </summary>
        /// <param name="dataTable"></param>
        private void RemoveEmptyData(DataTable dataTable) 
        {
            //Remove empty columns
            for (int it = dataTable.Columns.Count - 1; it >= 0; it--)
            {
                bool IsColumnEmpty = dataTable.AsEnumerable().All(dr => string.IsNullOrEmpty(dr[it].ToString()));

                if (IsColumnEmpty)
                    dataTable.Columns.RemoveAt(it);
            }

            //Remove empty rows
            for (int ir = dataTable.Rows.Count - 1; ir >= 0; ir--)
            {
                bool IsColumnEmpty = dataTable.Rows[ir].ItemArray.All(dr => string.IsNullOrEmpty(dr.ToString()));

                if (IsColumnEmpty)
                    dataTable.Rows.RemoveAt(ir);
            }
        }

        private bool AddData(DataSet data)
        {
            if (data == null)
                return false;

            foreach (DataTable dataTable in data.Tables) 
            {
                int type = TableType(dataTable);
                if (type < 0)
                    return false;
                
                foreach (DataRow dataRow in dataTable.Rows) 
                {
                    switch (type)
                    {
                        case 1:
                            AddContract(dataRow);
                            break;
                        case 2:
                            AddContractStage(dataRow);
                            break;
                    }
                }
            }
            return false;
        }
        
        /// <summary>
        /// Define table type
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private int TableType(DataTable dataTable) 
        {
            int type = -1;
            switch (dataTable.TableName)
            {
                case "Договор":
                case "Договора":
                case "Договоры":
                case "Contracts":
                    type = 1;
                    break;
                case "Этап":
                case "Этапы":
                case "ContractStages":
                    type = 2;
                    break;
            }
            if (type > 0)
                return type;
            foreach (var value in dataTable.Rows[0].ItemArray) 
            {
                DateTime.TryParse(value.ToString(), out DateTime date);
                if (date > DateTime.MinValue)
                    return 2;
            }

            return 1;
        }
        
        /// <summary>
        /// Save Contract to DB
        /// </summary>
        /// <param name="dataRow"></param>
        private void AddContract(DataRow dataRow) 
        {
            if (dataRow.ItemArray.Length < 3)
                return;
            Contract newContract = new Contract();

            if (dataRow.ItemArray[0] != null)
                newContract.Code = dataRow.ItemArray[0].ToString();
            if (dataRow.ItemArray[1] != null)
                newContract.Caption = dataRow.ItemArray[1].ToString();
            if (dataRow.ItemArray[2] != null)
                newContract.Client = dataRow.ItemArray[2].ToString();

            _contractRep.Add(newContract);
        }
        
        /// <summary>
        /// Save ContractStage to DB
        /// </summary>
        /// <param name="dataRow"></param>
        private void AddContractStage(DataRow dataRow)
        {
            if (dataRow.ItemArray.Length < 3)
                return;
            ContractStage newContractStage= new ContractStage();

            if (dataRow.ItemArray[0] != null)
                newContractStage.Caption = dataRow.ItemArray[0].ToString();
            if(DateTime.TryParse(dataRow.ItemArray[1].ToString(), out DateTime DateBegin))
                newContractStage.DateBegin = DateBegin;
            if(DateTime.TryParse(dataRow.ItemArray[2].ToString(), out DateTime DateEnd))
                newContractStage.DateEnd = DateEnd;

            if (dataRow.ItemArray[3] != null) 
                newContractStage.Contract = _contractRep.Items.FirstOrDefault(C => C.Code == dataRow.ItemArray[3].ToString());

            _contractStageRep.Add(newContractStage);
        }
    }
}
