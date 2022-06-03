using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using WebScraping.DTOs;

namespace WebScraping
{
    public class CSVHelper
    {
        public static string SaveIbovespa(IbovespaInfoDTO ibovespa)
        {
            string folderName = "results";
            string fileName = "ibovespaInfo.csv";
            string fileNameLista = "ibovespaInfoLista.csv";
            var filePath = $"{folderName}\\{fileName}";
            var filePathLista = $"{folderName}\\{fileNameLista}";
            if (File.Exists(fileName) is false)
            {
                Directory.CreateDirectory(folderName);
                File.Create(filePath).Dispose();
            }

            ExecutaProcesso(filePath, ibovespa, false);

            if (File.Exists(fileNameLista) is false)
                File.Create(fileNameLista).Dispose();

            ExecutaProcesso(filePathLista, ibovespa, true);

            return Path.GetFullPath(filePath);
        }

        private static void ExecutaProcesso(string filePath, IbovespaInfoDTO ibovespa, bool listaCSV)
        {
            using (var write = new StreamWriter(filePath))
            using (var csv = new CsvWriter(write, new CsvConfiguration(
                CultureInfo.InvariantCulture)
            { Delimiter = ";" }))
            {
            
                string cabecalho1, cabecalho2;
                if(listaCSV is false)
                {
                    csv.WriteField("Total Points");
                    csv.WriteField("Percentage Variation");
                    csv.NextRecord();
                    csv.WriteRecord(ibovespa);
                }
                else
                {
                    csv.WriteField("Description");
                    csv.WriteField("Value");
                    csv.NextRecord();
                    if (ibovespa.Lista is not null)
                    {
                        foreach (var item in ibovespa.Lista)
                        {
                            csv.WriteField(item.Description);
                            csv.WriteField(item.Value.Replace(" ", "").Replace("\n", "").Trim());
                            csv.NextRecord();
                        }

                    }
                }

            }

        }
    }
}
