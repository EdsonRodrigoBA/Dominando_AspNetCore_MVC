using Dev.NetCore.Identity.Extensions;
using Dev.NetCore.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.NetCore.Identity.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy ="PodeExcluir")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Policy = "PodeEscrever")]
        public IActionResult PodeGravar()
        {
            return View();
        }

       [ClaimsAuthorizeAttribute("Produtos", "Ler")]
        public IActionResult TesteFiltroClaimPersonalizado()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
