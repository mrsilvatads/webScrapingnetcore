using System.Collections.Generic;

namespace WebScraping.DTOs
{
    public class IbovespaInfoDTO
    {
        public IbovespaInfoDTO()
        {
        }

        public IbovespaInfoDTO(int totalPoints, string percentageVariation)
        {
            TotalPoints = totalPoints;
            PercentageVariation = percentageVariation;
        }
        public IbovespaInfoDTO(int totalPoints, string percentageVariation, List<IbovespaInfoDTO> lista)
        {
            TotalPoints = totalPoints;
            PercentageVariation = percentageVariation;
            Lista = lista;
        }
        public IbovespaInfoDTO(string description, string value)
        {
            Description = description;
            Value = value;
        }
        public int TotalPoints { get; set; }
        public string PercentageVariation { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }

        public List<IbovespaInfoDTO> Lista { get; set; }
    }
}
