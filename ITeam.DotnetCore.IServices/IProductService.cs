using ITeam.DotnetCore.Models;
using ITeam.DotnetCore.Models.SearchCriterias;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ITeam.DotnetCore.IServices
{

    public interface IProductService : IEntityService<Product>
    {
        Product Get(string barcode);
        ICollection<Product> Get(ProductSearchCriteria searchCriteria);
        
    }
   
}
