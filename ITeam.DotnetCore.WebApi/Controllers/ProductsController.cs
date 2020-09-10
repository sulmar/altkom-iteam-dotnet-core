using ITeam.DotnetCore.IServices;
using ITeam.DotnetCore.Models;
using ITeam.DotnetCore.Models.SearchCriterias;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace ITeam.DotnetCore.WebApi.Controllers
{

    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            ICollection<Product> products = productService.Get();

            return Ok(products);
        }

        [HttpGet("{id:int}", Name = nameof(Get))]
        public IActionResult Get(int id)
        {
            Product product = productService.Get(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("{barcode}")]
        public IActionResult Get(string barcode)
        {
            Product product = productService.Get(barcode);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            productService.Add(product);

            return CreatedAtRoute(nameof(Get), new { Id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            productService.Update(product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            productService.Remove(id);

            return NoContent();
        }


        [HttpGet]
        public IActionResult Get([FromQuery] ProductSearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

    }
}
