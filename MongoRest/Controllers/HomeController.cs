using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// using MongoRest.Models;
using Microsoft.Extensions.Logging;
// using MongoRest.Services;


namespace MongoRest.Controllers
{    
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        public HomeController(ILogger <HomeController> logger)
        {
            _logger = logger;
        }

        // default route for controller without actions
        public string Index()
        {
            _logger.LogInformation("Home request...");
            return "Home sweet home...";
        }
    }
}
