using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraping.DTOs;

namespace WebScraping
{
    public class HorarioOnibusWebScraping
    {
        private List<HorarioOnibusDTO> lista = new List<HorarioOnibusDTO>();
        public bool GetHorarioOnibus()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://onibus.online/mg/ribeiraodasneves/");

            var listaCidades = doc.DocumentNode.SelectNodes("//div[@class='meio']/div")
                                               .Select(tr => tr.Elements("div")
                                               .Select(td => td.InnerText.Trim()).ToList()).ToList();

            var listaEmpresa = doc.DocumentNode.SelectNodes("//div[@class='card-header cia']/h2")
                                      .Skip(0)
                                      .Select(tr => tr.Elements("a")
                                      .Select(td => td.InnerText.Trim() + ";" + td.GetAttributeValue("href", "")).ToList()).ToList();


            //List<Listagem> lista = new List<Listagem>();
            var listaHorario = doc.DocumentNode.SelectNodes("//div[@class='card-body']/table");

            for (int i = 0; i < listaHorario.Count - 1; i++)
            {
                HtmlNode node = listaHorario[i];
                var line = node.InnerHtml.Replace("<tr><td><h3 class=\"linhas\"><a class=\"linklinha\" href=\"", "")
                         .Replace("</h3></td></tr>", "")
                         .Replace("</a>", ";").Replace("\n", "");

                var listStrLineElements = line.Split(';').ToList();

                var nomeEmpresa = listaEmpresa[i][0].Split(";");
                foreach (var item in listStrLineElements)
                {

                    if (item.Trim() != "")
                    {
                        var line1 = item.Replace("\">", ";");
                        var listStrLineElements1 = line1.Split(';').ToList();
                        AdicionarLista(i + 1, nomeEmpresa, listStrLineElements1);
                    }
                }

            }

            return true;

        }

        private void AdicionarLista(int id, string[] nomeEmpresa, List<string> listStrLineElements1)
        {
            HorarioOnibusDTO dados = new HorarioOnibusDTO();
            dados.Id = id;
            dados.Empresa = nomeEmpresa[0];
            dados.EmpresaLink = nomeEmpresa[1];
            dados.Onibus = listStrLineElements1[1];
            dados.OnibusLink = listStrLineElements1[0];
            lista.Add(dados);
        }
    }
}
