using ITeam.DotnetCore.IServices;
using ITeam.DotnetCore.Models;
using ITeam.DotnetCore.Models.SearchCriterias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ITeam.DotnetCore.WebApi.Controllers
{

    [Route("api/products")]
    [ApiController]
    [Authorize(Roles = "Trainer, Developer")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

     //   [AllowAnonymous]
        [HttpGet("{format?}"), FormatFilter]
        public IActionResult Get()
        {
            if (User.IsInRole("Trainer"))
            {

            }

            if (User.HasClaim(p=>p.Type == ClaimTypes.Email))
            {
                string email = User.FindFirst(ClaimTypes.Email).Value;

                // TODO: send email
            }

            if (this.User.Identity.IsAuthenticated)
            {
                ICollection<Product> products = productService.Get();

                return Ok(products);
            }
            else
            {
                return Unauthorized();
            }
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

        [Authorize(Policy = "AtLeast18")]
        [HttpPost]
        public IActionResult Post(Product product)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            productService.Add(product);

            // 1. ...
            // 2. ...
            // 3. ...

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


        //[HttpGet]
        //public IActionResult Get([FromQuery] ProductSearchCriteria searchCriteria)
        //{
        //    var products = productService.Get(searchCriteria);

        //    return Ok(products);
        //}

    }
}
