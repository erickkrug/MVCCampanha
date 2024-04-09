using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public class ReadXlsxController : ControllerBase
{
    [HttpPost]
    public List<string> Importar(string path)
    {
        List<string> ids = new List<string>();
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var file = Request.Form.Files["arquivo"];
        var stream = file.OpenReadStream();
        try
        {
            

            using (ExcelPackage excelPackage = new(stream))
            {
                var worksheet = excelPackage.Workbook.Worksheets.First();

                int totalRows = worksheet.Dimension.End.Row;

               
                for (int rowNum = 1; rowNum <= totalRows; rowNum++)
                {
                    var cell = worksheet.Cells[rowNum, 1]; 

                    if (cell.Value != null) 
                    {
                        string ?cellValue = cell.Value.ToString();

                        if (!string.IsNullOrEmpty(cellValue.Trim()))
                        {
                            ids.Add(cellValue); 
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao extrair IDs do arquivo Excel: {ex.Message}");
           
        }

        return ids;
    }


}

