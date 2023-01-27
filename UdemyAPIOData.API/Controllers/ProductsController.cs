using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyAPIOData.API.Models;

namespace UdemyAPIOData.API.Controllers
{
   [ODataRoutePrefix("Products")]
    public class ProductsController : ODataController
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }
        [EnableQuery]
        public IActionResult GetProducts()
        {
            return Ok(_context.Products.AsQueryable());
        }
        [ODataRoute("({Item})")]
        [EnableQuery]
        public IActionResult GetUrun([FromODataUri]int item)
        {
            return Ok(_context.Products.Where(x => x.Id == item));
        }
        
    }
}
