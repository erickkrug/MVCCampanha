using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCCampanha.Controllers;
using MVCCampanha.Models;
using OfficeOpenXml;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class LeitorArquivoController : ControllerBase
{
    [HttpPost]
    public IActionResult Importar()
     {
        List<string> ids = new List<string>();
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var file = Request.Form.Files["arquivo"];

        if (file == null)
        {
            return NotFound("Envie um Arquivo válido");
        }
        var stream = file.OpenReadStream();
        try
        {

            int countId = 0;

            using (ExcelPackage excelPackage = new(stream))
            {
                var worksheet = excelPackage.Workbook.Worksheets.First();

                int totalRows = worksheet.Dimension.End.Row;

                for (int rowNum = 1; rowNum <= totalRows; rowNum++)
                {
                    var cell = worksheet.Cells[rowNum, 1];

                    if (cell.Value != null)
                    {
                        string? cellValue = cell.Value.ToString();

                        if (!string.IsNullOrEmpty(cellValue.Trim()))
                        {
                            ids.Add(cellValue);
                        }
                    }
                }
                Querys.DeleteFunc();
                ids.ForEach(id =>
                {
                    Querys.InsertFuncionario(id);
                    countId++;
                });

            }

            return Ok(countId);

        }
        catch (Exception ex)
        {
            return StatusCode(500, "Erro ao extrair IDs do arquivo Excel");
        }
        
    }
}

