using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyAPIOData.API.Controllers
{
   
    public class HelperController : ODataController
    {
        [HttpGet]
        [ODataRoute("GetKdv")]
        public IActionResult GetKdv()
        {
            return Ok(18);
        }
    }
}
