using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetShop.Web.Controllers
{
    public class OperacoesController : Controller
    {
        [Authorize]
        public ActionResult InventarioEstoque()
        {
            return View();
        }
        [Authorize]
        public ActionResult EntradaProduto()
        {
            return View();
        }
        [Authorize]
        public ActionResult SaidaProduto()
        {
            return View();
        }
    }
}