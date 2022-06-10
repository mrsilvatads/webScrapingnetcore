using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using WebScraping.DTOs;

namespace WebScraping
{
    public class IbovespaWebScraping
    {
        public IbovespaInfoDTO GetIbovespaInfo()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://www.infomoney.com.br/cotacoes/b3/indice/ibovespa/");

            var ibovespaPointsNode = doc.DocumentNode.SelectNodes("//div[@class='value']/p");
            var ibovespaPercentageNode = doc.DocumentNode.SelectNodes("//div[@class='percentage']/p");
            var tabela = doc.DocumentNode.SelectNodes("//div[@class='tables']/table");
            List<List<string>> table = doc.DocumentNode.SelectNodes("//div[@class='tables']/table")
                       .Descendants("tr")
                       .Skip(0)
                       .Where(tr => tr.Elements("td").Count() > 1)
                       .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                       .ToList();

            List<string> ids = new List<string>();

            var teste = doc.DocumentNode.SelectNodes("//div[@id='ticker-carroussel']/a");
            //foreach (XmlNode node in teste )
            //{
            //    ids.Add(node.InnerText);
            //}
            var lista = new List<IbovespaInfoDTO>();
            lista = preencheLista(table);
            var ibovespaTotalPoints = int.Parse(ibovespaPointsNode[0].InnerText.Replace(".",""));
            var ibovespaPercentageVariation = ibovespaPercentageNode[0].InnerText.Trim();

            return new IbovespaInfoDTO(ibovespaTotalPoints, ibovespaPercentageVariation, lista);

        }

        private List<IbovespaInfoDTO> preencheLista(List<List<string>> table)
        {
            var lista = new List<IbovespaInfoDTO>();
            foreach (var item in table)
                lista.Add(new IbovespaInfoDTO(item[0].Trim(), item[1].Trim()));

            return lista;
        }
    }
}
