using System;
using System.Collections.Generic;
using System.Text;

namespace ITeam.DotnetCore.Models.SearchCriterias
{
    public class ProductSearchCriteria
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal? From { get; set; }
        public decimal? To { get; set; }
    }
}
