using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestProj.Models;

namespace TestProj.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttributesController : Controller
    {


        [HttpPost]
        public IActionResult Index(AttributeModel attribute)
        {
            return Json(attribute);
        }
    }
}
