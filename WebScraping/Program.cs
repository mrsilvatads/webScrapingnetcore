using System;

namespace WebScraping
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting web scraping...");

            //var ibovespaWebScraping = new IbovespaWebScraping();
            //var ibovespaInfo = ibovespaWebScraping.GetIbovespaInfo();
            var onibus = new HorarioOnibusWebScraping();

            var horarioOnibus = onibus.GetHorarioOnibus();
            //var filePath = CSVHelper.SaveIbovespa(ibovespaInfo);
            //Console.WriteLine($"Results in {filePath}");

        }
    }
}
