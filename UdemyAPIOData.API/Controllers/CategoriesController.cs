using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyAPIOData.API.Models;

namespace UdemyAPIOData.API.Controllers
{
    //route olayı odata üzerine geçti startup tarfındaki endpoints ile

    public class CategoriesController : ODataController
    {
        private readonly AppDbContext _context;
        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }
        [EnableQuery]  //sorgulama özelliği kazandırdık
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Categories);
        }
        [EnableQuery]
        public IActionResult Get([FromODataUri] int key)
        {
            return Ok(_context.Categories.Where(x => x.Id == key));
        }
        [HttpGet]
        [EnableQuery]
        [ODataRoute("Categories({id})/products({item})")]
        public IActionResult ProductById([FromODataUri] int id,
            [FromODataUri] int item)
        {
            return Ok(_context.Products.Where(x => x.CategoryId == id
            && x.Id == item));
        }
        [HttpGet]
        [EnableQuery]
        [ODataRoute("Categories({id})/products")]
        public IActionResult GetProducts([FromODataUri] int id)
        {
            return Ok(_context.Products.Where(x => x.CategoryId == id));
        }

        [HttpPost]
        public IActionResult TotalProductPrice([FromODataUri] int key)
        {
            var total = _context.Products.Where(x => x.CategoryId == key).Sum(x => x.Price);
            return Ok(total);
               
        }
        [HttpPost]
        public IActionResult TotalProductPrice2()
        {
            var total = _context.Products.Sum(x => x.Price);
            return Ok(total);

        }
        [HttpPost]
        public IActionResult TotalProductPriceWithParameter(ODataActionParameters parameters)
        {
            var categoryId =(int) parameters["categoryId"];
            var total = _context.Products.Where(x => x.CategoryId == categoryId)
               .Sum(x => x.Price);
            return Ok(total);
        }
        [HttpPost]
        public IActionResult Total(ODataActionParameters parameters)
        {
            int a = (int)parameters["a"];
            int b = (int)parameters["b"];
            int c = (int)parameters["c"];
            return Ok(a + b + c);
        }
        [HttpGet]
        public IActionResult CategoryCount()
        {
            var count = _context.Categories.Count();
            return Ok(count);
        }
       
    }
}
