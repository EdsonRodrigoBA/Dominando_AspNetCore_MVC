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

        [Route("Error/{id:Length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel();
            if (id == 500)
            {
                modelErro.Mensagem = "Ocorreu um erro interno.";
                modelErro.Titulo = "Erro Inesperado";
                modelErro.ErroCode = id.ToString();

            }

            return View("Error");
        }
    }
}
