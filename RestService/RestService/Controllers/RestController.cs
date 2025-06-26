using Microsoft.AspNetCore.Mvc;

namespace RestService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestController :  ControllerBase
    {
        private const int CallsToCauseError = 10;

        private static int CallsCount = 0;

        [HttpGet]
        [Route("/somedata")]
        public IActionResult GetSomeData()
        {
            CallsCount++;
            if (CallsCount % CallsToCauseError == 0)
            {
                throw new Exception($"somedata not returned due to this being call no. {CallsToCauseError}");
            }

            return Ok("somedata");
        }
    }
}
