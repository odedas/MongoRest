using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoRest.Models;
using Microsoft.Extensions.Logging;
using MongoRest.Utils;
using System.IO;
using System.Text.Json;
//using Microsoft.Json.Schema;
//using Microsoft.Json.Schema.Validation;
//using Microsoft.Json;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoRest.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountCrud crud;
        private readonly ILogger _logger;

        public AccountController(IAccountCrud crud, ILoggerFactory loggerFactory) 
        {
            // create custom category log with ILoggerFactory 
            _logger = loggerFactory.CreateLogger("AccountLog");
            this.crud = crud;
        }

        //public JsonResult Index()
        //{
        //    _logger.LogInformation("default route for Accounts request...");
        //    return Json((List<Account>)crud.Get());
        //}

        [Route("{id?}")]
        public JsonResult Accounts(int? id, [FromQuery] int? top)
        {
            string msg = id != null ? $"returns account with id {id}" :
                 top == null ? "returns all accounts..." : $"returns top {top} accounts";
            _logger.Log(LogLevel.Information, msg);

            if (id == null)
            {
                if(top == null)
                {
                    return Json((List<Account>)crud.GetAll());
                }
                else
                {
                    int _top = (int)top;
                    if(_top > 0)
                    {
                        return Json((List<Account>)crud.GetTop(_top));
                    }
                    else
                    {
                        return Json("error, paramater top should be greater then 0");
                    }
                }
            }
            else
            {
                int _id = (int)id;
                return Json((new List<Account>{ crud.Get(_id)}));
            }
            // return Json(msg);
        }

        [AcceptVerbs("POST")]
        // [HttpPost("")]
        [Route("pp")]
        public JsonResult Accounts([FromBody] JsonDocument req)
        {
            dynamic jsonText = JsonSerializer.Serialize<dynamic>(req);
            JsonDocumentOptions jsonDocumentOptions = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            JsonDocument doc2 = JsonDocument.Parse(jsonText, jsonDocumentOptions);
            
            //JsonSchema jsonSchema;
            //Validator validator = new Validator(jsonSchema);
            //validator.Validate(jsonText, "");

            JsonElement root = req.RootElement;
            int id = root.GetProperty("id").GetInt32();
            //req.RootElement.TryGetProperty("id", out id);
            Account acc = crud.Get(id);
            return Json(acc);
        }

        // using Log scopes
        [HttpGet]
        [Route("logs")]
        public JsonResult logs()
        {
            // List<Account> acc;
            using (_logger.BeginScope("using Log scopes"))
            {
                _logger.Log(LogLevel.Critical, "inside Log scopes");
                // or 
                // _logger.LogError("Request body is null");
                // or using custom event ids, also notice the Log message template
                _logger.LogWarning(AppLogEvents.Read,"anothr one inside Log scopes");
                // acc = crud.Get();
            }
            return Json("anothr one inside Log scopes");
        }

    }
}
