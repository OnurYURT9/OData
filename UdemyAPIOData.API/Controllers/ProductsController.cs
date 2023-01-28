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
   
    public class ProductsController : ODataController
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [EnableQuery(PageSize =2)]
        public IActionResult GetProducts()
        {
            return Ok(_context.Products.AsQueryable());
        }

        [ODataRoute("Products")]
        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            _context.Products.Add(product);

            _context.SaveChanges();
            return Ok(product);
        }

        [ODataRoute("Products({Id})")]
        [HttpPut]
        public IActionResult Update([FromODataUri] int Id, [FromBody] Product product)

        {
            product.Id = Id;
            _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteProduct([FromODataUri] int key)
        {
            var product = _context.Products.Find(key);

            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPost]
        public IActionResult LoginUser(ODataActionParameters parameters)
        {
            Login login = parameters["UserLogin"] as Login;
            return Ok(login.Email + "-" + login.Password);
        }
        
        [HttpGet]
        public IActionResult MultiplyFunction([FromODataUri]int a1, [FromODataUri] int a2, [FromODataUri] int a3)
        {
            return Ok(a1 * a2 * a3);
        }
        [HttpGet]
        public IActionResult KdvHesapla(int key, [FromODataUri] double kdv)
        {
            var product = _context.Products.Find(key);
            return Ok(product.Price + (product.Price * kdv));
        }
    }
}
