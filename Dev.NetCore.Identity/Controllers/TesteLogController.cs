using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Dev.NetCore.Identity.Controllers
{
    [AllowAnonymous]
    public class TesteLogController : Controller
    {
        private readonly ILogger<TesteLogController> _logger;

        public TesteLogController(ILogger<TesteLogController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogError("Aconteceu um erro por aqui");
            return View();
        }
    }
}
