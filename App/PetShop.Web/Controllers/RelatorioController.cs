using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetShop.Web.Controllers
{
    public class RelatorioController : Controller
    {
        [Authorize]
        public ActionResult Cliente()
        {
            return View();
        }
        [Authorize]
        public ActionResult Vendas()
        {
            return View();
        }
    }
}