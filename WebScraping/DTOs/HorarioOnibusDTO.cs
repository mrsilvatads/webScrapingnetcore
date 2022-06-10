using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraping.DTOs
{
    public class HorarioOnibusDTO
    {
        public HorarioOnibusDTO()
        {
        }

        public int Id { get; set; }
        public string Empresa { get; set; }
        public string EmpresaLink { get; set; }
        public string Onibus { get; set; }
        public string OnibusLink { get; set; }
    }
}
