using Microsoft.AspNet.OData;
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
    }
}
