using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SoapService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestCallSoapController : ControllerBase
    {
        private const string ServiceAddress = "localhost:7044";

        [HttpGet]
        public ActionResult CallSoapRequestTask()
        {
            return Ok();
        }
    }
}
